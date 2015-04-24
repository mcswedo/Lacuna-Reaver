using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CapturedWillSceneC : Scene
    {
        #region Fields

        List<Direction> Steps;

        Point destinationPoint;

        Point getToPoint;

        bool arrivedDestination1;

        NPCMapCharacter Will;

        #endregion

        #region Dialog

        Dialog amazingDialog =
        new Dialog(DialogPrompt.NeedsClose, Strings.Z936, Strings.Z937, Strings.Z938, Strings.Z939);

        Dialog stillAThiefDialog =
       new Dialog(DialogPrompt.NeedsClose, Strings.Z940, Strings.Z941);

        #endregion

        public CapturedWillSceneC(Screen screen)
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
                            "blind_mans_forest_5",
                            Party.will,
                            Party.Singleton.Leader.TilePosition,
                            ModAction.Add,
                            false);

            Will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            Will.MoveSpeed = 2.00f;
            getToPoint = new Point(Party.Singleton.Leader.TilePosition.X+1, Party.Singleton.Leader.TilePosition.Y);

            Steps = Utility.GetPathTo(
                Will.TilePosition,
                getToPoint);

            destinationPoint = Utility.GetMapPositionFromDirection(Will.TilePosition, Steps[0]);
            Will.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            if (arrivedDestination1 == false)
            {
                arrivedDestination1 = MoveNPC(gameTime);
            }
            else
            {
                Will.FaceDirection(Direction.South);
                amazingDialog.DialogCompleteEvent += nextDialog;
                ScreenManager.Singleton.AddScreen(new DialogScreen(amazingDialog, DialogScreen.Location.TopRight, "Will"));
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
            Will.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (Will.TilePosition == destinationPoint)
            {
                Steps.RemoveAt(0);

                if (Steps.Count == 0)
                {
                    Will.IdleOnly = true;
                    return true;
                }
                else
                {
                    Will.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

                    destinationPoint = Utility.GetMapPositionFromDirection(Will.TilePosition, Steps[0]);
                }
            }

            return false;
        }
        
        void nextDialog(object sender, PartyResponseEventArgs e)
        {
            amazingDialog.DialogCompleteEvent -= nextDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(stillAThiefDialog, DialogScreen.Location.TopLeft, "Cara"));

            state = SceneState.Complete;
        }

    }
}
