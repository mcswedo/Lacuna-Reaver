using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToThroneRoomScene : Scene
    {
        Portal portal;
        public static Point savePoint1;
        public static Point savePoint2;
        public static Direction saveDirection;

        public PortalToThroneRoomScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            savePoint1 = Party.Singleton.Leader.TilePosition;
            savePoint2 = new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y + 1);
            saveDirection = Party.Singleton.Leader.Direction;
            Party.Singleton.Leader.FaceDirection(Direction.North);
            portal = new Portal { DestinationMap = Maps.throneRoomFlashBack, DestinationPosition = new Point(6, 19), Position = Point.Zero };

            MusicSystem.Play(AudioCues.flashbackCue);
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
 