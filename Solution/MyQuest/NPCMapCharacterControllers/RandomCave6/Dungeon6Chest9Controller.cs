using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Dungeon6Chest9Controller : NPCMapCharacterController
    {
        Dialog chestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA565);

        public override void Interact()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(DivineRing), 1);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Dungeon6Chest9",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest9",
                new Point(2, 18),
                ModAction.Add,
                true);

            ScreenManager.Singleton.AddScreen(new DialogScreen(chestDialog, DialogScreen.Location.TopLeft));

            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
        }
    }
}