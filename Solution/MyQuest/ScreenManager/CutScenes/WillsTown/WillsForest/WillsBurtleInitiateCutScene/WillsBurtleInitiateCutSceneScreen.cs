
namespace MyQuest
{
    public class WillsBurtleInitiateCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "WillsBurtleInitiate";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }

        public WillsBurtleInitiateCutSceneScreen()
            : base()
        {
            scenes.Add(new WillsBurtleInitiateSceneA(this));
            scenes.Add(new WillsBurtleInitiateSceneB(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new WillsBurtleInitiateSceneC(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(playedAchievement))
            {
                return true;
            }
     
                return false;

        }
    }
}
