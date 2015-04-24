using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class MonsterChestController : NPCMapCharacterController
    {
        Dialog surpriseDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z133);

        public override void Interact()
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "MonsterChest",
                Point.Zero,
                ModAction.Remove,
                true);

            surpriseDialog.DialogCompleteEvent += MonsterChestBattle;

            ScreenManager.Singleton.AddScreen(new DialogScreen(surpriseDialog, DialogScreen.Location.TopLeft));
        }

        void MonsterChestBattle(object sender, PartyResponseEventArgs e)
        {
            //Party.Singleton.ModifyNPC(
            //    Party.Singleton.CurrentMap.Name,
            //    "MonsterChest",
            //    Point.Zero,
            //    ModAction.Remove,
            //    true);

            CombatZone zone = CombatZonePool.keepZoneSingleImp;

            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));
        }
    }
}