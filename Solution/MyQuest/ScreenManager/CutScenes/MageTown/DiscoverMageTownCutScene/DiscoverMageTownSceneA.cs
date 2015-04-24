using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class DiscoverMageTownSceneA : Scene
    {
        public DiscoverMageTownSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Portal portal;

            if (Party.Singleton.PartyAchievements.Contains(WillsBlacksmithsController.receivedScytheAchievement))
            {
                portal = new Portal { DestinationMap = "mage_town_entrance_after", DestinationPosition = new Point(5, 18), Position = Point.Zero };
            }
            else
            {
                portal = new Portal { DestinationMap = "mage_town_entrance_before", DestinationPosition = new Point(5, 18), Position = Point.Zero };
            }

            Party.Singleton.PortalToMap(portal);
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
