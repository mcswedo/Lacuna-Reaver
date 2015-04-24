
namespace MyQuest
{
    public class CantLeaveEastCutSceneScreen : CutSceneScreen
    {
        const string defeatBurtleAchievement = "defeatBurtle";

        public CantLeaveEastCutSceneScreen()
            : base()
        {
            scenes.Add(new CantLeaveEastSceneA(this));
            scenes.Add(new CantLeaveEastSceneB(this));
            scenes.Add(new CantLeaveEastSceneC(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(defeatBurtleAchievement))
            {
                return true;
            }
     
                return false;

        }
    }
}
