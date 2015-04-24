using System.Collections.Generic;

namespace MyQuest
{
    public class LydiasController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Dialogs

        static readonly Dialog historyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z206, Strings.Z207, Strings.Z208, Strings.Z209);

        static readonly Dialog honoredDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z210, Strings.Z211);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z507, Strings.Z508);

        #endregion

        #region Interact

        public override void Interact()
        {

            dialog = historyDialog;

            if (Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight))
            {
                dialog = willDialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains(AgoraRiftCutSceneScreen.myAchievement))
            {
                dialog = honoredDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Lydia"));
        }

        #endregion
    }
}
      

     
