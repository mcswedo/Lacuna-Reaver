using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class MushroomController : NPCMapCharacterController
    {
        #region Fields

        protected string mushroomName;

        #endregion

        #region Achivements

        public const string acquireSwordHiltAchievement = "SwordHilt";

        #endregion

        #region Dialogs

        static readonly Dialog foundMushroomDialog = new Dialog( DialogPrompt.NeedsClose, Strings.Z082);

        static readonly Dialog getHiltDialog = new Dialog( DialogPrompt.NeedsClose, Strings.Z083, Strings.Z084);

        #endregion

        #region Interact

        public override void Interact()
        {
            SoundSystem.Play(AudioCues.ItemPickup);
            Complete();
            int mushroomsCollected = Party.Singleton.GameState.Inventory.ItemCount(typeof(Mushroom));

            if (mushroomsCollected == 1)
            {
                    ScreenManager.Singleton.AddScreen(new DialogScreen(foundMushroomDialog, DialogScreen.Location.TopLeft));
            }
            else if (mushroomsCollected == 4)
            {
                ScreenManager.Singleton.AddScreen(new DialogScreen(getHiltDialog, DialogScreen.Location.TopLeft));
            }
        }

        #endregion

        #region Callbacks

        public void Complete()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(Mushroom), 1);

            Party.Singleton.ModifyNPC(
                Maps.mushroomForest,
                mushroomName,
                Point.Zero,
                ModAction.Remove,
                true);

            int mushroomsCollected = Party.Singleton.GameState.Inventory.ItemCount(typeof(Mushroom));
            if (mushroomsCollected == 4)
            {
                Party.Singleton.AddAchievement(acquireSwordHiltAchievement);
                Party.Singleton.GameState.Inventory.AddItem(typeof(SwordHilt), 1);
            }
        }

        #endregion
    }
}
