using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class GrandsonAddScene : Scene
    {

        #region Fields

        List<Direction> Steps;

        Point destinationPoint;

        Point getToPoint;

        bool arrivedDestination1;

        NPCMapCharacter grandson;

        NPCMapCharacter grandmother;

        #endregion

        #region Achievements

        internal const string thisAchievement = "grandsonLostCutSceneEnded";

        #endregion

        #region Dialog

        static readonly Dialog foundDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z110);

        static readonly Dialog ringRewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z536);
     
        #endregion

        public GrandsonAddScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            grandmother = Party.Singleton.CurrentMap.GetNPC("OldLady");

            Party.Singleton.ModifyNPC(
                            "healers_village_npchouse_se",
                            "Grandson",
                            Party.Singleton.Leader.TilePosition,
                            ModAction.Add,
                            true);

            grandson = Party.Singleton.CurrentMap.GetNPC("Grandson");
            grandson.IdleOnly = false;
            grandson.MoveSpeed = 2.00f;
            getToPoint = new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y-1);

            Steps = Utility.GetPathTo(
                grandson.TilePosition,
                getToPoint);

            destinationPoint = Utility.GetMapPositionFromDirection(grandson.TilePosition, Steps[0]);
            grandson.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            if (arrivedDestination1 == false)
            {
                arrivedDestination1 = MoveNPC(gameTime);
            }
            else
            {
                grandson.FaceDirection(Direction.South);
                grandson.IdleOnly = false;
               
                if (Party.Singleton.Leader.Direction == Direction.West)
                {
                    grandmother.FaceDirection(Direction.East);
                }
                else if (Party.Singleton.Leader.Direction == Direction.East)
                {
                    grandmother.FaceDirection(Direction.West);
                }
                else if (Party.Singleton.Leader.Direction == Direction.South)
                {
                    grandmother.FaceDirection(Direction.North);
                }

                foundDialog.DialogCompleteEvent += reward;
                ScreenManager.Singleton.AddScreen(new DialogScreen(foundDialog, DialogScreen.Location.TopLeft, "OldLady"));
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
            grandson.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (grandson.TilePosition == destinationPoint)
            {
                Steps.RemoveAt(0);

                if (Steps.Count == 0)
                {
                    grandson.IdleOnly = true;
                    return true;
                }
                else
                {
                    grandson.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

                    destinationPoint = Utility.GetMapPositionFromDirection(grandson.TilePosition, Steps[0]);
                }
            }

            return false;
        }

        void reward(object sender, PartyResponseEventArgs e)
        {
            foundDialog.DialogCompleteEvent -= reward;
            ScreenManager.Singleton.AddScreen(new DialogScreen(ringRewardDialog, DialogScreen.Location.TopLeft));
            Party.Singleton.AddAchievement(thisAchievement);
            state = SceneState.Complete;
        }
    }
}
