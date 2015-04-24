using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Keepf2CutSceneScreen : CutSceneScreen
    {
        const string achievement = "keepf2CutScenePlayed";

        public Keepf2CutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new Keepf2CutSceneA(this));
            scenes.Add(new Keepf2CutSceneB(this));

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
