using System.Collections.Generic;

namespace MyQuest
{
    public class RuithsController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Achievement

        internal const string armorRepareAchievement = "reparingArmor";
        internal const string defeatBossAchievement = "defeatLibraryBoss";
  
        #endregion

        #region Dialogs

        static readonly Dialog fooledDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z243);

        static readonly Dialog dontTrustDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z244);

        static readonly Dialog riftDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z245);

        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(defeatBossAchievement))
            {
                dialog = dontTrustDialog;
            }
            else
            {
                dialog = fooledDialog; 
            }

            if(Party.Singleton.PartyAchievements.Contains(AgoraRiftCutSceneScreen.myAchievement))
            {
                dialog = riftDialog;
            }

             ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Ruith"));
        }

        #endregion
    }
}
      

     
