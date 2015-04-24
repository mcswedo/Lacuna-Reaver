using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class WillEndorsStudyCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedWillEndorStudy";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }


        public WillEndorsStudyCutSceneScreen()
            : base()
        {
            scenes.Add(new WillEndorsStudySceneA(this));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new WillEndorsStudySceneB(this));
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new WillEndorsStudySceneC(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new DoneScene(this));
            scenes.Add(new PortalToMagesInnBedScene(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(1.5)));
            scenes.Add(new GetOutOfBedScene(this));
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
