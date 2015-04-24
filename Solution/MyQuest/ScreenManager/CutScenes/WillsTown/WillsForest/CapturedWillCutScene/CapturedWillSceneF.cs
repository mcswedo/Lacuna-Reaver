using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CapturedWillSceneF : Scene
    {
        Portal portal;
        public static Point savePoint;
        public static Direction saveDirection;
        NPCMapCharacter cara;
        NPCMapCharacter will;

        public CapturedWillSceneF(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.South); 
            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            portal = new Portal { DestinationMap = "blind_mans_town", DestinationPosition = new Point(4, 9), Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.PortalToMap(portal);
            Party.Singleton.ModifyNPC(
                       "blind_mans_town",
                       Party.cara,
                       new Point(3, 9),
                       ModAction.Add,
                       false);
            Party.Singleton.ModifyNPC(
                       "blind_mans_town",
                       Party.will,
                       new Point(3, 10),
                       ModAction.Add,
                       false);
            will.FaceDirection(Direction.West);
            cara.FaceDirection(Direction.South);
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
 