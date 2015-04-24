using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

/// Needs graphics and the final option list

namespace MyQuest
{
    public class MainMenuScreen : MenuScreen
    {
        #region Fields

        Texture2D background;

//        bool isTrial = false;

        StaticMenuOption fullscreenModeEntry = null;

        #endregion

        #region Initialization

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.8);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);

            float xpos = GlobalSettings.TitleSafeArea.Center.X;
            float ypos = GlobalSettings.TitleSafeArea.Center.Y;

            menuOptions = new List<MenuOption>();

            StaticMenuOption newGameEntry = new StaticMenuOption(this, Strings.MainMenuScreen_NewGame, new Vector2(xpos, ypos));
            ypos += Fonts.MenuItem2.LineSpacing;

            StaticMenuOption continueEntry = new StaticMenuOption(this, Strings.MainMenuScreen_Continue, new Vector2(xpos, ypos));
            if (Party.Singleton.CanContinue)
            {
                ypos += Fonts.MenuItem2.LineSpacing;
            }
            String itemName = null;
            if (GameLoop.Instance.IsFullScreen)
            {
                itemName = Strings.MainMenuScreen_Windowed;
            }
            else
            {
                itemName = Strings.MainMenuScreen_FullScreen;
            }
            fullscreenModeEntry = new StaticMenuOption(this, itemName, new Vector2(xpos, ypos));
            ypos += Fonts.MenuItem2.LineSpacing;

            StaticMenuOption controlsEntry = new StaticMenuOption(this, Strings.MainMenuScreen_Controls, new Vector2(xpos, ypos));
            ypos += Fonts.MenuItem2.LineSpacing;

            StaticMenuOption creditsEntry = new StaticMenuOption(this, Strings.MainMenuScreen_Credits, new Vector2(xpos, ypos));
            ypos += Fonts.MenuItem2.LineSpacing;

            StaticMenuOption exitEntry = new StaticMenuOption(this, Strings.MainMenuScreen_Exit, new Vector2(xpos, ypos));
            ypos += Fonts.MenuItem2.LineSpacing;

            newGameEntry.OnSelectEvent += new EventHandler(newGameEntry_OnSelectEvent);
            continueEntry.OnSelectEvent += new EventHandler(continueEntry_OnSelectEvent);
            exitEntry.OnSelectEvent += new EventHandler(exitEntry_OnSelectEvent);
            controlsEntry.OnSelectEvent += new EventHandler(controlsEntry_OnSelectEvent);
            creditsEntry.OnSelectEvent += new EventHandler(creditsEntry_OnSelectEvent);
            fullscreenModeEntry.OnSelectEvent += new EventHandler(toggleFullscreenEntry_OnSelectEvent);

            menuOptions.Add(newGameEntry);
            if (Party.Singleton.CanContinue)
            {
                menuOptions.Add(continueEntry);
            }
            menuOptions.Add(fullscreenModeEntry);
            menuOptions.Add(controlsEntry);
            menuOptions.Add(creditsEntry);
            menuOptions.Add(exitEntry);

//            isTrial = Guide.IsTrialMode;

            //if (Guide.IsTrialMode)
            //    menuOptions.Add(purchaseEntry);

            MusicSystem.Play(AudioCues.menuCue);
        }        

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "MainMenuBackground");
        }


        #endregion

        #region Callbacks


        void newGameEntry_OnSelectEvent(object sender, EventArgs e)
        {
            LoadingPartyScreen.Singleton.IsNewGame = true;
            ScreenManager.AddScreen(LoadingPartyScreen.Singleton);
        }

        void loadingScreen_LoadingFinished(object sender, EventArgs e)
        {
            /// We loaded from storage so this can't be a new game
            LoadingPartyScreen.Singleton.IsNewGame = false;
            LoadingScreen.Singleton.LoadingFinished -= loadingScreen_LoadingFinished;
            ScreenManager.AddScreen(LoadingPartyScreen.Singleton);
        }

        void continueEntry_OnSelectEvent(object sender, EventArgs e)
        {
            LoadingScreen.Singleton.LoadingFinished += new EventHandler(loadingScreen_LoadingFinished);
            ScreenManager.AddScreen(LoadingScreen.Singleton);
            PartySerializer2.LoadFrom(Party.saveFileName);

            //if (PartySerializer.SaveGameFiles.Count == 1)
            //{
            //    LoadingScreen.Singleton.LoadingFinished += new EventHandler(loadingScreen_LoadingFinished);
            //    ScreenManager.AddScreen(LoadingScreen.Singleton);
            //    PartySerializer.LoadFrom(Party.saveFileName);
            //    return;
            //}
            //ScreenManager.AddScreen(new OpenSaveGameMenuScreen());
        }

        void controlsEntry_OnSelectEvent(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new HelpScreen());
        }

        void creditsEntry_OnSelectEvent(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new CreditsScreen(false));
        }

        void exitEntry_OnSelectEvent(object sender, EventArgs e)
        {
            GameLoop.ShouldExit = true;
        }

        void toggleFullscreenEntry_OnSelectEvent(object sender, EventArgs e)
        {
            if (GameLoop.Instance.IsFullScreen)
            {
                fullscreenModeEntry.Description = Strings.MainMenuScreen_FullScreen;
                GameLoop.Instance.ToggleFullScreen();
            }
            else
            {
                fullscreenModeEntry.Description = Strings.MainMenuScreen_Windowed;
                GameLoop.Instance.ToggleFullScreen();
            }
        }

        //void purchaseEntry_OnSelectEvent(object sender, EventArgs e)
        //{
        //    /// needs to be implemented
        //}

        #endregion

        #region Update and Draw


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            /// In case the user purchased the game from this screen
            //if (isTrial && !Guide.IsTrialMode)
            //{
            //    menuOptions.RemoveAt(menuOptions.Count - 1);
            //    isTrial = false;
            //}
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color color = Color.White * TransitionAlpha;

            spriteBatch.Draw(background, Vector2.Zero, color);

            foreach (MenuOption entry in menuOptions)
            {
                entry.Draw(spriteBatch, gameTime);
            }

            Vector2 position = menuOptions[selectedOption].Position + new Vector2(-29, -1);
            spriteBatch.Draw(ScreenManager.PointerTexture, position, null, color, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
        }

        #endregion
    }
}
