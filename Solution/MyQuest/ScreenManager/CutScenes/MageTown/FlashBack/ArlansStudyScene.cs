using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ArlansStudyScene : Scene
    {

        #region Dialog

        Dialog kingDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z703, Strings.Z704, Strings.Z705);

        #endregion

        public ArlansStudyScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Camera.Singleton.CenterOnTarget(
                Party.Singleton.Leader.WorldPosition,
                Party.Singleton.CurrentMap.DimensionsInPixels,
                ScreenManager.Singleton.ScreenResolution);

            ScreenManager.Singleton.AddScreen(new DialogScreen(kingDialog, DialogScreen.Location.BottomRight, "King"));
            state = SceneState.Complete; 
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

       

        #endregion
    }
}
 