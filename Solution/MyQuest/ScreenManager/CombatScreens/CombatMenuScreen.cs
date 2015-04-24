using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    class CombatMenuScreen : MenuScreen
    {
        #region Fields

        Texture2D background;
        Texture2D staminaBar;
        PCFightingCharacter fighter;
        List<FightingCharacter> combatants;
        int currentFighter;
        bool runningEnabled;
        Vector2 menuPosition;
        Vector2 topCombatMenuItemPosition;
        
        bool finalBattle = false;

        public bool FinalBattle
        {
            set { finalBattle = value; }
        }

        #endregion

        #region Initialization

        public CombatMenuScreen(PCFightingCharacter fighter, bool runningEnabled)
        {
            this.fighter = fighter;
            this.runningEnabled = runningEnabled;
            menuPosition = CombatScreen.HudPosition + new Vector2(7, 5);
            topCombatMenuItemPosition = menuPosition + new Vector2(35, 5);
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.15);
            TransitionOffTime = TimeSpan.FromSeconds(0.15);

            float xpos = GlobalSettings.TitleSafeArea.Center.X;
            float ypos = GlobalSettings.TitleSafeArea.Center.Y;

            IsPopup = true;

            combatants = TurnExecutor.Singleton.Fighters;
            currentFighter = TurnExecutor.Singleton.CurrentFighterIndex;

            Debug.Assert(combatants[currentFighter] == fighter);

            menuOptions = new List<MenuOption>();

            if (fighter.State != State.Stunned)
            {
                StaticMenuOption attackEntry = new StaticMenuOption(this, Strings.ZA169, topCombatMenuItemPosition);
                attackEntry.OnSelectEvent += new EventHandler(attackEntry_OnSelectEvent);
                menuOptions.Add(attackEntry);
                topCombatMenuItemPosition.Y += Fonts.CombatMenuItem2.LineSpacing;

                StaticMenuOption skillsEntry = new StaticMenuOption(this, Strings.ZA170, topCombatMenuItemPosition);
                skillsEntry.OnSelectEvent += new EventHandler(skillsEntry_OnSelectEvent);
                menuOptions.Add(skillsEntry);
                topCombatMenuItemPosition.Y += Fonts.CombatMenuItem2.LineSpacing;
            }

            StaticMenuOption itemsEntry = new StaticMenuOption(this, Strings.ZA171, topCombatMenuItemPosition);
            topCombatMenuItemPosition.Y += Fonts.CombatMenuItem2.LineSpacing;
            itemsEntry.OnSelectEvent += new EventHandler(itemsEntry_OnSelectEvent);
            menuOptions.Add(itemsEntry);

            StaticMenuOption defendEntry = new StaticMenuOption(this, Strings.ZA172, topCombatMenuItemPosition);
            topCombatMenuItemPosition.Y += Fonts.CombatMenuItem2.LineSpacing;
            defendEntry.OnSelectEvent += new EventHandler(defendEntry_OnSelectEvent);
            menuOptions.Add(defendEntry);

            if (fighter.State != State.Stunned && runningEnabled)
            {
                StaticMenuOption runEntry = new StaticMenuOption(this, Strings.ZA173, topCombatMenuItemPosition);
                runEntry.OnSelectEvent += new EventHandler(runEntry_OnSelectEvent);
                menuOptions.Add(runEntry);
            }

        }
        
        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "CombatMenuScreen");
            staminaBar = content.Load<Texture2D>(interfaceTextureFolder + "Stamina_Bar");
        }

        #endregion

        #region Callbacks


        void attackEntry_OnSelectEvent(object sender, EventArgs e)
        {
            if (!finalBattle)
            {
                ScreenManager.AddScreen(new SkillTargetSelectionScreen(fighter, SkillPool.RequestByName(fighter.BaseAttackName)));

                SoundSystem.Play(AudioCues.menuConfirm);
            }
            else
            {
                SoundSystem.Play(AudioCues.menuDeny);
            }
        }

        void skillsEntry_OnSelectEvent(object sender, EventArgs e)
        {
            FightingCharacter nathan = Party.Singleton.GetFightingCharacter(Party.nathan);
            if (nathan != null && nathan.SkillNames.Count == 0)
            {
                SoundSystem.Play(AudioCues.menuDeny);
            }
            else
            {
                ScreenManager.AddScreen(new InCombatSkillScreen(fighter, menuPosition, currentFighter));
                SoundSystem.Play(AudioCues.menuConfirm);
            }
        }

        void itemsEntry_OnSelectEvent(object sender, EventArgs e)
        {
            if (Party.Singleton.GameState.Inventory.Items.Count == 0 || finalBattle)
            {
                SoundSystem.Play(AudioCues.menuDeny);
            }
            else
            {
                ScreenManager.AddScreen(new InCombatInventoryScreen(fighter, menuPosition));
                SoundSystem.Play(AudioCues.menuConfirm);
            }
        }

        void defendEntry_OnSelectEvent(object sender, EventArgs e)
        {
            if (!finalBattle)
            {
                TurnExecutor.Singleton.Action = FighterAction.Defend;
                ScreenManager.RemoveScreen(this);

                SoundSystem.Play(AudioCues.menuConfirm);
            }
            else
            {
                SoundSystem.Play(AudioCues.menuDeny);
            }
        }

        void runEntry_OnSelectEvent(object sender, EventArgs e)
        {
            TurnExecutor.Singleton.Action = FighterAction.Run;
            ScreenManager.RemoveScreen(this);

            SoundSystem.Play(AudioCues.menuConfirm);
        }


        #endregion

        #region Update and Draw

        public override void HandleInput(GameTime gameTime)
        {
            base.HandleInput(gameTime);

            if (InputState.IsMenuCancel())
            {
                ScreenManager.AddScreen(new QuitConfirmationScreen());
                SoundSystem.Play(AudioCues.menuConfirm);
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (TurnExecutor.Singleton.Action != FighterAction.Deciding)
            {
                ScreenManager.RemoveScreen(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (OtherScreenHasFocus)
            {
                return;
            }

            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color whiteColor = Color.White * TransitionAlpha;

            foreach (MenuOption entry in menuOptions)
            {
                entry.Draw(spriteBatch, gameTime);
            }

            Vector2 turnIndicatorPosition = menuOptions[selectedOption].Position + new Vector2(-29, -1);
            spriteBatch.Draw(ScreenManager.PointerTexture, turnIndicatorPosition, null, whiteColor, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
        }


        #endregion
    }
}
