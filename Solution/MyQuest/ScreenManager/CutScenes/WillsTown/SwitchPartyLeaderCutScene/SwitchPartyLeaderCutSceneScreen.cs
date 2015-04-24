
using System;
namespace MyQuest
{
    public class SwitchPartyLeaderCutSceneScreen : CutSceneScreen
    {
        public SwitchPartyLeaderCutSceneScreen()
            : base()
        {
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(2.5)));
            scenes.Add(new SwitchPartyLeaderScene(this));
            scenes.Add(new FadeInScene(this, TimeSpan.FromSeconds(1.5)));
            scenes.Add(new CaraTalksScene(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new CaraPortalScene(this));
            scenes.Add(new FadeInScene(this));
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
