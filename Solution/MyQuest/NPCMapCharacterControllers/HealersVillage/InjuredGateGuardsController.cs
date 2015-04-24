using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class InjuredGateGuardsController : NPCMapCharacterController
    {
        #region Dialogs

        static readonly Dialog imOkDialog = new Dialog( DialogPrompt.NeedsClose, Strings.Z067);

        #endregion

        #region Interact

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(imOkDialog, DialogScreen.Location.TopLeft, "InjuredGuard"));
        }

        #endregion
    }
}
