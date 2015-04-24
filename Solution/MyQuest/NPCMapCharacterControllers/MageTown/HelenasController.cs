using System.Collections.Generic;

//celindar is a mage town name we came up with

namespace MyQuest
{
    public class HelenasController : NPCMapCharacterController
    {
        Dialog dialog;

        internal const string hatFoundAchievement = "hatFound";

        #region Dialogs

        static readonly Dialog hintDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z187, Strings.Z188, Strings.Z189);

        static readonly Dialog congratsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z190, Strings.Z191);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z502, Strings.Z503, Strings.Z504, Strings.Z505);

        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
            {
                dialog = willDialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains(hatFoundAchievement))
            {
                dialog = congratsDialog;
            }

            else
            {
                dialog = hintDialog;
                Party.Singleton.AddLogEntry("Celindar", "Helena", Strings.Z187, Strings.Z188, Strings.Z189);
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Helena"));
        }

        #endregion
    }
}
      

     
