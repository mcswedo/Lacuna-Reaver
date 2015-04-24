using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

/// Needs "tap" detection. A mechanism for determining if the
/// user has just "tapped" the button or if the user has 
/// fully held down the button. This should be easily 
/// accomplished via some timing variables and possibly
/// an extra class

/// Needs commenting.

namespace MyQuest
{
    public static class InputState
    {
        #region Fields

        private static bool isPlayerIndexDetermined = false;
        private static PlayerIndex playerIndex;
        private static KeyboardState currentKeyboardState;
        private static GamePadState currentGamePadState;
        private static KeyboardState previousKeyboardState;
        private static GamePadState previousGamePadState;
       // private static bool gamePadWasConnected = false;

        #endregion

        #region Update

        /// <summary>
        /// Reads the latest state of the keyboard and gamepad.
        /// </summary>
        public static void Update()
        {
            if (!isPlayerIndexDetermined)
            {
                return;
            }
            previousKeyboardState = currentKeyboardState;
            previousGamePadState = currentGamePadState;
            currentKeyboardState = Keyboard.GetState(playerIndex);
            currentGamePadState = GamePad.GetState(playerIndex);
            if (!currentGamePadState.IsConnected)
            {
                // I don't know what to do now.
            }
        }

        #endregion

        #region Splash screen


        public static bool IsSplashScreenAction()
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex index = (PlayerIndex)i;

                KeyboardState keyboardState = Keyboard.GetState(index);
                if (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter))
                {
                    playerIndex = index;
                    currentKeyboardState = keyboardState;
                    isPlayerIndexDetermined = true;
                    return true;
                }

