using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class LabyrinthBossCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedLabyrinthBoss";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }
        public LabyrinthBossCutSceneScreen()
            : base()
        {
            scenes.Add(new LabyrinthBossSceneA(this));
            scenes.Add(new LabyrinthBossSceneB(this));
            scenes.Add(new BossRoar(this));
            scenes.Add(new LabyrinthBossSceneC(this));
            scenes.Add(new BossRoar(this,true));
            scenes.Add(new LabyrinthBossSceneD(this));
            scenes.Add(new LabyrinthBossSceneE(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(achievement))
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
