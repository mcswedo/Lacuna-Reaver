using System.Collections.Generic;

namespace MyQuest
{
    public class MansionGuard1Controller : NPCMapCharacterController
    {
        Dialog dialog;

        static readonly Dialog runDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z370, Strings.Z371);

        public override void Interact()
        {
            dialog = runDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Guard"));
        }
    }
}
