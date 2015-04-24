using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class HealersBattleCutSceneScreen : CutSceneScreen
    {
        internal const string achievement = "playedHealersBattleCutScene";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }
        public HealersBattleCutSceneScreen()
            : base()
        {            
            scenes.Add(new HealersBattleSceneA(this));
            scenes.Add(new HealersBattleSceneB(this));
            scenes.Add(new HealersBattleSceneC(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(achievement) && Party.Singleton.PartyAchievements.Contains(HealersBlacksmithsController.receivedSwordAchievement))
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
