using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// Finished under current specifications

namespace MyQuest
{
    /// <summary>
    /// Enum describing the state of the Dialog Engine
    /// </summary>
    public enum DialogState
    {
        /// <summary>
        /// Currently processing the dialog text
        /// </summary>
        Processing,

        /// <summary>
        /// All text has been rendered and a response has been received
        /// </summary>
        Finished,

        /// <summary>
        /// The current line of text has been rendered and the engine
        /// is in a pause state. There are still more lines of text to 
        /// process but we're waiting for the user to press continue
        /// </summary>
        WaitingToContinue,

        /// <summary>
        /// All text has been rendered and the engine is
        /// waiting for the user to select a response
        /// </summary>
        WaitingForResponse,

        /// <summary>
        /// The DialogEngine is waiting for the user
        /// to "close" the dialog by pressing a button.
        /// </summary>
        WaitingOnClose
    }

    public class DialogScreen : Screen
    {
        Dialog dialog;
        
        #region Defaults

        static readonly Rectangle DefaultTextBoxPosition = new Rectangle(
            GlobalSettings.TitleSafeArea.Left + 155,
            GlobalSettings.TitleSafeArea.Top + 15,
            400,
            GlobalSettings.TitleSafeArea.Top + 155);

        static readonly Rectangle DefaultTextBoxBounds = new Rectangle(
            DefaultTextBoxPosition.X + 20,
            DefaultTextBoxPosition.Y + 20,
            DefaultTextBoxPosition.Width - 35,
            DefaultTextBoxPosition.Height - 20);

        static readonly Rectangle DefaultPortraitPosition = new Rectangle(
            DefaultTextBoxPosition.X - 150,
            DefaultTextBoxPosition.Y - 10,
            DefaultTextBoxPosition.Width - 200,
            DefaultTextBoxPosition.Height - 50);

        #endregion

        #region Text


        /// <summary>
        /// A collection of strings to be processed
        /// </summary>
        string[] pendingText;
        //int numberOfLinesRendered = 0;

        /// <summary>
        /// The collection of strings that is being rendered. Note that we use StringBuilder
        /// instead of string. A string is immutable which means anytime you modify
        /// its contents, it is replaced by a newly allocated string. We can avoid
        /// unnecessary GC by using the StringBuilder, which will only allocate a new
        /// object when it needs to be resized.
        /// </summary>
        List<StringBuilder> renderedText = new List<StringBuilder>();

        List<string> menuOptions = new List<string>();

        int selectedOption;


        #endregion

        #region State


        DialogState state = DialogState.Finished;

        /// <summary>
        /// The current state of the DialogEngine
        /// </summary>
        public DialogState State
        {
            get { return state; }
        }
        

        #endregion
    
        #region Enum
        
        public enum Location 
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        #endregion

        #region Processing Data


        /// <summary>
        /// The current line of text that is being processed
        /// </summary>
        int pendingTextIndex;
        

        /// <summary>
        /// The current character within the current line being processed
        /// </summary>
        int toRenderChar;
        

        /// <summary>
        /// The first line of text to be rendered inside the textbox
        /// </summary>
        int firstLineToRender;
        

        /// <summary>
        /// The maximum number of lines to render at once (within the textbox)
        /// </summary>
        int maxLines;

        TimeSpan letterDelay;

        TimeSpan shortLetterDelay;

//        DialogPrompt options = DialogPrompt.None;
        DialogPrompt options = DialogPrompt.NeedsClose;

        PartyResponse partyResponse;

        string portraitName = "Stub"; 

        bool hasPortrait;

        bool flipped; 

        #endregion

        #region Graphics


        Rectangle textBoxPosition;
        Rectangle textBoxBounds;
        Rectangle portraitPosition;

        Texture2D textBox;
        Texture2D portrait;
        Texture2D arrow;
//        Texture2D selectionArrow;

        Vector2 textPosition;
        
        //float arrowAlphaPosition;
        //TransitionDirection arrowFadeDirection;

//        SpriteFont textFont;
        
        int fontHeight;


        #endregion

        #region Initialization

        public DialogScreen(Dialog dialog, Location location)
        {
            this.dialog = dialog;
            Positioning(location);
            hasPortrait = false;
            Debug.Assert(dialog != null);
            Debug.Assert(state == DialogState.Finished);
        }

        public DialogScreen(Dialog dialog, Location location, string portraitName)
        {
            this.dialog = dialog;
            Positioning(location);
            this.portraitName = portraitName;
            hasPortrait = true; 
            Debug.Assert(dialog != null);
            Debug.Assert(state == DialogState.Finished);
        }

