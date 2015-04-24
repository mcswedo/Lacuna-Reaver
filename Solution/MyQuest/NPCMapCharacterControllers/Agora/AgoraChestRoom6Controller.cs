using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class AgoraChestRoom6Controller : NPCMapCharacterController
    {
        Dialog giveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA624);
        Point chestPos;
        public void Complete()
        {
            chestPos = Party.Singleton.CurrentMap.GetNPC("AgoraChestRoom6").TilePosition;
            Party.Singleton.GameState.Inventory.AddItem(typeof(HugeEnergyPotion), 1);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "AgoraChestRoom6",  
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


