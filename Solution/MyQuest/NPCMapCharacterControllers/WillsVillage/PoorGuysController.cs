using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace MyQuest
{
    public class PoorGuysController : NPCMapCharacterController
    {
        #region Fields

        const int minGold = 100;
        const int maxGold = 1362;
        
        Dialog dialog;

        List<SaleItem> inventory = new List<SaleItem>()
                {
                    new SaleItem(typeof(ObsidianCharm), 397),
                    new SaleItem(typeof(LapizLazuliRing), 412),
                    new SaleItem(typeof(RingOfTheSages), 377),                   
                    new SaleItem(typeof(RingOfVeneration), 409)
                };

        #endregion

        #region Achievements

        const string minGoldGivenAchievement = "oneGoldGiven";
        const string poorGuyMoved = "poorGuyMoved";
        internal const string maxGoldGivenAchievement = "maxGoldGiven";
        internal const string poorGuyComplete = "poorGuyComplete";
        
        #endregion

        #region Dialog

        static readonly Dialog askMinGoldDialog = new Dialog(DialogPrompt.YesNo, Strings.Z433, Strings.Z434);

        static readonly Dialog rejectedMinGoldDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z435);
       
        static readonly Dialog askMaxGoldDialog = new Dialog(DialogPrompt.YesNo, Strings.Z436);

        static readonly Dialog rejectedMaxGoldDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z437, Strings.Z438);

        static readonly Dialog leavingWillsVillageDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z439);

        static readonly Dialog noMoneyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z440);


        static readonly Dialog doYouWantToBuySomethingDialog = new Dialog(DialogPrompt.YesNo, Strings.Z441);

        static readonly Dialog askForMaxGoldAfterShopDialog = new Dialog(DialogPrompt.YesNo, Strings.Z442);

        static readonly Dialog rejectedMaxGoldAfterShopDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z443);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z444, Strings.Z445);

        static readonly Dialog itemReceived = new Dialog(DialogPrompt.NeedsClose, Strings.ZA610); 

        static readonly Dialog goodByeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z446, Strings.Z447);
        
        #endregion 

        #region Interact

        public override void Interact()
        {
            //IList<string> achievements = Party.Singleton.PartyAchievements;

            if (Party.Singleton.PartyAchievements.Contains(poorGuyComplete))
            {
                Debug.Assert(Party.Singleton.CurrentMap.Name == Maps.snowTownNpchouseSSe);
                dialog = goodByeDialog;
            }

            else if (Party.Singleton.PartyAchievements.Contains(maxGoldGivenAchievement))
            {
                Debug.Assert(Party.Singleton.CurrentMap.Name == Maps.snowTownNpchouseSSe);
                Debug.Assert(Party.Singleton.PartyAchievements.Contains(minGoldGivenAchievement));
                Party.Singleton.AddAchievement(poorGuyComplete);
                dialog = thankYouDialog; //Make a callback that puts the dialog.
                dialog.DialogCompleteEvent += YouReceivedItemCallback;
                Party.Singleton.GameState.Inventory.AddItem(typeof(NatureCharm), 1);
            }

            else if (Party.Singleton.PartyAchievements.Contains(minGoldGivenAchievement))
            {
                dialog = doYouWantToBuySomethingDialog;
                dialog.DialogCompleteEvent += DoYouWantToBuySomethingCallback;
            }
         
            else
            {
                dialog = askMinGoldDialog;
                dialog.DialogCompleteEvent += AskMinGoldCallback;
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "PoorMan"));
        }
        #endregion
        
        #region Callbacks


        void AskMinGoldCallback(object sender, PartyResponseEventArgs e)
        {
            askMinGoldDialog.DialogCompleteEvent -= AskMinGoldCallback;

            if (e.Response == PartyResponse.Yes)
            {
                if (Party.Singleton.GameState.Gold < minGold)
                {
                    dialog = noMoneyDialog;
                }
                else
                {
                    Party.Singleton.AddAchievement(minGoldGivenAchievement);
                    Party.Singleton.GameState.Gold -= minGold;

                    askMaxGoldDialog.DialogCompleteEvent += AskForMaxGoldCallback;
                    dialog = askMaxGoldDialog;
                }
            }

            else  //User selected No.
            {
                dialog = rejectedMinGoldDialog;
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "PoorMan"));
        }

        void AskForMaxGoldCallback(object sender, PartyResponseEventArgs e)
        {
            Debug.Assert(Party.Singleton.PartyAchievements.Contains(minGoldGivenAchievement));
            dialog.DialogCompleteEvent -= AskForMaxGoldCallback;

            if (e.Response == PartyResponse.Yes)
            {
                if (Party.Singleton.GameState.Gold < maxGold)
                {
                    noMoneyDialog.DialogCompleteEvent += RejectedMaxGoldCallback;
                    dialog = noMoneyDialog;
                    //ScreenManager.Singleton.AddScreen(new DialogScreen(noMoneyDialog, DialogScreen.Location.TopLeft, "PoorMan"));
                }
                else
                {
                    Party.Singleton.AddAchievement(maxGoldGivenAchievement);
                    Party.Singleton.GameState.Gold -= maxGold;

                    leavingWillsVillageDialog.DialogCompleteEvent += LeavingWillsVillageCallback;
                    dialog = leavingWillsVillageDialog;
                    Party.Singleton.AddLogEntry("Tamarel", "Poor Guy", dialog.Text);
                    //ScreenManager.Singleton.AddScreen(new DialogScreen(leavingWillsVillageDialog, DialogScreen.Location.TopLeft, "PoorMan"));
                }
            }

            else
            {
                if (!Party.Singleton.PartyAchievements.Contains(PoorGuysCutSceneA.achievement))
                {
                    rejectedMaxGoldDialog.DialogCompleteEvent += RejectedMaxGoldCallback;
                    dialog = rejectedMaxGoldDialog;
                }
                else
                {
                    dialog = rejectedMaxGoldAfterShopDialog;
                }
                //ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "PoorMan"));
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "PoorMan"));
        }

        void RejectedMaxGoldCallback(object sender, PartyResponseEventArgs e)
        {
            rejectedMaxGoldDialog.DialogCompleteEvent -= RejectedMaxGoldCallback;

            if (!Party.Singleton.PartyAchievements.Contains(poorGuyMoved))
            {
                Party.Singleton.AddLogEntry("Tamarel", "Poor Guy", rejectedMaxGoldDialog.Text);
                Party.Singleton.AddAchievement(poorGuyMoved);
                ScreenManager.Singleton.AddScreen(
                        (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "PoorGuysCutSceneScreenA"));
            }
        }

        void LeavingWillsVillageCallback(object sender, PartyResponseEventArgs e)
        {
            leavingWillsVillageDialog.DialogCompleteEvent -= LeavingWillsVillageCallback;

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "PoorGuysCutSceneScreenB"));
        }


        ItemShopScreen itemShopScreen;

        void DoYouWantToBuySomethingCallback(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= DoYouWantToBuySomethingCallback;

            if (e.Response == PartyResponse.Yes)
            {
                itemShopScreen = new ItemShopScreen(inventory);
                itemShopScreen.ExitScreenEvent += OnShopScreenExit;
                ScreenManager.Singleton.AddScreen(itemShopScreen);
            }

            else
            {
                dialog = askForMaxGoldAfterShopDialog;
                dialog.DialogCompleteEvent += AskForMaxGoldCallback;

                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "PoorMan"));
            }
        }

        void OnShopScreenExit(object sender, EventArgs e)
        {
            itemShopScreen.ExitScreenEvent -= OnShopScreenExit;

            dialog = askForMaxGoldAfterShopDialog;
            dialog.DialogCompleteEvent += AskForMaxGoldCallback;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "PoorMan"));
        }

        void YouReceivedItemCallback(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= YouReceivedItemCallback;
            SoundSystem.Play(AudioCues.ChestOpen);
            dialog = itemReceived;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }
        #endregion
    }
}
