using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class LibraryBossSceneC : Scene
    {
        #region Dialog

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z710);

        #endregion

        public LibraryBossSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            ScreenManager.Singleton.AddScreen(
            new DialogScreen(caraDialog, DialogScreen.Location.BottomLeft, "Cara"));

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
