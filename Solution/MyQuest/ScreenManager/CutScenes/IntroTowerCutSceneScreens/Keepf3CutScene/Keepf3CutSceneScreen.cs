using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Keepf3CutSceneScreen : CutSceneScreen
    {
        const string achievement = "keepf3CutScenePlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public Keepf3CutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new Keepf3CutSceneA(this));
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
