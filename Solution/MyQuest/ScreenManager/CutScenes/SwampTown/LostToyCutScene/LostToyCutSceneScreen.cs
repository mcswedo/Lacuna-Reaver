using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class LostToyCutSceneScreen : CutSceneScreen
    {
        const string toyachievement = "lostToyCutScenePlayed";
        

        public LostToyCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            Party.Singleton.AddAchievement(toyachievement);
            scenes.Add(new LostToyCutSceneA(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(toyachievement))
            {
                return false;
            }

            Party.Singleton.AddAchievement(toyachievement);
            return true;
        }
    }
}
