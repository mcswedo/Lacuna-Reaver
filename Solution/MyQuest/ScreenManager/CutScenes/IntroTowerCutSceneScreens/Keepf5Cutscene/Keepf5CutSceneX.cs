using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Keepf5CutSceneX : Scene
    {
        Portal portal;

        public Keepf5CutSceneX(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            portal = new Portal { DestinationMap = "keepf5", DestinationPosition = new Point(6, 0), Position = Point.Zero };
            Party.Singleton.PortalToMap(portal);
        }

        void EndScene(object sender, EventArgs e)
        {
            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
 