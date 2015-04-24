using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class StatusScreen : MenuScreen
    {
        /// <summary>
        /// The Y distance in pixels between party members
        /// </summary>
        const float memberYSpacing = 171f;

        #region Fields

        Texture2D background;
        StaticMenuOption fullscreenToggle;

        #endregion

        #region Initialization


        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            IsPopup = true;

            menuOptions = new List<MenuOption>();

            float xpos = 865;
            float ypos = 185;
            StaticMenuOption inventory = new StaticMenuOption(this, Strings.StatusScreen_Inventory, new Vector2(xpos, ypos));
            StaticMenuOption spells = new StaticMenuOption(this, Strings.StatusScreen_Skills, new Vector2(xpos, ypos + 54));
            StaticMenuOption log = new StaticMenuOption(this, Strings.StatusScreen_Log, new Vector2(xpos + 3, ypos + 108));
            if (GameLoop.Instance.IsFullScreen)
            {
                fullscreenToggle = new StaticMenuOption(this, Strings.MainMenuScreen_Windowed, new Vector2(xpos + 2, ypos + 162));
            }
            else
            {
                fullscreenToggle = new StaticMenuOption(this, Strings.MainMenuScreen_FullScreen, new Vector2(xpos + 2, ypos + 162));
            }
            StaticMenuOption exit = new StaticMenuOption(this, Strings.StatusScreen_Quit, new Vector2(xpos + 2, ypos + 216));
            
            inventory.OnSelectEvent += new EventHandler(inventory_OnSelectEvent);
            spells.OnSelectEvent += new EventHandler(spells_OnSelectEvent);
            log.OnSelectEvent += new EventHandler(log_OnSelectEvent);
            fullscreenToggle.OnSelectEvent += new EventHandler(fullscreen_OnSelectEvent);
            exit.OnSelectEvent += new EventHandler(exit_OnSelectEvent);

            menuOptions.Add(inventory);
            menuOptions.Add(spells);
            menuOptions.Add(log);
            menuOptions.Add(fullscreenToggle);
            menuOptions.Add(exit);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "StatusScreenBackground_b");
        }


        #endregion

        #region Callbacks


        void inventory_OnSelectEvent(object sender, EventArgs e)
        {
            //ExitAfterTransition();
            ScreenManager.AddScreen(new InventoryScreen());
        }

        void spells_OnSelectEvent(object sender, EventArgs e)
        {
            //ExitAfterTransition();
            ScreenManager.AddScreen(new SkillScreen());
        }

        void log_OnSelectEvent(object sender, EventArgs e)
        {
            //ExitAfterTransition();
            ScreenManager.AddScreen(new ConversationLogScreen());
        }

        //void map_OnSelectEvent(object sender, EventArgs e)
        //{
        //    //ExitAfterTransition();
        //    ScreenManager.AddScreen(new GameMapScreen());
        //}

        void fullscreen_OnSelectEvent(object sender, EventArgs e)
        {
            if (GameLoop.Instance.IsFullScreen)
            {
                fullscreenToggle.Description = Strings.MainMenuScreen_FullScreen;
                GameLoop.Instance.ToggleFullScreen();
            }
            else
            {
                fullscreenToggle.Description = Strings.MainMenuScreen_Windowed;
                GameLoop.Instance.ToggleFullScreen();
            }
        }

        void exit_OnSelectEvent(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new QuitConfirmationScreen());
        }


        #endregion

        #region Methods


        public override void HandleInput(GameTime gameTime)
        {
            base.HandleInput(gameTime);

            if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

//            spriteBatch.Begin();

            spriteBatch.Draw(background, GlobalSettings.TitleSafeArea, Color.White * TransitionAlpha);

            int position = 0;
            foreach (PCFightingCharacter member in Party.Singleton.GameState.Fighters)
            {
                RenderFighter(member, position++, spriteBatch);
            }

            foreach (MenuOption entry in menuOptions)
            {
                entry.Draw(spriteBatch, gameTime);
            }

            spriteBatch.DrawString(Fonts.MenuTitle2, Strings.StatusScreen_Gold + " ", new Vector2(910 /* To center: - 43 */, 492 - 5), Fonts.MenuTitleColor * TransitionAlpha);

            string goldAmount = Party.Singleton.GameState.Gold.ToString();
            Vector2 goldTextSize = Fonts.MenuTitle2.MeasureString(goldAmount);

            spriteBatch.DrawString(Fonts.MenuTitle2, goldAmount,
                new Vector2(907 + 128, 550), Fonts.MenuTitleColor * TransitionAlpha, 0.0f,
                new Vector2(goldTextSize.X, goldTextSize.Y / 2 - 5),
                1.0f, SpriteEffects.None, 1.0f);

            Vector2 arrowPosition = menuOptions[selectedOption].Position + new Vector2(-38, 0);
            spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, Color.White * TransitionAlpha, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);

//            spriteBatch.End();
        }

        private void RenderFighter(PCFightingCharacter member, int position, SpriteBatch spriteBatch)
        {
//            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Color color = Color.White * TransitionAlpha;

            Vector2 portraitLocation = new Vector2(222, 152 + (position * memberYSpacing));
            Vector2 nameLocation = new Vector2(portraitLocation.X + 129, portraitLocation.Y - 38);

            spriteBatch.Draw(member.Portrait, portraitLocation, color);
            spriteBatch.DrawString(Fonts.MenuTitle2, member.Name, nameLocation, Fonts.MenuItemColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuTitle2, 
                Strings.StatusScreen_Level + " " + member.FighterStats.Level.ToString(), 
                new Vector2(nameLocation.X + 200, nameLocation.Y), 
                Fonts.MenuTitleColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                Strings.StatusScreen_EXP + " " + member.FighterStats.Experience,
                new Vector2(nameLocation.X + 208, nameLocation.Y + 25),
                Fonts.MenuTitleColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuTitle2, 
                Strings.StatusScreen_HP + " " + member.FighterStats.Health.ToString() + " / " + member.FighterStats.ModifiedMaxHealth.ToString(),
                new Vector2(nameLocation.X - 20, nameLocation.Y + 40), Fonts.MenuTitleColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuTitle2,
                Strings.StatusScreen_MP + " " + member.FighterStats.Energy.ToString() + " / " + member.FighterStats.ModifiedMaxEnergy.ToString(),
                new Vector2(nameLocation.X - 20, nameLocation.Y + 100), Fonts.MenuTitleColor * TransitionAlpha);
            
            Vector2 attr = new Vector2(535, 180 + (position * memberYSpacing));

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                Strings.StatusScreen_Str + " " + member.FighterStats.ModifiedStrength,
                attr,
                Fonts.MenuItemColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                Strings.StatusScreen_Def + " " + member.FighterStats.ModifiedDefense,
                new Vector2(attr.X, attr.Y + 25),
                Fonts.MenuItemColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                Strings.StatusScreen_Agil + " " + member.FighterStats.ModifiedAgility,
                new Vector2(attr.X, attr.Y + 50),
                Fonts.MenuItemColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                Strings.StatusScreen_Will + " " + member.FighterStats.ModifiedWillpower,
                new Vector2(attr.X + 110, attr.Y + 10),
                Fonts.MenuItemColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                Strings.StatusScreen_Int + " " + member.FighterStats.ModifiedIntelligence,
                new Vector2(attr.X + 110, attr.Y + 35),
                Fonts.MenuItemColor * TransitionAlpha);
        }

        #endregion
    }
}
