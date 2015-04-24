using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

/// Finished

namespace MyQuest
{
    public class ScreenManager
    {
        #region Singleton


        static ScreenManager singleton = new ScreenManager();

        public static ScreenManager Singleton
        {
            get { return singleton; }
            set { singleton = value; }
        }


        private ScreenManager()
        {
        }


        #endregion

        #region Fields


        /// <summary>
        /// A blank texture which can be used to draw rectangles of any size and color
        /// </summary>
        Texture2D blankTexture;

        public Texture2D BlankTexture
        {
            get { return blankTexture; }
        }


        /// <summary>
        /// Represents the current screen resolution. Usefull for stretching things to fill the screen
        /// </summary>
//        Rectangle screenResolution;

        public Rectangle ScreenResolution
        {
            get { return GameLoop.Instance.ScreenDimensions; }
        }

        private static Vector2 arrowScale = new Vector2(0.55f, 0.55f);
        public static Vector2 ArrowScale
        {
            get { return ScreenManager.arrowScale; }
        }

        private static Vector2 smallArrowScale = new Vector2(0.35f, 0.35f);
        public static Vector2 SmallArrowScale
        {
            get { return ScreenManager.smallArrowScale; }
        }

        private static Texture2D pointerTexture;
        public static Texture2D PointerTexture
        {
            get { return ScreenManager.pointerTexture; }
        }

        #endregion

        #region SpriteBatch


        //SpriteBatch spriteBatch;

        //internal SpriteBatch SpriteBatch
        //{
        //    get { return spriteBatch; }
        //}


        #endregion

        #region ContentManager


        /// <summary>
        /// We maintain a reference to the GameEngine's content 
        /// manager so that we can load screens whenever we want
        /// </summary>
        //ContentManager content;


        #endregion

        #region Screens


        /// <summary>
        /// The list of active screens
        /// </summary>
        List<Screen> screens = new List<Screen>();


        /// <summary>
        /// The list of screens we are currently updating. Making this a field
        /// means we don't have to create a new list every update
        /// </summary>
        List<Screen> screensToUpdate = new List<Screen>();


        #endregion

        #region Initialization

        public void Initialize()
        {
            //this.content = content;
            
            blankTexture = GameLoop.Instance.TemporaryContentManager.Load<Texture2D>(GameLoop.textureFolder + "BlankTexture");
            pointerTexture = GameLoop.Instance.TemporaryContentManager.Load<Texture2D>(Screen.interfaceTextureFolder + "Arrow");
        }

        #endregion

        #region Public Methods


        public void Update(GameTime gameTime)
        {
            screensToUpdate.Clear();

            /// Make a working copy of all the active screens. We do this 
            /// because the list of active screens may change during this update.
            foreach (Screen screen in screens)
            {
                screensToUpdate.Add(screen);
            }

            bool coveredByOtherScreen = false;
            bool otherScreenHasFocus = false;

            while (screensToUpdate.Count > 0)
            {
                /// Update the screens from back to front because we use it as a stack.
                Screen screen = screensToUpdate[screensToUpdate.Count - 1];
                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.FullyOn || screen.ScreenState == ScreenState.TransitionOn)
                {
                    /// If no other screens have focus, then this one can HandleInput. We only allow the
                    /// topmost screen to handle input so once we find that one, we make a note of it.
                    if (!otherScreenHasFocus)
                    {
                        Debug.Assert(screen.WillHandleUserInput);
                        screen.HandleInput(gameTime);

                        /// No other screen has focus.
                        if (screen.WillHandleUserInput)
                        {
                            otherScreenHasFocus = true;
                        }
                    }

                    /// If this screen isn't a pop up and the screen is still Active
                    /// then all the screens beneath it need to be told that they are covered.
                    if (!screen.IsPopup && screen.WillHandleUserInput)
                    {
                        coveredByOtherScreen = true;
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            bool drawn = false;

            foreach (Screen screen in screens)
            {
                if (screen.ScreenState != ScreenState.Hidden)
                {
                    drawn = true;
                    screen.Draw(gameTime);
                }
            }

            if (!drawn)
            {
                //The following assertion fails if all screens are hidden.
                Debug.Assert(false);
            }
            MessageOverlay.Draw(gameTime);
        }


        public void AddScreen(Screen screen)
        {
            screens.Add(screen);
            screen.ScreenManager = this;
            screen.LoadContent(GameLoop.Instance.TemporaryContentManager);
            screen.Initialize();
        }

        public void RemoveScreen(Screen screen)
        {
            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }

        /// <summary>
        /// Remove all screens, ignoring their off transitions
        /// </summary>
        public void ExitAllScreens()
        {
            screens.Clear();
            screensToUpdate.Clear();
        }

        public void ExitAllScreensAboveTileMapScreen()
        {
            Screen screen = screens[screens.Count - 1];
            while (!(screen is TileMapScreen))
            {
                RemoveScreen(screen);
                screen = screens[screens.Count - 1];
            }
        }

        public void TintBackBuffer(float alpha, Color color, SpriteBatch spriteBatch)
        {
            //SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            color.A = (byte)(alpha * 255);
            spriteBatch.Draw(BlankTexture, ScreenResolution, color);

            //SpriteBatch.End();
        }

        #endregion
    }
}
