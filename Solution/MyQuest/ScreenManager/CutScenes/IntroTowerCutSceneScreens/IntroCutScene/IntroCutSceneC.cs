using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class IntroCutSceneC : Scene
    { 
        Dialog dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z555, Strings.Z556);

        public IntroCutSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        public override void Update(GameTime gameTime)
        {
            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
