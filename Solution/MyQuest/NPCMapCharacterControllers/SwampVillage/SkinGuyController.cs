using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class SkinGuyController : NPCMapCharacterController
    {
        static readonly Dialog SkinGuyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z320);

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(SkinGuyDialog, DialogScreen.Location.TopLeft, NPCPool.stub));

        }
    }
}
