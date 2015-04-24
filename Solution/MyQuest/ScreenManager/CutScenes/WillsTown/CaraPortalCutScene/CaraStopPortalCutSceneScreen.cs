
namespace MyQuest
{
    public class CaraStopPortalCutSceneScreen : CutSceneScreen
    {
        public CaraStopPortalCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new CaraStopPortalSceneA(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            return true;
        }
    }
}
