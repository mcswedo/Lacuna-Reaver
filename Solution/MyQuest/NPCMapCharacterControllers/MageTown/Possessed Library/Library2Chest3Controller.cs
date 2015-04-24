using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Library2Chest3Controller : NPCMapCharacterController
    {
        Dialog giveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z128);

        public void Complete()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);

            //This removes the closed chest from LEDGE
            Party.Singleton.ModifyNPC(
                "possessed_library_2ledge",
                "Library2Chest3",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_2ground",
                "Library2Chest3",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_2ledge",
                "OpenChest3",
                new Point(0, 18),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_2ground",
                "OpenChest3",
                new Point(0, 18),
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