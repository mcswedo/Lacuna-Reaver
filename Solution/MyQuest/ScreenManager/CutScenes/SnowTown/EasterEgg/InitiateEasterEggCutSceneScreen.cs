
namespace MyQuest
{
    public class InitiateEasterEggCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "InitiateEasterEggPlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }
        public InitiateEasterEggCutSceneScreen()
            : base()
        {
            scenes.Add(new InitiateEasterEggSceneA(this));
            scenes.Add(new DelayScene(this, 1));
            scenes.Add(new InitiateEasterEggSceneB(this));
        }
  
        public override bool CanPlay()
        {

            if (!Party.Singleton.PartyAchievements.Contains(playedAchievement) && Party.Singleton.PartyAchievements.Contains(PeeperController.stubSpawned))
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
