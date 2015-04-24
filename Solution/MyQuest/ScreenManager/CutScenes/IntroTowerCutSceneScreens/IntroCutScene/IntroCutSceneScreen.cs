
namespace MyQuest
{
    public class IntroCutSceneScreen : CutSceneScreen
    {
        const string achievement = "introCutScenePlayed";

        public IntroCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new IntroCutSceneA(this));
            scenes.Add(new IntroCutSceneB(this));
            scenes.Add(new IntroCutSceneC(this));
            scenes.Add(new IntroCutSceneD(this));
            scenes.Add(new IntroCutSceneE(this));
            scenes.Add(new IntroCutSceneF(this));
            //scenes.Add(new IntroCutSceneG(this));
            scenes.Add(new IntroCutSceneH(this));
            base.Initialize();        
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(achievement))
            {
                return false;
            }

            Party.Singleton.AddAchievement(achievement);
            return true;
        }
    }
}
