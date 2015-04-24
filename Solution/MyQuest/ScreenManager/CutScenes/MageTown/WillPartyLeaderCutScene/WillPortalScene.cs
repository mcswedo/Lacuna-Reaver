using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    public class WillPortalScene : Scene
    {
        Portal portal;

        public WillPortalScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.RemoveAllFightingCharacters();
            Party.Singleton.AddFightingCharacter(Will.Instance);

            portal = new Portal { DestinationMap = Maps.mageTownNight, DestinationPosition = new Point(24, 31), Position = Point.Zero };

            Party.Singleton.PortalToMap(portal);
            Debug.Assert(Party.Singleton.CurrentMap.Name.Equals(Maps.mageTownNight));

            Camera.Singleton.CenterOnTarget(
                 Party.Singleton.Leader.WorldPosition,
                 Party.Singleton.CurrentMap.DimensionsInPixels,
                 ScreenManager.Singleton.ScreenResolution);
        }

        public override void Update(GameTime gameTime)
        {
            state = SceneState.Complete;

            base.Update(gameTime);
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
 