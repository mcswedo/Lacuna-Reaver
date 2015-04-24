using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class ArlansStudyCutSceneScreen : CutSceneScreen
    {
        const string achievement = "StudyCutScenePlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }


        public ArlansStudyCutSceneScreen()
            : base()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToArlansStudyScene(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(0.15)));
            scenes.Add(new ArlansStudyScene(this));
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(0.15)));
            scenes.Add(new PortalToLibraryScene(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new BackFromArlansStudyScene(this));
            scenes.Add(new PartyRejoinScene(this));
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(achievement))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
