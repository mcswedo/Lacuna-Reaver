using System.Collections.Generic;

namespace MyQuest
{
    public class Merchant3Controller : NPCMapCharacterController
    {
        #region fields

        Dialog dialog;

        List<SaleItem> inventory = new List<SaleItem>()
                {
                    new SaleItem(typeof(EarthenGloves), 7500),
                    new SaleItem(typeof(ChainOfFlame), 7000),
                    new SaleItem(typeof(ChainOfTheSea), 7000),                   
                    new SaleItem(typeof(ChainOfSkies), 7000)
                };

        #endregion

        #region Dialog

        static readonly Dialog merchant3Dialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z410, Strings.Z411, Strings.Z412);

        static readonly Dialog merchant3Dialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z413, Strings.Z414);

        static readonly Dialog shopDialog = new Dialog(DialogPrompt.YesNo, Strings.Z415);

        #endregion

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(PoorGuysController.poorGuyComplete))
            {
                dialog = shopDialog;
                dialog.DialogCompleteEvent += OpenShop;
            }

            else if(Party.Singleton.PartyAchievements.Contains(PoorGuysController.maxGoldGivenAchievement))
            {
                dialog = merchant3Dialog2;
            }

            else
            {
                dialog = merchant3Dialog1;
            }
               
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Merchant3"));
        }

        void OpenShop(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= OpenShop;

            if (e.Response == PartyResponse.Yes)
            {
                ScreenManager.Singleton.AddScreen(new ItemShopScreen(inventory));
            }
            else
            {
            }
        }
    }
}
