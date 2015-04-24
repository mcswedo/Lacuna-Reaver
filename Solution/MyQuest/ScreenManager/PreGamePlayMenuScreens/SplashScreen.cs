using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class SplashScreen : Screen
    {
        static SplashScreen singleton = new SplashScreen();

        internal static SplashScreen Singleton
        {
            get { return singleton; }
        }

        private SplashScreen() :
            base()
        {
        }

        Texture2D background;

        // The amount of time to wait until we will tell the user to press spacebar.
        TimeSpan inputWaitDelay = TimeSpan.FromSeconds(3.0);

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.0);
            MusicSystem.Play(AudioCues.menuCue);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "IntroScreenBackground");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (ScreenState == ScreenState.FullyOn)
            {
                inputWaitDelay -= gameTime.ElapsedGameTime;
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsSplashScreenAction())
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new MainMenuScreen());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            spriteBatch.Draw(background, Vector2.Zero, Color.White * TransitionAlpha);

            if (inputWaitDelay <= TimeSpan.Zero && WillHandleUserInput)
            {
                spriteBatch.DrawString(Fonts.SplashScreenText, Strings.SplashScreen_PressSpace, new Vector2(553, 661), Fonts.SplashScreenTextColor * TransitionAlpha);
            }

            spriteBatch.DrawString(Fonts.SplashScreenText, "Rev. 1721", new Vector2(1163, 670), Fonts.SplashScreenTextColor * TransitionAlpha);
        }
    }
}