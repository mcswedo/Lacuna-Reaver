using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class MayorsController : NPCMapCharacterController
    {
        #region Fields

        const int goldReward = 250;
       
        #endregion

        #region Achivements

        const string savedVillageAchievement = HealersBattleSceneA.savedVillageAchievement;
        const string returnAchievement = WillsBurtleInitiateSceneC.achievement;
        internal const string receivedRewardAchievement = "receivedReward";
        internal const string receivedBanditRewardAchievement = "receivedBanditKingReward";

        #endregion

        #region Dialogs

        static readonly Dialog gladYourBetterDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z069);

        static readonly Dialog savedVillageDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z070, Strings.Z071 + " " + goldReward + " " + Strings.Z072);

        static readonly Dialog gotRewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z073);

        static readonly Dialog takeCareDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z074, Strings.Z075, Strings.Z076);

        static readonly Dialog stayDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z077, Strings.Z078);

        static readonly Dialog myWordDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z079, Strings.Z080);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z081);

        static readonly Dialog goodToHaveBackDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA319);

        #endregion

        #region Interact

        public override void Interact()
        {
            Dialog dialog;
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (achievements.Contains(receivedRewardAchievement))
            {
                if (achievements.Contains("healerJoins"))
                {
                    dialog = takeCareDialog;
                }
                else
                dialog = gotRewardDialog;
            }
            else if (achievements.Contains(savedVillageAchievement))
            {
                dialog = savedVillageDialog;
                Party.Singleton.GameState.Gold += goldReward;
                Party.Singleton.AddAchievement(receivedRewardAchievement);
            }
            else
            {
                dialog = gladYourBetterDialog;
            }

            if (achievements.Contains(returnAchievement))
            {
                dialog = stayDialog;
                Party.Singleton.AddLogEntry("Mushroom Hollow", "Mayor", dialog.Text);
            }

            if (achievements.Contains(receivedBanditRewardAchievement))
            {
                dialog = goodToHaveBackDialog;
            }

            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(BanditCrown)) == 1)
            {
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(BanditCrown), 1); 
                myWordDialog.DialogCompleteEvent += Reward;
                dialog = myWordDialog;
                Party.Singleton.AddAchievement(receivedBanditRewardAchievement);
            }
       
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Mayor"));
            
        }

        void Reward(object sender, PartyResponseEventArgs e)
        {
            myWordDialog.DialogCompleteEvent -= Reward;
            Party.Singleton.GameState.Gold += 1000;
            Party.Singleton.GameState.Inventory.AddItem(typeof(PearlBand), 1);
            ScreenManager.Singleton.AddScreen(new DialogScreen(rewardDialog, DialogScreen.Location.TopLeft));
        }

        #endregion
    }
}
