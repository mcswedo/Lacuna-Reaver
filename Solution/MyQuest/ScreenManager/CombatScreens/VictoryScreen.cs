using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    class VictoryScreen : Screen
    {
        #region Fields


        Texture2D background;
        Texture2D xpBar;

        int experienceGain = 0;
        int goldGain = 0;
        int[] currentXp = new int[3];
        int[] nextLevel = new int[3];
        int totalGold = 0;
        Dictionary<string, int> itemsDropped;
        List<FightingCharacter> enemies;

        bool nathanLeveled = false;
        bool caraLeveled = false;
        bool willLeveled = false;
        bool maxLeveled = false;
        bool sidLeveled = false;

        #endregion

        #region Positions


        Vector2 backgroundPos;
        Vector2 experienceGainPos;
        Vector2 goldGainPos;
        Vector2 itemGainPos;
        Vector2 title1TextPos;
        Vector2 topNamePos;
        Vector2 topCurrentXpText;
        Vector2 topCurrentLevelText;
        Vector2 totalGoldText;

        #endregion

        #region Initialization

        public VictoryScreen(List<FightingCharacter> enemies)
        {
            this.enemies = enemies;
            
            backgroundPos = new Vector2(400, 200);
            title1TextPos = new Vector2(600, 120);
            topNamePos = new Vector2(200, 170);
            topCurrentXpText = new Vector2(topNamePos.X + 100, topNamePos.Y);
            topCurrentLevelText = new Vector2(topNamePos.X + 100, topNamePos.Y + 25);
            experienceGainPos = new Vector2(200, 270);
            goldGainPos = new Vector2(850, 510);
            itemGainPos = new Vector2(200, 480);
            totalGoldText = new Vector2(goldGainPos.X + 200 + goldGainPos.Y);
            itemsDropped = new Dictionary<string, int>();
        }

        public void PreInitialize()
        {
            experienceGain = 0;
            goldGain = 0;
            nathanLeveled = false;
            caraLeveled = false;
            willLeveled = false;

            foreach (NPCFightingCharacter enemy in enemies)
            {
                experienceGain += enemy.FighterStats.Experience;
                goldGain += enemy.FighterStats.Gold;
            }

            totalGold = Party.Singleton.GameState.Gold + goldGain;

            DetermineItems(enemies);
            enemies.Clear();
            UpdateParty();
        }

        void DetermineItems(List<FightingCharacter> enemies)
        {
            int quantity;

            foreach (FightingCharacter combatant in enemies)
            {
                for (int i = 0; i < combatant.ItemsDropped.Count; i++)
                {
                    Item item = ItemPool.RequestItem(combatant.ItemsDropped[i]);

                    if (Utility.RNG.Next(1, 100) <= (item.DropChance * 100))
                    {
                        Party.Singleton.GameState.Inventory.AddItem(item, 1);

                        if (itemsDropped.ContainsKey(item.DisplayName))
                        {
                            quantity = itemsDropped[item.DisplayName] + 1;
                            itemsDropped[item.DisplayName] = quantity;
                            quantity = 0;
                        }
                        else
                            itemsDropped.Add(item.DisplayName, 1);
                    }
                }
            }
        }

        public void UpdateParty()
        {
            foreach (PCFightingCharacter fighter in Party.Singleton.GameState.Fighters)
            {
                if (fighter.State != State.Dead)
                {
                    fighter.FighterStats.Experience += experienceGain;

                    if (fighter.FighterStats.Experience >= fighter.FighterStats.NextLevelXp && !fighter.FighterStats.IsMaxLevel)
                    {
                        ScreenManager.AddScreen(new LevelUpScreen(fighter));

                        if (fighter.Name == Party.nathan)
                        {
                            nathanLeveled = true;
                        }
                        else if (fighter.Name == Party.cara)
                        {
                            caraLeveled = true;
                        }
                        else if (fighter.Name == Party.will)
                        {
                            willLeveled = true;
                        }
                        else if (fighter.Name == Party.max)
                        {
                            maxLeveled = true;
                        }
                        else if (fighter.Name == Party.sid)
                        {
                            sidLeveled = true;
                        }
                    }
                }
                else
                {
                    if (fighter.State == State.Dead)
                    {
                        fighter.OnResurrection();
                    }
                }
            }

            Party.Singleton.GameState.Gold = totalGold;
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            MusicSystem.Play(AudioCues.battleVictory);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "VictoryScreenBackground");
            xpBar = content.Load<Texture2D>(interfaceTextureFolder + "HpManaBar");
        }


        #endregion

        #region Handle Input


        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsMenuSelect())
            {
                experienceGain = 0;
                goldGain = 0;
                enemies.Clear();

                ExitAfterTransition();
                //ScreenManager.RemoveScreen(this);
                MusicSystem.Play(Party.Singleton.CurrentMap.MusicCueName);
            }
        }


        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            if (!OtherScreenHasFocus)
            {
                spriteBatch.Draw(background, GlobalSettings.TitleSafeArea, Color.White * TransitionAlpha);
                renderDetails(spriteBatch);
            }
        }

        void renderDetails(SpriteBatch spriteBatch)
        {
            Color textColor = Fonts.CombatMenuItemColor * TransitionAlpha;

            int offset = 0;
            float recWidth = 0.0f;
            float barPercent = 0.0f;
            string characterName, currentXP, currentLevel, xpGain, nextLevelExperience;
            int nextLevelXp, currentExperience;
            bool characterHasAlreadyLeveled = false;

            spriteBatch.DrawString(Fonts.MenuTitle2, Strings.ZA184, title1TextPos, textColor);

            if (nathanLeveled)
                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA185, new Vector2(topNamePos.X + 70, topNamePos.Y), textColor);

            if ((caraLeveled || willLeveled) && (Party.Singleton.GameState.Fighters[0].Name.Equals("Cara") || Party.Singleton.GameState.Fighters[0].Name.Equals("Will")))
            {
                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA185, new Vector2(topNamePos.X + 70, topNamePos.Y), textColor);
                characterHasAlreadyLeveled = true;
            }

            if (!characterHasAlreadyLeveled)
            {
                if (caraLeveled || sidLeveled)
                    spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA185, new Vector2(topNamePos.X + 70, topNamePos.Y + 92), textColor);

                if (willLeveled || maxLeveled)
                    spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA185, new Vector2(topNamePos.X + 70, topNamePos.Y + 184), textColor);
            }

            for (int i = 0; i < Party.Singleton.GameState.Fighters.Count; i++)
            {
                characterName = Party.Singleton.GameState.Fighters[i].Name;
                currentXP = Party.Singleton.GameState.Fighters[i].FighterStats.Experience.ToString();
                currentLevel = Party.Singleton.GameState.Fighters[i].FighterStats.Level.ToString();
                if (Party.Singleton.GameState.Fighters[i].State != State.Dead)
                {
                    xpGain = experienceGain.ToString();
                }
                else
                {
                    int zero = 0;
                    xpGain = zero.ToString();
                }
                currentExperience = Party.Singleton.GameState.Fighters[i].FighterStats.Experience;
                nextLevelXp = Party.Singleton.GameState.Fighters[i].FighterStats.NextLevelXp;
                nextLevelExperience = Party.Singleton.GameState.Fighters[i].FighterStats.NextLevelXp.ToString();

                spriteBatch.Draw(Party.Singleton.GameState.Fighters[i].Icon, new Vector2(topNamePos.X, topNamePos.Y + offset + 8), Color.White * TransitionAlpha);

                spriteBatch.DrawString(Fonts.CombatMenuItem2, characterName, new Vector2(topNamePos.X + 200, topNamePos.Y + offset), textColor);
                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA186 + " " + xpGain, new Vector2(topCurrentXpText.X + 200, topCurrentXpText.Y + offset), textColor);
                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA187 + " " + currentXP, new Vector2(topCurrentXpText.X + 500, topCurrentXpText.Y + offset), textColor);
                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA188 + " " + currentLevel, new Vector2(topCurrentLevelText.X + 200, topCurrentLevelText.Y + offset), textColor);

                if (Party.Singleton.GameState.Fighters[i].FighterStats.IsMaxLevel)
                {
                    spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA189, new Vector2(topCurrentXpText.X + 500, topCurrentXpText.Y + offset + 28), textColor);
                }
                else
                {
                    spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA190 + " " + nextLevelExperience, new Vector2(topCurrentXpText.X + 500, topCurrentXpText.Y + offset + 28), textColor);
                }

                if (!Party.Singleton.GameState.Fighters[i].FighterStats.IsMaxLevel)
                {
                    barPercent = ((float)currentExperience / (float)nextLevelXp); 
                }
                //Leave the bar maxed out at 99% when at level 25.
                else
                {
                    barPercent = .99f;
                }

                if (barPercent >= 1)
                {
                    barPercent -= 1;
                }

                Debug.Assert(barPercent < 1);

                recWidth = 160 * barPercent * 1.6f;

                spriteBatch.Draw(
                                 ScreenManager.BlankTexture,
                                 new Rectangle((int)topCurrentXpText.X + 620 - 120, (int)topCurrentXpText.Y + offset + 60 - 4, (int)recWidth + 1, 9),
                                 Color.Green * TransitionAlpha);
                spriteBatch.Draw(xpBar, new Vector2(topCurrentXpText.X + 620 - 120, topCurrentXpText.Y + offset + 60 - 4), null, textColor, 0, Vector2.Zero, new Vector2(1.6f, 1.0f), SpriteEffects.None, 0);

                //spriteBatch.Draw(xpBar, new Rectangle(topCurrentXpText.X + 620 - 120, topCurrentXpText.Y + offset + 60 - 4, (int)xpBar.Width*1.7, xpBar.Height), textColor);
                //spriteBatch.Draw(xpBar, new Vector2(topCurrentXpText.X + 620 - 120, topCurrentXpText.Y + offset + 60 - 4), textColor);

                offset += 92;
            }

            spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA191 + " " + goldGain.ToString(), goldGainPos, textColor);
            spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA192 + " " + totalGold.ToString(), new Vector2(goldGainPos.X, goldGainPos.Y + 50), textColor);
            spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA193, itemGainPos, textColor);

            offset = 30;

            foreach (string key in itemsDropped.Keys.ToList())
            {
                spriteBatch.DrawString(Fonts.CombatMenuItem2, itemsDropped[key].ToString() + " " + key + "(s)", new Vector2(itemGainPos.X + 50, itemGainPos.Y + offset), textColor);
                offset += 25;
            }
        }


        #endregion
    }
}
