using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Keepf1CutSceneScreen : CutSceneScreen
    {
        const string achievement = "keepf1CutScenePlayed";
        internal const string secondBattle = "secondMonsterDefeated";

        public Keepf1CutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            Party.Singleton.AddAchievement(secondBattle);
            scenes.Add(new Keepf1CutSceneA(this));
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
