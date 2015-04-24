using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class LabyrinthBossSceneB : Scene
    {
        #region Dialog

        static readonly Dialog willsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z837);

        static readonly Dialog carasDialog = new Dialog(DialogPrompt.NeedsClose, "...", Strings.Z838, Strings.Z839);

        #endregion

        Dialog dialog;

        public LabyrinthBossSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = willsDialog;

            dialog.DialogCompleteEvent += carasResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void carasResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse;

            dialog = carasDialog;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));

            state = SceneState.Complete;
        }    

        #endregion
    }
}
