using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class InsideHermitHouseSceneA : Scene
    {
        #region Dialog

        static readonly Dialog carasDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z748, Strings.Z749, Strings.Z750, Strings.Z751);

        static readonly Dialog willsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z752, Strings.Z753);

        static readonly Dialog carasDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z754);

        static readonly Dialog willsDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z755);

        static readonly Dialog carasDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z756, Strings.Z757);

        #endregion

        Dialog dialog;

        public InsideHermitHouseSceneA(Screen screen)
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

            SceneHelper caraHelper = new MoveNpcCharacterHelper(
                Party.cara,
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                false,
                new Point(Party.Singleton.Leader.TilePosition.X - 1, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            caraHelper.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);

            helpers.Add(caraHelper);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {

            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            SceneHelper willHelper = new MoveNpcCharacterHelper(
            Party.will,
            new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
            false,
            new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y),
            1.7f);

            willHelper.OnCompleteEvent += new EventHandler(caraResponse);

            helpers.Add(willHelper);
      
        }

        void caraResponse(object sender, EventArgs e)
        {
            dialog = carasDialog;

            dialog.DialogCompleteEvent += willsResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void willsResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willsResponse;

            dialog = willsDialog;

            dialog.DialogCompleteEvent += carasResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopRight, "Will"));
        }

        void carasResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse2;

            dialog = carasDialog2;

            dialog.DialogCompleteEvent += willsResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void willsResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willsResponse2;

            dialog = willsDialog2;

            dialog.DialogCompleteEvent += carasResponse3;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopRight, "Will"));
        }

        void carasResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse3;

            dialog = carasDialog3;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));

            state = SceneState.Complete;
        }

        #endregion
    }
}
