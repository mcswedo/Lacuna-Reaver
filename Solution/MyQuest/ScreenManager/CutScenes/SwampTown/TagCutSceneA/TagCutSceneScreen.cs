﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class TagCutSceneScreen : CutSceneScreen
    {
      


        public TagCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            
            scenes.Add(new TagCutSceneA(this));
          
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains("taginitiated"))
            {
                return true;
            }
            else return false;
        }
    }
}
