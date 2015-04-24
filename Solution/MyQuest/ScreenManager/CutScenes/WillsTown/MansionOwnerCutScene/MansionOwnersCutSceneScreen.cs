
namespace MyQuest
{
    public class MansionOwnersCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "mansionOwnerCutScene";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }

        public MansionOwnersCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new MansionOwnersSceneA(this));
            scenes.Add(new MansionOwnersSceneB(this));
            scenes.Add(new MansionOwnersSceneC(this));
            scenes.Add(new MansionOwnersSceneD(this));
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(playedAchievement))
            {
                return true;
            }
       
            return false;
        }
    }
}
