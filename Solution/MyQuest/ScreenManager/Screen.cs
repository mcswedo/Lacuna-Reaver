using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// Finished

namespace MyQuest
{
    /// <summary>
    /// Enum describes the screen transition state.
    /// </summary>
    public enum ScreenState
    {
        TransitionOn,
        FullyOn,
        TransitionOff,
        Hidden,
    }

    /// <summary>
    /// So we don't have to remember which value 
    /// represents which transition direction
    /// </summary>
    public enum TransitionDirection
    {
        On = -1,
        Off = 1
    }

    /// <summary>
    /// A screen is a single layer that has update and draw logic, and which
    /// can be combined with other layers to build up a complex menu system.
    /// For instance the main menu, the options menu, the "are you sure you
    /// want to quit" message box, and the main game itself are all implemented
    /// as screens.
    /// </summary>
    public abstract class Screen
    {
        protected const string backgroundTextureFolder = GameLoop.textureFolder + "Backgrounds/";
        internal const string interfaceTextureFolder = GameLoop.textureFolder + "Interface/";
        internal const string portaitTextureFolder = GameLoop.textureFolder + "Portraits/";

        internal event EventHandler<EventArgs> ExitScreenEvent;

        /// <summary>
        /// Gets the manager that this screen belongs to.
        /// </summary>
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            internal set { screenManager = value; }
        }

        ScreenManager screenManager;


        protected void InvokeExitScreenHandlers()
        {
            if (ExitScreenEvent != null)
            {
                ExitScreenEvent(this, EventArgs.Empty);
            }
        }

        #region Transition Fields


        /// <summary>
        /// Indicates how long the screen takes to
        /// transition on when it is activated.
        /// </summary>
        public TimeSpan TransitionOnTime
        {
            get { return transitionOnTime; }
            protected set { transitionOnTime = value; }
        }

        TimeSpan transitionOnTime = TimeSpan.FromSeconds(0.00001);


        /// <summary>
        /// Indicates how long the screen takes to
        /// transition off when it is deactivated.
        /// </summary>
        public TimeSpan TransitionOffTime
        {
            get { return transitionOffTime; }
            protected set { transitionOffTime = value; }
        }

        TimeSpan transitionOffTime = TimeSpan.FromSeconds(0.00001);


        /// <summary>
        /// Gets the current position of the screen transition, ranging
        /// from zero (fully active, no transition) to one (transitioned
        /// fully off to nothing).
        /// </summary>
        public float TransitionPosition
        {
            get { return transitionPosition; }
            protected set { transitionPosition = value; }
        }

        float transitionPosition = 1;


        /// <summary>
        /// Gets the current alpha of the screen transition, ranging
        /// from 1 (fully active, no transition) to 0 (transitioned
        /// fully off to nothing).
        /// </summary>
        public float TransitionAlpha
        {
            get { return 1f - TransitionPosition; }
        }


        #endregion

        #region State and Status


        /// <summary>
        /// Normally when one screen is brought up over the top of another,
        /// the first screen will transition off to make room for the new
        /// one. This property indicates whether the screen is only a small
        /// popup, in which case screens underneath it do not need to bother
        /// transitioning off.
        /// </summary>
        public bool IsPopup
        {
            get { return isPopup; }
            protected set { isPopup = value; }
        }

        bool isPopup = false;


        /// <summary>
        /// Gets the current screen transition state.
        /// </summary>
        public ScreenState ScreenState
        {
            get { return screenState; }
            protected set { screenState = value; }
        }

        ScreenState screenState = ScreenState.TransitionOn;


        /// <summary>
        /// There are two possible reasons why a screen might be transitioning
        /// off. It could be temporarily going away to make room for another
        /// screen that is on top of it, or it could be going away for good.
        /// This property indicates whether the screen is exiting for real:
        /// if set, the screen will automatically remove itself as soon as the
        /// transition finishes.
        /// </summary>
        public bool IsExiting
        {
            get { return isExiting; }
            protected internal set { isExiting = value; }
        }

        bool isExiting = false;


        /// <summary>
        /// Checks whether this screen can respond to user input.
        /// </summary>
        public bool WillHandleUserInput
        {
            get
            {
                return !otherScreenHasFocus &&
                       (screenState == ScreenState.TransitionOn ||
                        screenState == ScreenState.FullyOn);
            }
        }


