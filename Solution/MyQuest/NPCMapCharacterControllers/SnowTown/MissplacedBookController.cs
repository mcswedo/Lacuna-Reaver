using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class MissplacedBookController : NPCMapCharacterController
    {
        #region Achivements

        internal const string foundMissplacedBookAchievement = "foundmissplacedbook";

        #endregion

        #region Dialogs

        static readonly Dialog retrievedBookDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z256);

        #endregion

        #region Interact

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(retrievedBookDialog, DialogScreen.Location.TopLeft));
            Party.Singleton.GameState.Inventory.AddItem(typeof(MissplacedBook), 1);

            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "MissplacedBook",
                   Point.Zero,
                   ModAction.Remove,
                   true);
            Party.Singleton.AddAchievement(foundMissplacedBookAchievement);

        }

        #endregion
    }
}
