using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class RoyalFamilyCutSceneScreen : CutSceneScreen
    {
        const string achievement = "RoyalFamilyCutScenePlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public RoyalFamilyCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToThroneRoomScene(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(0.15)));
            scenes.Add(new RoyalFamilyScene(this));
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(0.15)));
            scenes.Add(new PortalToMansionScene(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new CaraScene(this));
            scenes.Add(new CaraRejoinScene(this));
   
            base.Initialize();
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
