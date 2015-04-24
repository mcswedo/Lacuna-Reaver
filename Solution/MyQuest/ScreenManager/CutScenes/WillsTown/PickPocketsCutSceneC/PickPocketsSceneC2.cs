using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PickPocketsSceneC2 : Scene
    {
        Dialog sleepDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA455);

        public PickPocketsSceneC2(Screen screen)
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
                new Point(Party.Singleton.Leader.TilePosition.X-1, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            helpers.Add(moveCara);

            moveCara.OnCompleteEvent += new EventHandler(CaraDialog);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void CaraDialog(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);
            Party.Singleton.Leader.FaceDirection(Direction.West);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(sleepDialog, DialogScreen.Location.TopRight, "Cara"));
            sleepDialog.DialogCompleteEvent += ReturnCara;
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
    }
}