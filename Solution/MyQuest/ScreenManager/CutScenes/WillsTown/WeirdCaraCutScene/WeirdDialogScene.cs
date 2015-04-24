using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WeirdDialogScene : Scene
    {
        Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z933);

        public WeirdDialogScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(caraDialog, DialogScreen.Location.TopLeft, "Cara"));
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
 