using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class MysteriaPlantsController : NPCMapCharacterController
    {
        internal const string myAchievement = "acquiredPlant";
        const string farmerMentionedAchievement = FarmersController.mentionedPlantAchievement;

        #region Dialog

        static readonly Dialog doneGrownDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z091,
                                                Strings.Z039, Strings.Z040);
       
        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z092);
        
        #endregion

        public override void Interact()
        {
            Dialog dialog;
            if(!Party.Singleton.PartyAchievements.Contains(farmerMentionedAchievement))            
            {
                doneGrownDialog.DialogCompleteEvent += Reward;
                dialog = doneGrownDialog;
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Farmer"));
            }
            else
            {
                Party.Singleton.GameState.Inventory.AddItem(typeof(MysteriaPlant), 1);
                Party.Singleton.ModifyNPC(Party.Singleton.CurrentMap.Name, "MysteriaPlant", Point.Zero, ModAction.Remove, true);
                dialog = rewardDialog;
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));               
            }

            Party.Singleton.AddAchievement(myAchievement);        
    
        }

        void Reward(object sender, PartyResponseEventArgs e)
        {
            doneGrownDialog.DialogCompleteEvent -= Reward;
            Party.Singleton.GameState.Inventory.AddItem(typeof(MysteriaPlant), 1);
            Party.Singleton.ModifyNPC(Party.Singleton.CurrentMap.Name, "MysteriaPlant", Point.Zero, ModAction.Remove, true);
            ScreenManager.Singleton.AddScreen(new DialogScreen(rewardDialog, DialogScreen.Location.TopLeft));
        }
    }


}
