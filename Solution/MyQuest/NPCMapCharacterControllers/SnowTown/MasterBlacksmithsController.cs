using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    public class MasterBlacksmithsController : NPCMapCharacterController
    {
        internal const string bloodMetalAchievement = "bloodMetal";

        internal const string starfireAchievement = "starfire";

        #region Dialogs

        static readonly Dialog seeEndorDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z255);

        static readonly Dialog bloodUpgradeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA625);

        static readonly Dialog starUpgradeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA631, Strings.ZA632);

        static readonly Dialog oneUpgradeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA633);

        static readonly Dialog bothUpgradeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA634);

        #endregion

        #region Interact

        public override void Interact()
        {
            Dialog dialog;

            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(BloodMetalOre)) >= 1)
            {
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(BloodMetalOre), 1);

                dialog = bloodUpgradeDialog;

                dialog.DialogCompleteEvent += WillCutScene;
            }
            else if (Party.Singleton.GameState.Inventory.ItemCount(typeof(StarfireOre)) >= 1)
            {
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(StarfireOre), 1);

                dialog = starUpgradeDialog;

                dialog.DialogCompleteEvent += CaraCutScene;
            }
            else if (Party.Singleton.PartyAchievements.Contains(bloodMetalAchievement) ^ Party.Singleton.PartyAchievements.Contains(starfireAchievement))
            {
                dialog = oneUpgradeDialog;
                Party.Singleton.AddLogEntry("Zella's Cabin", "Zella", dialog.Text);
            }
            else if (Party.Singleton.PartyAchievements.Contains(bloodMetalAchievement) && Party.Singleton.PartyAchievements.Contains(starfireAchievement))
            {
                dialog = bothUpgradeDialog;
            }
            else
            {
                dialog = seeEndorDialog;
            }
                  
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "MasterBlacksmith"));
        }

        #endregion

        void WillCutScene(object sender, EventArgs e)
        {
            Party.Singleton.AddAchievement(bloodMetalAchievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WillUpgradeCutSceneScreen"));
        }

        void CaraCutScene(object sender, EventArgs e)
        {
            Party.Singleton.AddAchievement(starfireAchievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "CaraUpgradeCutSceneScreen"));
        }
    }
}
