using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class TrappedGirlsController : NPCMapCharacterController
    {
        internal const string foundAchievement = "girlFound";
        internal const string freedAchievement = "girlFreed";

        static readonly Dialog savedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z136, Strings.Z137, Strings.Z138);

        static readonly Dialog youAgainDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z139, Strings.Z140, Strings.Z141);

        static readonly Dialog debtDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z142, Strings.Z143);

        public override void Interact()
        {
            Dialog dialog;

            if (Party.Singleton.PartyAchievements.Contains(TrappedGirlsController.foundAchievement))
            {
                dialog = debtDialog;
            }

            else if (Party.Singleton.PartyAchievements.Contains(TrappedGirlsController.freedAchievement))
            {
                dialog = youAgainDialog;
                dialog.DialogCompleteEvent += SavedGirl;
            }
            else
            {
                dialog = savedDialog;
                dialog.DialogCompleteEvent += MonsterDefeated;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "TrappedGirl"));
        }

        void MonsterDefeated(object sender, PartyResponseEventArgs e)
        {
            savedDialog.DialogCompleteEvent -= MonsterDefeated;
            Party.Singleton.AddAchievement(freedAchievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "Keepf3CutSceneScreen"));
        }

        void SavedGirl(object sender, PartyResponseEventArgs e)
        {
            Party.Singleton.AddAchievement(foundAchievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "TrappedGirlsCutSceneScreen"));  //CutScene is in the Snow Town
        }
    }
}
