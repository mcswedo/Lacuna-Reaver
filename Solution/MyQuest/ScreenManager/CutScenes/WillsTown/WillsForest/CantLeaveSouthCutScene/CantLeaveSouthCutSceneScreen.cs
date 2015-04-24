
namespace MyQuest
{
    public class CantLeaveSouthCutSceneScreen : CutSceneScreen
    {
        const string defeatBurtleAchievement = "defeatBurtle";

        public CantLeaveSouthCutSceneScreen()
            : base()
        {
            scenes.Add(new CantLeaveSouthSceneA(this));
            scenes.Add(new CantLeaveSouthSceneB(this));
            scenes.Add(new CantLeaveSouthSceneC(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(defeatBurtleAchievement) && Party.Singleton.PartyAchievements.Contains(WillsWanderCutSceneScreen.achievement))
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
