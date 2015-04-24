using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class AgoraRiftCutSceneScreen : CutSceneScreen
    {
        public const string myAchievement = "playedAgoraRift";
        const string requriedAchievement = EndorsController.achievement;

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(myAchievement);
        }

        public AgoraRiftCutSceneScreen()
            : base()
        {
            scenes.Add(new AgoraRiftSceneA(this));
            scenes.Add(new AgoraRiftSceneB(this));
            scenes.Add(new AgoraRiftSceneC(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(myAchievement) && Party.Singleton.PartyAchievements.Contains(requriedAchievement))
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
