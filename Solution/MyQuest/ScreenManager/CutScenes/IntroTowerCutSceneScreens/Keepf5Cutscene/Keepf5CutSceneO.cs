using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    class Keepf5CutSceneO : Scene
    {
        Dialog dialog1 = new Dialog(DialogPrompt.NeedsClose, false, Strings.Z570);

        Dialog dialog2 = new Dialog(DialogPrompt.NeedsClose, false, Strings.Z571);

        public Keepf5CutSceneO(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog1, DialogScreen.Location.TopLeft, "Nathan"));
            dialog1.DialogCompleteEvent += Dialog2;
        }

        void Dialog2(object sender, PartyResponseEventArgs e)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog2, DialogScreen.Location.TopLeft, "Arlan"));
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
