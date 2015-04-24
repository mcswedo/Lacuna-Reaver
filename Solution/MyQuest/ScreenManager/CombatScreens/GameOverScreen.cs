using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class GameOverScreen : MenuScreen
    {
        Texture2D background;

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "gameover");
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(5.0);
            TransitionOffTime = TimeSpan.FromSeconds(2.0);

            float xpos = GlobalSettings.TitleSafeArea.Center.X;
            float ypos = GlobalSettings.TitleSafeArea.Center.Y + 200;

            menuOptions = new List<MenuOption>();

            StaticMenuOption loadGameEntry = null;

            if (Party.Singleton.CanContinue)
            {
                loadGameEntry = new StaticMenuOption(this, Strings.ZA176, new Vector2(xpos, ypos));
                loadGameEntry.OnSelectEvent += new EventHandler(loadGameEntry_OnSelectEvent);
            }
            else
            {
                loadGameEntry = new StaticMenuOption(this, Strings.ZA177, new Vector2(xpos, ypos));
                loadGameEntry.OnSelectEvent += new EventHandler(restartGameEntry_OnSelectEvent);
            }

            ypos += Fonts.MenuItem2.LineSpacing;

            StaticMenuOption exitEntry = new StaticMenuOption(this, Strings.ZA178, new Vector2(xpos, ypos));
            ypos += Fonts.MenuItem2.LineSpacing;

            exitEntry.OnSelectEvent += new EventHandler(exitEntry_OnSelectEvent);

            menuOptions.Add(loadGameEntry);
            menuOptions.Add(exitEntry);

            MusicSystem.Play("GameOver");
        }

        void loadingScreen_LoadingFinished(object sender, EventArgs e)
        {
            /// We loaded from storage so this can't be a new game
            LoadingPartyScreen.Singleton.IsNewGame = false;
            LoadingScreen.Singleton.LoadingFinished -= loadingScreen_LoadingFinished;
            ScreenManager.AddScreen(LoadingPartyScreen.Singleton);
        }

        void restartGameEntry_OnSelectEvent(object sender, EventArgs e)
        {
            ScreenManager.ExitAllScreens();
            LoadingPartyScreen.Singleton.IsNewGame = true;
            ScreenManager.AddScreen(LoadingPartyScreen.Singleton);
        }

        void loadGameEntry_OnSelectEvent(object sender, EventArgs e)
        {
            LoadingScreen.Singleton.LoadingFinished += new EventHandler(loadingScreen_LoadingFinished);
            ScreenManager.AddScreen(LoadingScreen.Singleton);
            PartySerializer2.LoadFrom(Party.saveFileName);
        }

        void exitEntry_OnSelectEvent(object sender, EventArgs e)
        {
            ScreenManager.RemoveScreen(this);
            ScreenManager.AddScreen(new MainMenuScreen());
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
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
    }
}
