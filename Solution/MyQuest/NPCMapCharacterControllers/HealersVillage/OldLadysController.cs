using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class OldLadysController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;
        const int goldReward = 200;
        int mushroomsCollected = Party.Singleton.GameState.Inventory.ItemCount(typeof(Mushroom));

        #endregion

        #region Achivements

        const string savedVillageAchievement = HealersBattleSceneA.savedVillageAchievement;
        const string requiredSideQuestAchievement = WillsBurtleInitiateSceneC.achievement;
        const string requiredAchievement = MushroomController.acquireSwordHiltAchievement;
        const string cutSceneEndedAchievement = GrandsonAddScene.thisAchievement;

        internal const string myAchievement = "OldLadysReward1";
        internal const string hasSpokenAchievement = "HasSpokenTo";
        internal const string rewardSideQuestAchievement = GrandsonsController.myAchievement;
       
                
        #endregion
      
        #region Dialogs

        static readonly Dialog thatWillBeAllDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z093);

        static readonly Dialog collectMoreDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z094, Strings.Z095);

        static readonly Dialog requestMushroomsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z096, Strings.Z097);

        static readonly Dialog myRewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z098 + " " + goldReward + " " + Strings.Z099, Strings.Z100);

        static readonly Dialog myReward2Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z101, Strings.Z102, Strings.Z103 + " " + goldReward + " " + Strings.Z099, Strings.Z100); 

        static readonly Dialog myOldTickeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z106);

        static readonly Dialog missingDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z107, Strings.Z108, Strings.Z109);

        static readonly Dialog foundDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z110);

        static readonly Dialog thanksDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z111);
      
        #endregion 

        #region Interact

        public void CompleteMushroomCollectionQuest()
        {
            Party.Singleton.AddAchievement(myAchievement);
            Party.Singleton.GameState.Inventory.RemoveItem(typeof(Mushroom), 4);
            Party.Singleton.GameState.Gold += goldReward;
        }

        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (mushroomsCollected > 0 && mushroomsCollected < 4)
            {
                dialog = collectMoreDialog;
            }
            else
            {
                dialog = requestMushroomsDialog;
            }
     
            if (achievements.Contains(myAchievement))
            {
                dialog = thatWillBeAllDialog;
            }

            else if (mushroomsCollected == 4)
            {
                CompleteMushroomCollectionQuest();
                if (Party.Singleton.PartyAchievements.Contains(hasSpokenAchievement))
                {
                    dialog = myRewardDialog;
                }
                else
                {
                    dialog = myReward2Dialog;
                }
            }

            if (Party.Singleton.PartyAchievements.Contains(savedVillageAchievement))
            {
                dialog = myOldTickeDialog;
            }

            if (Party.Singleton.PartyAchievements.Contains(requiredSideQuestAchievement))
            {
                dialog = missingDialog;
            }
            if (Party.Singleton.PartyAchievements.Contains(rewardSideQuestAchievement) && !Party.Singleton.PartyAchievements.Contains(cutSceneEndedAchievement))
            {
                Party.Singleton.GameState.Inventory.AddItem(typeof(LapizLazuliRing), 1);
                Party.Singleton.GameState.Gold += 500;
                ScreenManager.Singleton.AddScreen((CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "GrandsonLostCutSceneScreenB"));
                return;          
            }

            if (Party.Singleton.PartyAchievements.Contains(cutSceneEndedAchievement))
            {
                dialog = thanksDialog;
            }
            Party.Singleton.ModifyMapLayer("healers_village", Layer.MonsterZone, new Point(21, 15), 2, true); 
            Party.Singleton.AddLogEntry("Mushroom Hollow", "Old Lady", dialog.Text);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "OldLady"));

            if (!Party.Singleton.PartyAchievements.Contains(hasSpokenAchievement))
            {
                Party.Singleton.AddAchievement(hasSpokenAchievement);
            }
        }

        #endregion
    }
}
