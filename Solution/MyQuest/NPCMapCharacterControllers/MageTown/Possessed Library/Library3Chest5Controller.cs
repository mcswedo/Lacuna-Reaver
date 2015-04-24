using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Library3Chest5Controller : NPCMapCharacterController
    {
        internal const string chest5 = "chest5";

        internal const string hatFoundAchievement = "hatFound";

        public void Complete()
        {
            Open();
        }

        public override void Interact()
        {
            if (Library3Chest1Controller.openedChests.Contains(Library3Chest4Controller.chest4))
            {
                Complete(); 

                MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            }
            else
            {
                MusicSystem.InterruptMusic(AudioCues.MonsterRoar);

                Library3Chest1Controller.Close();
                Library3Chest2Controller.Close();
                Library3Chest3Controller.Close();
            }
        }

        internal static void Open()
        {
            Dialog giveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z249);

            Library3Chest1Controller.openedChests.Add(chest5);
            Party.Singleton.AddAchievement(hatFoundAchievement);

            Party.Singleton.GameState.Inventory.AddItem(typeof(WizardHat), 1);
            ScreenManager.Singleton.AddScreen(new DialogScreen(giveDialog, DialogScreen.Location.TopLeft));

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "Library3Chest5",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "OpenChest5",
                new Point(9, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3ledge,
                "Library3Chest5",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3ledge,
                "OpenChest5",
                new Point(9, 5),
                ModAction.Add,
                true);
        }

        internal static void Close()
        {
            Library3Chest1Controller.openedChests.Remove(chest5);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest5",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Library3Chest5",
                new Point(9, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "OpenChest5",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "Library3Chest5",
                new Point(9, 5),
                ModAction.Add,
                true);
        }
    }
}