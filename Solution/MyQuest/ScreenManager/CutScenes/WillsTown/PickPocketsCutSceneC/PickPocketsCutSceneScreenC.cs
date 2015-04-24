using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PickPocketsCutSceneScreenC : CutSceneScreen
    {
        const string achievement = "pickPocketsCutSceneCPlayed";

        public PickPocketsCutSceneScreenC()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PickPocketsSceneC(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new GetOutOfBedScene(this));
            scenes.Add(new PickPocketsSceneC2(this));
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
