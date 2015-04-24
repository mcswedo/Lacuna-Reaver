using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class WillsWanderSceneA : Scene
    {
        #region Fields

        List<Direction> Steps;
        Point destinationPoint;
        Point getToPoint;
        bool arrivedDestination1;

        List<Direction> Steps2;
        Point destinationPoint2;
        Point getToPoint2;
        bool arrivedDestination2;
        NPCMapCharacter man;

        #endregion

        public override void Complete()
        {
            Party.Singleton.ModifyNPC(
                  "blind_mans_forest_5",
                  "Ruith",
                  Party.Singleton.Leader.TilePosition,
                  ModAction.Remove,
                  true);
        }

        public WillsWanderSceneA(Screen screen)
            : base(screen)
        {
        }

        public WillsWanderSceneA()
            : base(null)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            getToPoint = new Point(9, 8);

            Steps = Utility.GetPathTo(
                Party.Singleton.Leader.TilePosition,
                getToPoint);

            destinationPoint = Utility.GetMapPositionFromDirection(Party.Singleton.Leader.TilePosition, Steps[0]);

            Party.Singleton.Leader.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

            man = Party.Singleton.CurrentMap.GetNPC("Ruith");
            man.MoveSpeed = 4.50f;
            getToPoint2 = new Point(19, 5);

            Steps2 = Utility.GetPathTo(
                man.TilePosition,
                getToPoint2);

            destinationPoint2 = Utility.GetMapPositionFromDirection(man.TilePosition, Steps2[0]);
            man.SetAutoMovement(Steps2[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            if (arrivedDestination1 == false)
            {
                arrivedDestination1 = MovePlayer(gameTime);
            }
            else
            {
                 Party.Singleton.Leader.FaceDirection(Direction.North);
            }
         
            if (arrivedDestination2 == false)
            {
                arrivedDestination2 = MoveNPC(gameTime);
            }
            else
            {
                Complete();
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

        private bool MoveNPC(GameTime gameTime)
        {
            man.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (man.TilePosition == destinationPoint2)
            {
                Steps2.RemoveAt(0);

                if (Steps2.Count == 0)
                {
                    man.IdleOnly = true;
                    return true;
                }
                else
                {
                    man.SetAutoMovement(Steps2[0], Party.Singleton.CurrentMap);

                    destinationPoint2 = Utility.GetMapPositionFromDirection(man.TilePosition, Steps2[0]);
                }
            }

            return false;
        }

    }
}
