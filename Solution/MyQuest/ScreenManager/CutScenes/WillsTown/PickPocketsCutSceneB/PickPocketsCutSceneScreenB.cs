using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PickPocketsCutSceneScreenB : CutSceneScreen
    {
        const string achievement = "pickPocketsCutSceneBPlayed";

        public PickPocketsCutSceneScreenB()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PickPocketsSceneB1(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new PickPocketsSceneB2(this));
            scenes.Add(new PickPocketsSceneB3(this));
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
