using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class InsideHermitHouseCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedInsideHermitHouse";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public InsideHermitHouseCutSceneScreen()
            : base()
        {
            scenes.Add(new InsideHermitHouseSceneA(this));
            scenes.Add(new InsideHermitHouseSceneB(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(achievement))
            {
                return true;
            }

            return false;

        }
    }
}
