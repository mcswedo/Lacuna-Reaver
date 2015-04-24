using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    /// <summary>
    /// Example implementation of the CutSceneScreen
    /// </summary>
    public class WakeUpCutSceneScreen : CutSceneScreen
    {
        public WakeUpCutSceneScreen()
            : base()
        {
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(2.5)));
            scenes.Add(new PortalToBedScene(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(1.5)));
            scenes.Add(new GetOutOfBedScene(this));
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
