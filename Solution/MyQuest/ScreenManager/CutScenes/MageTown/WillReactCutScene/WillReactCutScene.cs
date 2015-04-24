using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class WillReactCutScene : Scene
    {
        #region Fields

        List<Direction> Steps;

        Point destinationPoint;

        Point getToPoint;

        bool arrivedDestination1;

        NPCMapCharacter will;

        #endregion

        #region Dialog

        Dialog lookDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z744, Strings.Z745);

        #endregion

        public WillReactCutScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.East);

            Party.Singleton.ModifyNPC(
                            Party.Singleton.CurrentMap.Name,
                            Party.will,
                            Party.Singleton.Leader.TilePosition,
                            ModAction.Add,
                            false);

            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            will.MoveSpeed = 1.25f;
            getToPoint = new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y);

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
                will.FaceDirection(Direction.North);
                
                ScreenManager.Singleton.AddScreen(new DialogScreen(lookDialog, DialogScreen.Location.BottomRight, "Will"));

                lookDialog.DialogCompleteEvent += LookBack;

                state = SceneState.Complete;
            }

        }

        void LookBack(object sender, PartyResponseEventArgs e)
        {
                Party.Singleton.Leader.FaceDirection(Direction.North);
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
