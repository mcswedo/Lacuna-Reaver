using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToWillsInnBedScene : Scene
    {
        Portal portal;

        public PortalToWillsInnBedScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            if (!Party.Singleton.Leader.Name.Equals(Party.nathan))
            {
                Party.Singleton.SetPartyLeaderNathan();
            }

            Party.Singleton.Leader.FaceDirection(Direction.South);
            portal = new Portal { DestinationMap = "blind_mans_town_inn_f2", DestinationPosition = new Point(8, 5), Position = Point.Zero };
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
 