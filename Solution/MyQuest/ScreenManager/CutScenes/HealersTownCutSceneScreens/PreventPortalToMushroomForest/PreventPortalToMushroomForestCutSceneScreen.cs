
namespace MyQuest
{
    public class PreventPortalToMushroomForestCutSceneScreen : CutSceneScreen
    {
        public override void Initialize()
        {
            //scenes.Add(new HealersGateSceneA(this));
            scenes.Add(new PreventPortalToMushroomForestSceneA(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(HealersBattleCutSceneScreen.achievement) && Party.Singleton.PartyAchievements.Contains(HealersBlacksmithCutSceneScreen.achievement))
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
