using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class WillsBlacksmithsController : NPCMapCharacterController
    {
        #region Fields
        
        const int charge = 500;

        #endregion

        #region Achievement

        internal const string requiredAchievement = "brokenScythe";
        internal const string myAchievement = "reparingScythe";
        internal const string nextDayAchievement = "nextDayInWillsVillage";
        internal const string receivedScytheAchievement = "receivedScythe";
        internal const string gotBladeAchievement = "receivedBlade";

        #endregion

        #region Dialogs

        Dialog dialog;

        static readonly Dialog repairedScytheDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z462);

        static readonly Dialog getOutDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z463, Strings.Z464);

        static readonly Dialog offerAchievementDialog = new Dialog(DialogPrompt.YesNo, Strings.Z465, Strings.Z466,  Strings.Z467 + " " + charge + " " + Strings.Z072);

        static readonly Dialog yesResponseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z468, Strings.ZA511, Strings.ZA512);

        static readonly Dialog noResponseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z469, Strings.Z470);

        static readonly Dialog notEnoughMoneyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z471);
        
        static readonly Dialog hateRepeatingDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z472);

        static readonly Dialog nothingToRepairDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z473, Strings.Z474);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z475);

        static readonly Dialog giveTrimmerDialog = new Dialog(DialogPrompt.YesNo, Strings.Z476);

        static readonly Dialog carayesDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z477, Strings.Z478, Strings.Z479, Strings.Z480, Strings.Z481);

        static readonly Dialog caranoDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z482);

        static readonly Dialog receivedSwordDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z483);

        static readonly Dialog merchantCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z484);


        #endregion

        #region Interact

        public void CompleteRepairedScytheAchievement()
        {
            Party.Singleton.AddAchievement(receivedScytheAchievement);
        }

        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.CurrentMap.Name.Equals(Maps.blindMansTownNight))
            {
                if (Party.Singleton.GameState.PartyAchievements.Contains(Merchant1Controller.trimmerAchievement))
                {
                    if (!Party.Singleton.PartyAchievements.Contains(gotBladeAchievement))
                    {
                        dialog = caraDialog;
                        dialog.DialogCompleteEvent += GiveTrimmer;
                    }
                    else
                    {
                        dialog = merchantCompleteDialog;
                    }
                }
                else
                {
                    dialog = caraDialog;
                }
            }

            else
            {

                if (achievements.Contains(requiredAchievement))
                {
                    dialog = offerAchievementDialog;
                    dialog.DialogCompleteEvent += SpokenToBlacksmith;
                }
                else
                {
                    dialog = getOutDialog;
                }

                if (achievements.Contains(myAchievement))
                {
                    if (achievements.Contains(nextDayAchievement))
                    {
                        dialog = repairedScytheDialog;
                        if (achievements.Contains(receivedScytheAchievement))
                        {
                            dialog = nothingToRepairDialog;
                        }
                        else
                        {

                            Equipment scythe = EquipmentPool.RequestEquipment("AdvancedScythe");
                            Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(scythe);
                            CompleteRepairedScytheAchievement();

                        }
                    }
                    else
                    {
                        dialog = hateRepeatingDialog;
                    }
                }

            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "WillsBlacksmith"));
        }


        #endregion

        #region Callbacks

        void GiveTrimmer(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= GiveTrimmer;
            dialog = giveTrimmerDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
            dialog.DialogCompleteEvent += CaraResponse;
        }

        void CaraResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse;
            if (e.Response == PartyResponse.Yes)
            {
                Party.Singleton.AddAchievement(gotBladeAchievement);
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(BeardTrimmer), 1);
                dialog = carayesDialog;
                dialog.DialogCompleteEvent += ReceivedDisplaySword;
            }
            else
            {
                dialog = caranoDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "WillsBlacksmith"));
        }

        void ReceivedDisplaySword(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= ReceivedDisplaySword;
            dialog = receivedSwordDialog;
            Party.Singleton.GameState.Inventory.AddItem(typeof(DisplaySword), 1);
            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

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

                    Equipment scythe = EquipmentPool.RequestEquipment("RustyScythe");
                    Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(scythe);
                    dialog = yesResponseDialog;
                }
                else
                    dialog = notEnoughMoneyDialog;
            }
            else
            {
                dialog = noResponseDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "WillsBlacksmith"));
        }

        #endregion
    }
}
