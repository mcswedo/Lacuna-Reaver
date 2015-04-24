using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// Finished

namespace MyQuest
{
    /// <summary>
    /// Serves as the base class simple menu screens. Provides
    /// basic logic and input handling.
    /// </summary>
    public abstract class MenuScreen : Screen
    {
        #region Fields


        /// <summary>
        /// List of menu options for this screen
        /// </summary>
        protected List<MenuOption> menuOptions = new List<MenuOption>();

        /// <summary>
        /// The currently selected menu option
        /// </summary>
        protected int selectedOption;

        #endregion

        #region Methods

        public override void Initialize() { }

        /// <summary>
        /// Handles only up/down and select input logic.
        /// </summary>
        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsMenuUp())
            {
                if (--selectedOption < 0)
                {
                    selectedOption = menuOptions.Count - 1;

                    SoundSystem.Play(AudioCues.menuMove);
                }
                else 
                {
                    SoundSystem.Play(AudioCues.menuMove);
                } 
                    
            }
            else if (InputState.IsMenuDown())
            {
                if (++selectedOption >= menuOptions.Count)
                {
                    selectedOption = 0;

                    SoundSystem.Play(AudioCues.menuMove);
                }
                else
                {
                    SoundSystem.Play(AudioCues.menuMove);
                }
            }
            else
            {
                if (InputState.IsMenuSelect())
                {
                    menuOptions[selectedOption].OnSelectEntry();
                    SoundSystem.Play(AudioCues.menuConfirm);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <remarks>This method should not need to be overriden as it handles all required the logic</remarks>
        public override void Update(GameTime gameTime, bool coveredByOtherScreen, bool otherScreenHasFocus)
        {
            base.Update(gameTime, coveredByOtherScreen, otherScreenHasFocus);

            if (WillHandleUserInput)
            {
                for (int i = 0; i < menuOptions.Count; ++i)
                {
                    bool isSelected = (i == selectedOption);
                    menuOptions[i].Update(gameTime, isSelected);
                }
            }
        }

        //public override void Draw(GameTime gameTime)
        //{
        //}


        #endregion
    }
}
