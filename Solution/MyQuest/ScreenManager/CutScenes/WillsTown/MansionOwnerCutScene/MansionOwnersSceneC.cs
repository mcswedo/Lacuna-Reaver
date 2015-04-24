using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class MansionOwnersSceneC : Scene
    {
        #region Fields

        List<Direction> Steps;

        Point destinationPoint;

        Point getToPoint;

        bool arrivedDestination1;

        NPCMapCharacter cara;

        #endregion

        #region Dialog

        Dialog helpDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z921);

        Dialog catchDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z922, Strings.Z923);

        Dialog dontWorryDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z924);

        #endregion

        public MansionOwnersSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

            Party.Singleton.Leader.FaceDirection(Direction.North);

            Party.Singleton.ModifyNPC(
                            "blind_mans_town_mansion",
                            Party.cara,
                            Party.Singleton.Leader.TilePosition,
                            ModAction.Add,
                            false);

            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            cara.MoveSpeed = 1.25f;
            getToPoint = new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y);

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
                cara.FaceDirection(Direction.North);
                helpDialog.DialogCompleteEvent += richManResponse;
                ScreenManager.Singleton.AddScreen(new DialogScreen(helpDialog, DialogScreen.Location.BottomLeft, "Cara"));
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

        #region Callbacks

        void richManResponse(object sender, EventArgs e)
        {
            helpDialog.DialogCompleteEvent -= richManResponse;
            catchDialog.DialogCompleteEvent += caraResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(catchDialog, DialogScreen.Location.BottomRight, "MansionOwner"));
            Party.Singleton.AddLogEntry("Tamarel", "Rich Man", catchDialog.Text); 
        }

        void caraResponse(object sender, EventArgs e)
        {
            catchDialog.DialogCompleteEvent -= caraResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dontWorryDialog, DialogScreen.Location.BottomLeft, "Cara"));
            state = SceneState.Complete;
        }

        #endregion
    }
}
