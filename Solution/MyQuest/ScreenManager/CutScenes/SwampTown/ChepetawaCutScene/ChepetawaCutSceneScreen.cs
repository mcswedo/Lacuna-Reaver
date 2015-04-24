
namespace MyQuest
{
    public class ChepetawaCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "chepetawaPlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement); 
        }

        public ChepetawaCutSceneScreen()
            : base()
        {
            scenes.Add(new ChepetawaSceneA(this));
            scenes.Add(new ChepetawaSceneB(this));
            scenes.Add(new ChepetawaSceneC(this));
            scenes.Add(new ChepetawaSceneD(this));
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
