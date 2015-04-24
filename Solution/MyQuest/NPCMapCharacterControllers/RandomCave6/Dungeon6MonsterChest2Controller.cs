using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Dungeon6MonsterChest2Controller : NPCMapCharacterController
    {
        Dialog surpriseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z133);

        public override void Interact()
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Dungeon6Chest2",
                Point.Zero,
                ModAction.Remove,
                true);

            surpriseDialog.DialogCompleteEvent += MonsterChestBattle;

            ScreenManager.Singleton.AddScreen(new DialogScreen(surpriseDialog, DialogScreen.Location.TopLeft));
        }

        void MonsterChestBattle(object sender, PartyResponseEventArgs e)
        {
            bool runningEnabled = false;
            //Party.Singleton.ModifyNPC(
            //    Party.Singleton.CurrentMap.Name,
            //    "MonsterChest",
            //    Point.Zero,
            //    ModAction.Remove,
            //    true);

            Monster[] monsters = new Monster[] { new Monster(Monster.polarBurtle, 1, SlotSize.Medium, 1), new Monster(Monster.polarBurtle, 1, SlotSize.Medium, 1) };
            CombatZone zone = new CombatZone("6Dungeon2", 0f, CombatZonePool.caveBG, AudioCues.minibossCue, runningEnabled, CombatZonePool.twoMediumLayoutCollection, monsters);
            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));
        }
    }
}