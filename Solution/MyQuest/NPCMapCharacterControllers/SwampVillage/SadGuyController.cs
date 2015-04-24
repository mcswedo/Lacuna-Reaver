using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class SadGuyController : NPCMapCharacterController
    {
        static readonly Dialog SadGuyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z316);

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(SadGuyDialog, DialogScreen.Location.TopLeft, NPCPool.stub));

        }
    }
}
