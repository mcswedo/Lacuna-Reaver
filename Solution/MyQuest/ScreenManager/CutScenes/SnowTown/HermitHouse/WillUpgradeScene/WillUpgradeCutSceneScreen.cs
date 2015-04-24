using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillUpgradeCutSceneScreen : CutSceneScreen
    {
        const string achievement = "WillUpgradeCutScenePlayed";

        public WillUpgradeCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new WillUpgradeSceneA(this));
            scenes.Add(new DelayScene(this, 1.25));
            scenes.Add(new WillUpgradeSceneB(this));

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
