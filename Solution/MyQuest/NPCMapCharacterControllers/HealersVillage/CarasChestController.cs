using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class CarasChestController : NPCMapCharacterController
    {
        Dialog giveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z013);

        public override void Interact()
        {
            if (Party.Singleton.GameState.Inventory.ItemCount(new SmallHealthPotion()) < 3 &&
                !Party.Singleton.PartyAchievements.Contains(MushroomController.acquireSwordHiltAchievement))
            {
                Party.Singleton.GameState.Inventory.AddItem(typeof(SmallHealthPotion), 3);

                Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "CarasChest",
                    Point.Zero,
                    ModAction.Remove,
                    false);

                Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "OpenChest1",
                    new Point(0, 4),
                    ModAction.Add,
                    false);

                MusicSystem.InterruptMusic(AudioCues.ChestOpen);
                ScreenManager.Singleton.AddScreen(new DialogScreen(giveDialog, DialogScreen.Location.TopLeft));
            }
            else if (Party.Singleton.PartyAchievements.Contains(MushroomController.acquireSwordHiltAchievement))
            {
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 2);

                Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "CarasChest",
                    Point.Zero,
                    ModAction.Remove,
                    true);

                Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "OpenChest1",
                    new Point(0, 4),
                    ModAction.Add,
                    true);

                MusicSystem.InterruptMusic(AudioCues.ChestOpen);
                ScreenManager.Singleton.AddScreen(new DialogScreen(giveDialog, DialogScreen.Location.TopLeft));
            }
            else
            {
                MusicSystem.InterruptMusic(AudioCues.menuDeny);
            }
        }
    }
}