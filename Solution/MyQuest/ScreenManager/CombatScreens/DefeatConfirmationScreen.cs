using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    /*
     * I am modifying this class so that it displays nothing, accepts no user input,
     * and just clears itself after 3 seconds.
     */
    class DefeatConfirmationScreen : Screen
    {
        #region Fields

        Texture2D background;

        static readonly Vector2 position = new Vector2(GlobalSettings.TitleSafeArea.Left + 5, GlobalSettings.TitleSafeArea.Top + 5);
        static readonly Rectangle DefaultTextBoxPosition = GlobalSettings.TitleSafeArea;

        TimeSpan timeToClose;

        #endregion

        #region Initialization

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            IsPopup = true;

            float xpos = position.X + 350;
            float ypos = position.Y + 100;

            timeToClose = TimeSpan.FromSeconds(3);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Yes_no_box");
        }

        #endregion

        #region Methods


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            timeToClose -= gameTime.ElapsedGameTime;
            if (timeToClose < TimeSpan.Zero)
            {
                ScreenManager.ExitAllScreens();
                ScreenManager.AddScreen(new GameOverScreen());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Vector2 boxPosition;
            boxPosition.X = DefaultTextBoxPosition.X + 300;
            boxPosition.Y = DefaultTextBoxPosition.Y;

            spriteBatch.Draw(background, new Rectangle((int)boxPosition.X, (int)boxPosition.Y, 500, 100), Color.White * TransitionAlpha);

            Vector2 textPosition = position;
            textPosition.X = position.X + 355;
            textPosition.Y = position.Y + 35;

            spriteBatch.DrawString(Fonts.MenuTitle2, Strings.ZA175, textPosition, Fonts.MenuTitleColor * TransitionAlpha);
        }

        #endregion
    }
}
