using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class DistressedGirlsController : NPCMapCharacterController
    {
        static readonly Dialog whitePendantDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z253);

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(whitePendantDialog, DialogScreen.Location.TopLeft, "DistressedGirl"));

        }
    }
}
