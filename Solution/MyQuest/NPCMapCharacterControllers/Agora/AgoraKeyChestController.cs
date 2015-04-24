using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class AgoraKeyChestController : NPCMapCharacterController
    {
        Dialog giveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z489);
        Point chestPos;
        public void Complete()
        {
            chestPos = Party.Singleton.CurrentMap.GetNPC("AgoraKeyChest").TilePosition;
            Party.Singleton.GameState.Inventory.AddItem(typeof(Courtyardkey), 1); 

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name, 
                "AgoraKeyChest",  
                Point.Zero,  
                ModAction.Remove, 
                true);  

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest1",
                chestPos,  
                ModAction.Add,
                true);
        }

        public override void Interact()  
        {
            Complete();
            ScreenManager.Singleton.AddScreen(new DialogScreen(giveDialog, DialogScreen.Location.TopLeft));  
            MusicSystem.InterruptMusic(AudioCues.ChestOpen);             
        }
    }
}


