using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// Finished

namespace MyQuest
{
    /// <summary>
    /// Simple static text menu option
    /// </summary>
    public class StaticMenuOption : MenuOption
    {
        public StaticMenuOption(Screen container, string description, Vector2 position)
            :base(container, description, position)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(Fonts.MenuItem, Description, Position, GlobalSettings.MenuTextColor * Container.TransitionAlpha);
        }

        public override void Update(GameTime gameTime, bool isSelected)
        {            
        }
    }
}
