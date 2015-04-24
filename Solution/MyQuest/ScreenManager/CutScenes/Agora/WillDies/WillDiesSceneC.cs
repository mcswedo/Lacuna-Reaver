using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    class WillDiesSceneC : Scene
    {
        Dialog dialog = new Dialog(DialogPrompt.NeedsClose, false, Strings.ZA035);

        public WillDiesSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.Leader.FaceDirection(Direction.West);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
            
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
