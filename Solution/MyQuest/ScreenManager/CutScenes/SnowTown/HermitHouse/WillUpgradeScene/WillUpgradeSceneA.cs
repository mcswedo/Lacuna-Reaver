using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class WillUpgradeSceneA : Scene
    {
        Dialog dialog;

        Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA626);

        public WillUpgradeSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            if (Party.Singleton.Leader.TilePosition.X == 4)
            {
                SceneHelper willHelper = new MoveNpcCharacterHelper(
                    Party.will,
                    Party.Singleton.Leader.TilePosition,
                    false,
                    new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y + 1),
                    3f);

                helpers.Add(willHelper);

                willHelper.OnCompleteEvent += new EventHandler(onCompleteEvent);
            }
            else if (Party.Singleton.Leader.TilePosition.Y == 5 && Party.Singleton.Leader.TilePosition.X < 8)
            {
                SceneHelper willHelper = new MoveNpcCharacterHelper(
                    Party.will,
                    Party.Singleton.Leader.TilePosition,
                    false,
                    new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y),
                    3f);

                helpers.Add(willHelper);

                willHelper.OnCompleteEvent += new EventHandler(onCompleteEvent);
            }
            else// if (Party.Singleton.Leader.TilePosition.Equals(new Point(8, 3)))
            {
                SceneHelper willHelper = new MoveNpcCharacterHelper(
                    Party.will,
                    Party.Singleton.Leader.TilePosition,
                    false,
                    new Point(Party.Singleton.Leader.TilePosition.X - 1, Party.Singleton.Leader.TilePosition.Y),
                    3f);

                helpers.Add(willHelper);

                willHelper.OnCompleteEvent += new EventHandler(onCompleteEvent);
            }
        }

        void onCompleteEvent(object sender, EventArgs e)
        {
            dialog = willDialog;
            dialog.DialogCompleteEvent += Response;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void Response(object sender, EventArgs e)
        {
            if (Party.Singleton.Leader.TilePosition.X == 4)
            {
                Party.Singleton.Leader.FaceDirection(Direction.South);
            }
            else if (Party.Singleton.Leader.TilePosition.Y == 5 && Party.Singleton.Leader.TilePosition.X < 8)
            {
                Party.Singleton.Leader.FaceDirection(Direction.East);
            }
            else
            {
                Party.Singleton.Leader.FaceDirection(Direction.West);
            }

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