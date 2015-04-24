using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PickPocketsCutSceneScreenA : CutSceneScreen
    {
        const string achievement = "pickPocketsCutSceneAPlayed";

        public PickPocketsCutSceneScreenA()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(.9)));
            scenes.Add(new PickPocketsSceneA1(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(.9)));
            scenes.Add(new PickPocketsSceneA2(this));
            scenes.Add(new PickPocketsSceneA3(this));
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
