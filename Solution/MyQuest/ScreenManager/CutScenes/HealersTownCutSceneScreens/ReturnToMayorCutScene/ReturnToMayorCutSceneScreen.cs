
namespace MyQuest
{
    public class ReturnToMayorCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "ReturnToMayorPlayed";
        const string returnToVillageAchievement = "defeatBurtle";
        public ReturnToMayorCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new ReturnToMayorSceneA(this));
            scenes.Add(new ReturnToMayorSceneB(this));
            base.Initialize();        
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(playedAchievement) || !Party.Singleton.PartyAchievements.Contains(returnToVillageAchievement))
            {
                return false;
            }

            Party.Singleton.AddAchievement(playedAchievement);
            return true;
        }
    }
}
