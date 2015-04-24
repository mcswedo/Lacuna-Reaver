using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CapturedWillSceneA : Scene
    {
        #region Fields

        List<Direction> Steps;
        Point destinationPoint;
        Point getToPoint;
        bool arrivedDestination;

        #endregion

        public CapturedWillSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

            getToPoint = new Point(9, 6);

            Steps = Utility.GetPathTo(
                Party.Singleton.Leader.TilePosition,
                getToPoint);

            destinationPoint = Utility.GetMapPositionFromDirection(Party.Singleton.Leader.TilePosition, Steps[0]);

            Party.Singleton.Leader.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Singleton.CenterOnTarget(
                 Party.Singleton.Leader.WorldPosition,
                 Party.Singleton.CurrentMap.DimensionsInPixels,
                 ScreenManager.Singleton.ScreenResolution);

            if (arrivedDestination == false)
            {
                arrivedDestination = MovePlayer(gameTime);
            }
            else
            {
                Party.Singleton.Leader.FaceDirection(Direction.South);
                state = SceneState.Complete;
            }

        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        private bool MovePlayer(GameTime gameTime)
        {
            Party.Singleton.Leader.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (Party.Singleton.Leader.TilePosition == destinationPoint)
            {
                Steps.RemoveAt(0);

                if (Steps.Count == 0)
                {
                    return true;
                }
                else
                {
                    Party.Singleton.Leader.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

                    destinationPoint = Utility.GetMapPositionFromDirection(Party.Singleton.Leader.TilePosition, Steps[0]);
                }
            }

            return false;
        }

    }
}
