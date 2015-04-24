
namespace MyQuest
{
    public class GrandsonLostCutSceneScreenA : CutSceneScreen
    {
        public GrandsonLostCutSceneScreenA()
            : base()
        { 
        }

        public override void Initialize()
        {
            scenes.Add(new GrandsonRemoveScene(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            //GW: Triggered by Controller
                return true;
        }
    }
}
