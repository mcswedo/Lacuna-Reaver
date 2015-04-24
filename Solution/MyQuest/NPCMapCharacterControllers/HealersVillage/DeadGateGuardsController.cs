using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class DeadGateGuardsController : NPCMapCharacterController
    {
        static readonly Dialog hesDeadDialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z016);

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(hesDeadDialog, DialogScreen.Location.TopLeft));
        }
    }
}
