using System.Collections.Generic;

namespace MyQuest
{
    public class BillsController : NPCMapCharacterController
    {
        static readonly Dialog dialog = new Dialog(
            DialogPrompt.NeedsClose, Strings.ZA518);

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Bill"));
        }
    }
}
