
using System;
namespace MyQuest
{
    public class WillPartyLeaderCutSceneScreen : CutSceneScreen
    {
        public WillPartyLeaderCutSceneScreen()
            : base()
        {
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(2.5)));
            scenes.Add(new WillPartyLeaderScene(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(1.5)));
            scenes.Add(new WillTalksScene(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new WillPortalScene(this));
            scenes.Add(new FadeInScene(this));
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
