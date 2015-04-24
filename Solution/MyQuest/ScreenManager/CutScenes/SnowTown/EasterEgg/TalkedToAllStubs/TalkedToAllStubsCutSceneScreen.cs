
namespace MyQuest
{
    public class TalkedToAllStubsCutSceneScreen : CutSceneScreen
    {
        internal const string playedAchievement = "TalkedToAllStubsPlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }
        public TalkedToAllStubsCutSceneScreen()
            : base()
        {
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new TalkedToAllStubsSceneA(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new TalkedToAllStubsSceneB(this));
            //scenes.Add(new DelayScene(this, 1));
            //scenes.Add(new InitiateEasterEggSceneB(this));
        }
  
        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(playedAchievement))
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
