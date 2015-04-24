using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class BackToQueenSceneC : Scene
    {
        Dialog dialog;

        #region Dialog

        static readonly Dialog queenDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z858, Strings.Z859);

        static readonly Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z860);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z861);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z862);

        #endregion

        public BackToQueenSceneC(Screen screen)
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

            dialog = queenDialog;

            dialog.DialogCompleteEvent += reward;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "SwampQueen"));
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void reward(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= reward;

            dialog = rewardDialog;

            Party.Singleton.GameState.Inventory.AddItem(typeof(DivineRing), 1); 

            dialog.DialogCompleteEvent += willsResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopRight));
        }

        void willsResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willsResponse;

            dialog = willDialog;

            dialog.DialogCompleteEvent += carasResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void carasResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse;

            dialog = caraDialog;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));

            state = SceneState.Complete; 
        }

        #endregion
    }
}
