
namespace MyQuest
{
    public class HealerJoinsCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "healerJoins";
        const string receivedSwordAchievement = HealersBlacksmithsController.receivedSwordAchievement;

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement); 
        }
        public HealerJoinsCutSceneScreen()
            : base()
        {
            scenes.Add(new HealerJoinsSceneA(this));
            scenes.Add(new HealerJoinsSceneB(this));
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(receivedSwordAchievement) && !Party.Singleton.PartyAchievements.Contains(playedAchievement))
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
