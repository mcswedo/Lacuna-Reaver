using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class FinalBattleSceneC : Scene
    {
        #region Dialog

        static readonly Dialog arlanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA010, Strings.ZA011, Strings.ZA012, Strings.ZA013, Strings.ZA014, Strings.ZA015, Strings.ZA016);

        static readonly Dialog  nathanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA017);

        static readonly Dialog arlanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA018);


        #endregion

        Dialog dialog;

        public FinalBattleSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MusicSystem.Play(AudioCues.finalBossTransition);
            dialog = arlanDialog;
            dialog.DialogCompleteEvent += nathansResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "InjuredArlan"));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void nathansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse;

            dialog = nathanDialog;
            dialog.DialogCompleteEvent += arlansResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan")); 
        }

        void arlansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= arlansResponse;

            dialog = arlanDialog2;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "InjuredArlan"));

            state = SceneState.Complete;
        }

        #endregion
    }
}
