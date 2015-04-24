using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Library3Chest3Controller : NPCMapCharacterController
    {
        internal const string chest3 = "chest3";

        public void Complete()
        {
            Open();
        }

        public override void Interact()
        {
            if (Library3Chest1Controller.openedChests.Contains(Library3Chest2Controller.chest2))
            {
                Complete(); 

                MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            }
            else
            {
                MusicSystem.InterruptMusic(AudioCues.MonsterRoar);

                Library3Chest1Controller.Close();
            }
        }

        internal static void Open()
        {
            Library3Chest1Controller.openedChests.Add(chest3);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "Library3Chest3",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "OpenChest3",
                new Point(10, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3ledge,
                "Library3Chest3",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                 Maps.possessedLibrary3ledge,
                "OpenChest3",
                new Point(10, 5),
                ModAction.Add,
                true);
        }

        internal static void Close()
        {
            Library3Chest1Controller.openedChests.Remove(chest3);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest3",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Library3Chest3",
                new Point(10, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "OpenChest3",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "Library3Chest3",
                new Point(10, 5),
                ModAction.Add,
                true);
        }
    }
}