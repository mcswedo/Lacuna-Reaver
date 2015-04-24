using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToMansionScene : Scene
    {
        Portal portal;
        NPCMapCharacter cara;

        public PortalToMansionScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            portal = new Portal { DestinationMap = "blind_mans_town", DestinationPosition = PortalToThroneRoomScene.savePoint1, Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.Leader.FaceDirection(Direction.South);
            Party.Singleton.PortalToMap(portal);
            
            Camera.Singleton.CenterOnTarget(            
                Party.Singleton.Leader.WorldPosition,
                Party.Singleton.CurrentMap.DimensionsInPixels,
                ScreenManager.Singleton.ScreenResolution);

            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);

            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   Party.cara,
                   PortalToThroneRoomScene.savePoint2,
                   ModAction.Add,
                   false);

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
 