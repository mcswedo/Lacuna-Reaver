using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class ScarfBanditController : NPCMapCharacterController
    {
        static readonly Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA323);

        static readonly Dialog banditDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA324);

        static readonly Dialog pickedUpDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA325);
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
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "ScarfBandit"));
        }

        void BanditBattle(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= BanditBattle;

            combatScreen = new CombatScreen(CombatZonePool.scarfBanditZone);

            combatScreen.ExitScreenEvent += EndCombat;

            ScreenManager.Singleton.AddScreen(combatScreen);

            Party.Singleton.ModifyNPC(
                Maps.mushroomForest,
                "ScarfBandit",
                Point.Zero,
                ModAction.Remove,
                true);
        }

        void EndCombat(object sender, EventArgs e)
        {
            combatScreen.ExitScreenEvent -= EndCombat;
            Party.Singleton.GameState.Inventory.AddItem(typeof(RedScarf), 1);
            dialog = pickedUpDialog; 
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

    }
}
