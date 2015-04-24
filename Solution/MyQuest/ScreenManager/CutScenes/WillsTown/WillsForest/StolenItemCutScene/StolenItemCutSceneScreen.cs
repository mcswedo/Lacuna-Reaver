
namespace MyQuest
{
    public class StolenItemCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "stolenItemCutScene";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }

        public StolenItemCutSceneScreen()
            : base()
        {
            scenes.Add(new StolenItemSceneA(this));
            scenes.Add(new StolenItemSceneB(this));
            scenes.Add(new StolenItemSceneC(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains("stolenItemCutScene"))
            {
                return true;
            }

            return false;
        }
    }
}
