using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class HealingWellCutSceneScreen : CutSceneScreen
    {
        public HealingWellCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new HealingWellCutSceneA(this));

            base.Initialize();
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
