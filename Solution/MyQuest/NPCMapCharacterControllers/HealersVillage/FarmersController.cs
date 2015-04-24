using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

/// An example of how a controller is implemented

namespace MyQuest
{
    public class FarmersController : NPCMapCharacterController
    {
        #region Achievements

        const string savedVillageAchievement = HealersBattleSceneA.savedVillageAchievement;
        const string returnToVillageAchievement = WillsBurtleInitiateSceneC.achievement;

        internal const string myAchievement = "plantedSeeds";
        internal const string grownAchievement = "specialPlant";
        internal const string mentionedPlantAchievement = "mentionedPlant";
        internal const string acquiredPlantAchievement = MysteriaPlantsController.myAchievement;

        #endregion

        #region Dialogs

        static readonly Dialog morningDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z028);

        static readonly Dialog seedsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z029, 
                                                Strings.Z030);

        static readonly Dialog wowLaddyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z031);
        
        static readonly Dialog welcomeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z032);
       
        static readonly Dialog foundSpadeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z033, 
                                                Strings.Z034, 
                                                Strings.Z035);

        static readonly Dialog patientDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z036, 
                                                Strings.Z037);

        static readonly Dialog doneGrownDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z038,
                                                Strings.Z039, Strings.Z040);

        static readonly Dialog gladDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z041);

        #endregion

        #region Interact

        public override void Interact()
        {
            Dialog dialog;
            if (Party.Singleton.PartyAchievements.Contains(savedVillageAchievement))
            {
                dialog = wowLaddyDialog;
            }
            else
            {
                dialog = morningDialog;
            }

            if (Party.Singleton.PartyAchievements.Contains(returnToVillageAchievement))
            {
                dialog = welcomeDialog; 
            }

            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(RareSeeds)) > 0)
            {
                dialog = seedsDialog;

                Party.Singleton.AddLogEntry("Mushroom Hollow", "Farmer", dialog.Text);

                Party.Singleton.ModifyNPC(
                    Maps.blindMansForest2,
                    "SpadeBandit",
                     new Point(12, 32),
                    Direction.South,
                    true,
                    ModAction.Add,
                    true);
            }

            if (Party.Singleton.PartyAchievements.Contains(myAchievement))
            {
                dialog = patientDialog;
            }

            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(FarmersSpade)) > 0)
            {
                dialog = foundSpadeDialog;

                Party.Singleton.AddLogEntry("Mushroom Hollow", "Farmer", dialog.Text);
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(FarmersSpade), 1);
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(RareSeeds), 1);
                Party.Singleton.AddAchievement(myAchievement);
            }
    
            if (Party.Singleton.PartyAchievements.Contains(grownAchievement))
            {
                dialog = doneGrownDialog;
                Party.Singleton.AddAchievement(mentionedPlantAchievement);
            }
            if (Party.Singleton.PartyAchievements.Contains(acquiredPlantAchievement))
            {
                dialog = gladDialog;
            }
            
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Farmer"));
        }


        #endregion
    }
}
