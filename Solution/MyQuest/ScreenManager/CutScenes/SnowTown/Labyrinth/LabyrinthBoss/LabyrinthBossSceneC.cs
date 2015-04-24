using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class LabyrinthBossSceneC : Scene
    {
        #region Dialog

        static readonly Dialog serlynxDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z840);

        static readonly Dialog willsDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z841);

        #endregion

        Dialog dialog;

        public LabyrinthBossSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = serlynxDialog;

            dialog.DialogCompleteEvent += willsResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Serlynx"));
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void willsResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willsResponse2;

            dialog = willsDialog2;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));

            state = SceneState.Complete;
        }

        #endregion
    }
}
