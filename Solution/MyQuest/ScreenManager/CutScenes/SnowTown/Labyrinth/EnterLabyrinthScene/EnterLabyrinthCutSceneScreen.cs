using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class EnterLabyrinthCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedEnterLabyrinth";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }
        public EnterLabyrinthCutSceneScreen()
            : base()
        {
            scenes.Add(new EnterLabyrinthSceneA(this));
            scenes.Add(new EnterLabyrinthSceneB(this));
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
