using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class PickPocketsController : NPCMapCharacterController
    {
        //Needs more work, need to put dialog into conversation log

        #region Achievements

        internal const string foundCriminal1Achievement = "foundCriminal1";

        internal const string foundCriminal2Achievement = "foundCriminal2";

        internal const string foundCriminal3Achievement = "foundCriminal3";

        internal const string foundCriminal4Achievement = "foundCriminal4";

        const string foundPurseAchievement = "foundPurse";

        #endregion

        #region Dialog

        static readonly Dialog weirdDialog = new Dialog(DialogPrompt.NeedsClose,  Strings.Z416, Strings.Z417, Strings.Z418, Strings.Z419);

        static readonly Dialog found2Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z420);//, "?", Strings.Z421, Strings.Z422, Strings.Z423);

        static readonly Dialog nathanQuestionMark = new Dialog(DialogPrompt.NeedsClose, "?");

        static readonly Dialog found2DialogEnd = new Dialog(DialogPrompt.NeedsClose, Strings.Z421, Strings.Z422, Strings.Z423);

        static readonly Dialog found3Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z424, Strings.Z425, Strings.Z426, Strings.Z427, Strings.Z428);

        static readonly Dialog lastDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z429, Strings.Z430, Strings.Z431, Strings.Z432);
        #endregion

        #region Interact

        public override void Interact()
        {
            Dialog dialog;
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.PartyAchievements.Contains(foundCriminal4Achievement))
            {
                dialog = lastDialog;
                Party.Singleton.AddLogEntry("Tamarel", "Pick Pocket", dialog.Text);
                dialog.DialogCompleteEvent += LastFind;
            }

            else if (Party.Singleton.PartyAchievements.Contains(foundCriminal3Achievement))
            {
                dialog = found3Dialog;
                Party.Singleton.AddLogEntry("Tamarel", "Pick Pocket", dialog.Text);
                dialog.DialogCompleteEvent += ThirdFind;
            }

            else if (Party.Singleton.PartyAchievements.Contains(foundCriminal2Achievement))
            {
                dialog = found2Dialog;
                Party.Singleton.AddLogEntry("Tamarel", "Pick Pocket", dialog.Text);
                dialog.DialogCompleteEvent += SecondFind;
            }

            else
            {
                dialog = weirdDialog;
                Party.Singleton.AddLogEntry("Tamarel", "Pick Pocket", dialog.Text);
                dialog.DialogCompleteEvent += FirstFind;
            }

            if (Party.Singleton.PartyAchievements.Contains(foundCriminal3Achievement) 
                && !Party.Singleton.PartyAchievements.Contains(foundCriminal4Achievement))
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "PickPocket"));
            }

            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "PickPocket"));
            }
        }
        #endregion

        #region Callbacks

        void FirstFind(object sender, PartyResponseEventArgs e)
        {
            weirdDialog.DialogCompleteEvent -= FirstFind;

            Party.Singleton.AddAchievement(foundCriminal2Achievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace,"PickPocketsCutSceneScreenA"));
        }

        void SecondFind(object sender, PartyResponseEventArgs e)
        {
            weirdDialog.DialogCompleteEvent -= SecondFind;

            Party.Singleton.AddAchievement(foundCriminal3Achievement);

            ScreenManager.Singleton.AddScreen(new DialogScreen(nathanQuestionMark, DialogScreen.Location.TopLeft, "Nathan"));

            nathanQuestionMark.DialogCompleteEvent += SecondFindFinalDialog;
        }

        void SecondFindFinalDialog(object sender, PartyResponseEventArgs e)
        {
            nathanQuestionMark.DialogCompleteEvent -= SecondFindFinalDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(found2DialogEnd, DialogScreen.Location.TopLeft, "PickPocket"));

            found2DialogEnd.DialogCompleteEvent += SecondFindCutScene;
        }

        void SecondFindCutScene(object sender, PartyResponseEventArgs e)
        {
            found2DialogEnd.DialogCompleteEvent -= SecondFindCutScene;

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "PickPocketsCutSceneScreenB"));
        }

        void ThirdFind(object sender, PartyResponseEventArgs e)
        {
            weirdDialog.DialogCompleteEvent -= ThirdFind;

            Party.Singleton.AddAchievement(foundCriminal4Achievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "PickPocketsCutSceneScreenC"));
        }

        void LastFind(object sender, PartyResponseEventArgs e)
        {
            weirdDialog.DialogCompleteEvent -= LastFind;

            Party.Singleton.AddAchievement(foundPurseAchievement);

            Party.Singleton.GameState.Inventory.AddItem(typeof(Purse), 1);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "PickPocketsCutSceneScreenD"));
        }

        #endregion
    }
}
