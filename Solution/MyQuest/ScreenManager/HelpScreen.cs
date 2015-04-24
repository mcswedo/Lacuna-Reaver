using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    /// <summary>
    /// The HelpScreen provides instruction to new Players to help them
    /// learn the controls of the game for a variety of important screens.
    /// This is nothing more than a series of static images that the Player
    /// can cycle through by pressing the shoulder buttons or the spacebar.
    /// 
    /// Right now the images are just placeholders. We need to replace them.
    /// </summary>
    public class HelpScreen : Screen
    {
        #region Fields

        Texture2D[] background = new Texture2D[2];

        int currentScreen = 0;

        #endregion

        #region Initialization


        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);
        }

        public override void LoadContent(ContentManager content)
        {
            // This is where we must put the HelpScreen images.
            background[0] = content.Load<Texture2D>(backgroundTextureFolder + "KeyboardControlLayout");
            background[1] = content.Load<Texture2D>(backgroundTextureFolder + "XboxControlLayout");
        }


        #endregion

        #region Update Logic


        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsSwitchCharacterLeft() || InputState.IsSwitchCharacterRight() ||
                InputState.IsMenuSelect())
            {
                // Previous Screen, if there is one
                if (currentScreen < background.Length)
                {
                    ++currentScreen;
                }

                if (currentScreen >= background.Length)
                {
                    currentScreen = 0;
                }
            }
            else if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();
                return;
            }
        }


        #endregion

        #region Render

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;
            Color color = Color.White * TransitionAlpha;
            spriteBatch.Draw(background[currentScreen], GlobalSettings.TitleSafeArea, color);
        }

        #endregion
    }
}
