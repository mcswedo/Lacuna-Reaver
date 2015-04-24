using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class HermitHouseAttackedCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedHermitHouseAttacked";
        Portal portal = new Portal { DestinationMap = "snow_town_blacksmith_interior", DestinationPosition = new Point(4, 6), Position = new Point(6, 7), DestinationDirection = Direction.North };
        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }
        public HermitHouseAttackedCutSceneScreen()
            : base()
        {
            scenes.Add(new HermitHouseAttackedSceneA(this));
            scenes.Add(new HermitHouseAttackedSceneB(this));
            scenes.Add(new HermitHouseAttackedSceneC(this));
            scenes.Add(new PortalScene(this, portal)); 
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(achievement))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
