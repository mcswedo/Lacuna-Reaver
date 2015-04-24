using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class TagCutSceneScreenB : CutSceneScreen
    {
      


        public TagCutSceneScreenB()
            : base()
        {
        }

        public override void Initialize()
        {
            
            
            scenes.Add(new TagCutSceneB(this));
           
            base.Initialize();
        }

        public override bool CanPlay()
        {

            if (Party.Singleton.PartyAchievements.Contains("taginitiatedb"))
            {
                return true;
            }
            else return false;
        }
    }
}
