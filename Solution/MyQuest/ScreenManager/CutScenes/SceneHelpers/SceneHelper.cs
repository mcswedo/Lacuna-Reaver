using Microsoft.Xna.Framework;
using System;

namespace MyQuest
{
    /// <summary>
    /// Serves as the base class for SceneHelpers
    /// </summary>
    public abstract class SceneHelper
    {
        /// <summary>
        /// An event that will be raised when the
        /// scene helper completes its task
        /// </summary>
        public event EventHandler OnCompleteEvent;

        public virtual void OnComplete()
        {
            if (OnCompleteEvent != null)
            {
                OnCompleteEvent(this, new EventArgs());
            }
        }

        protected bool isComplete = false;

        /// <summary>
        /// Whether or not the helper has completed its task
        /// </summary>
        public bool IsComplete
        {
            get { return isComplete; }
        }

        public abstract void Update(GameTime gameTime);
    }
}
