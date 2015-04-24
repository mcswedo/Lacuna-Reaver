using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PoorGuysCutSceneScreenB : CutSceneScreen
    {
        const string achievement = "poorGuysCutScenePlayed";

        public PoorGuysCutSceneScreenB()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PoorGuysCutSceneB(this));
            scenes.Add(new FadeInScene(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(achievement))
            {
                return false;
            }

            Party.Singleton.AddAchievement(achievement);
            return true;
        }
    }
}
