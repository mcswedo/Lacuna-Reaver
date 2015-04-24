using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Dungeon4Chest7Controller : NPCMapCharacterController
    {
        Dialog chestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z511);

        public override void Interact()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(LargeHealthPotion), 3);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Dungeon4Chest7",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest2",
                new Point(4, 5),
                ModAction.Add,
                true);

            ScreenManager.Singleton.AddScreen(new DialogScreen(chestDialog, DialogScreen.Location.TopLeft));

            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
        }
    }
}