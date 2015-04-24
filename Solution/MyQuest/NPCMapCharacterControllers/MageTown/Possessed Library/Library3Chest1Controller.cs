using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Library3Chest1Controller : NPCMapCharacterController
    {
        internal const string chest1 = "chest1";

        internal static List<string> openedChests = new List<string>();

        public void Complete()
        {
            Open();
        }
        public override void Interact()
        {
            Complete();
            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
        }

        internal static void Open()
        {
            openedChests.Add(chest1);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "Library3Chest1",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
               Maps.possessedLibrary3,
                "OpenChest1",
                new Point(8, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3ledge,
                "Library3Chest1",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                 Maps.possessedLibrary3ledge,
                "OpenChest1",
                new Point(8, 5),
                ModAction.Add,
                true);
        }

        internal static void Close()
        {
            openedChests.Remove(chest1);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest1",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Library3Chest1",
                new Point(8, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "OpenChest1",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "Library3Chest1",
                new Point(8, 5),
                ModAction.Add,
                true);
        }
    }
}