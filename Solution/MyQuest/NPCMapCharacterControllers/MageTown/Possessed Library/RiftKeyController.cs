using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class RiftKeyController : NPCMapCharacterController
    {
        #region Achivements

        internal const string foundRiftKeyAchievement = "foundriftkey";

        #endregion

        #region Dialogs

        static readonly Dialog obtainedKeyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z251);

        #endregion

        #region Interact

        public override void Interact()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(obtainedKeyDialog, DialogScreen.Location.TopLeft));
            Party.Singleton.GameState.Inventory.AddItem(typeof(RiftKey), 1);

            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "RiftKey",
                   Point.Zero,
                   ModAction.Remove,
                   true);
            Party.Singleton.AddAchievement(foundRiftKeyAchievement);

           

        }

        #endregion


    }
}
