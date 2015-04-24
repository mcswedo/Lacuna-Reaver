using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class TagCutSceneScreenE : CutSceneScreen
    {
      


        public TagCutSceneScreenE()
            : base()
        {
        }

        public override void Initialize()
        {
            
            scenes.Add(new TagCutSceneE(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains("taginitiatede"))
            {
                return true;
            }
            else return false;
        }
    }
}
