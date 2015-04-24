using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    /// <summary>
    /// The HelpScreen provides instruction to new Players to help them
    /// learn the controls of the game for a variety of important screens.
    /// This is nothing more than a series of static images that the Player
    /// can cycle through by pressing the shoulder buttons or A and B.
    /// 
    /// Right now the images are just placeholders. We need to replace them.
    /// </summary>
    public class HelpScreen : Screen
    {

        #region Fields

        Texture2D[] background = new Texture2D[2];

        int numberOfScreens = 2;

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
            background[0] = content.Load<Texture2D>(ContentPath.ToBackgrounds + "InventoryScreenBackground");
            background[1] = content.Load<Texture2D>(ContentPath.ToBackgrounds + "Shop_screen");
        }


        #endregion

        #region Update Logic


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void HandleInput()
        {
            if (InputState.IsSwitchCharacterLeft() ||
                InputState.IsMenuCancel())
            {
                // Previous Screen, if there is one
                if(currentScreen > 0)
                    --currentScreen;
                if(currentScreen < 0)
                    currentScreen = 0;
            }
            else if (InputState.IsSwitchCharacterRight() ||
                     InputState.IsMenuSelect())
            {
                // Next Screen, if there is one
                if (currentScreen < numberOfScreens - 1)
                    ++currentScreen;
                // If not, close the HelpScreen
                else if (currentScreen == numberOfScreens - 1)
                {
                    ExitAfterTransition();
                    return;
                }
            }
            else if (InputState.IsTileMapExit())
            {
                ExitAfterTransition();
                return;
            }
        }


        #endregion

        #region Render


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            spriteBatch.Draw(background[currentScreen], GlobalSettings.TitleSafeArea, color);

            spriteBatch.End();
        }


        #endregion

        #region Transitions

        protected override bool TransitionOn(GameTime gameTime)
        {
            return base.FadeTransitionOn(gameTime);
        }

        protected override bool TransitionOff(GameTime gameTime)
        {
            return base.FadeTransitionOff(gameTime);
        }

        #endregion
    }
}
