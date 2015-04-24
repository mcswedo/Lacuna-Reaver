using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class RoofTopDoorCutSceneScreen : CutSceneScreen
    {
        public const string achievement = "roofTopDoorOpen";

        Portal portal;

        Portal portalLeft = new Portal { DestinationMap = "agora_castle_rooftop_throne_room_transition", DestinationPosition = new Point(1, 13), Position = new Point(20, 12) };

        Portal portalRight = new Portal { DestinationMap = "agora_castle_rooftop_throne_room_transition", DestinationPosition = new Point(2, 13), Position = new Point(21, 12) };
    
        protected override void Complete()
        {
        }

        public RoofTopDoorCutSceneScreen()
            : base()
        {
            if (Party.Singleton.Leader.TilePosition.X == 20)
            {
                portal = portalLeft;
            }


            else
            {
                portal = portalRight;
            }

            if (!Party.Singleton.PartyAchievements.Contains(achievement))
            {
                scenes.Add(new RoofTopDoorSceneA(this));
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
