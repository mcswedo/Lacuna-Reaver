using System.Collections.Generic;

namespace MyQuest
{
    public class GreetersController : NPCMapCharacterController
    {
        Dialog dialog; 

        static readonly Dialog welcomeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z356);

        static readonly Dialog nightDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z357, Strings.Z358);

       
        public override void Interact()
        {
            if (Party.Singleton.Leader.Name == Party.nathan)
            {
                dialog = welcomeDialog;
            }
            else
            {
                dialog = nightDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Greeter"));
        }
    }
}
