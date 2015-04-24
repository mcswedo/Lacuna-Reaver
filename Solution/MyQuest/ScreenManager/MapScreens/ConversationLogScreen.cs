using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// Finished

namespace MyQuest
{
    class ConversationLogScreen : Screen
    {
        #region Constants

        /// <summary>
        /// The allotted width for the dialog column
        /// </summary>
        const int dialogColumWidth = 740;

        /// <summary>
        /// Controls the scroll speed
        /// </summary>
        const float scrollSpeed = 2.5f;

        const float dividerSpacing = 20f;


        #endregion

        #region Fields


        Vector2 backgroundLocation;
        Vector2 upArrowLocation;
        Vector2 downArrowLocation;

        /// <summary>
        /// The offset of the divider bar into the background image
        /// </summary>
        float dividerBarXOffset;

        Texture2D background;
        Texture2D dividerBar;
        Texture2D upArrow;
        Texture2D downArrow;

        List<StringBuilder> locations;
        List<StringBuilder> speakers;
        List<StringBuilder> dialog;

        GraphicsDevice device;
        //static readonly Viewport viewport = new Viewport(220, 162, 780, 398);
        Viewport viewport;
        Viewport defaultViewport;

        float scrollY;
        float totalTextHeight;
        float lineSpacing;

        #endregion

        #region Initialization


        public override void Initialize()
        {
            TransitionOffTime = TimeSpan.FromSeconds(0.15);
            TransitionOnTime = TimeSpan.FromSeconds(0.15);

            if (GameLoop.Instance.IsFullScreen)
            {
                viewport = new Viewport(220, 162, 780, 398);
            }
            else
            {
                viewport = new Viewport(160, 112, 492, 258);
            }

            backgroundLocation = new Vector2(
                ScreenManager.ScreenResolution.Width / 2 - background.Width / 2,
                ScreenManager.ScreenResolution.Height / 2 - background.Height / 2);

            upArrowLocation = new Vector2(backgroundLocation.X + 905, backgroundLocation.Y + 100);
            downArrowLocation = new Vector2(backgroundLocation.X + 905, backgroundLocation.Y + 430);

            scrollY = 0f;
            totalTextHeight = 0f;

//            device = GameLoop.Instance.Services.GetService(typeof(GraphicsDevice)) as GraphicsDevice;
            device = GameLoop.Instance.GraphicsDevice;
            defaultViewport = device.Viewport;

            dividerBarXOffset = backgroundLocation.X + 77;

            locations = new List<StringBuilder>();
            speakers = new List<StringBuilder>();
            dialog = new List<StringBuilder>();

            /// The mess below examines each string of dialog in the log. When
            /// a piece of dialog is too long (in pixels) to fit within the
            /// viewport, the code below separates the dialog into multiple
            /// lines.

            for(int i = Party.Singleton.GameState.Log.Count - 1; i >= 0; i--)
            {
                LogEntry entry = Party.Singleton.GameState.Log[i];
                speakers.Add(new StringBuilder(Strings.ConversationLogScreen_Speaker + " " + entry.Speaker));
                locations.Add(new StringBuilder(Strings.ConversationLogScreen_Location + " " + entry.Location));

                int endOfName = entry.Dialog.IndexOf(":");

                string theDialog;
                if (endOfName != -1)
                {
                    while (entry.Dialog[++endOfName] == ' ') ;
                    theDialog = entry.Dialog.Remove(0, endOfName);
                }
                else
                {
                    theDialog = entry.Dialog;
                }

                /// If this will fit in the viewport than we don't have to do anything special
                if (Fonts.MenuItem2.MeasureString(theDialog).X < dialogColumWidth - 65)
                {
                    dialog.Add(new StringBuilder(theDialog));
                    totalTextHeight +=
                        (Fonts.MenuItem2.MeasureString(theDialog).Y + 2 * Fonts.MenuItem2.LineSpacing + 2 * dividerSpacing);
                }
                else
                {
                    /// Otherwise, grab each word from the dialog string and add it to the current line
                    /// if it will fit. If not, add a newline and then add the word. Repeat until the
                    /// entire string has been processed

                    float width = 0f;
                    int index = 0;
                    StringBuilder temp = new StringBuilder();
                    bool finished = false;
                    while (!finished)
                    {
                        int word = theDialog.IndexOf(' ', index);
                        string sub;

                        if (word < 0)
                        {
                            sub = theDialog.Substring(index);
                            finished = true;
                        }
                        else
                            sub = theDialog.Substring(index, word - index + 1);

                        width += Fonts.MenuItem2.MeasureString(sub).X;

                        if (width > dialogColumWidth - 65)
                        {
                            width = 0;
                            temp.Append("\n");
                        }

                        temp.Append(sub);

                        index = word + 1;
                        if (index >= theDialog.Length - 1)
                            break;
                    }

                    dialog.Add(temp);
                    totalTextHeight += 
                        (Fonts.MenuItem2.MeasureString(temp).Y + 2 * Fonts.MenuItem2.LineSpacing + 2 * dividerSpacing);
                }
            }

            totalTextHeight -= 2 * dividerSpacing;
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Conversation_Log_Screen");
            dividerBar = content.Load<Texture2D>(interfaceTextureFolder + "Divider_bar");
            upArrow = content.Load<Texture2D>(interfaceTextureFolder + "Large_up_arrow");
            downArrow = content.Load<Texture2D>(interfaceTextureFolder + "Large_down_arrow");
            lineSpacing = Fonts.MenuItem2.LineSpacing;
        }


