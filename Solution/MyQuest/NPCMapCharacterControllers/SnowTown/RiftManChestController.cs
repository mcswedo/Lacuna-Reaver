using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class RiftManChestController : NPCMapCharacterController
    {
        Dialog threeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z277, Strings.ZA619);  //Declare the dialog variable
        Dialog twoDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z171);
        Dialog dialog;
        public override void Interact()
        {
            if (Party.Singleton.PartyAchievements.Contains(RiftManController.openedRiftChestAchievement))
            {
                dialog = threeDialog;

                Party.Singleton.PartyAchievements.Add(RiftManController.openedRiftChestAchievement);

                Party.Singleton.GameState.Inventory.AddItem(typeof(HugeHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(HugeEnergyPotion), 1);

                Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,  //Select the map where the modification is applied. Current map in this case.
                    "RiftManChest",  //The name of the npc being modified
                    Point.Zero,  //Position where the modification is being done. Point.Zero is used when removing an npc.
                    ModAction.Remove,  //Add or Remove the npc
                    true);  //Is the change being done permanent? If it is set, this to true. If it's false, the modification will revert to it's original state when the player leaves the map.

                Party.Singleton.ModifyNPC(
                     Party.Singleton.CurrentMap.Name,  //Select the map where the modification is applied. Current map in this case.
                     "OpenChest1",  //The name of the npc being modified
                     new Point(33, 4),  //Position where the modification is being done. Point.Zero is used when removing an npc.
                     ModAction.Add,  //Add or Remove the npc
                     true);

                MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            }
            else
            {
                dialog = twoDialog;

                MusicSystem.InterruptMusic(AudioCues.menuDeny);
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));  //Brings the Dialog up, Location at the topleft.           
        }
    }
}