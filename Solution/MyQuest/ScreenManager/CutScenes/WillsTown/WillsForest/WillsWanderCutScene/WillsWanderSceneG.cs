using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class WillsWanderSceneG : Scene
    {

        #region Fields

        List<Direction> Steps;
        Point destinationPoint;
        Point getToPoint;
        bool arrivedDestination;

        NPCMapCharacter will;

        #endregion

        public override void Complete()
        {
            Party.Singleton.ModifyNPC(
               "blind_mans_forest_5",
               Party.will,
               Party.Singleton.Leader.TilePosition,
               ModAction.Remove,
               true);
        }

        public WillsWanderSceneG(Screen screen)
            : base(screen)
        {
        }

        public WillsWanderSceneG()
            : base(null)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            will.MoveSpeed = 1.0f;
            getToPoint = new Point(9, 1);

            Steps = Utility.GetPathTo(
                will.TilePosition,
                getToPoint);

            destinationPoint = Utility.GetMapPositionFromDirection(will.TilePosition, Steps[0]);
            will.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

        }

        public override void Update(GameTime gameTime)
        {
            if (arrivedDestination == false)
            {
                arrivedDestination = MoveNPC(gameTime);
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


        private bool MoveNPC(GameTime gameTime)
        {
            will.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (will.TilePosition == destinationPoint)
            {
                Steps.RemoveAt(0);

                if (Steps.Count == 0)
                {
                    will.IdleOnly = true;
                    return true;
                }
                else
                {
                    will.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

                    destinationPoint = Utility.GetMapPositionFromDirection(will.TilePosition, Steps[0]);
                }
            }

            return false;
        }

    }
}

