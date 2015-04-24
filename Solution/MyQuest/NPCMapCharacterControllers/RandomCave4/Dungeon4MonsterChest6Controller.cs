using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Dungeon4MonsterChest6Controller : NPCMapCharacterController
    {
        Dialog chestDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA543);

        public override void Interact()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(LargeEnergyPotion), 3);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Dungeon4Chest6",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest1",
                new Point(4, 3),
                ModAction.Add,
                true);

            ScreenManager.Singleton.AddScreen(new DialogScreen(chestDialog, DialogScreen.Location.TopLeft));

            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
        }
    }
}