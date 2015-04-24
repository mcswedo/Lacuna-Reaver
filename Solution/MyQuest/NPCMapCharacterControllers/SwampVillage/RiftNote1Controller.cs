using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class RiftNote1Controller : NPCMapCharacterController
    {
        #region Dialogs

        static readonly Dialog retrievedNote1Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z315);

        #endregion

        #region Interact

        public override void Interact()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(RiftNote1), 1);
            Party.Singleton.AddAchievement(RiftManController.foundRiftKeyAchievement);

            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "RiftNote1",
                   Point.Zero,
                   ModAction.Remove,
                   true);

            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            ScreenManager.Singleton.AddScreen(new DialogScreen(retrievedNote1Dialog, DialogScreen.Location.TopLeft));
        }

        #endregion
    }
}
