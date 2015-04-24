using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class PortalCutSceneScreen : CutSceneScreen
    {

        public PortalCutSceneScreen(Portal portal)
            : base()
        {
            scenes.Add(new FadeOutScene(this));    
            scenes.Add(new PortalScene(this, portal));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new TryTriggerNewCutScene(this));
        }

        public override bool CanPlay()
        {
           
            return true;

        }
    }
}
