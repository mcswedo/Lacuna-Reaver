using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalScene : Scene
    {
        Portal portal;

        public PortalScene(Screen screen, Portal portal)
            : base(screen)
        {
            this.portal = portal; 
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
        }
        public override void Update(GameTime gameTime)
        {
            //Party.Singleton.Leader.FaceDirection(PortalToRoyalHouseScene.saveDirection);
           
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
 