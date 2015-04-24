using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class WillEndorsStudySceneC : Scene
    {
        Dialog dialog;

        #region Dialog

        static readonly Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA254);
        static readonly Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA255, Strings.ZA256);
        static readonly Dialog endorDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA257, Strings.ZA258);
        static readonly Dialog ruithDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA259);
        static readonly Dialog endorDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA260);
        static readonly Dialog willDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA261);
        static readonly Dialog endorDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA262, Strings.ZA263);

        #endregion

        public WillEndorsStudySceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            NPCMapCharacter ruith = Party.Singleton.CurrentMap.GetNPC("RuithNight");
            ruith.FaceDirection(Direction.South);

            Party.Singleton.Leader.FaceDirection(Direction.North);

            dialog = willDialog1;
            dialog.DialogCompleteEvent += CombatStart;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Will"));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void CombatStart(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CombatStart;

            CombatScreen combatScreen;
            combatScreen = new CombatScreen(CombatZonePool.negaWillZone);
            ScreenManager.Singleton.AddScreen(combatScreen);

            combatScreen.ExitScreenEvent += CombatDone;
        }

        void CombatDone(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CombatDone;

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "NegaWill",
                new Point(6, 10),
                ModAction.Remove,
                false);

            dialog = willDialog2;
            dialog.DialogCompleteEvent += EndorResponse1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void EndorResponse1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= EndorResponse1;

            dialog = endorDialog1;
            dialog.DialogCompleteEvent += RuithResponse1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void RuithResponse1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= RuithResponse1;

            dialog = ruithDialog1;
            dialog.DialogCompleteEvent += EndorResponse2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Ruith"));
        }

        void EndorResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= EndorResponse2;

            dialog = endorDialog2;
            dialog.DialogCompleteEvent += WillResponse1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void WillResponse1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= WillResponse1;

            dialog = willDialog3;
            dialog.DialogCompleteEvent += EndorResponse3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void EndorResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= EndorResponse3;

            dialog = endorDialog3;
            dialog.DialogCompleteEvent += Done;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void Done(object sender, EventArgs e)
        {
            state = SceneState.Complete;
        }

        #endregion
    }
}
