using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class TimidWomansController : NPCMapCharacterController
    {
        static readonly Dialog cheerfulManDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z298, "...", Strings.Z299, Strings.Z300);//"What a wonderful day, isn't it, stranger?");
        

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(cheerfulManDialog, DialogScreen.Location.TopLeft, "TimidWoman"));
        }
    }
}