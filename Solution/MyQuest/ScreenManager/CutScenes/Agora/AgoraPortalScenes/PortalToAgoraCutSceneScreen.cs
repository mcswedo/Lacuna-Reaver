using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class PortalToAgoraCutSceneScreen : CutSceneScreen
    {
        Portal portal = new Portal { DestinationMap = "agora_portal_room3", DestinationPosition = new Point(1, 4), Position = new Point(4, 13) };
        const string requriedAchievement = AgoraRiftCutSceneScreen.myAchievement;
        public PortalToAgoraCutSceneScreen()
            : base()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalScene(this, portal));
            scenes.Add(new FadeInScene(this));
        }

        public override bool CanPlay()
        {
            if(Party.Singleton.PartyAchievements.Contains(requriedAchievement))
            {
                Party.Singleton.GameState.InAgora = true;
                return true;
            }

            else 
            {
                return false; 
            }
        }
    }
}
