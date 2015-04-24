using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CapturedWillSceneB : Scene
    {
        #region Fields

        List<Direction> Steps;

        Point destinationPoint;

        Point getToPoint;

        bool arrivedDestination1;

        NPCMapCharacter cara;

        #endregion

        public CapturedWillSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

            //Party.Singleton.Leader.FaceDirection(Direction.North);

            Party.Singleton.ModifyNPC(
                            "blind_mans_forest_5",
                            Party.cara,
                            Party.Singleton.Leader.TilePosition,
                            ModAction.Add,
                            false);

            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            cara.MoveSpeed = 2.00f;
            getToPoint = new Point(Party.Singleton.Leader.TilePosition.X-1, Party.Singleton.Leader.TilePosition.Y);

            Steps = Utility.GetPathTo(
                cara.TilePosition,
                getToPoint);

            destinationPoint = Utility.GetMapPositionFromDirection(cara.TilePosition, Steps[0]);
            cara.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            if (arrivedDestination1 == false)
            {
                arrivedDestination1 = MoveNPC(gameTime);
            }
            else
            {
                cara.FaceDirection(Direction.East);
                Party.Singleton.CurrentMap.NpcEntries[0].SpawnPosition = new Point(8, 6);
                Party.Singleton.CurrentMap.NpcEntries[0].SpawnDirection = Direction.East;
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
            cara.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (cara.TilePosition == destinationPoint)
            {
                Steps.RemoveAt(0);

                if (Steps.Count == 0)
                {
                    cara.IdleOnly = true;
                    return true;
                }
                else
                {
                    cara.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

                    destinationPoint = Utility.GetMapPositionFromDirection(cara.TilePosition, Steps[0]);
                }
            }

            return false;
        }

    }
}
