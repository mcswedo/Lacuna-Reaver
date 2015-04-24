using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class LabyrinthBossSceneD : Scene
    {
        #region Dialog

        static readonly Dialog laughDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z842);

        static readonly Dialog serlynxEndDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z843, Strings.Z844);

        #endregion

        Dialog dialog;

        public LabyrinthBossSceneD(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = laughDialog;

            dialog.DialogCompleteEvent += serlynxEnd;
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

        void serlynxEnd(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= serlynxEnd;

            dialog = serlynxEndDialog;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Serlynx"));

            state = SceneState.Complete;
        }

        #endregion
    }
}
