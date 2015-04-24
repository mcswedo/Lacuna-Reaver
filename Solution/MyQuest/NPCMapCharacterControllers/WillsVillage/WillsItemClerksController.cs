using System.Collections.Generic;

namespace MyQuest
{
    public class WillsItemClerksController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;
        SaleItem villageMap = new SaleItem(typeof(WillsVillageMap), 50);
        SaleItem forestMap = new SaleItem(typeof(WillsForestMap), 300);
        List<SaleItem> inventory = new List<SaleItem>()
                {
                    new SaleItem(typeof(SmallHealthPotion), 25),
                    new SaleItem(typeof(MediumHealthPotion), 150),
                    new SaleItem(typeof(LargeHealthPotion), 750),                   
                    new SaleItem(typeof(SmallEnergyPotion), 30),
                    new SaleItem(typeof(MediumEnergyPotion), 200),
                    new SaleItem(typeof(LargeEnergyPotion), 925)
                };

        #endregion

        #region Achievement

        const string brokenScytheAchievement = "brokenScythe";

        internal const string scytheRepareAchievement = "reparingScythe";

        #endregion

        #region Dialogs

        static readonly Dialog soldOutDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z065);

        static readonly Dialog likeToViewDialog = new Dialog(DialogPrompt.YesNo, Strings.Z066);

        #endregion

        #region Interact

        public override void Interact()
        {
            inventory.Add(villageMap);
            inventory.Add(forestMap); 

            dialog = likeToViewDialog;
            dialog.DialogCompleteEvent += DisplayInventory;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersMerchant"));
        }

        #endregion

        #region Callbacks

        void DisplayInventory(object sender, PartyResponseEventArgs e)
        {
            likeToViewDialog.DialogCompleteEvent -= DisplayInventory;

            if (Party.Singleton.GameState.Inventory.containsMapItem("blind_mans_town"))
            {
                inventory.Remove(villageMap);
            }
            if (Party.Singleton.GameState.Inventory.containsMapItem("blind_mans_forest_1"))
            {
                inventory.Remove(forestMap);
            }

            if (e.Response == PartyResponse.Yes)
            {
                ScreenManager.Singleton.AddScreen(new ItemShopScreen(inventory));
            }
        }

        #endregion
    }
}
