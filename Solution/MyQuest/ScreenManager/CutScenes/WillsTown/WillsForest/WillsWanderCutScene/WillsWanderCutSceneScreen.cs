
namespace MyQuest
{
    public class WillsWanderCutSceneScreen : CutSceneScreen
    {
        public const string achievement = "willsWanderCutScene";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public WillsWanderCutSceneScreen()
            : base()
        {
            scenes.Add(new WillsWanderSceneA(this));
            scenes.Add(new WillsWanderSceneB(this));
            scenes.Add(new WillsWanderSceneC(this));
            scenes.Add(new WillsWanderSceneD(this));
            scenes.Add(new WillsWanderSceneE(this));
            scenes.Add(new WillsWanderSceneF(this));
            scenes.Add(new WillsWanderSceneG(this));
            scenes.Add(new WillsWanderSceneH(this));
            scenes.Add(new WillsWanderSceneI(this));
            scenes.Add(new WillsWanderSceneJ(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains("willsWanderCutScene"))
            {
                return true;
            }

            return false;
        }
    }
}
