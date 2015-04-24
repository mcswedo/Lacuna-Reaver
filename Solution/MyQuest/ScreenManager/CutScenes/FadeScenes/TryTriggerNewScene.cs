using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class TryTriggerNewCutScene : Scene
    {
        public TryTriggerNewCutScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {    
        }

        public override void Initialize()
        {
            TileMapScreen.Instance.TryTriggerCutScene();
            state = SceneState.Complete;
        }

        public override void Update(GameTime gameTime)
        {
         
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
 