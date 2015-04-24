using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class FinalBattleCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedFinalBattle";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public FinalBattleCutSceneScreen()
            : base()
        {
            scenes.Add(new FinalBattleSceneA(this));
            scenes.Add(new FinalBattleSceneB(this));
            scenes.Add(new FinalBattleSceneC(this));
            scenes.Add(new FinalBattleSceneD(this));
            scenes.Add(new FinalBattleSceneE(this));
            scenes.Add(new FinalBattleSceneF(this));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new FinalBattleSceneG(this));       
            scenes.Add(new SlowFadeInWhiteScene(this));
            scenes.Add(new FinalBattleSceneH(this));
            //scenes.Add(new FinalBattleSceneI(this));      // Remove Mal'Ticar from screen
            scenes.Add(new FinalBattleSceneJ(this));      // Final dialog
            scenes.Add(new FadeOutScene(this, TimeSpan.FromSeconds(2)));
        }
       
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (scenes.Count == 0)
            {
                ScreenManager.Singleton.RemoveScreen(TileMapScreen.Instance);
                ScreenManager.Singleton.AddScreen(new EndingScreen());
            }
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
