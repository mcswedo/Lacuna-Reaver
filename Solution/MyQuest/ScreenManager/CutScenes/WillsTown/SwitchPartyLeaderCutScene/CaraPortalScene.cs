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
    public class CaraPortalScene : Scene
    {
        Portal portal;

        public CaraPortalScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.RemoveFightingCharacter(Nathan.Instance);

            if (Party.Singleton.PartyAchievements.Contains(CapturedWillCutSceneScreen.achievement))
            {
                Party.Singleton.RemoveFightingCharacter(Will.Instance);
            }

            portal = new Portal { DestinationMap = "blind_mans_town_night", DestinationPosition = new Point(43, 6), Position = Point.Zero };

            Party.Singleton.PortalToMap(portal);
            Debug.Assert(Party.Singleton.CurrentMap.Name.Equals(Maps.blindMansTownNight));

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
 