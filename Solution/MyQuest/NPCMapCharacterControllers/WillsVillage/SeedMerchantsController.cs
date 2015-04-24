using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class SeedMerchantsController : NPCMapCharacterController
    {
        public void Complete()
        {
            Party.Singleton.AddAchievement(myAchievement);
            Party.Singleton.GameState.Inventory.AddItem(typeof(RareSeeds), 1);
        }

        #region Fields
        
        const int charge = 50;
        Dialog dialog;

        #endregion

        #region Achievement

        internal const string myAchievement = "soldSeeds";
        internal const string necklaceAchievement = "gotNecklace";

        #endregion

        #region Dialogs

        static readonly Dialog offerAchievementDialog = new Dialog(DialogPrompt.YesNo, Strings.Z448, Strings.Z449, Strings.Z450, Strings.Z451 + " " + charge + " " + Strings.Z072);

        static readonly Dialog yesResponseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z452);

        static readonly Dialog noResponseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z453);

        static readonly Dialog sorryDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z454);
        
        static readonly Dialog moreMoneyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z455);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z456);

        static readonly Dialog nightDialog = new Dialog(DialogPrompt.YesNo, Strings.Z457);

        static readonly Dialog tookNecklaceDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z458);

        static readonly Dialog refusedNecklaceDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z459);

        static readonly Dialog merchantCompleteDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z460);

        static readonly Dialog receivedNecklaceDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z461);

        #endregion

        #region Interact


        public override void Interact()
        {
            IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.CurrentMap.Name.Equals(Maps.blindMansTownNight))
            {
                if (!Party.Singleton.PartyAchievements.Contains(necklaceAchievement))
                {
                    dialog = nightDialog;
                    dialog.DialogCompleteEvent += CaraResponse;
                }
                else
                {
                    dialog = merchantCompleteDialog;
                }
            }
            else if (achievements.Contains(myAchievement))
            {
                dialog = sorryDialog;
            }
            else
            {
                dialog = offerAchievementDialog;
                Party.Singleton.AddLogEntry("Tamarel", "Seed Merchant", dialog.Text);
                dialog.DialogCompleteEvent += SpokenToMerchant;
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SeedMerchant"));
        }


        #endregion

        #region Callbacks

        void CaraResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse;

            if (e.Response == PartyResponse.Yes)
            {
                dialog = tookNecklaceDialog;
                Party.Singleton.AddAchievement(necklaceAchievement);
                Party.Singleton.GameState.Inventory.AddItem(typeof(Necklace), 1);
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SeedMerchant"));
                dialog.DialogCompleteEvent += ReceivedNecklace;
            }
            else
            {
                dialog = refusedNecklaceDialog;
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SeedMerchant"));
            }
        }

        void ReceivedNecklace(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= ReceivedNecklace;
            dialog = receivedNecklaceDialog;
            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

        void SpokenToMerchant(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= SpokenToMerchant;

            if (e.Response == PartyResponse.Yes)
            {
                if (Party.Singleton.GameState.Gold >= charge)
                {
                    Party.Singleton.GameState.Gold -= charge;

                    dialog = yesResponseDialog;

                    Complete();

                    ScreenManager.Singleton.AddScreen(new DialogScreen(rewardDialog, DialogScreen.Location.TopLeft));

                    ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SeedMerchant"));
                }
                else
                {
                    dialog = moreMoneyDialog;

                    ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SeedMerchant"));
                }
            }
            else
            {
                dialog = noResponseDialog;

                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "SeedMerchant"));
            }          
        }

        #endregion
    }
}
