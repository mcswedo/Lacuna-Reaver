using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class BackFromArlansStudyScene : Scene
    {
        Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z706);

        Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z707);

        Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, "...");

        public BackFromArlansStudyScene(Screen screen)
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
            ScreenManager.Singleton.AddScreen(new DialogScreen(willDialog, DialogScreen.Location.TopLeft, "Will"));
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
 