using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class MagesBlacksmithsController : NPCMapCharacterController
    {
        #region Fields
        
        const int charge = 9250;

        #endregion

        #region Achievement

        internal const string myAchievement = "reparingArmor";
        internal const string nextDayAchievement = "nextDayInMageTown";
        internal const string receivedArmorAchievement = "receivedArmor";

        #endregion

        #region Dialogs


        static readonly Dialog repairedScytheDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z212);

        static readonly Dialog offerAchievementDialog = new Dialog(DialogPrompt.YesNo, Strings.Z213, Strings.Z214, Strings.Z215, Strings.Z216 + " " + charge + " " + Strings.Z217, Strings.Z218);

        static readonly Dialog yesResponseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z219, Strings.ZA513);

        static readonly Dialog noResponseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z220);

        static readonly Dialog notEnoughMoneyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z221);
        
        static readonly Dialog hateRepeatingDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z222);

        static readonly Dialog nothingToRepairDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z223);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z224, Strings.Z225);

        #endregion

        #region Interact


        public override void Interact()
        {
            Dialog dialog;
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (achievements.Contains(myAchievement))
            {
                if (achievements.Contains(nextDayAchievement))
                {
                    dialog = repairedScytheDialog;
                    if (achievements.Contains(receivedArmorAchievement))
                    {
                        dialog = nothingToRepairDialog;
                    }
                    else
                    {
                        Equipment nathanArmor = EquipmentPool.RequestEquipment("AdvancedArmor");
                        Equipment caraArmor = EquipmentPool.RequestEquipment("WoolArmor");
                        Equipment willArmor = EquipmentPool.RequestEquipment("RawhideLeatherArmor");
                        Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(nathanArmor);
                        Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(caraArmor);
                        Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(willArmor);

                        Party.Singleton.AddAchievement(receivedArmorAchievement); 
                    }
                }
                else
                {
                    dialog = hateRepeatingDialog;
                }
            }
            else
            {
                dialog = offerAchievementDialog;
                dialog.DialogCompleteEvent += SpokenToBlacksmith;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "MagesBlacksmith"));
        }


        #endregion

        #region Callbacks

        void SpokenToBlacksmith(object sender, PartyResponseEventArgs e)
        {
            offerAchievementDialog.DialogCompleteEvent -= SpokenToBlacksmith;

            Dialog dialog;

            if (e.Response == PartyResponse.Yes)
            {
                if (Party.Singleton.GameState.Gold >= charge)
                {
                    Party.Singleton.AddAchievement(myAchievement);
                    Party.Singleton.GameState.Gold -= charge;
                    Equipment armor = EquipmentPool.RequestEquipment("PlainArmor");
                    Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                    Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor);
                    Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(armor);
                    dialog = yesResponseDialog;
                }
                else
                    dialog = notEnoughMoneyDialog;
            }
            else
            {
                dialog = noResponseDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "MagesBlacksmith"));
        }

        #endregion
    }
}
