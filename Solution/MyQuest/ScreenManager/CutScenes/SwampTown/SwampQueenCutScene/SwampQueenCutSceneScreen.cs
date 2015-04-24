
namespace MyQuest
{
    public class SwampQueenCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "SwampQueenPlayed";

        protected override void Complete()
        {
           Party.Singleton.AddAchievement(playedAchievement); 
        }
        public SwampQueenCutSceneScreen()
            : base()
        {
            scenes.Add(new SwampQueenSceneA(this));
            scenes.Add(new SwampQueenSceneB(this));
            scenes.Add(new SwampQueenSceneC(this));
        }
        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(playedAchievement))
            {
                return false;
            }
            else
            {
                return true;
            }
 
        }
    }
}
