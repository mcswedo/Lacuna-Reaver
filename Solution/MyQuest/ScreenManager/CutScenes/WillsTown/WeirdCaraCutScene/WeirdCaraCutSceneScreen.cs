using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class WeirdCaraCutSceneScreen : CutSceneScreen
    {
        public WeirdCaraCutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new CaraExitScene(this));
            scenes.Add(new WeirdDialogScene(this));
            scenes.Add(new WeirdCaraRejoinScene(this));
   
            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(Merchant2Controller.caraQuestCompleteAchievement))
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
