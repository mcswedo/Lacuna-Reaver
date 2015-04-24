
namespace MyQuest
{
    public class WillsBlindedCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "willsBlindedCutScene";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }

        public WillsBlindedCutSceneScreen()
            : base()
        {
            scenes.Add(new WillsBlindedSceneA(this));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new WillsBlindedSceneB(this));
            scenes.Add(new WillsBlindedSceneC(this));
            scenes.Add(new WillsBlindedSceneD(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains("willsBlindedCutScene"))
            {
                return true;
            }

            return false;
        }
    }
}
