using System.Collections.Generic;
using System;

/// Finished under current specifications

namespace MyQuest
{
    /// <summary>
    /// A dialog which can be presented to the user. A dialog
    /// is a series of text followed by an optional Prompt. The 
    /// dialog is ended once the user responds to the prompt if one exists.
    /// </summary>
    public class Dialog
    {
        #region Text


        List<string> text = new List<string>();

        /// <summary>
        /// The text representing the Dialog
        /// </summary>
        public string[] Text
        {
            get { return text.ToArray(); }
        }


        #endregion

        #region Prompt


        DialogPrompt prompt;

        /// <summary>
        /// The options presented to the user when the Dialog ends
        /// </summary>
        public DialogPrompt Prompt
        {
            get { return prompt; }
        }


        #endregion

        #region Transition

        bool fadeTransition = true;

        public bool FadeTransition
        {
            get { return fadeTransition; }
        }

        #endregion

        #region Constructor


        /// <summary>
        /// Construct a new Dialog option.
        /// </summary>
        /// <param name="text">The text composing the Dialog</param>
        /// <param name="prompt">The prompt presented to the user once all of the text is rendered</param>
        public Dialog(DialogPrompt prompt, params string[] text)
        {
            foreach (string s in text)
                this.text.Add(s);

            this.prompt = prompt;
        }

        /// <summary>
        /// Construct a new Dialog option.
        /// </summary>
        /// <param name="text">The text composing the Dialog</param>
        /// <param name="prompt">The prompt presented to the user once all of the text is rendered</param>
        public Dialog(DialogPrompt prompt, bool fadeTransition, params string[] text)
        {
            foreach (string s in text)
                this.text.Add(s);

            this.fadeTransition = fadeTransition;

            this.prompt = prompt;
        }

        #endregion

        #region Event


        /// <summary>
        /// This event will be fired when the dialog is ended by the user
        /// </summary>
        internal event EventHandler<PartyResponseEventArgs> DialogCompleteEvent;

        /// <summary>
        /// /// Method for raising the ScriptCompleteEvent
        /// </summary>
        /// <param name="partyResponse">The party's response to the script. If no 
        /// response was required, the caller should pass in PartyResponse.None</param>
        protected internal virtual void OnDialogComplete(PartyResponse partyResponse)
        {
            if (DialogCompleteEvent != null)
                DialogCompleteEvent(this, new PartyResponseEventArgs(partyResponse));
        }


        #endregion
    }
}
