using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class TippersController : NPCMapCharacterController
    {
        #region Achievements

        internal const string tippersFoundAchievement = "tippersFound";

        #endregion

        #region Dialog

        static readonly Dialog foundTippersDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z252);

        #endregion

        #region Interact

        public void CompleteAchievement()
        {

            Party.Singleton.GameState.Inventory.AddItem(typeof(Tippers), 1);
            Party.Singleton.AddAchievement(tippersFoundAchievement);
        }

        public void CompleteMapMod()
        {
            Party.Singleton.ModifyNPC(
                  "possessed_library_4ground",
                  "Tippers",
                  Point.Zero,
                  ModAction.Remove,
                  true);

            Party.Singleton.ModifyNPC(
                    "possessed_library_4ledge",
                    "Tippers",
                    Point.Zero,
                    ModAction.Remove,
                    true);

            Party.Singleton.ModifyNPC(
                    "possessed_library_4secret1",
                    "Tippers",
                    Point.Zero,
                    ModAction.Remove,
                    true);

            Party.Singleton.ModifyNPC(
                    "possessed_library_4secret2",
                    "Tippers",
                    Point.Zero,
                    ModAction.Remove,
                    true);
        }

        public override void Interact()
        {
            CompleteAchievement();

            SoundSystem.Play("ChestOpen"); //temporary soundeffect

            CompleteMapMod();

            ScreenManager.Singleton.AddScreen(new DialogScreen(foundTippersDialog, DialogScreen.Location.TopLeft));
        }

        #endregion

    }
}
