using System.Collections.Generic;

/// An example of how a controller is implemented

namespace MyQuest
{
    public class DrifterController : NPCMapCharacterController
    {
        
        #region Fields

        const int reward = 50;
        static bool hasSpoken;

        #endregion

        #region Achievements

        internal const string returnedItemAchievement = "returnedItem";
        const string foundItemAchievement = LostItemController.foundItemAchievement;
        internal const string logAchievement = "logAchievement";

        #endregion

        #region Dialogs

        Dialog dialog;

        static readonly Dialog needHelpDialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z017,
            Strings.Z018,
            Strings.Z019,
            Strings.Z020);

        static readonly Dialog itemFoundDialog = new Dialog(DialogPrompt.NeedsClose,  //If the item was found before Nathan speaks to Drifter
            Strings.Z021,
            Strings.Z022,
            Strings.Z023 + " " + reward + " " + Strings.Z024);

        static readonly Dialog foundItemDialog = new Dialog(DialogPrompt.NeedsClose,
            Strings.Z025 + " " + reward + " " + Strings.Z026);

        static readonly Dialog thankYouDialog = new Dialog(DialogPrompt.NeedsClose,
            Strings.Z027);


        #endregion

        #region Interact

        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(returnedItemAchievement))
            {
                dialog = thankYouDialog;
            }
            else if (Party.Singleton.PartyAchievements.Contains(foundItemAchievement))
            {
                if (hasSpoken == false)
                {
                    dialog = itemFoundDialog;  //First time speaking to Drifter
                }

                else if(hasSpoken)
                {
                    dialog = foundItemDialog;
                }

                Party.Singleton.GameState.Inventory.RemoveItem(typeof(LostItem), 1);

                Party.Singleton.GameState.Gold += reward;

                Party.Singleton.AddAchievement(returnedItemAchievement);
            }
            else
            {
                dialog = needHelpDialog;

                if (!Party.Singleton.PartyAchievements.Contains(logAchievement))
                {
                    Party.Singleton.AddLogEntry("Mushroom Forest", "Drifter", needHelpDialog.Text);
                    Party.Singleton.AddAchievement(logAchievement);
                      hasSpoken = true;
                }
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Drifter"));
        }

        #endregion
    }
}
