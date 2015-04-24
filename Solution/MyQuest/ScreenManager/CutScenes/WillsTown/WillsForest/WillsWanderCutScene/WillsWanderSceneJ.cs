using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class WillsWanderSceneJ : Scene
    {

        #region Fields

        List<Direction> Steps;
        Point destinationPoint;
        Point getToPoint;
        bool arrivedDestination;

        const string playedAchievement = "willsWanderCutScene";

        NPCMapCharacter cara;

        #endregion

        public override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }

        public WillsWanderSceneJ(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

            Party.Singleton.AddAchievement("willsWanderCutSceneEnded"); 

            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
          

            getToPoint = Party.Singleton.Leader.TilePosition; 
            System.Console.WriteLine(Party.Singleton.Leader.TilePosition);
          
            Steps = Utility.GetPathTo(
                cara.TilePosition,
                getToPoint);

            destinationPoint = Utility.GetMapPositionFromDirection(cara.TilePosition, Steps[0]);

            cara.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

        }

        public override void Update(GameTime gameTime)
        {

            if (arrivedDestination == false)
            {
                arrivedDestination = MoveNPC(gameTime);
            }
            else
            {
                Party.Singleton.ModifyNPC(
                           "blind_mans_forest_5",
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

