using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    /// <summary>
    /// Example implementation of the CutSceneScreen
    /// </summary>
    public class WakeUpSnowInnCutSceneScreen : CutSceneScreen
    {
        public WakeUpSnowInnCutSceneScreen()
            : base()
        {
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(2.5)));
            scenes.Add(new PortalToSnowInnBedScene(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(1.5)));
            scenes.Add(new GetOutOfBedScene(this, Direction.East));
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
