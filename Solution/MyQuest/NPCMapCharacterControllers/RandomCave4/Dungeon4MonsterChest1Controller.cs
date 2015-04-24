using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class Dungeon4MonsterChest1Controller : NPCMapCharacterController
    {
        Dialog surpriseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z133);

        public override void Interact()
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Dungeon4Chest1",
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

            Monster[] monsters = new Monster[] { new Monster(Monster.voodooDoll, 1, SlotSize.Medium, 1), new Monster(Monster.voodooDoll, 1, SlotSize.Medium, 1), new Monster(Monster.voodooDoll, 1, SlotSize.Medium, 1), 
                new Monster(Monster.voodooDoll, 1, SlotSize.Medium, 1), new Monster(Monster.voodooDoll, 1, SlotSize.Medium, 1) };
            CombatZone zone = new CombatZone("4Dungeon1", 0f, CombatZonePool.caveBG, AudioCues.minibossCue, runningEnabled, CombatZonePool.fiveMediumLayoutCollection, monsters);
            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));
        }
    }
}