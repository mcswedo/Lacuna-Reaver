using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    /*
     * I am modifying this class so that it displays nothing, accepts no user input,
     * and just clears itself after 3 seconds.
     */
    class VictoryDelayScreen : Screen
    {
        #region Fields

        TimeSpan timeToClose;
        List<FightingCharacter> npcCombatants;

        #endregion

        #region Initialization

        public VictoryDelayScreen(List<FightingCharacter> combatants)
        {
            npcCombatants = combatants;
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            IsPopup = true;

            timeToClose = TimeSpan.FromSeconds(1);
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            timeToClose -= gameTime.ElapsedGameTime;
            if (timeToClose < TimeSpan.Zero && ScreenState == ScreenState.FullyOn)
            {
                VictoryScreen victoryScreen = new VictoryScreen(npcCombatants);
                ScreenManager.AddScreen(victoryScreen);
                victoryScreen.PreInitialize();
                ExitAfterTransition();
            }
        }

        public override void Draw(GameTime gameTime)
        {
        }

        #endregion
    }
}
