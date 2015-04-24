using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class GateGuards2Controller : NPCMapCharacterController
    {      
        #region Dialogs

        static readonly Dialog cantLeaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA454);

        #endregion 

        #region Interact

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(cantLeaveDialog, DialogScreen.Location.TopLeft, "Guard"));
            cantLeaveDialog.DialogCompleteEvent += new EventHandler<PartyResponseEventArgs>(dialog_DialogCompleteEvent);
        }

        void dialog_DialogCompleteEvent(object sender, PartyResponseEventArgs e)
        {
            cantLeaveDialog.DialogCompleteEvent -= dialog_DialogCompleteEvent;
            NPCMapCharacter npc = Party.Singleton.CurrentMap.GetNPC("GateGuard2");
            npc.FaceDirection(Direction.East);
        }

        #endregion       
    }
}
