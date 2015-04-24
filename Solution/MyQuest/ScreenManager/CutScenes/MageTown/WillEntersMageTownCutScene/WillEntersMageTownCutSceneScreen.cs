
namespace MyQuest
{
    public class WillEntersMageTownCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "WillEntersMageTownPlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }
        public WillEntersMageTownCutSceneScreen()
            : base()
        {
            scenes.Add(new WillEntersMageTownSceneA(this));
            scenes.Add(new WillEntersMageTownSceneB(this));
        }
  
        public override bool CanPlay()
        {

            if (!Party.Singleton.PartyAchievements.Contains(playedAchievement) && Party.Singleton.PartyAchievements.Contains(WillsBurtleInitiateSceneC.achievement))
            {          
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
