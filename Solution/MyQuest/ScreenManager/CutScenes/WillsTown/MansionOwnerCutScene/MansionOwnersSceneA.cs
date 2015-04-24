using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class MansionOwnersSceneA : Scene
    {

        #region Dialog

        Dialog robbedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z920);

        #endregion

        public MansionOwnersSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation; 
        }

        public override void Update(GameTime gameTime)
        {

            ScreenManager.Singleton.AddScreen(new DialogScreen(robbedDialog, DialogScreen.Location.BottomRight, "MansionOwner"));
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
