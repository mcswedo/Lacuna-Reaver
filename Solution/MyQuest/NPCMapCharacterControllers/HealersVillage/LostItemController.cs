using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class LostItemController : NPCMapCharacterController
    {
        #region Achievements

        internal const string foundItemAchievement = "foundItem";

        #endregion

        #region Dialogs

        static readonly Dialog foundLostItemDialog = new Dialog( DialogPrompt.NeedsClose, Strings.Z068);

        #endregion

        #region Interact

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(foundLostItemDialog, DialogScreen.Location.TopLeft));
            Party.Singleton.GameState.Inventory.AddItem(typeof(LostItem), 1);

            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "LostItem",
                   Point.Zero,
                   ModAction.Remove,
                   true);
            Party.Singleton.AddAchievement(foundItemAchievement);

        }

        #endregion


    }
}
