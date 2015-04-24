
namespace MyQuest
{
    public class MansionGuardsCutSceneScreen : CutSceneScreen
    {
        const string receivedScytheAchievement = "receivedScythe";
        const string defeatBurtleAchievement = "defeatBurtle";

        public MansionGuardsCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new MansionGuardsSceneA(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(receivedScytheAchievement) && Party.Singleton.PartyAchievements.Contains(defeatBurtleAchievement))
            {
                return true;
            }

            return false;
        }
    }
}
