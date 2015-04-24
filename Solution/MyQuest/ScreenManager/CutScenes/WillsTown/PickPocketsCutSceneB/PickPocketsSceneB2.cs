using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class PickPocketsSceneB2 : Scene
    {
        #region Fields

        List<Direction> Steps;

        Point destinationPoint;

        Point getToPoint;

        bool arrivedDestination1;

        NPCMapCharacter cara;

        #endregion

        #region Dialog

        Dialog lookDialog =
        new Dialog(DialogPrompt.NeedsClose, Strings.Z929);

        #endregion

        public PickPocketsSceneB2(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.ModifyNPC(
                            Party.Singleton.CurrentMap.Name,
                            Party.cara,
                            Party.Singleton.Leader.TilePosition,
                            ModAction.Add,
                            false);

            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            cara.MoveSpeed = 1.25f;
            getToPoint = new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y);
            Party.Singleton.Leader.FaceDirection(Direction.East);

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
                cara.FaceDirection(Direction.West);
                ScreenManager.Singleton.AddScreen(new DialogScreen(lookDialog, DialogScreen.Location.TopLeft, "Cara"));
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
