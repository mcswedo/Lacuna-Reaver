
namespace MyQuest
{
    public class LostboyCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "LostBoyPlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }
        public LostboyCutSceneScreen()
            : base()
        {
            scenes.Add(new LostboyScene(this));
            
        }
  
        public override bool CanPlay()
        {

            if (!Party.Singleton.PartyAchievements.Contains(playedAchievement))
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
