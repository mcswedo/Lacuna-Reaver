using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class PortalToHermitHouseAgain : Scene
    {
        Portal portal;
    
        public PortalToHermitHouseAgain(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }
        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);
            portal = new Portal { DestinationMap = "snow_town_blacksmith_interior", DestinationPosition = new Point(6, 5), Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.PortalToMap(portal);

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
