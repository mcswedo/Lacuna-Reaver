using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RoyalFamilyScene : Scene
    {
        Dialog dialog; 

        #region Dialog

        Dialog kingDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z909, Strings.Z910);

        Dialog queenDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z911, Strings.Z912);

        Dialog kingDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z913, Strings.Z914);

        Dialog queenDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z915);

        Dialog kingDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z916);

        Dialog queenDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z917);

        Dialog kingDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z918);

        #endregion

        public RoyalFamilyScene(Screen screen)
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

            MovePCMapCharacterHelper nathanHelper = new MovePCMapCharacterHelper(new Point(6,4));
            MoveCameraHelper cameraHelper = new MoveCameraHelper(new Point(6, 4), 4.0f);
            helpers.Add(nathanHelper);
            helpers.Add(cameraHelper);

            nathanHelper.OnCompleteEvent += kingDialog1;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void kingDialog1(object sender, EventArgs e)
        {
            dialog = kingDialog;
            dialog.DialogCompleteEvent += queenResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "King"));
        }

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
            dialog.DialogCompleteEvent += queenResponse3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "King"));
        }

        void queenResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= queenResponse3;
            dialog = queenDialog3;
            dialog.DialogCompleteEvent += kingResponse4;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Queen"));
        }


        void kingResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= kingResponse4;
            dialog = kingDialog4;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "King"));
            state = SceneState.Complete;
        }

        #endregion
    }
}
 