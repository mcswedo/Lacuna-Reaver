using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class EndCaraGamePlayCutSceneScreen : CutSceneScreen
    {
        public EndCaraGamePlayCutSceneScreen()
            : base()
        {
            scenes.Add(new EndCaraGamePlaySceneA(this));
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(2.5)));
            scenes.Add(new PortalToWillsInnBedScene(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(1.5)));
            scenes.Add(new GetOutOfBedScene(this));
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(Merchant2Controller.swordTradedAchievement))
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
