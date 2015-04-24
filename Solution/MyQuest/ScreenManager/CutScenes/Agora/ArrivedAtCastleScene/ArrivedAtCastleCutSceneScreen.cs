using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class ArrivedAtCastleCutSceneScreen : CutSceneScreen
    {
        public const string achievement = "playedArrivedAtCastleScene";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public ArrivedAtCastleCutSceneScreen()
            : base()
        {
            scenes.Add(new ArrivedAtCastleSceneA(this));
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
