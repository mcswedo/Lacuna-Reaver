using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class PortalToHermitHouse : Scene
    {
        Portal portal;
        public static Point savePoint;
        public static Direction saveDirection;
        NPCMapCharacter will;
        NPCMapCharacter cara;

        public PortalToHermitHouse(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Complete()
        {
            Party.Singleton.ModifyNPC(
             Maps.caveLabyrinthBoss,
             "MasterBlacksmith",
             Party.Singleton.Leader.TilePosition,
             ModAction.Remove,
             true);

            Party.Singleton.ModifyNPC(
                       Maps.snowTownBlacksmithInterior,
                       Party.cara,
                       new Point(5, 5),
                       Direction.North,
                       true,
                       ModAction.Add,
                       true);
            Party.Singleton.ModifyNPC(
                       Maps.snowTownBlacksmithInterior,
                       Party.will,
                       new Point(7, 5),
                       Direction.East,
                       true,
                       ModAction.Add,
                       true);

            Party.Singleton.ModifyNPC(
                      Maps.snowTownBlacksmithInterior,
                      "MasterBlacksmith",
                       new Point(6, 3),
                       Direction.South,
                       true,
                       ModAction.Add,
                       true);   
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);
            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);

            portal = new Portal { DestinationMap = "snow_town_blacksmith_interior", DestinationPosition = new Point(6, 5), Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.PortalToMap(portal);

            Complete();

            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(1, Color.Black, spriteBatch);
        }
    }
}
