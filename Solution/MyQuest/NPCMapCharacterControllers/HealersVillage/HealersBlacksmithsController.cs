using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class HealersBlacksmithsController : NPCMapCharacterController
    {
        #region Fields
        
        const int charge = 50;

        #endregion

        #region Achievement

        const string requiredAchievement = MushroomController.acquireSwordHiltAchievement;
        internal const string turnInSwordHiltAchievement = "reparingSwordHilt";
        const string nextDayAchievement = HealersInnClerksController.myAchievement; 
        internal const string receivedSwordAchievement = "receivedSword";
        const string savedVillageAchievement =  HealersBattleSceneA.savedVillageAchievement;

        #endregion

        #region Dialogs


        static readonly Dialog repairedSwordDialog = new Dialog( DialogPrompt.NeedsClose, Strings.Z054);

        static readonly Dialog dontLookAtMeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z055);

        static readonly Dialog getOutDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z056);

        static readonly Dialog offerAchievementDialog = new Dialog( DialogPrompt.YesNo, Strings.Z057, Strings.Z058 + " " + charge + " " + Strings.Z059);

        static readonly Dialog yesResponseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z060);

        static readonly Dialog noResponseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z061);

        static readonly Dialog goOnGetDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z062);

        static readonly Dialog aMedalDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z063);
      
        static readonly Dialog notEnoughMoneyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z064);
        
        #endregion

        #region Interact


        public override void Interact()
        {
            Dialog dialog;
            IList<string> achievements = Party.Singleton.PartyAchievements;
            if (achievements.Contains(requiredAchievement))
            {
                dialog = offerAchievementDialog;
                dialog.DialogCompleteEvent += SpokenToBlacksmith;
            }
            else
            {
                dialog = getOutDialog;
            }

            if (achievements.Contains(turnInSwordHiltAchievement))
            {
                if (achievements.Contains(nextDayAchievement))
                {
                    dialog = repairedSwordDialog;
                    if (achievements.Contains(receivedSwordAchievement))
                    {
                        dialog = dontLookAtMeDialog;
                    }
                    else
                    {
                        // Add sword to Nathan's equipment

                        CompleteRecievedSwordHiltAchievement();
                        dialog.DialogCompleteEvent += SwordHiltRepaired;    
                    }
                }
                else
                {
                    dialog = goOnGetDialog;
                }
            }
            
            if(Party.Singleton.PartyAchievements.Contains(savedVillageAchievement))
            {
                dialog = aMedalDialog; 
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "CarasBlacksmith"));
        }


        #endregion

        #region Callbacks

        public void CompleteTurnInSwordHiltAchievement()
        {
            Party.Singleton.AddAchievement(turnInSwordHiltAchievement);
            Party.Singleton.GameState.Inventory.RemoveItem(typeof(SwordHilt), 1);
            Party.Singleton.GameState.Gold -= charge;
        }

        public void CompleteRecievedSwordHiltAchievement()
        {
            Equipment sword = EquipmentPool.RequestEquipment("AdvancedSword");
            Equipment armor = EquipmentPool.RequestEquipment("Armor");
            Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
            Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
            Party.Singleton.AddAchievement(receivedSwordAchievement); 
        }

        void SwordHiltRepaired(object sender, PartyResponseEventArgs e)
        {
            repairedSwordDialog.DialogCompleteEvent -= SwordHiltRepaired;

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "HealersBlacksmithCutSceneScreen"));
        }

        void SpokenToBlacksmith(object sender, PartyResponseEventArgs e)
        {
            offerAchievementDialog.DialogCompleteEvent -= SpokenToBlacksmith;

            Dialog dialog;

            if (e.Response == PartyResponse.Yes)
            {
                if (Party.Singleton.GameState.Gold >= charge)
                {
                    CompleteTurnInSwordHiltAchievement();
                    dialog = yesResponseDialog;
                }
                else
                {
                    dialog = notEnoughMoneyDialog;
                }

                Party.Singleton.AddLogEntry("Mushroom Hollow", "Blacksmith", dialog.Text);
            }
            else
            {
                dialog = noResponseDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "CarasBlacksmith"));
        }


        #endregion
    }
}
