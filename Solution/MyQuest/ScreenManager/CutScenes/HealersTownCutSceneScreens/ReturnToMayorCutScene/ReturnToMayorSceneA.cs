using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class ReturnToMayorSceneA: Scene
    {
        #region Fields

        List<Direction> steps;
        Point destinationPoint = new Point(8, 5); //in front of mayor
        bool arrivedDestination = false;

        #endregion

        public ReturnToMayorSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            steps = Utility.GetPathTo(
                Party.Singleton.Leader.TilePosition,
                destinationPoint);

            Party.Singleton.Leader.SetAutoMovement(steps[0], Party.Singleton.CurrentMap);
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
                Party.Singleton.Leader.FaceDirection(Direction.North);
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

            if(!Party.Singleton.Leader.IsMoving)
            {
                steps.RemoveAt(0);

                if (steps.Count == 0)
                {
                    return true;
                }
                else
                {
                    Party.Singleton.Leader.SetAutoMovement(steps[0], Party.Singleton.CurrentMap);
                }
            }

            return false;
        }

    }
}
