using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class ArrivedAtAgoraCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedArrivedAtAgora";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public ArrivedAtAgoraCutSceneScreen()
            : base()
        {
            scenes.Add(new ArrivedAtAgoraSceneA(this));       
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
