using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Library1Chest3Controller : NPCMapCharacterController
    {
        Dialog giveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z087, " " + Strings.Z086);

        public void Complete()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(SmallHealthPotion), 1);
            Party.Singleton.GameState.Inventory.AddItem(typeof(SmallEnergyPotion), 1);

            Party.Singleton.ModifyNPC(
                "possessed_library_1ground",
                "Library1Chest3",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_1ledge",
                "Library1Chest3",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_1ledge",
                "OpenChest3",
                new Point(10, 6),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_1ground",
                "OpenChest3",
                new Point(10, 6),
                ModAction.Add,
                true);
        }
        public override void Interact()
        {
            Complete();

            ScreenManager.Singleton.AddScreen(new DialogScreen(giveDialog, DialogScreen.Location.TopLeft));

            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            //SoundSystem.Play(SoundSystem.ChestOpen);
            
        }
    }
}