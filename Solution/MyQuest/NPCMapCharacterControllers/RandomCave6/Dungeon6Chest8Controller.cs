using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Dungeon6Chest8Controller : NPCMapCharacterController
    {
        Dialog chestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z120);

        public override void Interact()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(TrainingBelt), 1);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Dungeon6Chest8",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest8",
                new Point(10, 10),
                ModAction.Add,
                true);

            ScreenManager.Singleton.AddScreen(new DialogScreen(chestDialog, DialogScreen.Location.TopLeft));

            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
        }
    }
}