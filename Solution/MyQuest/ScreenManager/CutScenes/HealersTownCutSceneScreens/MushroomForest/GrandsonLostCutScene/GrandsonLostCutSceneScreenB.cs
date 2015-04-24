
namespace MyQuest
{
    public class GrandsonLostCutSceneScreenB : CutSceneScreen
    {
        public GrandsonLostCutSceneScreenB()
            : base()
        {           
        }

        public override void Initialize()
        {
            scenes.Add(new GrandsonAddScene(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            //GW: Triggered by Controller
      
                return true; 
        }
    }
}
