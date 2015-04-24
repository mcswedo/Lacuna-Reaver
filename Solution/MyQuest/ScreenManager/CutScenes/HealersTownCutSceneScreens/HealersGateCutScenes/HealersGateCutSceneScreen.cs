
namespace MyQuest
{
    public class HealersGateCutSceneScreen : CutSceneScreen
    {
        const string achievement = HealersBattleSceneA.savedVillageAchievement;

        public HealersGateCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new HealersGateSceneA(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(achievement))
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
