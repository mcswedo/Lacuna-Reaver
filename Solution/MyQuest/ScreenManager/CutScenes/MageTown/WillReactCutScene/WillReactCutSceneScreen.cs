using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillReactCutSceneScreen : CutSceneScreen
    {
        const string achievement = "willReactCutScenePlayed";

        public WillReactCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new WillReactCutScene(this));
            scenes.Add(new ReturnWillCutScene(this));
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
