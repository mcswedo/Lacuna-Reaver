//#define IGNORECOSTS

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class InCombatSkillScreen : Screen
    {
        #region Fields


        Texture2D background;
        Texture2D staminaBar;
        //Texture2D pointer;

        const int maxSkillDisplay = 5;
        int firstDisplaySkill;
        int currentSelection;
        int currentFighter;
        int maxSkills;

        PCFightingCharacter fighter;

        List<Skill> skillList = new List<Skill>();
        List<FightingCharacter> combatants;


        #endregion

        #region Positions


        Vector2 menuPosition;
        Vector2 pointerPosition;
        Vector2 menuNamePosition;
        Vector2 skillNamePosition;
        Vector2 mpPosition;
        Vector2 spPosition;


        #endregion

        #region Initialization


        public InCombatSkillScreen(PCFightingCharacter fighter, Vector2 menuPosition, int selectedTarget)
        {
            this.fighter = fighter;
            currentFighter = selectedTarget;
            combatants = TurnExecutor.Singleton.Fighters;

            for (int i = 0; i < fighter.SkillNames.Count; i++)
            {
                if (fighter.SkillNames[i] != "Attack")
                {
                    Skill skill = SkillPool.RequestByName(fighter.SkillNames[i]);

                    if (skill.BattleSkill)
                        skillList.Add(skill);
                }
            }

            maxSkills = skillList.Count;

            this.menuPosition = menuPosition - new Vector2(6,4);

            pointerPosition = menuPosition + new Vector2(35, 30);
            menuNamePosition = menuPosition + new Vector2(35, 1);
            skillNamePosition = menuNamePosition + new Vector2(25, 25);
            mpPosition = menuNamePosition + new Vector2(175, 1);
            spPosition = mpPosition + new Vector2(50, 0);
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.1);
            TransitionOffTime = TimeSpan.FromSeconds(0.1);

            IsPopup = true;
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "hudFrames");

            //pointer = content.Load<Texture2D>(interfaceTextureFolder + "Small_arrow");

            staminaBar = content.Load<Texture2D>(interfaceTextureFolder + "Stamina_Bar");
        }


        #endregion

        #region Handle Input


        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsMenuCancel())
            {
                ScreenManager.RemoveScreen(this);

                SoundSystem.Play(AudioCues.menuConfirm);
            }

            if (skillList.Count > 0)
            {
                if (TurnExecutor.Singleton.Action == FighterAction.Skill)
                {
                    ScreenManager.RemoveScreen(this);
                    return;
                }

                if (InputState.IsMenuDown())
                {
                    AdjustPointerDown();

                    SoundSystem.Play(AudioCues.menuMove);
                }
                else if (InputState.IsMenuUp())
                {
                    AdjustPointerUp();

                    SoundSystem.Play(AudioCues.menuMove);
                }
                else if (InputState.IsMenuSelect())
                {
                    if ((skillList[currentSelection].SpCost <= fighter.FighterStats.Stamina &&
                       skillList[currentSelection].MpCost <= fighter.FighterStats.Energy))
                    {
                        if (skillList[currentSelection] is Ressurection && (Nathan.Instance.FighterStats.Health > 0 && Will.Instance.FighterStats.Health > 0))
                        {
                            SoundSystem.Play(AudioCues.menuDeny);
                        }
                        else
                        {
                            ScreenManager.AddScreen(new SkillTargetSelectionScreen(fighter, skillList[currentSelection]));
                            SoundSystem.Play(AudioCues.menuConfirm);
                        } 
                    }
                    else
                    {
                        SoundSystem.Play(AudioCues.menuDeny);
                    }
                }
            }

        }


        #endregion

        #region Helpers


        /// <summary>
        /// Attempts to decrement the currentSelection by one.
        /// </summary>
        private void AdjustPointerUp()
        {
            currentSelection = Math.Max(currentSelection - 1, 0);
//            currentSelection = (currentSelection - 1 + skillList.Count) % skillList.Count;
            if (currentSelection < firstDisplaySkill)
            {
                firstDisplaySkill = Math.Max(firstDisplaySkill - 1, 0);
            }
        }

        /// <summary>
        /// Attempts to increment the currentSelection by one
        /// </summary>
        private void AdjustPointerDown()
        {
            currentSelection = Math.Min(currentSelection + 1, maxSkills - 1);
            if (currentSelection - firstDisplaySkill >= maxSkillDisplay)
            {
                firstDisplaySkill = Math.Min(firstDisplaySkill + 1, maxSkills - maxSkillDisplay);
            }
        }


        #endregion

        #region Update and Draw


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color color = Color.White * TransitionAlpha;

            spriteBatch.Draw(background, menuPosition, color);

            spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA181, menuNamePosition, Fonts.CombatMenuItemColor * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuItem2, "MP", mpPosition, Fonts.MenuItemColor * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuItem2, "SP", spPosition, Fonts.MenuItemColor * TransitionAlpha);

            RenderSkillList(spriteBatch);
        }


        private void RenderSkillList(SpriteBatch spriteBatch)
        {
            Vector2 location = pointerPosition;

            int lastDisplayItem = Math.Min(skillList.Count, firstDisplaySkill + maxSkillDisplay);

            for (int i = firstDisplaySkill; i < lastDisplayItem; ++i)
            {
                Skill currentSkill = skillList[i];
                if (i == currentSelection)
                {
                    //spriteBatch.Draw(pointer, new Vector2(location.X - pointer.Width, location.Y), color);
                    Vector2 arrowPosition = new Vector2(location.X, location.Y) + new Vector2(-39, -1);
                    spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, Color.White * TransitionAlpha, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
                }
                spriteBatch.DrawString(Fonts.MenuItem2, currentSkill.Name, location, Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2, currentSkill.MpCost.ToString(), new Vector2(location.X + 175, location.Y), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2, currentSkill.SpCost.ToString(), new Vector2(location.X + 233, location.Y), Fonts.MenuItemColor * TransitionAlpha);

                location.Y += Fonts.MenuItem2.LineSpacing;
            }
        }


        #endregion
    }
}
