using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public enum SceneState
    {
        Working,
        Complete
    }

    /// <summary>
    /// Handles the logic and rendering for a scene within a cut scene
    /// </summary>
    public abstract class Scene
    {
        #region Fields
       
        protected Screen screen;

        protected List<SceneHelper> helpers = new List<SceneHelper>();


        bool helpersAreComplete = false;

        /// <summary>
        /// Whether or not all of the SceneHelpers have completed
        /// </summary>
        protected bool HelperesAreComplete
        {
            get { return helpersAreComplete; }
        }


        protected SceneState state;

        public SceneState State
        {
            get { return state; }
        }

        #endregion

        #region Methods

        public virtual void Complete()
        {
        }

        public Scene(Screen screen)
        {
            this.screen = screen;

            state = SceneState.Working;
        }

        protected bool MoveNPC(GameTime gameTime, NPCMapCharacter npc, List<Direction> steps, ref Point destinationPoint)
        {
            npc.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (npc.TilePosition == destinationPoint)
            {
                steps.RemoveAt(0);

                if (steps.Count == 0)
                {
                    npc.IdleOnly = true;

                    return true;
                }
                else
                {
                    npc.SetAutoMovement(steps[0], Party.Singleton.CurrentMap);

                    destinationPoint = Utility.GetMapPositionFromDirection(npc.TilePosition, steps[0]);
                }
            }

            return false;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void Initialize();
        public abstract void HandleInput(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        //public virtual void AltDraw(SpriteBatch spriteBatch) { }

        public virtual void Update(GameTime gameTime)
        {

            if (helpers.Count > 0)
            {
                for(int i = helpers.Count - 1; i >= 0; --i)
                {
                    helpers[i].Update(gameTime);

                    if (helpers[i].IsComplete)
                    {
                        helpers[i].OnComplete();
                        helpers.RemoveAt(i);
                    }
                }

                helpersAreComplete = (helpers.Count == 0);
            }
        }

        #endregion
    }
}
