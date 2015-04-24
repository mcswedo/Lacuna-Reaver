using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class SnowTownChestController : NPCMapCharacterController
    {
        static readonly Dialog lockedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA528);

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(lockedDialog, DialogScreen.Location.TopLeft));
        }
    }
}
