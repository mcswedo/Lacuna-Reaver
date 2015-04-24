using System.Collections.Generic;

namespace MyQuest
{
    public class BoyMagesController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Achievement

        internal const string defeatBossAchievement = LibraryBossSceneD.achievement;
  
        #endregion

        #region Dialogs

        static readonly Dialog saveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z160);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z161);

        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(defeatBossAchievement))
            {
                dialog = thankYouDialog;
            }
            else
            {
                dialog = saveDialog; 
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "BoyMage"));
        }

        #endregion
    }
}
      

     
