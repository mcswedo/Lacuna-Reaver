using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class WillDiesCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedWillDies";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }

        public WillDiesCutSceneScreen()
            : base()
        {
            scenes.Add(new WillDiesSceneA(this));
            scenes.Add(new WillDiesSceneB(this));
            scenes.Add(new WillDiesSceneC(this));
            scenes.Add(new WillDiesSceneD(this));
            scenes.Add(new WillDiesSceneE(this));
            scenes.Add(new WillDiesSceneF(this));
            scenes.Add(new WillDiesSceneG(this));
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
