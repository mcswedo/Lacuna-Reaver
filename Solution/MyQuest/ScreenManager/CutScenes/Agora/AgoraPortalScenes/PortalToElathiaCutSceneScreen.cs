using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class PortalToElathiaCutSceneScreen : CutSceneScreen
    {
        Portal portal = new Portal { DestinationMap = "agora_portal_room2", DestinationPosition = new Point(4, 12), Position = new Point(1, 3) };

        public PortalToElathiaCutSceneScreen()
            : base()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalScene(this, portal));
            scenes.Add(new FadeInScene(this));
        }

        public override bool CanPlay()
        {
            Party.Singleton.GameState.InAgora = false;
            return true;
        }
    }
}