        //public DialogScreen(Dialog dialog, Rectangle? position, Rectangle? bounds)
        //{
        //    this.dialog = dialog;

        //    Debug.Assert(dialog != null);
        //    Debug.Assert(state == DialogState.Finished);

        //    textBoxPosition = position ?? DefaultTextBoxPosition;
        //    textBoxBounds = bounds ?? DefaultTextBoxBounds;
        //    portraitPosition = position ?? DefaultPortraitPosition;
        //}

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            IsPopup = true;

            //textFont = Fonts.MenuTitle2;
            //fontHeight = textFont.LineSpacing;
            fontHeight = Fonts.MenuTitle2.LineSpacing;

            letterDelay = GlobalSettings.DialogueLetterDelay;
            shortLetterDelay = GlobalSettings.DialogueShortLetterDelay;

            textPosition = new Vector2(textBoxBounds.X, textBoxBounds.Y);

            selectedOption = 0;

            menuOptions = new List<string>();
            
            switch (dialog.Prompt)
            {
                case DialogPrompt.Continue:
                    throw new Exception("We are not using Continue anymore.");
                    //menuOptions.Add("Continue");
                    //break;
                case DialogPrompt.Ok:
                    menuOptions.Add(Strings.OK);
                    break;
                case DialogPrompt.OkCancel:
                    menuOptions.Add(Strings.OK);
                    menuOptions.Add(Strings.Cancel);
                    break;
                case DialogPrompt.YesNo:
                    menuOptions.Add(Strings.Yes);
                    menuOptions.Add(Strings.No);
                    break;
            }

            options = dialog.Prompt;

            state = DialogState.Processing;
            partyResponse = PartyResponse.None;

            pendingTextIndex = 0;
            toRenderChar = 0;
            firstLineToRender = 0;

            maxLines = textBoxBounds.Height / fontHeight;

            renderedText.Clear();
            renderedText.Add(new StringBuilder());

            pendingText = dialog.Text;
        }

        public override void LoadContent(ContentManager content)
        {
            textBox = content.Load<Texture2D>(backgroundTextureFolder + "Instruction_box");
            portrait = content.Load<Texture2D>(portaitTextureFolder + portraitName);
            //arrow = content.Load<Texture2D>(interfaceTextureFolder + "xboxControllerButtonA");
            arrow = content.Load<Texture2D>(interfaceTextureFolder + "CheckMark");
        }

        #endregion

        #region Helper

        private void Positioning(Location location)
        {
            switch (location)
            {
                case Location.TopLeft:

                    textBoxPosition = DefaultTextBoxPosition;
                    textBoxBounds = DefaultTextBoxBounds;
                    portraitPosition = DefaultPortraitPosition;
                    flipped = false;
                    break;

                case Location.TopRight:

                    textBoxPosition = new Rectangle(GlobalSettings.TitleSafeArea.Right - 605,
                                                    GlobalSettings.TitleSafeArea.Top + 15,
                                                    400,
                                                    GlobalSettings.TitleSafeArea.Top + 155);

                    textBoxBounds = new Rectangle(textBoxPosition.X + 20,
                                                  textBoxPosition.Y + 20,
                                                  textBoxPosition.Width - 35,
                                                  textBoxPosition.Height - 20);

                    portraitPosition = new Rectangle(textBoxPosition.X + 350,
                                                     textBoxPosition.Y - 10,
                                                     textBoxPosition.Width - 200,
                                                     textBoxPosition.Height - 50);
                    flipped = true;
                    break;
                case Location.BottomRight:

                    textBoxPosition = new Rectangle(GlobalSettings.TitleSafeArea.Right - 605,
                                                    GlobalSettings.TitleSafeArea.Bottom - 227,
                                                    400,
                                                    GlobalSettings.TitleSafeArea.Top + 155);

                    textBoxBounds = new Rectangle(textBoxPosition.X + 20,
                                                  textBoxPosition.Y + 20,
                                                  textBoxPosition.Width - 45,
                                                  textBoxPosition.Height - 20);

                    portraitPosition = new Rectangle(textBoxPosition.X + 350,
                                                     textBoxPosition.Y - 10,
                                                     textBoxPosition.Width - 200,
                                                     textBoxPosition.Height - 50);
                    flipped = true;
                    break;
                case Location.BottomLeft:

                    textBoxPosition = new Rectangle(GlobalSettings.TitleSafeArea.Left + 155,
                                                    GlobalSettings.TitleSafeArea.Bottom - 227,
                                                    400,
                                                    GlobalSettings.TitleSafeArea.Top + 155);

                    textBoxBounds = new Rectangle(textBoxPosition.X + 20,
                                                  textBoxPosition.Y + 20,
                                                  textBoxPosition.Width - 35,
                                                  textBoxPosition.Height - 20);

                    portraitPosition = new Rectangle(textBoxPosition.X - 150,
                                                     textBoxPosition.Y - 10,
                                                     textBoxPosition.Width - 200,
                                                     textBoxPosition.Height - 50);
                    flipped = false; 
                    break;
            }
        }

