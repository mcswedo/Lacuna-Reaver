using System.Collections.Generic;

namespace MyQuest
{
    public class MansionGuard2Controller : NPCMapCharacterController
    {
        Dialog dialog;

        static readonly Dialog mapsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z373, Strings.Z374);
       
        public override void Interact()
        {
            dialog = mapsDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Guard"));
        }
    }
}
