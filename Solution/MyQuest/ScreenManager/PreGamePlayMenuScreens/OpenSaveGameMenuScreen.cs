using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

/// Needs graphics

namespace MyQuest
{
    /// <summary>
    /// This screen allows the user to load a previously saved game
    /// </summary>
    public class OpenSaveGameMenuScreen : MenuScreen
    {
        #region Fields


        Texture2D background;

        Texture2D pointer;


        #endregion

        #region Initialization


        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);

            menuOptions = new List<MenuOption>();

            float offset = 0f;
            foreach (string savefile in PartySerializer.SaveGameFiles)
            {
                menuOptions.Add(new StaticMenuOption(this, savefile, new Vector2(650, 250 + offset)));
                offset += Fonts.MenuTitle.LineSpacing;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(ContentPath.ToBackgrounds + "IntroScreenBackground");

            pointer = content.Load<Texture2D>(ContentPath.ToInterface + "MainMenuPointer");
        }


        #endregion

        #region Update and Draw


        public override void HandleInput(GameTime gameTime)
        {
            if(menuOptions.Count > 0)
                base.HandleInput(gameTime);

            if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();
            }
            else if (menuOptions.Count > 0 && InputState.IsMenuSelect())
            {
                Debug.Assert(false);
                PartySerializer.LoadFrom(menuOptions[selectedOption].Description);
                LoadingScreen.Singleton.LoadingFinished += new EventHandler(loadingScreen_LoadingFinished);
                ScreenManager.AddScreen(LoadingScreen.Singleton);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, color);
            spriteBatch.DrawString(Fonts.MenuItem, "Continue From", new Vector2(550, 200), color);

            if (menuOptions.Count > 0)
            {
                foreach (MenuOption entry in menuOptions)
                    entry.Draw(spriteBatch, gameTime);

                spriteBatch.Draw(pointer, menuOptions[selectedOption].Position - new Vector2(pointer.Width + 4, 0), color);
            }
            else
            {
                spriteBatch.DrawString(Fonts.MenuTitle, "No Save Files Found", new Vector2(650, 250), color);
            }

            spriteBatch.End();
        }

        void loadingScreen_LoadingFinished(object sender, EventArgs e)
        {
            /// We loaded from storage so this can't be a new game
            LoadingPartyScreen.Singleton.IsNewGame = false;
            LoadingScreen.Singleton.LoadingFinished -= loadingScreen_LoadingFinished;
            ScreenManager.AddScreen(LoadingPartyScreen.Singleton);
        }


        #endregion        
    }
}
