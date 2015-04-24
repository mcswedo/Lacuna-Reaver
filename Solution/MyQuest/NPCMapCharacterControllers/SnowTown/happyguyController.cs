using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class happyguyController : NPCMapCharacterController
    {
        static readonly Dialog happyguyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z254);

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(happyguyDialog, DialogScreen.Location.TopLeft, "HappyGuy"));

        }
    }
}
