using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// Finished

namespace MyQuest
{
    /// <summary>
    /// Serves as the base class for simple text based menu options
    /// </summary>
    public abstract class MenuOption
    {
        #region Fields


        Screen container;

        /// <summary>
        /// The screen that this option belongs to
        /// </summary>
        public Screen Container
        {
            get { return container; }
        }
        
        
        string description;

        /// <summary>
        /// The textual representation of the option
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        Vector2 position;

        /// <summary>
        /// The option's position on screen
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }


        #endregion

        #region Events


        /// <summary>
        /// This event will be fired if the option is selected by the user.
        /// </summary>
        public event EventHandler OnSelectEvent;

        /// <summary>
        /// Method for raising the Selected event.
        /// </summary>
        protected internal virtual void OnSelectEntry()
        {
            if (OnSelectEvent != null)
                OnSelectEvent(this, new EventArgs());
        }


        #endregion

        #region Constructor


        /// <summary>
        /// Construct a new menu option
        /// </summary>
        /// <param name="container">The screen that contains the option</param>
        /// <param name="description">The text that represents this optio</param>
        /// <param name="position">The position on screen to render</param>
        public MenuOption(Screen container, string description, Vector2 position)
        {
            this.container = container;
            this.description = description;
            this.position = position;
        }


        #endregion

        #region Methods


        /// <summary>
        /// Draw the menu entry.
        /// </summary>
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        /// <summary>
        /// This method can be overriden to handle custom visual effects. Alternatively, it can do nothing.
        /// </summary>
        /// <param name="isSelected">Whether or not this menu entry is currently selected</param>
        public abstract void Update(GameTime gameTime, bool isSelected);


        #endregion
    }
}