        #endregion

        #region Update and Draw


        public override void HandleInput(GameTime gameTime)
        {
            base.HandleInput(gameTime);
            if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();
                SoundSystem.Play(AudioCues.menuDeny);
            }
            else if (InputState.IsScrollDown())
            {
                scrollY += scrollSpeed;
            }
            else if (InputState.IsScrollUp())
            {
                scrollY -= scrollSpeed;
            }

            scrollY = MathHelper.Clamp(scrollY, 0, totalTextHeight - viewport.Height);
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color whiteColor = Color.White * TransitionAlpha;
            Color blackColor = Color.Black * TransitionAlpha;
            Color redColor = Color.Red * TransitionAlpha;

            Vector2 titleLocation = backgroundLocation + new Vector2(100, 90);

            spriteBatch.Draw(background, backgroundLocation, whiteColor);

            spriteBatch.Draw(upArrow, upArrowLocation, scrollY > 0 ? whiteColor : redColor);

            spriteBatch.Draw(
                downArrow,
                downArrowLocation,
                scrollY >= totalTextHeight - viewport.Height || totalTextHeight - viewport.Height < 0 ? redColor : whiteColor);

            spriteBatch.End();

            device.Viewport = viewport;

            //altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, windowedScalingMatrix);

            //Matrix transform = GameLoop.Instance.windowedScalingMatrix;
            Matrix transform = Matrix.Identity;
            if (!GameLoop.Instance.IsFullScreen)
            {
                transform = GameLoop.Instance.windowedScalingMatrix;
            }
            transform.Translation = new Vector3(0, -scrollY, 0);

            spriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                transform);
//                Matrix.CreateTranslation(0, -scrollY, 0));

            Vector2 textLocation = new Vector2(0, 0);

            for (int i = 0; i < dialog.Count; ++i)
            {
                spriteBatch.DrawString(Fonts.MenuItem2, locations[i], textLocation, Fonts.MenuItemColor * TransitionAlpha);
                spriteBatch.DrawString(Fonts.MenuItem2, speakers[i], new Vector2(textLocation.X, textLocation.Y + lineSpacing), Fonts.MenuItemColor * TransitionAlpha);
                spriteBatch.DrawString(Fonts.MenuItem2, dialog[i], new Vector2(textLocation.X, textLocation.Y + 2 * lineSpacing), Fonts.MenuItemColor * TransitionAlpha);

                textLocation.Y += Fonts.MenuItem2.MeasureString(dialog[i]).Y + 2 * lineSpacing;

                if (i < dialog.Count - 1)
                {
                    spriteBatch.Draw(dividerBar, new Vector2(0, textLocation.Y + 20), whiteColor);
                }

                textLocation.Y += 40;
            }

            device.Viewport = defaultViewport;
        }



        #endregion
    }
}
