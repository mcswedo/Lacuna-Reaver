using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class EndorsStudyCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedEndorStudy";

        const string defeatBossAchievement = LibraryBossSceneD.achievement;

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }


        public EndorsStudyCutSceneScreen()
            : base()
        {
            scenes.Add(new EndorsStudySceneA(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new EndorsStudySceneB(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new EndorsStudySceneC(this));
            scenes.Add(new EndorsStudySceneD(this));
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
