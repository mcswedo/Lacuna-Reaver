using System.Collections.Generic;

/// An example of how a controller is implemented

namespace MyQuest
{
    public class SickGrandsonsController : NPCMapCharacterController
    {
        #region Achievements

        const string savedVillageAchievement = HealersBattleSceneA.savedVillageAchievement;

        #endregion

        #region Dialogs
        
        Dialog dialog;

        static readonly Dialog notSickDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z113, Strings.Z114, Strings.Z115);

        static readonly Dialog coughDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z116);

        static readonly Dialog iWouldaHelpedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z117);

        #endregion

        #region Interact
   
        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.PartyAchievements.Contains(savedVillageAchievement))
            {
                dialog = iWouldaHelpedDialog; 
            }
            else
            {
                dialog = notSickDialog;
                dialog.DialogCompleteEvent += coughing;
            }

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SickGrandson"));
        }

        #endregion

        #region Callbacks

        void coughing(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= coughing;
            dialog = coughDialog;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SickGrandson"));
        }

        #endregion
    }
}
