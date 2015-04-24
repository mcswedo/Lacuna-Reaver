using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class StolenItemSceneC : Scene
    {
        #region Fields

        List<Direction> Steps;
        Point destinationPoint;
        Point getToPoint;
        bool arrivedDestination1;
        NPCMapCharacter cara;

        const string playedAchievement = "stolenItemCutScene";

        #endregion

        public override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }

        public StolenItemSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            cara.IdleOnly = false;
            cara.MoveSpeed = 1.25f;
            getToPoint = new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y);

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
                Party.Singleton.ModifyNPC(
                           "blind_mans_forest_1",
                           Party.cara,
                           Party.Singleton.Leader.TilePosition,
                           ModAction.Remove,
                           true);

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

