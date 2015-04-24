using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Battle1CutSceneScreen : CutSceneScreen
    {
        const string achievement = "Battle1CutScenePlayed";

        public Battle1CutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new Battle1SceneA(this));
            scenes.Add(new Battle1SceneB(this));
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
