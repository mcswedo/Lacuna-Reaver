using System.Collections.Generic;

namespace MyQuest
{
    public class WesleysController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Achievement

        internal const string defeatBossAchievement = ChepetawaSceneD.achievement;
  
        #endregion

        #region Dialogs

        static readonly Dialog swampDialog = new Dialog(DialogPrompt.NeedsClose,
            Strings.Z246, Strings.Z247,
            Strings.Z248);

        static readonly Dialog saveDialog = new Dialog(DialogPrompt.NeedsClose,
            Strings.ZA722,  
            Strings.ZA723,
            Strings.ZA724);


        #endregion

        #region Interact

        public override void Interact()
        {

            if (Party.Singleton.PartyAchievements.Contains(defeatBossAchievement))
            {
                dialog = saveDialog;
            }
            else
            {
                dialog = swampDialog;
                Party.Singleton.AddLogEntry("Celindar", "Wesley", Strings.Z246, Strings.Z247, Strings.Z248);
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Wesley"));
        }

        #endregion
    }
}
      

     
