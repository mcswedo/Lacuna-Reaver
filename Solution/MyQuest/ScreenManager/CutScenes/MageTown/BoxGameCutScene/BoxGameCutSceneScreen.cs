using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class BoxGameCutSceneScreen : CutSceneScreen
    {
        public const string achievement = "BoxGameCutScenePlayed";

        public BoxGameCutSceneScreen()
            : base()
        {
        }

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public override void Initialize()
        {
            scenes.Add(new BoxGameCutSceneA(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(achievement))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
