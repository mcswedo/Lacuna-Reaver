using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToKeepf5Scene : Scene
    {
        Portal portal;

        public PortalToKeepf5Scene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);
            portal = new Portal { DestinationMap = "keepf5", DestinationPosition = new Point(5, 5), Position = Point.Zero };
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
 