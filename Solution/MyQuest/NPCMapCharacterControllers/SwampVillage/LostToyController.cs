using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class LostToyController : NPCMapCharacterController
    {
        #region Achivements

        internal const string retrievedToyAchievement = "retrievedtoy";

        #endregion

        #region Dialogs

        static readonly Dialog retrievedLostToyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z311);

        #endregion

        #region Interact

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(retrievedLostToyDialog, DialogScreen.Location.TopLeft));
            Party.Singleton.GameState.Inventory.AddItem(typeof(LostToy), 1);

            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "LostToy",
                   Point.Zero,
                   ModAction.Remove,
                   true);
            Party.Singleton.AddAchievement(retrievedToyAchievement);

        }

        #endregion


    }
}