                GamePadState gamePadState = GamePad.GetState(index);
                if (gamePadState.IsButtonDown(Buttons.Start))
                {
                    playerIndex = index;
                    currentGamePadState = gamePadState;
                    //gamePadWasConnected = true;
                    isPlayerIndexDetermined = true;
                    return true;
                }
            }

            return false;
        }


        #endregion

        #region Tile Map Input


        static Vector2 moveDirection = Vector2.Zero;

        public static Vector2 GetPlayerMoveVector()
        {
            moveDirection.X = currentGamePadState.ThumbSticks.Left.X;
            moveDirection.Y = -currentGamePadState.ThumbSticks.Left.Y;
            moveDirection.X += currentGamePadState.ThumbSticks.Right.X;
            moveDirection.Y += -currentGamePadState.ThumbSticks.Right.Y;

            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                moveDirection.Y += 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                moveDirection.Y -= 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                moveDirection.X += 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                moveDirection.X -= 1;
            }
            if (moveDirection.LengthSquared() > 0.0001)
            {
                moveDirection.Normalize();
            }

            return moveDirection;
        }

        public static Direction GetPlayerMoveDirection()
        {
            moveDirection.X = currentGamePadState.ThumbSticks.Left.X;
            moveDirection.Y = currentGamePadState.ThumbSticks.Left.Y;
            moveDirection.X += currentGamePadState.ThumbSticks.Right.X;
            moveDirection.Y += currentGamePadState.ThumbSticks.Right.Y;

            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                moveDirection.Y -= 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                moveDirection.Y += 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                moveDirection.X += 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                moveDirection.X -= 1;
            }

            double angle = System.Math.Atan2(moveDirection.Y, moveDirection.X);

            if (angle < 0)
            {
                angle += 2 * System.Math.PI;
            }

            double sectionAngle = 2 * System.Math.PI / 8;
            double angleCutoff = 2 * System.Math.PI / 16;
            if (angle < angleCutoff)
            {
                return Direction.East;
            }
            angleCutoff += sectionAngle;
            if (angle < angleCutoff)
            {
                return Direction.NorthEast;
            }
            angleCutoff += sectionAngle;
            if (angle < angleCutoff)
            {
                return Direction.North;
            }
            angleCutoff += sectionAngle;
            if (angle < angleCutoff)
            {
                return Direction.NorthWest;
            }
            angleCutoff += sectionAngle;
            if (angle < angleCutoff)
            {
                return Direction.West;
            }
            angleCutoff += sectionAngle;
            if (angle < angleCutoff)
            {
                return Direction.SouthWest;
            }
            angleCutoff += sectionAngle;
            if (angle < angleCutoff)
            {
                return Direction.South;
            }
            angleCutoff += sectionAngle;
            if (angle < angleCutoff)
            {
                return Direction.SouthEast;
            }

            return Direction.East;
        }

        public static bool IsTileMapExit()
        {
            return IsNewButtonPress(Buttons.Back) || IsNewKeyPress(Keys.Escape);
        }

        public static bool IsViewMap()
        {
#if DEBUG
            return IsNewButtonPress(Buttons.RightTrigger) || IsNewKeyPress(Keys.M);
#else
            return IsNewButtonPress(Buttons.LeftTrigger) || IsNewButtonPress(Buttons.RightTrigger) || IsNewKeyPress(Keys.M);
#endif
        }

        public static bool IsRunning()
        {
            return IsButtonDown(Buttons.X) || IsKeyDown(Keys.LeftShift);
        }

        ///// <summary>
        ///// Checks for a "move party right" action.
        ///// </summary>
        ///// <param name="controllingPlayer">Specifies which player to read input from</param>
        //public static bool IsMovePlayerRight()
        //{
        //    return (IsKeyDown(Keys.Right) ||
        //            IsButtonDown(Buttons.DPadRight) ||
        //            IsButtonDown(Buttons.LeftThumbstickRight) ||
        //            IsButtonDown(Buttons.RightThumbstickRight));
        //}

        ///// <summary>
        ///// Checks for a "move party left" action.
        ///// </summary>
        ///// <param name="controllingPlayer">Specifies which player to read input from</param>
        //public static bool IsMovePlayerLeft()
        //{
        //    return (IsKeyDown(Keys.Left) ||
        //            IsButtonDown(Buttons.DPadLeft) ||
        //            IsButtonDown(Buttons.LeftThumbstickLeft) ||
        //            IsButtonDown(Buttons.RightThumbstickLeft));
        //}

        ///// <summary>
        ///// Checks for a "move party up" action.
        ///// </summary>
        ///// <param name="controllingPlayer">Specifies which player to read input from</param>
        //public static bool IsMovePlayerUp()
        //{
        //    return (IsKeyDown(Keys.Up) ||
        //            IsButtonDown(Buttons.DPadUp) ||
        //            IsButtonDown(Buttons.LeftThumbstickUp) ||
        //            IsButtonDown(Buttons.RightThumbstickUp));
        //}

        ///// <summary>
        ///// Checks for a "move party down" action.
        ///// </summary>
        ///// <param name="controllingPlayer">Specifies which player to read input from</param>
        //public static bool IsMovePlayerDown()
        //{
        //    return (IsKeyDown(Keys.Down) ||
        //            IsButtonDown(Buttons.DPadDown) ||
        //            IsButtonDown(Buttons.LeftThumbstickDown) ||
        //            IsButtonDown(Buttons.RightThumbstickDown));
        //}

        /// <summary>
        /// Checks for a "party interaction" action.
        /// </summary>
        /// <param name="controllingPlayer">The player to read input from</param>
        /// <returns></returns>
        public static bool IsPartyInteract()
        {
            return (IsNewButtonPress(Buttons.A) || IsNewKeyPress(Keys.Space));
        }


        #endregion

        #region Status Screen and children


        /// <summary>
        /// Checks for an "open the status screen" action
        /// </summary>
        /// <param name="controllingPlayer">The player to read input from</param>
        public static bool IsOpenStatusScreen()
        {
            return IsNewButtonPress(Buttons.Y) || IsNewKeyPress(Keys.Escape);
        }

        public static bool IsSwitchCharacterLeft()
        {
            return IsNewButtonPress(Buttons.LeftTrigger) || IsNewKeyPress(Keys.Left) || IsNewKeyPress(Keys.A);
        }

        public static bool IsSwitchCharacterRight()
        {
            return IsNewButtonPress(Buttons.RightTrigger) || IsNewKeyPress(Keys.Right) || IsNewKeyPress(Keys.D);
        }

        public static bool IsUnequipPressed()
        {
            return IsNewButtonPress(Buttons.Y) || IsNewKeyPress(Keys.LeftShift) || IsNewKeyPress(Keys.Y);
        }

        public static bool IsDestroyPressed()
        {
            return IsNewButtonPress(Buttons.X) || IsNewKeyPress(Keys.T);
        }

        public static bool IsMoveItemUp()
        {
            return IsNewKeyPress(Keys.Q) || IsNewButtonPress(Buttons.DPadUp);
        }

        public static bool IsMoveItemDown()
        {
            return IsNewKeyPress(Keys.E) || IsNewButtonPress(Buttons.DPadDown);
        }

        public static bool IsScrollDown()
        {
            return IsButtonDown(Buttons.LeftThumbstickDown) || IsKeyDown(Keys.Down) || IsKeyDown(Keys.S);
        }

        public static bool IsScrollUp()
        {
            return IsButtonDown(Buttons.LeftThumbstickUp) || IsKeyDown(Keys.Up) || IsKeyDown(Keys.W);
        }

        public static bool IsFastScrollUp()
        {
            return IsButtonDown(Buttons.LeftThumbstickUp) ||
                   IsButtonDown(Buttons.RightThumbstickUp) ||
                   IsKeyDown(Keys.Up) ||
                   IsKeyDown(Keys.W);
        }
        public static bool IsScrollRight()
        {
            return IsButtonDown(Buttons.LeftThumbstickRight) || IsKeyDown(Keys.Right);
        }

        public static bool IsScrollLeft()
        {
            return IsButtonDown(Buttons.LeftThumbstickLeft) || IsKeyDown(Keys.Left);
        }

        public static bool IsReset()
        {
            return IsNewKeyPress(Keys.S);
        }
        public static bool IsFastScrollDown()
        {
            return IsButtonDown(Buttons.LeftThumbstickDown) ||
                   IsButtonDown(Buttons.RightThumbstickDown) ||
                   IsKeyDown(Keys.Down) ||
                   IsKeyDown(Keys.S);
        }


        #endregion

        #region General Menu Input

        /// <summary>
        /// Checks for a "menu select" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public static bool IsMenuSelect()
        {
            return IsNewKeyPress(Keys.Space) ||
                   IsNewKeyPress(Keys.Enter) ||
                   IsNewButtonPress(Buttons.A) ||
                   IsNewButtonPress(Buttons.Start);
        }

        /// <summary>
        /// Checks for a "menu cancel" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public static bool IsMenuCancel()
        {
            return IsNewKeyPress(Keys.Escape) ||
                   IsNewButtonPress(Buttons.B) ||
                   IsNewButtonPress(Buttons.Back);
        }

        /// <summary>
        /// Checks for a "menu up" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsMenuUp()
        {
            return IsNewKeyPress(Keys.Up) ||
                   IsNewKeyPress(Keys.W) ||
                   //IsNewButtonPress(Buttons.DPadUp) ||
                   IsNewButtonPress(Buttons.LeftThumbstickUp) ||
                   IsNewButtonPress(Buttons.RightThumbstickUp);
        }

        /// <summary>
        /// Checks for a "menu down" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsMenuDown()
        {
            return IsNewKeyPress(Keys.Down) ||
                   IsNewKeyPress(Keys.S) ||
                   //IsNewButtonPress(Buttons.DPadDown) ||
                   IsNewButtonPress(Buttons.LeftThumbstickDown) ||
                   IsNewButtonPress(Buttons.RightThumbstickDown);
        }

        /// <summary>
        /// Checks for a "menu left" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsMenuLeft()
        {
            return IsNewKeyPress(Keys.Left) ||
                   IsNewKeyPress(Keys.A) ||
                   IsNewButtonPress(Buttons.DPadLeft) ||
                   IsNewButtonPress(Buttons.LeftThumbstickLeft) ||
                   IsNewButtonPress(Buttons.RightThumbstickLeft);
        }

        /// <summary>
        /// Checks for a "menu right" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsMenuRight()
        {
            return IsNewKeyPress(Keys.Right) ||
                   IsNewKeyPress(Keys.D) ||
                   IsNewButtonPress(Buttons.DPadRight) ||
                   IsNewButtonPress(Buttons.LeftThumbstickRight) ||
                   IsNewButtonPress(Buttons.RightThumbstickRight);
        }

        public static bool IsFastTextScroll()
        {
            return IsButtonDown(Buttons.A) || IsKeyDown(Keys.Space);
        }


        #endregion

        #region Combat Input


        /// <summary>
        /// Checks for a right trigger press for
        /// scrolling through the initiative list
        /// </summary>
        public static bool IsRightTrigger()
        {
            return IsNewButtonPress(Buttons.RightTrigger);
        }

        /// <summary>
        /// Checks for a left trigger press for
        /// scrolling through the initiative list
        /// </summary>
        public static bool IsLeftTrigger()
        {
            return IsNewButtonPress(Buttons.LeftTrigger);
        }


        #endregion

        #region Private Helpers


        /// <summary>
        /// Helper for checking if a key was newly pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        static bool IsNewKeyPress(Keys key)
        {
             return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Helper for checking if a button was newly pressed during this update.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a button press
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        static bool IsNewButtonPress(Buttons button)
        {
            return currentGamePadState.IsButtonDown(button) && previousGamePadState.IsButtonUp(button);
        }

        /// <summary>
        /// Helper for checking if a button is being pressed
        /// </summary>
        static bool IsButtonDown(Buttons button)
        {
            return IsButtonDown(button, playerIndex);
        }

        /// <summary>
        /// Helper for checking if a button is being pressed by a particular player
        /// </summary>
        static bool IsButtonDown(Buttons button, PlayerIndex controllingPlayer)
        {
            return currentGamePadState.IsButtonDown(button);
        }

        /// <summary>
        /// Helper for checking if a key is being pressed by a particular player
        /// </summary>
        static bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }


        #endregion

        public static void SetVibration(float leftMotor, float rightMotor)
        {
            GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
        }

        #region DEBUGGING CODE

        public static bool Temp_IsStockAndDisplayItemShopScreen()
        {
            return (IsNewButtonPress(Buttons.RightTrigger) || IsNewKeyPress(Keys.F6));
        }

        public static bool Temp_IsToggleCollisionDisplay()
        {
            return (IsNewButtonPress(Buttons.LeftTrigger) || IsNewKeyPress(Keys.F1));
        }

        public static bool Temp_IsTogglePortalDisplay()
        {
            return (IsNewButtonPress(Buttons.LeftTrigger) || IsNewKeyPress(Keys.F2));
        }

        public static bool Temp_IsToggleCombatActive()
        {
            return (IsNewButtonPress(Buttons.LeftTrigger) || IsNewKeyPress(Keys.B));
        }

        public static bool Temp_IsSaveGame()
        {
            return IsNewKeyPress(Keys.F5) || IsNewButtonPress(Buttons.RightStick);
        }

        public static bool Temp_IsToggleTitleSafeDisplay()
        {
            return IsNewKeyPress(Keys.F4) ||
                   IsNewButtonPress(Buttons.LeftShoulder);
        }

        // Gabriel is using '=' key for testing; it makes NPCs always defend.
        public static bool Temp_IsSetAlwaysDefend()
        {
            return IsNewKeyPress(Keys.P);
        }

        public static bool Temp_IsDebugInfoKeyPress()
        {
            return IsNewKeyPress(Keys.F3);
        }

        public static bool Temp_IsHelpScreen()
        {
            return IsNewKeyPress(Keys.H);
        }

        public static bool Temp_IsGameOverDisplay()
        {
            return IsNewKeyPress(Keys.RightControl);
        }

        public static bool Temp_IsDebugTextMode()
        {
            return IsNewKeyPress(Keys.F4);
        }

        public static bool Temp_IsTest()
        {
            return IsNewKeyPress(Keys.D0);
        }

        #endregion
    }
}
