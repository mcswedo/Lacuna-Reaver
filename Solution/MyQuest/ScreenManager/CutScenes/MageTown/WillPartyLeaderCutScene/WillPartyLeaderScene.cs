using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillPartyLeaderScene : Scene
    {
        Portal portal;

        public WillPartyLeaderScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.SetPartyLeaderWill();
            Party.Singleton.Leader.FaceDirection(Direction.South);
            portal = new Portal { DestinationMap = Maps.mageTownInn, DestinationPosition = new Point(6, 0), Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.PortalToMap(portal);

            Camera.Singleton.CenterOnTarget(
                Party.Singleton.Leader.WorldPosition,
                Party.Singleton.CurrentMap.DimensionsInPixels,
                ScreenManager.Singleton.ScreenResolution);

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
 