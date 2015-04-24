using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToLibraryScene : Scene
    {
        Portal portal;
        NPCMapCharacter cara;

        public PortalToLibraryScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            portal = new Portal { 
                DestinationMap = Maps.possessedLibrary5, 
                DestinationPosition = PortalToArlansStudyScene.savePoint1, 
                Position = Point.Zero 
            };
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
                   PortalToArlansStudyScene.savePoint2,
                   ModAction.Add,
                   false);

            if (PortalToArlansStudyScene.savePoint1 != new Point(37, 10))
            {
                Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   Party.will,
                   new Point(PortalToArlansStudyScene.savePoint2.X + 1, PortalToArlansStudyScene.savePoint2.Y),
                   ModAction.Add,
                   false);
            }
            else
            {
                Party.Singleton.ModifyNPC(
                  Party.Singleton.CurrentMap.Name,
                  Party.will,
                  new Point(PortalToArlansStudyScene.savePoint2.X - 1, PortalToArlansStudyScene.savePoint2.Y),
                  ModAction.Add,
                  false);
            }
            Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.East);
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
 