using System.Collections.Generic;

namespace MyQuest
{
    public class CuratorsController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Dialogs

        static readonly Dialog hintDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z164, Strings.Z165, Strings.Z166, Strings.Z167);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z168, Strings.Z169, Strings.Z170);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z501);

        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
            {
                dialog = willDialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains("defeatLibraryBoss"))
            {
                dialog = thankYouDialog;
            }
            else
            {
                dialog = hintDialog;
                Party.Singleton.AddLogEntry("Celindar", "Curator", Strings.Z166, Strings.Z167);
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Curator"));
        }

        #endregion
    }
}
      

     
