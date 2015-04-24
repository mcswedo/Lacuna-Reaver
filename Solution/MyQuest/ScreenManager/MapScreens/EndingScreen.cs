using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class EndingScreen : MenuScreen
    {
        #region Fields

        string storyText;
        string endText;
        const int dialogColumWidth = 745;
        Vector2 storyTextPos;
        SpriteFont font;
        Vector2 endTextPos;
        double secondsRemaining = 2;
        float alpha = 1;

        const float defaultScrollRate = .5f;
        const float fastScrollRate = 4.0f;
        const float reverseScrollRate = -defaultScrollRate;
        float scrollRate = defaultScrollRate;


        #endregion

        #region Initialization


        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(5.0);
            TransitionOffTime = TimeSpan.FromSeconds(2.0);

            font = Fonts.MenuTitle2;

            if (Party.Singleton.PartyAchievements.Contains(WillDiesSceneA.willDiedAchievement))
            {
                storyText = Strings.ZA674 +
                     "\n \n" +
                     Strings.ZA718 +
                     "\n \n" +
                    Strings.ZA676+
                    "\n \n" +
                    Strings.ZA670 +
                    "\n \n" +
                    Strings.ZA675;
            }
            else
            {
                storyText = Strings.ZA669 +
                     "\n \n" +
                    Strings.ZA670 +
                    "\n \n" +
                    Strings.ZA671 +
                    "\n \n" +
                    Strings.ZA672;
            }

            storyTextPos = new Vector2(300, 720);

            storyText = centerText(storyText);

            endText = Strings.ZA673;

            endTextPos = new Vector2(400, 720);

            MusicSystem.Play("GameOver");
        }

        #endregion

        public string centerText(String text)
        {
            int charIndex = text.Length;
            int i = 0;
            string textToWrite = text.Substring(0);
            Vector2 fontDimensions = font.MeasureString(textToWrite);
            while (true)
            {
                while (fontDimensions.X > dialogColumWidth || textToWrite[charIndex - 1] != ' ')
                {
                    charIndex--;
                    textToWrite = text.Substring(0, charIndex);
                    fontDimensions = font.MeasureString(textToWrite);
                    i = charIndex;
                }

                //return textToWrite; 


                text = text.Insert(i, "\n");

                charIndex = text.Length;

                textToWrite = text.Substring(i);

                fontDimensions = font.MeasureString(textToWrite);

                if (fontDimensions.X <= dialogColumWidth)
                    return text;

            }
            //return text; 
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsScrollUp())
            {
                scrollRate = fastScrollRate;
            }
            else if (InputState.IsScrollDown())
            {
                scrollRate = reverseScrollRate;
            }
            else
            {
                if (scrollRate != 0)
                {
                    scrollRate = defaultScrollRate;
                }
                if (InputState.IsMenuSelect())
                {
                    if (scrollRate == 0)
                    {
                        scrollRate = defaultScrollRate;
                    }
                    else
                    {
                        scrollRate = 0;
                    }
                }
            }
        }

        #region Update and Draw


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);


            if (storyTextPos.Y >= -font.MeasureString(storyText).Y)
                storyTextPos.Y -= scrollRate;
            else
            {
                if (endTextPos.Y >= -font.MeasureString(endText).Y + 360)
                    endTextPos.Y -= scrollRate;
                else
                {
                    secondsRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (secondsRemaining <= 0)
                    {
                        alpha -= .01f;
                        if (alpha <= 0)
                        {
                            ScreenManager.Singleton.ExitAllScreens();
                            ScreenManager.Singleton.AddScreen(new CreditsScreen(true));
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
          SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

          GameLoop.Instance.BeginNonPremultipliedBlendStateDraw();
          ScreenManager.Singleton.TintBackBuffer(1, Color.Black, spriteBatch);
   
          spriteBatch.DrawString(font, storyText, storyTextPos, Color.White);

          spriteBatch.DrawString(font, endText, endTextPos, new Color(255, 255, 255, alpha));
          GameLoop.Instance.RestoreNormalDraw();
        }

        #endregion
    }
}
