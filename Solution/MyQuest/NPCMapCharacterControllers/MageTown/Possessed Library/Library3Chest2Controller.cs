using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Library3Chest2Controller : NPCMapCharacterController
    {
        internal const string chest2 = "chest2";

        public void Complete()
        {
            Open();
        }
        public override void Interact()
        {
            if (Library3Chest1Controller.openedChests.Contains(Library3Chest1Controller.chest1))
            {
                Complete();
                MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            }
            else
            {
                MusicSystem.InterruptMusic(AudioCues.MonsterRoar);
            }
        }

        internal static void Open()
        {
            Library3Chest1Controller.openedChests.Add(chest2);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "Library3Chest2",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "OpenChest2",
                new Point(11, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3ledge,
                "Library3Chest2",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3ledge,
                "OpenChest2",
                new Point(11, 5),
                ModAction.Add,
                true);
        }

        internal static void Close()
        {
            Library3Chest1Controller.openedChests.Remove(chest2);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest2",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Library3Chest2",
                new Point(11, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "OpenChest2",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "Library3Chest2",
                new Point(11, 5),
                ModAction.Add,
                true);
        }
    }
}