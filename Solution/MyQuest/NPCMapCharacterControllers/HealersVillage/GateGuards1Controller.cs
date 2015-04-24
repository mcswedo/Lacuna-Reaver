using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class GateGuards1Controller : NPCMapCharacterController
    {
        Dialog dialog;

        const string requiredAchievement = WillsBurtleInitiateSceneC.achievement;
        internal const string myRewardAchievement = "myReward";

        #region Dialogs

        static readonly Dialog cantLeaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA453);

        static readonly Dialog avengeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z042,Strings.Z043,
            Strings.Z044, Strings.Z045);
       
        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z046,Strings.Z047,Strings.Z048);

        static readonly Dialog goodLuckDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z049);

        static readonly Dialog acquiredDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA613);

        #endregion

        #region Interact

        public override void Interact()
        {
            if(!Party.Singleton.PartyAchievements.Contains(requiredAchievement))
            {
                dialog = cantLeaveDialog;
            }
            else 
             {
                dialog = avengeDialog;
                Party.Singleton.AddLogEntry("Mushroom Hollow", "Gate Guard", dialog.Text); 
            }


            if (Party.Singleton.PartyAchievements.Contains(myRewardAchievement))
            {
                dialog = goodLuckDialog;
            }


            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(RedScarf)) == 1)
            {
                dialog = rewardDialog;
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(RedScarf), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(LapizLazuliRing), 1);
                Party.Singleton.AddAchievement(myRewardAchievement);
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Guard"));
            dialog.DialogCompleteEvent += new EventHandler<PartyResponseEventArgs>(dialog_DialogCompleteEvent);
        }
          
        void dialog_DialogCompleteEvent(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= dialog_DialogCompleteEvent;
            NPCMapCharacter npc = Party.Singleton.CurrentMap.GetNPC("GateGuard1");
            npc.FaceDirection(Direction.West);
            if (dialog == rewardDialog)
            {
                dialog = acquiredDialog; 
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
            }
        }

        #endregion
    }
}
