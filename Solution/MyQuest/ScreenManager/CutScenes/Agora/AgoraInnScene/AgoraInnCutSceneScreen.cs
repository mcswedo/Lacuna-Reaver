using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class AgoraInnCutSceneScreen : CutSceneScreen
    {
        const string thisAchievement = "playedAgoraInn";

        public AgoraInnCutSceneScreen()
            : base()
        {
            //scenes.Add(new AgoraRiftSceneA(this));
            //scenes.Add(new AgoraRiftSceneB(this));
            scenes.Add(new AgoraInnSceneA(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(thisAchievement))
            {
                Party.Singleton.AddAchievement(thisAchievement);
                return true;
            }

            return false;

        }
    }
}
