using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class FinalBattleSceneB : Scene
    {
        #region Dialog

        static readonly Dialog arlanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z996);

        static readonly Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z997);

        static readonly Dialog arlanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z998);
        #endregion

        Dialog dialog;

        CombatScreen combatScreen;

        public FinalBattleSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = arlanDialog;
            dialog.DialogCompleteEvent += nathansResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Arlan"));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void nathansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse;

            dialog = nathanDialog;

            dialog.DialogCompleteEvent += arlansResponse;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
        }

        void arlansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= arlansResponse;

            dialog = arlanDialog2;

            dialog.DialogCompleteEvent += cutToCombat;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Arlan"));
        }

   

        void cutToCombat(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= cutToCombat;

            combatScreen = new CombatScreen(CombatZonePool.arlanZone);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "ArlanS",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "ArlanInjured",
                new Point(20, 3),
                ModAction.Add,
                false);

            combatScreen.ExitScreenEvent += EndScene;

            ScreenManager.Singleton.AddScreen(combatScreen);
        }

        void EndScene(object sender, EventArgs e)
        {
            combatScreen.ExitScreenEvent -= EndScene;

            MusicSystem.Play(AudioCues.finalBossTransition);

            state = SceneState.Complete;
        }

        #endregion
    }
}
