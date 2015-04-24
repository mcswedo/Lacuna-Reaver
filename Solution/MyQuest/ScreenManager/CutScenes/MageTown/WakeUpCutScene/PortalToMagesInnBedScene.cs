using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToMagesInnBedScene : Scene
    {
        Portal portal;

        public PortalToMagesInnBedScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.South);
            portal = new Portal { DestinationMap = "mage_town_inn_f2", DestinationPosition = new Point(3, 2), Position = Point.Zero };
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
 