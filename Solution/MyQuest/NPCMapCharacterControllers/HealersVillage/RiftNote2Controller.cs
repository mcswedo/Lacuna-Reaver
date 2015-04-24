using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class RiftNote2Controller : NPCMapCharacterController
    {
        #region Dialogs

        static readonly Dialog retrievedNote1Dialog = new Dialog( DialogPrompt.NeedsClose, Strings.Z315);

        #endregion

        #region Interact

        public override void Interact()
        {

            Party.Singleton.AddAchievement(RiftManController.foundSecondRiftKeyAchievement);
            Party.Singleton.GameState.Inventory.AddItem(typeof(RiftNote2), 1);

            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "RiftNote2",
                   Point.Zero,
                   ModAction.Remove,
                   true);

            ScreenManager.Singleton.AddScreen(new DialogScreen(retrievedNote1Dialog, DialogScreen.Location.TopLeft));
        }

        #endregion
    }
}
