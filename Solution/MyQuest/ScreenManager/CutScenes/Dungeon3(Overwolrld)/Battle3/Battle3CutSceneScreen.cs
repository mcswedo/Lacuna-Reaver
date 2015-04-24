using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Battle3CutSceneScreen : CutSceneScreen
    {
        const string achievement = "Battle3CutScenePlayed";

        public Battle3CutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new Battle3SceneA(this));
            scenes.Add(new Battle3SceneB(this));
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
