using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class BackToQueenCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedBackToQueen";

        const string defeatBossAchievement = ChepetawaSceneD.achievement;

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public BackToQueenCutSceneScreen()
            : base()
        {
            scenes.Add(new BackToQueenSceneA(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new BackToQueenSceneB(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new BackToQueenSceneC(this));
            scenes.Add(new BackToQueenSceneD(this));
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(defeatBossAchievement) && !Party.Singleton.PartyAchievements.Contains(achievement))
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
