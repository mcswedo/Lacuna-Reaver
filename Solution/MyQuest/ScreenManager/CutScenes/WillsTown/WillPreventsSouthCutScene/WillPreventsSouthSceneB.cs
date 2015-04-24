using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class WillPreventsSouthSceneB : Scene
    {

        #region Fields

        List<Direction> Steps;

        Point destinationPoint;

        Point getToPoint;

        bool arrivedDestination1;

        NPCMapCharacter will;

        #endregion

        #region Dialog

        Dialog cantLeaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z934);

        #endregion

        public WillPreventsSouthSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

            Party.Singleton.Leader.FaceDirection(Direction.West);

            Party.Singleton.ModifyNPC(
                            "blind_mans_town",
                            Party.will,
                            Party.Singleton.Leader.TilePosition,
                            ModAction.Add,
                            false);

            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            will.MoveSpeed = 2.00f;
            getToPoint = new Point(Party.Singleton.Leader.TilePosition.X-1, Party.Singleton.Leader.TilePosition.Y);

            Steps = Utility.GetPathTo(
                will.TilePosition,
                getToPoint);

            destinationPoint = Utility.GetMapPositionFromDirection(will.TilePosition, Steps[0]);
            will.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            if (arrivedDestination1 == false)
            {
                arrivedDestination1 = MoveNPC(gameTime);
            }
            else
            {
                will.FaceDirection(Direction.South);
                ScreenManager.Singleton.AddScreen(new DialogScreen(cantLeaveDialog, DialogScreen.Location.TopLeft, "Will"));
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
