using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class BackToHermitHouseSceneB : Scene
    {
        Dialog dialog;

        #region Dialog

        static readonly Dialog blacksmithsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z770);

        static readonly Dialog carasDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA532);

        static readonly Dialog blacksmithsDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA533);

        static readonly Dialog willsDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA534);

        static readonly Dialog blacksmithsDialog1A = new Dialog(DialogPrompt.NeedsClose, Strings.Z771);

        static readonly Dialog carasDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z772);

        static readonly Dialog blacksmithsDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z773);

        static readonly Dialog willsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z774);

        static readonly Dialog carasDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z775, Strings.Z776, Strings.Z777);

        static readonly Dialog blacksmithsDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z778);

        static readonly Dialog carasDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z779, Strings.Z780);

        static readonly Dialog blacksmithsDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z781);

        static readonly Dialog carasDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z782);

        static readonly Dialog blacksmithsDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z783);

        static readonly Dialog carasDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z784, Strings.Z785);

        static readonly Dialog blacksmithsDialog6 = new Dialog(DialogPrompt.NeedsClose, Strings.Z786, Strings.Z787, Strings.Z788, Strings.Z789);

        static readonly Dialog nathansDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z790);

        #endregion

        public BackToHermitHouseSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Camera.Singleton.CenterOnTarget(
                Party.Singleton.Leader.WorldPosition,
                Party.Singleton.CurrentMap.DimensionsInPixels,
                ScreenManager.Singleton.ScreenResolution);

            Party.Singleton.Leader.FaceDirection(Direction.North);
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            dialog = blacksmithsDialog;

            dialog.DialogCompleteEvent += carasResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void carasResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse;

            dialog = carasDialog1;

            Party.Singleton.GameState.Inventory.AddItem(typeof(DivineRing), 1);

            dialog.DialogCompleteEvent += blacksmithsResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse;

            dialog = blacksmithsDialog1;

            dialog.DialogCompleteEvent += willsResponse;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void willsResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willsResponse;

            dialog = willsDialog1;

            dialog.DialogCompleteEvent += blacksmithsResponse1;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void blacksmithsResponse1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse1;

            dialog = blacksmithsDialog1A;

            dialog.DialogCompleteEvent += carasResponse1;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void carasResponse1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse1;

            dialog = carasDialog;

            Party.Singleton.GameState.Inventory.AddItem(typeof(DivineRing), 1);

            dialog.DialogCompleteEvent += blacksmithsResponse1A;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse1A(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse1A;

            dialog = blacksmithsDialog2;

            dialog.DialogCompleteEvent += willsResponse1;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void willsResponse1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willsResponse1;

            dialog = willsDialog;

            dialog.DialogCompleteEvent += carasResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void carasResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse2;

            dialog = carasDialog2;

            dialog.DialogCompleteEvent += blacksmithsResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse2;

            dialog = blacksmithsDialog3;

            dialog.DialogCompleteEvent += carasResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void carasResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse3;

            dialog = carasDialog3;

            dialog.DialogCompleteEvent += blacksmithsResponse3;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse3;

            dialog = blacksmithsDialog4;

            dialog.DialogCompleteEvent += carasResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void carasResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse4;

            dialog = carasDialog4;

            dialog.DialogCompleteEvent += blacksmithsResponse4;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse4;

            dialog = blacksmithsDialog5;

            dialog.DialogCompleteEvent += carasResponse5;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void carasResponse5(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse5;

            dialog = carasDialog5;

            dialog.DialogCompleteEvent += blacksmithsResponse5;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse5(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse5;

            dialog = blacksmithsDialog6;

            dialog.DialogCompleteEvent += nathansResponse;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void nathansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse;

            dialog = nathansDialog;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));

            state = SceneState.Complete;
        }


        #endregion
    }
}
