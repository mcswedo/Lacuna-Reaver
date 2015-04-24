using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    class ScaredCivilian1Controller : NPCMapCharacterController
    {
        Dialog dialog;

        #region Dialogs
        
        static readonly Dialog helpDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z134);

        static readonly Dialog thanksDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA601);

        static readonly Dialog sadDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA602);

        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(TrappedGirlsController.freedAchievement))
            {
                dialog = thanksDialog;
            }
            else if (Party.Singleton.CurrentMap.Name.Equals(Maps.keepf0))
            {
                dialog = helpDialog;
            }
            else
            {
                dialog = sadDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Woman"));
        }

        #endregion  
    }
}
