using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PartyReactCutSceneScreen : CutSceneScreen
    {
        const string achievement = "partyReactCutScenePlayed";

        public PartyReactCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new PartyReactCutScene(this));
            scenes.Add(new ReturnPartyCutScene(this));
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
