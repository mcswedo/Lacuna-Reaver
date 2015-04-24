using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Library1Chest2Controller : NPCMapCharacterController
    {
        Dialog giveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z119);

        public void Complete()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);

            Party.Singleton.ModifyNPC(
                "possessed_library_1ground",
                "Library1Chest2",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_1ledge",
                "Library1Chest2",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_1ledge",
                "OpenChest2",
                new Point(3, 15),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "possessed_library_1ground",
                "OpenChest2",
                new Point(3, 15),
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