using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class CaraStopPortalSceneA : Scene
    {
        Dialog cantLeaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z901);

        public CaraStopPortalSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            if (Party.Singleton.Leader.TilePosition.Y > 20)
            {
                //Prevent player from going into overworld.
                Party.Singleton.Leader.FaceDirection(Direction.North);
                ScreenManager.Singleton.AddScreen(new DialogScreen(cantLeaveDialog, DialogScreen.Location.TopLeft, "Cara"));
            }
            else
            {
                //Prevent player from going into the inn or mansion.
                Party.Singleton.Leader.FaceDirection(Direction.South);
                ScreenManager.Singleton.AddScreen(new DialogScreen(cantLeaveDialog, DialogScreen.Location.TopLeft, "Cara"));
            }
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.Leader.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (Party.Singleton.Leader.IsMoving == false)
            {
                state = SceneState.Complete;
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
