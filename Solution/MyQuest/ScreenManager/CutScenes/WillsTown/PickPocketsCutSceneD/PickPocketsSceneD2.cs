using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class PickPocketsSceneD2 : Scene
    {
        #region Dialog

        Dialog lookDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z930);

        #endregion

        public PickPocketsSceneD2(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MoveNpcCharacterHelper moveCara = new MoveNpcCharacterHelper(
                "Cara",
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                false,
                new Point(Party.Singleton.Leader.TilePosition.X+1, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            helpers.Add(moveCara);

            moveCara.OnCompleteEvent += new EventHandler(CaraDialog);
        }

        void CaraDialog(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.West);
            Party.Singleton.Leader.FaceDirection(Direction.East);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(lookDialog, DialogScreen.Location.TopRight, "Cara"));
            lookDialog.DialogCompleteEvent += ReturnCara;
        }

        void ReturnCara(object sender, PartyResponseEventArgs e)
        {
            MoveNpcCharacterHelper moveCara = new MoveNpcCharacterHelper(
                "Cara",
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            helpers.Add(moveCara);

            moveCara.OnCompleteEvent += new EventHandler(EndScene);
        }

        void EndScene(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Cara",
                Point.Zero,
                ModAction.Remove,
                true);

            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}
