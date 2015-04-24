using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToKeepf3CutSceneScreen : CutSceneScreen
    {
        public PortalToKeepf3CutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToKeepf3CutSceneA(this));
            scenes.Add(new FadeInScene(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
