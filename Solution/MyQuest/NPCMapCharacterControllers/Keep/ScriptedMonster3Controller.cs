using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class ScriptedMonster3Controller : NPCMapCharacterController
    {
        internal const string monsterAchievement = "monsterDefeated";

        public override void Interact()
        {
            Dialog helpDialog = new Dialog( DialogPrompt.NeedsClose, Strings.Z135);

            helpDialog.DialogCompleteEvent += ScriptedMonsterBattlef3;

            ScreenManager.Singleton.AddScreen(new DialogScreen(helpDialog, DialogScreen.Location.TopLeft, "TrappedGirl"));
        }

        void ScriptedMonsterBattlef3(object sender, PartyResponseEventArgs e)
        {
            Party.Singleton.AddAchievement(monsterAchievement);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "TrappedGirl",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "SavedGirl",
                new Point(10, 13),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "ScriptedMonster3",
                Point.Zero,
                ModAction.Remove,
                true);

            Portal portal;
            portal = new Portal { DestinationMap = "keepf3_after", DestinationPosition = new Point(8, 13), Position = Point.Zero };
            Party.Singleton.PortalToMap(portal);
            CombatZone zone = CombatZonePool.keepZoneSingleImp;

            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));
        }
    }
}
