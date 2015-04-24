using System.Collections.Generic;

/// An example of how a controller is implemented

namespace MyQuest
{
    public class GrandsonsController : NPCMapCharacterController
    {
        #region Achievements

        const string requiredSideQuestAchievement = WillsBurtleInitiateSceneC.achievement;
        const string cutSceneEndedAchievement = GrandsonAddScene.thisAchievement;
        internal const string myAchievement = "grandson";

        #endregion

        #region Dialogs
        
        Dialog dialog;

        static readonly Dialog lostDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z050, Strings.Z051, Strings.Z052);

        static readonly Dialog aweManDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z053);

        #endregion

        #region Interact
   
        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.PartyAchievements.Contains(requiredSideQuestAchievement))
            {
                dialog = lostDialog;
                Party.Singleton.AddLogEntry("Mushroom Forest", "Grandson", dialog.Text);
                dialog.DialogCompleteEvent += triggerCutScene;
                Party.Singleton.AddAchievement(myAchievement);             
            }

            if (Party.Singleton.PartyAchievements.Contains(cutSceneEndedAchievement))
            {
                dialog = aweManDialog;
            }

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Grandson"));
        }

        #endregion

        #region Callbacks

        void triggerCutScene(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= triggerCutScene;
            
            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "GrandsonLostCutSceneScreenA"));        
        }

        #endregion
    }
}
