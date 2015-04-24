
namespace MyQuest
{
    public class WillPreventsSouthCutSceneScreen : CutSceneScreen
    {
        const string receivedScytheAchievement = "receivedScythe";
        const string defeatBurtleAchievement = "defeatBurtle";

        public WillPreventsSouthCutSceneScreen()
            : base()
        {
            scenes.Add(new WillPreventsSouthSceneA(this));
            scenes.Add(new WillPreventsSouthSceneB(this));
            scenes.Add(new WillPreventsSouthSceneC(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(receivedScytheAchievement) && Party.Singleton.PartyAchievements.Contains(defeatBurtleAchievement))
            {
                return true;
            }
     
                return false;

        }
    }
}