        #endregion

        #region Update and Draw


        public override void HandleInput(GameTime gameTime)
        {
            switch (state)
            {
                case DialogState.Finished:
                    dialog.OnDialogComplete(partyResponse);

                    if (dialog.FadeTransition)
                    {
                        ExitAfterTransition();
                    }
                    else
                    {
                        ScreenManager.RemoveScreen(this);
                    }

                    break;

                case DialogState.Processing:
                    // Allow user to escape from dialog that doesn't require user decision.
                    if (InputState.IsMenuCancel() &&
                        (dialog.Prompt == DialogPrompt.Continue || dialog.Prompt == DialogPrompt.NeedsClose || dialog.Prompt == DialogPrompt.Ok))
                    {
                        state = DialogState.Finished;
                        break;
                    }

                    letterDelay -= gameTime.ElapsedGameTime;
                    shortLetterDelay -= gameTime.ElapsedGameTime;

                    bool fasterText = false;
                    if (InputState.IsFastTextScroll())
                        fasterText = true;
                    if ((fasterText ? shortLetterDelay : letterDelay) <= TimeSpan.Zero)
                    {
                        ProcessNextChar(gameTime);
                        letterDelay = GlobalSettings.DialogueLetterDelay;
                        shortLetterDelay = GlobalSettings.DialogueShortLetterDelay;
                    }

                    break;

                case DialogState.WaitingForResponse:
                    // Allow user to escape from dialog that doesn't require user decision.
                    if (InputState.IsMenuCancel() &&
                        (dialog.Prompt == DialogPrompt.Continue || dialog.Prompt == DialogPrompt.NeedsClose || dialog.Prompt == DialogPrompt.Ok))
                    {
                        state = DialogState.Finished;
                        break;
                    }

                    if (InputState.IsMenuDown())
                    {
                        if (++selectedOption >= menuOptions.Count)
                            selectedOption = 0;
                    }

                    else if (InputState.IsMenuUp())
                    {
                        if (--selectedOption < 0)
                            selectedOption = menuOptions.Count - 1;
                    }

                    else if (InputState.IsPartyInteract())
                    {
                        switch (options)
                        {
                            case DialogPrompt.Continue:
                                throw new Exception("We are not using Continue anymore.");
                                //partyResponse = PartyResponse.Continue;
                                //break;
                            case DialogPrompt.Ok:
                                partyResponse = PartyResponse.Ok;
                                break;
                            case DialogPrompt.OkCancel:
                                partyResponse = (selectedOption == 0 ? PartyResponse.Ok : PartyResponse.Cancel);
                                break;
                            case DialogPrompt.YesNo:
                                partyResponse = (selectedOption == 0 ? PartyResponse.Yes : PartyResponse.No);
                                break;
                        }
                        state = DialogState.Finished;
                    }
                    break;

                case DialogState.WaitingToContinue:

                    // Allow user to escape from dialog that doesn't require user decision.
                    if (InputState.IsMenuCancel() &&
                        (dialog.Prompt == DialogPrompt.Continue || dialog.Prompt == DialogPrompt.NeedsClose || dialog.Prompt == DialogPrompt.Ok))
                    {
                        state = DialogState.Finished;
                        break;
                    }

                    if (InputState.IsPartyInteract())
                    {
                        state = DialogState.Processing;
                        firstLineToRender = 0;
                        renderedText.Clear();
                        renderedText.Add(new StringBuilder());
                    }

                    break;

                case DialogState.WaitingOnClose:
                    // Allow user to escape from dialog that doesn't require user decision.
                    if (InputState.IsMenuCancel() &&
                        (dialog.Prompt == DialogPrompt.Continue || dialog.Prompt == DialogPrompt.NeedsClose || dialog.Prompt == DialogPrompt.Ok))
                    {
                        state = DialogState.Finished;
                        break;
                    }

                    if (InputState.IsPartyInteract())
                        state = DialogState.Finished;

                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color color = Color.White * TransitionAlpha;

            if (hasPortrait)
            {
                if (flipped)
                {
                    spriteBatch.Draw(portrait, portraitPosition, new Rectangle(0, 0, 150, 150), color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    spriteBatch.Draw(portrait, portraitPosition, color);
                }
            }

            spriteBatch.Draw(textBox, textBoxPosition, color);

            int lastRow = Math.Min(firstLineToRender + maxLines, renderedText.Count);

            Vector2 location = textPosition;

            for (int i = firstLineToRender; i < lastRow; ++i)
            {
                spriteBatch.DrawString(Fonts.MenuTitle2, renderedText[i], location, Fonts.MenuTitleColor * TransitionAlpha);
                location.Y += fontHeight;
            }

            if (state == DialogState.WaitingForResponse)
            {
                location.Y = textBoxBounds.Bottom - fontHeight - 10;
                for (int i = menuOptions.Count - 1; i >= 0; --i)
                {
                    float xpos = textBoxBounds.Right - Fonts.MenuTitle2.MeasureString(menuOptions[i]).X - 25;

                    spriteBatch.DrawString(Fonts.MenuTitle2, menuOptions[i], new Vector2(xpos, location.Y),
                        Fonts.MenuTitleColor * TransitionAlpha);
                    if (selectedOption == i)
                    {
                        Vector2 arrowPosition = new Vector2(xpos - 10, location.Y) + new Vector2(-24, -1);
                        spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, color, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
                    }

                    location.Y -= fontHeight;
                }
            }

            else if (state == DialogState.WaitingToContinue)
            {
                //spriteBatch.Draw(arrow,
                //    new Rectangle(textBoxBounds.Right - 33, textBoxBounds.Bottom - 50, 35, 35), color);
            }
            else if (state == DialogState.WaitingOnClose)
            {
                //spriteBatch.Draw(arrow,
                //    new Rectangle(textBoxBounds.Right - 33, textBoxBounds.Bottom - 50, 35, 35), color);
            }
        }

        void ProcessNextChar(GameTime gameTime)
        {
            /// Grab the next character and increment our pointer
            char currentChar = pendingText[pendingTextIndex][toRenderChar++];

            /// Append the current character to the last string in the rendering list
            renderedText[renderedText.Count - 1].Append(currentChar);

            /// If that character was a space then the next time we update, we'll be processing a new word which 
            /// means we need to see if that word will fit inside our textbox. If not we have to start a new line
            if (currentChar == ' ')
            {
                /// Find the index of the next whitespace character
                int nextWordIndex = pendingText[pendingTextIndex].IndexOf(' ', toRenderChar);

                /// If none exists, then the next word runs the remaining length of the string
                if (nextWordIndex == -1)
                    nextWordIndex = pendingText[pendingTextIndex].Length;

                string nextWord = pendingText[pendingTextIndex].Substring(toRenderChar, nextWordIndex - toRenderChar);

                /// If it won't fit in the textbox, start a newline
                if (Fonts.MenuTitle2.MeasureString(renderedText[renderedText.Count - 1] + nextWord).X > textBoxBounds.Width)
                {
                    //if (++numberOfLinesRendered >= 5)
                    //{
                    //    state = DialogState.WaitingToContinue;
                    //    numberOfLinesRendered = 0;
                    //}
                    //else
                    renderedText.Add(new StringBuilder());
                }
            }

            /// Check if we've reached the end of the current pending string
            if (toRenderChar >= pendingText[pendingTextIndex].Length)
            {
                /// If that was the last pending string
                if (++pendingTextIndex >= pendingText.Length)
                {
                    //if (options == DialogPrompt.None)
                    //    state = DialogState.Finished;

                    if (options == DialogPrompt.NeedsClose)
                        state = DialogState.WaitingOnClose;

                    else
                    {
                        state = DialogState.WaitingForResponse;
                        if (Math.Abs(renderedText.Count - firstLineToRender) + 2 > maxLines)
                        {
                            foreach (string s in menuOptions)
                            {
                                ++firstLineToRender;
                                renderedText.Add(new StringBuilder());
                            }
                        }
                    }
                }
                else
                {
                    state = DialogState.WaitingToContinue;
                    renderedText.Add(new StringBuilder());
                    toRenderChar = 0;
                }
            }

            /// If we have run out of space in the textbox, move to the next line
            if (renderedText.Count - firstLineToRender > maxLines)
            {
                firstLineToRender++;
            }
        }

        #endregion
    }
}
