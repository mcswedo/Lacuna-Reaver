using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class BanditKingController : NPCMapCharacterController
    {
        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA320);

        static readonly Dialog banditDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA321);


        static readonly Dialog pickedUpDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA322);
        Dialog dialog;
        CombatScreen combatScreen; 
        public override void Interact()
        {        
            dialog = caraDialog;
            dialog.DialogCompleteEvent += BanditResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void BanditResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= BanditResponse;
            dialog = banditDialog;
            dialog.DialogCompleteEvent += BanditBattle;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "BanditKing"));
        }

        void BanditBattle(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= BanditBattle;

            combatScreen = new CombatScreen(CombatZonePool.banditKingZone);

            combatScreen.ExitScreenEvent += EndCombat;

            ScreenManager.Singleton.AddScreen(combatScreen);

            Party.Singleton.ModifyNPC(
                Maps.mushroomForest,
                "BanditKing",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
               Maps.mushroomForest,
               "Bandit1",
               Point.Zero,
               ModAction.Remove,
               true);

            Party.Singleton.ModifyNPC(
              Maps.mushroomForest,
              "Bandit2",
              Point.Zero,
              ModAction.Remove,
              true);


            Party.Singleton.ModifyNPC(
              Maps.mushroomForest,
              "Bandit3",
              Point.Zero,
              ModAction.Remove,
              true);

            Party.Singleton.ModifyNPC(
              Maps.mushroomForest,
              "Bandit4",
              Point.Zero,
              ModAction.Remove,
              true);
        }

        void EndCombat(object sender, EventArgs e)
        {
            combatScreen.ExitScreenEvent -= EndCombat;
            Party.Singleton.GameState.Inventory.AddItem(typeof(BanditCrown), 1);
            dialog = pickedUpDialog; 
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

    }
}
