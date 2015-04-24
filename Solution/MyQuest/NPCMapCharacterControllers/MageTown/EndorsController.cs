using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    public class EndorsController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Achievement

        const string defeatBossAchievement = LibraryBossSceneD.achievement;
        const string foundBlacksmithAchievement =  LabyrinthBossSceneE.achievement;
        public const string achievement = "talkedToEndor";
  
        #endregion

        #region Dialogs

        static readonly Dialog libraryDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z172, Strings.Z173);

        static readonly Dialog blacksmithDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z174, Strings.Z173);


        static readonly Dialog foundDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z175);   //We need a name for the final blacksmith. Not "Gragas"

        static readonly Dialog nathansDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z176, Strings.Z177);

        static readonly Dialog endorsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z178, Strings.Z179, Strings.Z180, Strings.Z181, Strings.Z182, Strings.Z183, Strings.Z184);

        static readonly Dialog hurryDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z185);

         static readonly Dialog doNotWorryDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z186);

        #endregion

        #region Interact

        public void CompleteAchievement()
        {
             Party.Singleton.AddAchievement(achievement);
        }

        public void CompleteMapMod()
        {
            Party.Singleton.ModifyNPC(Maps.agoraPortalRoom2, "Endor", new Point(4, 14), Direction.North, true, ModAction.Add, true);
            Party.Singleton.ModifyNPC(Maps.agoraPortalRoom2, "Ruith", new Point(3, 13), Direction.North, true, ModAction.Add, true);
            Party.Singleton.ModifyNPC(Maps.agoraPortalRoom2, "Lydia", new Point(5, 13), Direction.North, true, ModAction.Add, true);
        }

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(defeatBossAchievement))
            {
                dialog = blacksmithDialog;
            }
    
            else
            {
                dialog = libraryDialog; 
            }

            if (Party.Singleton.PartyAchievements.Contains(foundBlacksmithAchievement)&& !Party.Singleton.PartyAchievements.Contains(achievement))
            {
                dialog = foundDialog;
                dialog.DialogCompleteEvent += NathansResponse;
                CompleteMapMod();
            }

            if (Party.Singleton.PartyAchievements.Contains(achievement))
            {
                dialog = hurryDialog;
            }

            if (Party.Singleton.PartyAchievements.Contains(AgoraRiftCutSceneScreen.myAchievement))
            {
                dialog = doNotWorryDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Endor"));
        }

        #endregion
       
        #region Callbacks

        void NathansResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= NathansResponse;

            dialog = nathansDialog;

            dialog.DialogCompleteEvent += EndorsResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Nathan"));
        }

        void EndorsResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= EndorsResponse;

            dialog = endorsDialog;

            CompleteAchievement();

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Endor"));
        }

        #endregion

    }
}
      

     
