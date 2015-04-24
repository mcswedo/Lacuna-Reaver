using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class CaraScene : Scene
    {
        Dialog caraDialog = new Dialog(
           DialogPrompt.NeedsClose,
           Strings.Z904);

        Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z905, Strings.Z906, Strings.Z907, Strings.Z908);

        Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, "?!");


        public CaraScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {         
            Party.Singleton.Leader.FaceDirection(Direction.South);
        }

        public override void Update(GameTime gameTime)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(caraDialog2, DialogScreen.Location.TopLeft, "Cara"));
            ScreenManager.Singleton.AddScreen(new DialogScreen(nathanDialog, DialogScreen.Location.TopLeft, "Nathan"));
            ScreenManager.Singleton.AddScreen(new DialogScreen(caraDialog, DialogScreen.Location.TopLeft, "Cara"));
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
 