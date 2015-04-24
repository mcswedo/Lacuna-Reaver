using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RoyalFamilyRememberedScene : Scene
    {
        Dialog dialog; 

        #region Dialog   

        Dialog kingDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z758);

        Dialog queenDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z759);

        Dialog kingDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z760);

        Dialog queenDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z761);

        Dialog kingDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z762, Strings.Z763, Strings.Z764, Strings.Z765);

        #endregion

        public RoyalFamilyRememberedScene(Screen screen)
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
            
            dialog = kingDialog;
            dialog.DialogCompleteEvent += queenResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "King"));

        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void queenResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= queenResponse;
            dialog = queenDialog;
            dialog.DialogCompleteEvent += kingResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Queen"));          
        }

        void kingResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= kingResponse;
            dialog = kingDialog2;
            dialog.DialogCompleteEvent += queenResponse2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "King"));
        }

        void queenResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= queenResponse2;
            dialog = queenDialog2;
            dialog.DialogCompleteEvent += kingResponse2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Queen"));
        }

        void kingResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= kingResponse2;
            dialog = kingDialog3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "King"));
            state = SceneState.Complete;
        }

        #endregion
    }
}
 