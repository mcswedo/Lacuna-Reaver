using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Library3Chest4Controller : NPCMapCharacterController
    {
        internal const string chest4 = "chest4";

        public void Complete()
        {
            Open();
        }

        public override void Interact()
        {
            if (Library3Chest1Controller.openedChests.Contains(Library3Chest3Controller.chest3))
            {
                Complete(); 

                MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            }
            else
            {
                MusicSystem.InterruptMusic(AudioCues.MonsterRoar);

                Library3Chest1Controller.Close();
                Library3Chest2Controller.Close();
            }
        }

        internal static void Open()
        {
            Library3Chest1Controller.openedChests.Add(chest4);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "Library3Chest4",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3,
                "OpenChest4",
                new Point(7, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                 Maps.possessedLibrary3ledge,
                "Library3Chest4",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.possessedLibrary3ledge,
                "OpenChest4",
                new Point(7, 5),
                ModAction.Add,
                true);
        }

        internal static void Close()
        {
            Library3Chest1Controller.openedChests.Remove(chest4);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest4",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Library3Chest4",
                new Point(7, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "OpenChest4",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_3ledge",
                "Library3Chest4",
                new Point(7, 5),
                ModAction.Add,
                true);
        }
    }
}