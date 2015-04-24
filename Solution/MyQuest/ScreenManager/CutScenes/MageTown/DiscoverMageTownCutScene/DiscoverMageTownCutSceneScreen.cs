
namespace MyQuest
{
    public class DiscoverMageTownCutSceneScreen : CutSceneScreen
    {
        public DiscoverMageTownCutSceneScreen()
            : base()
        {
            scenes.Add(new DiscoverMageTownSceneA(this));
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
