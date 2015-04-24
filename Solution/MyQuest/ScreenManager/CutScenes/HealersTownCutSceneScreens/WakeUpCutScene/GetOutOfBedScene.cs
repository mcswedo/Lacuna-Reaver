using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class GetOutOfBedScene : Scene
    {
        Point destinationPoint;
        Direction direction;
        int x;
        public GetOutOfBedScene(Screen screen, Direction direction = Direction.West)
            : base(screen)
        {
            this.direction = direction;
            if (direction == Direction.West)
                x = -1;
            else
            {
                x = 1; 
            }
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            if (Party.Singleton.PartyAchievements.Contains(MagesBlacksmithsController.myAchievement) && !Party.Singleton.PartyAchievements.Contains(MagesBlacksmithsController.nextDayAchievement))
            {
                Party.Singleton.AddAchievement(MagesBlacksmithsController.nextDayAchievement);
                Party.Singleton.SaveGameState(Party.saveFileName);
            }
            if (Party.Singleton.PartyAchievements.Contains(FarmersController.myAchievement)) //farmers side quest
            {

                if (!Party.Singleton.PartyAchievements.Contains(FarmersController.grownAchievement))
                {
                    Party.Singleton.ModifyNPC(
                       Maps.healersVillage,
                       "MysteriaPlant",
                       new Point(4, 17),
                       Direction.South,
                       true,
                       ModAction.Add,
                       true);

                    Party.Singleton.ModifyNPC(
                     Maps.healersVillage,
                     "Farmer",
                     Point.Zero,
                     Direction.South,
                     true,
                     ModAction.Remove,
                     true);

                    Party.Singleton.ModifyNPC(
                     Maps.healersVillage,
                     "Farmer",
                     new Point(9, 15),
                     Direction.South,
                     true,
                     ModAction.Add,
                     true);
                }

                Party.Singleton.AddAchievement(FarmersController.grownAchievement);
            } 
            
            destinationPoint = Party.Singleton.Leader.TilePosition;
            destinationPoint.X += x;
            Party.Singleton.Leader.SetAutoMovement(direction, Party.Singleton.CurrentMap);
            Party.Singleton.Leader.Velocity *= 0.50f;
            Party.Singleton.Leader.FaceDirection(Direction.South);
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.Leader.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (Party.Singleton.Leader.TilePosition == destinationPoint)
            {
                state = SceneState.Complete;
            }

        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
 