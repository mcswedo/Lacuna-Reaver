using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyQuest
{
    /// <summary>
    /// Simple loading screen that displays a message while
    /// the PartySerializer is loading from the Storage Device
    /// </summary>
    public class LoadingScreen : Screen
    {
        #region Singleton


        static LoadingScreen singleton = new LoadingScreen();

        public static LoadingScreen Singleton
        {
            get { return singleton; }
        }

        private LoadingScreen()
        {
            IsPopup = true;
        }


        #endregion

        Texture2D background;

        Vector2 textLocation = new Vector2(GlobalSettings.TitleSafeArea.Right - 100, GlobalSettings.TitleSafeArea.Bottom - 200);

        public event EventHandler LoadingFinished;
       
        public override void Initialize()
        {
            Debug.Assert(LoadingFinished != null);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Conversation_Log_Screen");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
//            if (!PartySerializer.WaitingOnStorage)
//            {
                ScreenManager.RemoveScreen(this);
                LoadingFinished(null, null);
//            }
        }

        public override void Draw(GameTime gameTime)
        {
            GameLoop.Instance.AltSpriteBatch.Draw(background, Vector2.Zero, Color.White * TransitionAlpha);
            GameLoop.Instance.AltSpriteBatch.DrawString(Fonts.MenuTitle2, "Loading Storage...", textLocation, Fonts.MenuTitleColor * TransitionAlpha);
        }
    }    
}
