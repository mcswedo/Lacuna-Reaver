using Microsoft.Xna.Framework;

namespace MyQuest
{
    /// <summary>
    /// Simple loading screen that displays a message while
    /// the Party performs initialization and startup
    /// </summary>
    public class LoadingPartyScreen : Screen
    {
        #region Singleton


        static LoadingPartyScreen singleton = new LoadingPartyScreen();

        public static LoadingPartyScreen Singleton
        {
            get { return singleton; }
        }

        private LoadingPartyScreen()
        {
            IsPopup = true;
        }


        #endregion

        #region Fields


        Vector2 textLocation = new Vector2(GlobalSettings.TitleSafeArea.Right - 100, GlobalSettings.TitleSafeArea.Bottom - 100);

        bool isNewGame = false;

        public bool IsNewGame
        {
            get { return isNewGame; }
            set { isNewGame = value; }
        }


        #endregion

        #region Update and Draw


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            ScreenManager.ExitAllScreens();
            //TileMapScreen.Instance.IsCutScenePlaying = false;
            TileMapScreen.Reset();
            Party.Singleton.Initialize(isNewGame);
            ScreenManager.AddScreen(TileMapScreen.Instance);
        }

        public override void Draw(GameTime gameTime)
        {
            GameLoop.Instance.AltSpriteBatch.DrawString(Fonts.MenuTitle2, "Loading Party ...", textLocation, Fonts.MenuTitleColor * TransitionAlpha);
        }

        #endregion
    }
}
