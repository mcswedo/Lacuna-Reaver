
namespace MyQuest
{
    public class MayorsHouseCutSceneScreen : CutSceneScreen
    {
        public const string achievement = "mayorCutScenePlayed";

        public MayorsHouseCutSceneScreen()
            : base()
        {
         
        }
        public override void Initialize()
        {
            scenes.Add(new MayorsHouseSceneA(this));
            scenes.Add(new MayorsHouseSceneB(this));
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
