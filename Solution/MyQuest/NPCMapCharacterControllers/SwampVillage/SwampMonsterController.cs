using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class SwampMonsterController : NPCMapCharacterController
    {
        public override void Interact()
        {
            Dialog helpDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z325);

            helpDialog.DialogCompleteEvent += ScriptedMonsterBattlef;

            ScreenManager.Singleton.AddScreen(new DialogScreen(helpDialog, DialogScreen.Location.TopLeft, NPCPool.stub));
        }

        void ScriptedMonsterBattlef(object sender, PartyResponseEventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "ToyMonster",
                Point.Zero,
                ModAction.Remove,
                true);

            CombatZone zone = CombatZonePool.swampZone3;

            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));
        }
    }
}

