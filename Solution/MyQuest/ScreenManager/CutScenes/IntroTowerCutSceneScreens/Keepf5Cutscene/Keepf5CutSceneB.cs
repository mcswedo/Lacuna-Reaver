using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    class Keepf5CutSceneB : Scene
    {
        Point destinationPoint1;
        Point destinationPoint2;

        List<Direction> steps1;
        List<Direction> steps2;

        NPCMapCharacter friend1;
        NPCMapCharacter friend2;

        bool isFriend1AtDestination = false; 
        bool isFriend2AtDestination = false;

        Dialog dialog = new Dialog(DialogPrompt.NeedsClose, false, Strings.Z562);

        public Keepf5CutSceneB(Screen screen)
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
               "Friend1",
               new Point(5, 5),
               ModAction.Add,
               false);

            Party.Singleton.ModifyNPC(
               Party.Singleton.CurrentMap.Name,
               "Friend2",
               new Point(5, 5),
               ModAction.Add,
               false);

            Party.Singleton.Leader.FaceDirection(Direction.North);

            friend1 = Party.Singleton.CurrentMap.GetNPC("Friend1");
            friend2 = Party.Singleton.CurrentMap.GetNPC("Friend2");

            friend1.IdleOnly = false;
            friend2.IdleOnly = false;

            steps1 = Utility.GetPathTo(
                friend1.TilePosition,
                new Point(4, 5));

            steps2 = Utility.GetPathTo(
                friend2.TilePosition,
                new Point(6, 5));

            destinationPoint1 = Utility.GetMapPositionFromDirection(friend1.TilePosition, steps1[0]);
            destinationPoint2 = Utility.GetMapPositionFromDirection(friend2.TilePosition, steps2[0]);

            friend1.SetAutoMovement(steps1[0], Party.Singleton.CurrentMap);
            friend2.SetAutoMovement(steps2[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            if (isFriend1AtDestination == false)
            {
                isFriend1AtDestination = MoveNPC(gameTime, friend1, steps1, ref destinationPoint1);
            }

            if (isFriend2AtDestination == false)
            {
                isFriend2AtDestination = MoveNPC(gameTime, friend2, steps2, ref destinationPoint2);
            }

            if (isFriend1AtDestination == true && isFriend2AtDestination == true)
            {
                state = SceneState.Complete;

                friend1.FaceDirection(Direction.North);
                friend2.FaceDirection(Direction.North);

                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Sid"));
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
