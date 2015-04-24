
namespace MyQuest
{
    public class HealersBlacksmithCutSceneScreen : CutSceneScreen
    {
        internal const string achievement = "playedHealersBlacksmithCutScene";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public HealersBlacksmithCutSceneScreen()
            : base()
        {
            scenes.Add(new HealersBlacksmithSceneA(this));
        }

        public override bool CanPlay()
        {
            return true; 
        }
    }
}
