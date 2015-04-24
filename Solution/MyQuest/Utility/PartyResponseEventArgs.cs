using System;

namespace MyQuest
{
    /// <summary>
    /// Describes the possible prompts that may
    /// be presented to the user at the end
    /// of a particular dialog.
    /// </summary>
    public enum DialogPrompt
    {
        /// <summary>
        /// No prompt will be presented, the dialog
        /// will close as soon as it ends
        /// </summary>
        //None,            never needed

        YesNo,
        OkCancel,
        Ok,
        Continue,

        /// <summary>
        /// Require the user to "close" the dialog by
        /// pressing a particular key. Functions the
        /// same as the Continue prompt but does not
        /// display a prompt
        /// </summary>
        NeedsClose       
    }

        
    public enum PartyResponse
    {
        Yes,
        No,
        Ok,
        Cancel,
        Continue,
        None
    }

    
    /// <summary>
    /// A custom event argument that allows passing 
    /// a PartyResponse to an event handler
    /// </summary>
    public class PartyResponseEventArgs : EventArgs
    {
        PartyResponse response;

        public PartyResponseEventArgs(PartyResponse response)
        {
            this.response = response;
        }

        public PartyResponse Response
        {
            get { return response; }
        }
    }
}
