using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class LibraryBossCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedLibraryBoss";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }
        public LibraryBossCutSceneScreen()
            : base()
        {
            scenes.Add(new LibraryBossSceneA(this));
            scenes.Add(new LibraryBossSceneB(this));
            scenes.Add(new LibraryBossSceneC(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new LibraryBossSceneD(this));       
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
