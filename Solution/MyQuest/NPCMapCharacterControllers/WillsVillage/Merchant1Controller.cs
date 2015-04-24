using System.Collections.Generic;

namespace MyQuest
{
    public class Merchant1Controller : NPCMapCharacterController
    {
        #region Achievements

        internal const string trimmerAchievement = "gotTrimmer";

        #endregion

        Dialog dialog;

        static readonly Dialog merchant1Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z384);

        static readonly Dialog nightMerchantDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z385);

        static readonly Dialog giveNecklaceDialog = new Dialog(DialogPrompt.YesNo, Strings.Z386);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z387, Strings.Z388, Strings.Z389, Strings.Z390);

        static readonly Dialog sadDialog = new Dialog(
            DialogPrompt.NeedsClose,
            "If only there was someone who could help me, I would repay them handsomely. ");

        static readonly Dialog receivedTrimmerDialog = new Dialog(
            DialogPrompt.NeedsClose,
            "Cara has recieved a Beard Trimmer. ");

        static readonly Dialog merchantCompleteDialog = new Dialog(
            DialogPrompt.NeedsClose,
            "Thanks again for helping me out. ",
            "I hate to think what would happen if I came home to my wife empty handed. ");

        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.CurrentMap.Name.Equals(Maps.blindMansTownNight))
            {
                if (Party.Singleton.GameState.PartyAchievements.Contains(SeedMerchantsController.necklaceAchievement))
                {
                    if (!Party.Singleton.PartyAchievements.Contains(trimmerAchievement))
                    {
                        dialog = nightMerchantDialog;
                        dialog.DialogCompleteEvent += GiveNecklace;
                    }
                    else
                    {
                        dialog = merchantCompleteDialog;
                    }
                }
                else
                {
                    dialog = nightMerchantDialog;
                }
            }
            else
            {
                dialog = merchant1Dialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Merchant1"));
        }

        void GiveNecklace(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= GiveNecklace;
            dialog = giveNecklaceDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
            dialog.DialogCompleteEvent += CaraResponse;
        }

        void CaraResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse;
            if (e.Response == PartyResponse.Yes)
            {
                Party.Singleton.AddAchievement(trimmerAchievement);
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(Necklace), 1);
                dialog = thankYouDialog;
                thankYouDialog.DialogCompleteEvent += ReceivedBeardTrimmer;
            }
            else
            {
                dialog = sadDialog;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Merchant1"));
        }

        void ReceivedBeardTrimmer(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= ReceivedBeardTrimmer;
            Party.Singleton.GameState.Inventory.AddItem(typeof(BeardTrimmer), 1);
            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            ScreenManager.Singleton.AddScreen(new DialogScreen(receivedTrimmerDialog, DialogScreen.Location.TopLeft));
        }
    }
}
