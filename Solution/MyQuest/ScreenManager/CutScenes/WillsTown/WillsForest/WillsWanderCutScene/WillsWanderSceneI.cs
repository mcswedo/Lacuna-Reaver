using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillsWanderSceneI : Scene
    {

        public WillsWanderSceneI(Screen screen)
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
            Dialog helpDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z970);

            ScreenManager.Singleton.AddScreen(new DialogScreen(helpDialog, DialogScreen.Location.TopLeft, "Cara"));    

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
