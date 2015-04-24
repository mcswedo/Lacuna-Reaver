using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PoorGuysCutSceneScreenA : CutSceneScreen
    {
        public PoorGuysCutSceneScreenA()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PoorGuysCutSceneA(this));
            scenes.Add(new FadeInScene(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            //if (Party.Singleton.PartyAchievements.Contains(achievement))
            //{
            //    return false;
            //}

            
            return true;
        }
    }
}
