using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillsWanderSceneC : Scene
    {

        public WillsWanderSceneC(Screen screen)
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

            Dialog blindedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z967);

            ScreenManager.Singleton.AddScreen(new DialogScreen(blindedDialog, DialogScreen.Location.TopLeft, "Will"));

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
