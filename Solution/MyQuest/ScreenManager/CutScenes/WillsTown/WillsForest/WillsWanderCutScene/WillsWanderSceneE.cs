using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillsWanderSceneE : Scene
    {
        List<Direction> Steps;
        Point destinationPoint;
        Point getToPoint;
        bool arrivedDestination;

        NPCMapCharacter will;

        public WillsWanderSceneE(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            will.MoveSpeed = 1.0f;
            getToPoint = new Point(will.TilePosition.X+1, will.TilePosition.Y);

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

                Dialog blindedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z968);

                ScreenManager.Singleton.AddScreen(new DialogScreen(blindedDialog, DialogScreen.Location.TopLeft, "Will"));

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
