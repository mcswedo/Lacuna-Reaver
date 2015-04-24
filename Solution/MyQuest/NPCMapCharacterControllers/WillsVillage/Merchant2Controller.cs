using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class Merchant2Controller : NPCMapCharacterController
    {

        #region Achievements

        internal const string swordTradedAchievement = "swordTraded";
        internal const string caraQuestCompleteAchievement = "caraQuestComplete";

        #endregion

        # region Dialog

        Dialog dialog;

        static readonly Dialog merchant2Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z391);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z392);

        static readonly Dialog giveSwordDialog = new Dialog(DialogPrompt.YesNo, Strings.Z393);

        static readonly Dialog caraYesDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z394, Strings.Z395);

        static readonly Dialog caraNoDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z396);

        static readonly Dialog refuseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z397, Strings.Z398, Strings.Z399);

        static readonly Dialog startCombatDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z400, Strings.Z401);

        static readonly Dialog chainDoneDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z402, Strings.Z403, Strings.Z404);

        static readonly Dialog receivedGiftDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z405);

        static readonly Dialog merchantCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z406);

        static readonly Dialog affraidDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z407);

        static readonly Dialog caraQuestCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z408, Strings.Z409);

        #endregion

        #region Fields
        //int nathansLevel;
        //int nathansExperience;
        //int carasLevel;
        //int carasExperience;
        //int willsLevel;
        //int willsExperience;

        //PCFightingCharacter nathan = Party.Singleton.GameState.Fighters[0];
        //PCFightingCharacter cara = Party.Singleton.GameState.Fighters[1];
        //PCFightingCharacter will = Party.Singleton.GameState.Fighters[2];
        #endregion

        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.CurrentMap.Name.Equals(Maps.blindMansTownNight))
            {
                if (Party.Singleton.GameState.PartyAchievements.Contains(WillsBlacksmithsController.gotBladeAchievement))
                {
                    if (!Party.Singleton.PartyAchievements.Contains(swordTradedAchievement))
                    {
                        dialog = caraDialog;
                        dialog.DialogCompleteEvent += TradeSword;
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
                if (achievements.Contains(swordTradedAchievement))
                {
                    if (achievements.Contains(caraQuestCompleteAchievement))
                    {
                        dialog = caraQuestCompleteDialog;
                    }
                    else
                    {
                        dialog = affraidDialog;
                        dialog.DialogCompleteEvent += ThatWasWeird;
                    }
                }
                else
                {
                    dialog = merchant2Dialog;
                }
            }

            if (dialog == merchantCompleteDialog)
            {
                 ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Merchant2"));
            }
        }

        void TradeSword(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= TradeSword;

            dialog = giveSwordDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
            dialog.DialogCompleteEvent += CaraResponse;
        }

        #region Callbacks

        void CaraResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse;
            if (e.Response == PartyResponse.Yes)
            {
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(DisplaySword), 1);
                dialog = caraYesDialog;
                dialog.DialogCompleteEvent += StartConfrontation;
            }
            else
            {
                dialog = caraNoDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Merchant2"));
        }

        void StartConfrontation(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= StartConfrontation;
            dialog = refuseDialog;
            dialog.DialogCompleteEvent += EndConfrontation;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Cara"));
        }

        void EndConfrontation(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= EndConfrontation;
            dialog = startCombatDialog;
            dialog.DialogCompleteEvent += StartCombat;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Merchant2"));
        }

        void StartCombat(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= StartCombat;
            CombatScreen combatScreen;
            combatScreen = new CombatScreen(CombatZonePool.forestCaraBanditZone);
            ScreenManager.Singleton.AddScreen(combatScreen);

            combatScreen.ExitScreenEvent += ChainQuestDone;
        }

        void ChainQuestDone(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ChainQuestDone;
            dialog = chainDoneDialog;
            chainDoneDialog.DialogCompleteEvent += ReceivedGift;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Cara"));
        }

        void ReceivedGift(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ReceivedGift;

            Party.Singleton.AddAchievement(swordTradedAchievement);
            Party.Singleton.GameState.Inventory.AddItem(typeof(NathansGift), 1);
            MusicSystem.InterruptMusic(AudioCues.ChestOpen);

            dialog = receivedGiftDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

        void ThatWasWeird(object sender, PartyResponseEventArgs e)
        {
            Party.Singleton.AddAchievement(caraQuestCompleteAchievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WeirdCaraCutSceneScreen"));
        }
    }

        #endregion
}
