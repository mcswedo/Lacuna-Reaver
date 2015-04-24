using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;   

namespace MyQuest
{
    /// <summary>
    /// Handles the logic and rendering of a cut scene
    /// </summary>
    public abstract class CutSceneScreen : Screen
    {
        protected List<Scene> scenes = new List<Scene>();

        #region Initialization

        protected virtual void Complete()
        {
        }

        public void CompleteForTesting()
        {
            foreach (Scene scene in scenes)
            {
                scene.Complete();
            }
            Complete();
        }

        public CutSceneScreen()
        {
            IsPopup = true;
        }

        public override void Initialize()
        {
            Debug.Assert(scenes.Count > 0);
            scenes[0].Initialize();
            TileMapScreen.Instance.IsCutScenePlaying = true;
        }

        public override void LoadContent(ContentManager content)
        {
            foreach (Scene scene in scenes)
            {
                scene.LoadContent(content);
            }
        }

        public abstract bool CanPlay();

        #endregion

        #region Update and Draw


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

#if DEBUG
            if (InputState.Temp_IsToggleCollisionDisplay())  // F1
            {
                MessageOverlay.AddMessage(this.GetType().ToString() + " ..... " + scenes[0].GetType().ToString(), 7);
            }
#endif

            if (WillHandleUserInput)
            {
                if (scenes[0].State == SceneState.Complete)
                {
                    scenes.RemoveAt(0);
                    if (scenes.Count == 0)
                    {
                        Complete();
                        ScreenManager.RemoveScreen(this);
                        TileMapScreen.Instance.IsCutScenePlaying = false;
                        return;
                    }

                    scenes[0].Initialize();
                }
                else
                {
                    scenes[0].Update(gameTime);
                }
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (scenes.Count > 0)
                scenes[0].HandleInput(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (scenes.Count > 0)
            {
                scenes[0].Draw(GameLoop.Instance.AltSpriteBatch);
            }
        }

        #endregion
    }
}
