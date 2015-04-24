using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class DelayScene : Scene
    {
        double secondsRemaining;

        public DelayScene(Screen screen, double secondsRemaining)
            : base(screen)
        {
            this.secondsRemaining = secondsRemaining;
        }

        public override void LoadContent(ContentManager content)
        {    
        }

        public override void Initialize()
        {        
        }

        public override void Update(GameTime gameTime)
        {
            secondsRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

            if (secondsRemaining <= 0)
            {
                state = SceneState.Complete;
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
 