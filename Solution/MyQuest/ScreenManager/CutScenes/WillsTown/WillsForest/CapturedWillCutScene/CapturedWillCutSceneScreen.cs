
namespace MyQuest
{
    public class CapturedWillCutSceneScreen : CutSceneScreen
    {
        internal const string achievement = "capturedWill";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public CapturedWillCutSceneScreen()
            : base()
        {
            scenes.Add(new CapturedWillSceneA(this));
            scenes.Add(new CapturedWillSceneB(this));
            scenes.Add(new CapturedWillSceneC(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new CapturedWillSceneD(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new CapturedWillSceneE(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new CapturedWillSceneF(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new CapturedWillSceneG(this));
            scenes.Add(new CapturedWillSceneH(this));
            scenes.Add(new CapturedWillSceneI(this));
            scenes.Add(new CapturedWillSceneJ(this));
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(WillsBurtleInitiateSceneC.achievement) && !Party.Singleton.PartyAchievements.Contains(achievement))
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
