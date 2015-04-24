using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class OldManController : NPCMapCharacterController
    {
        static readonly Dialog oldmanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z268);

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(oldmanDialog, DialogScreen.Location.TopLeft, NPCPool.stub));

        }
    }
}