        bool otherScreenHasFocus;

        protected bool OtherScreenHasFocus
        {
            get { return otherScreenHasFocus; }
        }


        #endregion

        #region Methods


        public virtual void Initialize() { }

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        public virtual void LoadContent(ContentManager content) { }
     

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            this.otherScreenHasFocus = otherScreenHasFocus;

            if (isExiting)
            {
                // If the screen is going away to die, it should transition off.
                screenState = ScreenState.TransitionOff;

                if (!TransitionOff(gameTime))
                {
                    // When the transition finishes, remove the screen.
                    ScreenManager.RemoveScreen(this);
                    if (ExitScreenEvent != null)
                    {
                        ExitScreenEvent(this, EventArgs.Empty);
                    }
                }
            }
            else if (coveredByOtherScreen)
            {
                // If the screen is covered by another, it should transition off.
                if (TransitionOff(gameTime))
                {
                    // Still busy transitioning.
                    screenState = ScreenState.TransitionOff;
                }
                else
                {
                    // Transition finished!
                    screenState = ScreenState.Hidden;
                }
            }
            else
            {
                // Otherwise the screen should transition on and become active.
                if (TransitionOn(gameTime))
                {
                    // Still busy transitioning.
                    screenState = ScreenState.TransitionOn;
                }
                else
                {
                    // Transition finished!
                    screenState = ScreenState.FullyOn;
                }
            }
        }


        /// <summary>
        /// Allows the screen to handle user input. Unlike Update, this method
        /// is only called when the screen is active, and not when some other
        /// screen has taken the focus.
        /// </summary>
        public virtual void HandleInput(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }


        /// <summary>
        /// Tells the screen to go away. Unlike ScreenManager.RemoveScreen, which
        /// instantly kills the screen, this method respects the transition timings
        /// and will give the screen a chance to gradually transition off.
        /// </summary>
        public void ExitAfterTransition()
        {
            // If the screen has a zero transition time, remove it immediately.
            if (TransitionOffTime <= TimeSpan.Zero)
            {
                /// Keep in mind that removing a screen during update doesn't actually
                /// take effect until the update is complete. We add this bit here
                /// to inform the ScreenManager that we're no longer active
                screenState = ScreenState.TransitionOff;

                ScreenManager.RemoveScreen(this);
            }
            else
            {
                // Otherwise flag that it should transition off and then exit.
                isExiting = true;
            }
        }


        #endregion

        #region Transition Methods

         
        /// <summary>
        /// Handles On Transition effects. The default is to immediately transition 
        /// on. Derived classes can override this function for a custom effect
        /// </summary>
        /// <returns>False if the transition has ended</returns>
        protected virtual bool TransitionOn(GameTime gameTime)
        {
            return TransitionHelper(gameTime, transitionOnTime, TransitionDirection.On);
        }


        /// <summary>
        /// Handles Off Transition effects. The default is to immediately transition 
        /// off. Derived classes can override this function for a custom effect
        /// </summary>
        /// <returns>False if the transition has ended</returns>
        protected virtual bool TransitionOff(GameTime gameTime)
        {
            return TransitionHelper(gameTime, transitionOffTime, TransitionDirection.Off);
        }


        /// <summary>
        /// Helper function for transition effects. Advances the TransitionPosition
        /// in the specified direction by some fraction of the effect's duration.
        /// </summary>
        /// <param name="time">The duration of the transition effect</param>
        /// <param name="direction">The direction of the transition (On or Off)</param>
        /// <returns>False if the transition has ended</returns>
        protected bool TransitionHelper(GameTime gameTime, TimeSpan time, TransitionDirection direction)
        {
            // How much should we move by?
            float transitionDelta;

            if (time == TimeSpan.Zero)
            {
                transitionDelta = 1;
            }
            else
            {
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
                                          time.TotalMilliseconds);
            }

            // Update the transition position.
            transitionPosition += transitionDelta * (int)direction;

            // Did we reach the end of the transition?
            if ((((int)direction < 0) && (transitionPosition <= 0)) ||
                (((int)direction > 0) && (transitionPosition >= 1)))
            {
                transitionPosition = MathHelper.Clamp(transitionPosition, 0, 1);
                return false;
            }

            // Otherwise we are still busy transitioning.
            return true;
        }


        #endregion
    }
}
