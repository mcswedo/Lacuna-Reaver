using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class SpadeBanditController : NPCMapCharacterController
    {
        static readonly Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA367);

        static readonly Dialog banditDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA368);


        static readonly Dialog pickedUpDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA369);
        Dialog dialog;
        CombatScreen combatScreen; 
        public override void Interact()
        {        
            dialog = nathanDialog;
            dialog.DialogCompleteEvent += BanditResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void BanditResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= BanditResponse;
            dialog = banditDialog;
            dialog.DialogCompleteEvent += BanditBattle;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Bandit"));
        }

        void BanditBattle(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= BanditBattle;

            combatScreen = new CombatScreen(CombatZonePool.scarfBanditZone);

            combatScreen.ExitScreenEvent += EndCombat;

            ScreenManager.Singleton.AddScreen(combatScreen);

            Party.Singleton.ModifyNPC(
                Maps.blindMansForest2,
                "SpadeBandit",
                Point.Zero,
                ModAction.Remove,
                true);
        }

        void EndCombat(object sender, EventArgs e)
        {
            combatScreen.ExitScreenEvent -= EndCombat;
            Party.Singleton.GameState.Inventory.AddItem(typeof(FarmersSpade), 1);
            dialog = pickedUpDialog; 
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

    }
}
