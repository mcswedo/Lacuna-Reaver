using System.Collections.Generic;

namespace MyQuest
{
    public class RefugeeItemClerksController : NPCMapCharacterController
    {
        #region Fields

        Dialog dialog;
        SaleItem villageMap = new SaleItem(typeof(RefugeeCampMap), 100);
        SaleItem agoraMap = new SaleItem(typeof(AgoraMap), 2000);
        SaleItem forbiddenCavernMap = new SaleItem(typeof(ForbiddenCavernMap), 5000);
        List<SaleItem> inventory = new List<SaleItem>()
                {
                    new SaleItem(typeof(MediumHealthPotion), 150),
                    new SaleItem(typeof(LargeHealthPotion), 750),                   
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
            inventory.Add(agoraMap);
            inventory.Add(forbiddenCavernMap);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "HealersMerchant"));
        }

        #endregion

        #region Callbacks

        void DisplayInventory(object sender, PartyResponseEventArgs e)
        {
            likeToViewDialog.DialogCompleteEvent -= DisplayInventory;

            if (Party.Singleton.GameState.Inventory.containsMapItem("agora_refugee_area"))
            {
                inventory.Remove(villageMap);
            }
            if (Party.Singleton.GameState.Inventory.containsMapItem("agora"))
            {
                inventory.Remove(agoraMap);
            }

            if (Party.Singleton.GameState.Inventory.containsMapItem("forbidden_cavern_high"))
            {
                inventory.Remove(forbiddenCavernMap);
            }

            if (e.Response == PartyResponse.Yes)
            {
                ScreenManager.Singleton.AddScreen(new ItemShopScreen(inventory));
            }
        }

        #endregion
    }
}
