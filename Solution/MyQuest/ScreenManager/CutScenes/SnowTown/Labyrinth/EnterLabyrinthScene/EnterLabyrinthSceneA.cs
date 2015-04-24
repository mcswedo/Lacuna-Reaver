using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class EnterLabyrinthSceneA : Scene
    {
        #region Dialog

        static readonly Dialog carasDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z833, Strings.Z834);

        static readonly Dialog willsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z835);

        static readonly Dialog carasDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z836);

        #endregion

        Dialog dialog;

        public EnterLabyrinthSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper nathanHelper1 = new MovePCMapCharacterHelper(new Point(Party.Singleton.Leader.TilePosition.X, 
                Party.Singleton.Leader.TilePosition.Y - 2));

            nathanHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(nathanHelper1);
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
            SceneHelper caraHelper = new MoveNpcCharacterHelper(
                 Party.cara,
                 new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                 false,
                 new Point(Party.Singleton.Leader.TilePosition.X - 1, Party.Singleton.Leader.TilePosition.Y),
                 1.7f);

            caraHelper.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);

            helpers.Add(caraHelper);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            SceneHelper willHelper = new MoveNpcCharacterHelper(
            Party.will,
            new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
            false,
            new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y),
            1.7f);

            willHelper.OnCompleteEvent += new EventHandler(carasResponse);

            helpers.Add(willHelper);
        }

        void carasResponse(object sender, EventArgs e)
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

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));

            state = SceneState.Complete;
        }

        #endregion
    }
}
