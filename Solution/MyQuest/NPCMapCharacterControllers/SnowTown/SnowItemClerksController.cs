using System.Collections.Generic;

namespace MyQuest
{
    public class SnowItemClerksController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;
        SaleItem villageMap = new SaleItem(typeof(SnowTownMap), 50);
        SaleItem caveMap = new SaleItem(typeof(CaveLabyrinthMap), 7000);
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

        static readonly Dialog likeToViewDialog = new Dialog(DialogPrompt.YesNo, Strings.Z226);

        #endregion

        #region Interact

        public override void Interact()
        {
            dialog = likeToViewDialog;
            dialog.DialogCompleteEvent += DisplayInventory;

            inventory.Add(villageMap);
            inventory.Add(caveMap);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersMerchant"));
        }

        #endregion

        #region Callbacks

        void DisplayInventory(object sender, PartyResponseEventArgs e)
        {
            likeToViewDialog.DialogCompleteEvent -= DisplayInventory;

            if (Party.Singleton.GameState.Inventory.containsMapItem("snow_town"))
            {
                inventory.Remove(villageMap);
            }
            if (Party.Singleton.GameState.Inventory.containsMapItem("cave_labyrinth"))
            {
                inventory.Remove(caveMap);
            }

            if (e.Response == PartyResponse.Yes)
            {
                ScreenManager.Singleton.AddScreen(new ItemShopScreen(inventory));
            }
        }

        #endregion
    }
}
