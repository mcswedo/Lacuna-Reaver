using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class WillEndorsStudySceneA : Scene
    {
        Dialog dialog;

        #region Dialog

        static readonly Dialog ruithDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z721);
        static readonly Dialog endorDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z722, Strings.Z723);
        static readonly Dialog ruithDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z724);
        static readonly Dialog endorDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z725, Strings.Z726);
        static readonly Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z999, Strings.ZA001);
        static readonly Dialog ruithDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA002, Strings.ZA003);
        static readonly Dialog endorDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA004, Strings.ZA005, Strings.ZA006);
        static readonly Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA007);
        static readonly Dialog endorDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA008, Strings.ZA009);

        #endregion

        public WillEndorsStudySceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);

            dialog = ruithDialog1;
            dialog.DialogCompleteEvent += EndorResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Ruith"));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

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

            dialog = ruithDialog2;
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

            NPCMapCharacter ruith = Party.Singleton.CurrentMap.GetNPC("RuithNight");
            ruith.FaceDirection(Direction.South);
            dialog = willDialog1;
            dialog.DialogCompleteEvent += RuithResponse2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void RuithResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= RuithResponse2;

            dialog = ruithDialog3;
            dialog.DialogCompleteEvent += EndorResponse3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Ruith"));
        }

        void EndorResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= EndorResponse3;

            dialog = endorDialog3;
            dialog.DialogCompleteEvent += WillResponse2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Endor"));
        }

        void WillResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= WillResponse2;

            dialog = willDialog2;
            dialog.DialogCompleteEvent += EndorResponse4;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Will"));
        }

        void EndorResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= EndorResponse4;

            dialog = endorDialog4;
            dialog.DialogCompleteEvent += Done;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Endor"));
        }

        //void NegaWillAppears(object sender, EventArgs e)
        //{
        //    dialog.DialogCompleteEvent -= NegaWillAppears;

        //    // Add NegaWill to the map 
        //    dialog.DialogCompleteEvent += Done;
        //    // Some dialog...
        //}

        //void StartCombat(object sender, EventArgs e)
        //{
        //    dialog.DialogCompleteEvent -= StartCombat;

        //    CombatScreen combatScreen;
        //    combatScreen = new CombatScreen(CombatZonePool.negaWillZone);
        //    ScreenManager.Singleton.AddScreen(combatScreen);

        //    combatScreen.ExitScreenEvent += CombatWon;
        //}

        //void CombatWon(object sender, EventArgs e)
        //{
        //    dialog.DialogCompleteEvent -= CombatWon;

        //    // Set dialog
        //    dialog.DialogCompleteEvent += Done;
        //    // Some dialog...
        //}

        void Done(object sender, EventArgs e)
        {
            state = SceneState.Complete;
        }

        #endregion
    }
}
