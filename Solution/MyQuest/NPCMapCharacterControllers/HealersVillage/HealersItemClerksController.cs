using System.Collections.Generic;

namespace MyQuest
{
    public class HealersItemClerksController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;
        SaleItem villageMap = new SaleItem(typeof(HealersVillageMap), 50);
        SaleItem forestMap = new SaleItem(typeof(MushroomForestMap),  150);
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

        #region Dialogs

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
            dialog.DialogCompleteEvent -= DisplayInventory;

            if (e.Response == PartyResponse.Yes)
            {
                if(Party.Singleton.GameState.Inventory.containsMapItem("healers_village"))
                {
                    inventory.Remove(villageMap); 
                }
                if (Party.Singleton.GameState.Inventory.containsMapItem("mushroom_forest"))
                {
                    inventory.Remove(forestMap);
                }
                ScreenManager.Singleton.AddScreen(new ItemShopScreen(inventory));
            }
        }

        #endregion
    }
}
