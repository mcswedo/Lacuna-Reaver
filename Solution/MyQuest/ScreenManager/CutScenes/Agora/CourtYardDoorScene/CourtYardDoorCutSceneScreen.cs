using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class CourtYardDoorCutSceneScreen : CutSceneScreen
    {
        public const string achievement = "courtYardDoorOpen";

        Portal portal;

        Portal portalLeft = new Portal { DestinationMap = "agora_castle_room1_ground", DestinationPosition = new Point(10, 19), Position = new Point(11, 5) };

        Portal portalRight = new Portal { DestinationMap = "agora_castle_room1_ground", DestinationPosition = new Point(11, 19), Position = new Point(12, 5) };
    
        protected override void Complete()
        {
        }

        public CourtYardDoorCutSceneScreen()
            : base()
        {
            if (Party.Singleton.Leader.TilePosition.X == 11)
            {
                portal = portalLeft;
            }

            else
            {
                portal = portalRight;
            }

            if (!Party.Singleton.PartyAchievements.Contains(achievement))
            {
                scenes.Add(new CourtYardDoorSceneA(this));
            }
            else
            {
                scenes.Add(new FadeOutScene(this));
  
                scenes.Add(new PortalScene(this, portal));

                scenes.Add(new FadeInScene(this));
            }
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
