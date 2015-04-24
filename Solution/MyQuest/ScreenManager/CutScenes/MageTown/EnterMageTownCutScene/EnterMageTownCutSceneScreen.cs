
namespace MyQuest
{
    public class EnterMageTownCutSceneScreen : CutSceneScreen
    {
        const string playedAchievement = "EnterMageTownPlayed";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);
        }

        public EnterMageTownCutSceneScreen()
            : base()
        {
            scenes.Add(new EnterMageTownSceneA(this));
            scenes.Add(new EnterMageTownSceneB(this));
            scenes.Add(new EnterMageTownSceneC(this));
            scenes.Add(new EnterMageTownSceneD(this));
            scenes.Add(new EnterMageTownSceneE(this));
            scenes.Add(new EnterMageTownSceneF(this));
            scenes.Add(new EnterMageTownSceneG(this));
            scenes.Add(new EnterMageTownSceneEnd(this));
        }
  
        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(playedAchievement))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
