using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class WillDiesSceneG : Scene
    {
        #region Dialog

        static readonly Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA042, Strings.ZA043, Strings.ZA044);

        static readonly Dialog  caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA045);

        static readonly Dialog nathanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA046, Strings.ZA047, Strings.ZA048);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA049, Strings.ZA050, Strings.ZA051);

        static readonly Dialog nathanDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA052);

        static readonly Dialog caraDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA053, Strings.ZA054, Strings.ZA055, Strings.ZA056);

        static readonly Dialog nathanDialog4 = new Dialog(DialogPrompt.NeedsClose, "...");

        static readonly Dialog caraDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA057);

        static readonly Dialog nathanDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA058, Strings.ZA719);

        static readonly Dialog caraDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA059, Strings.ZA060);

        #endregion

        Dialog dialog;

        public WillDiesSceneG(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = nathanDialog;
            dialog.DialogCompleteEvent += carasResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
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

           Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "InjuredCara",
                   Point.Zero,
                   ModAction.Remove,
                   false);

            Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "Cara",
                    new Point(19, 4),
                    Direction.East,
                    true,
                    ModAction.Add,
                    false);
           
            dialog = caraDialog;
            dialog.DialogCompleteEvent += nathansResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara")); 
        }

        void nathansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse;

            Party.Singleton.Leader.FaceDirection(Direction.West);

            dialog = nathanDialog2;

            dialog.DialogCompleteEvent += carasResponse2;      

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));

            state = SceneState.Complete;
        }

        void carasResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse2;

            dialog = caraDialog2;

            dialog.DialogCompleteEvent += nathansResponse2;          

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));

            state = SceneState.Complete;
        }

        void nathansResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse2;

            dialog = nathanDialog3;

            dialog.DialogCompleteEvent += carasResponse3;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
        }

        void carasResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse3;
            
            dialog = caraDialog3;

            dialog.DialogCompleteEvent += nathansResponse3;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
        }

        void nathansResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse3;

            dialog = nathanDialog4;

            dialog.DialogCompleteEvent += carasResponse4;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
        }

        void carasResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse4;

            dialog = caraDialog4;

            dialog.DialogCompleteEvent += nathansResponse4;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
        }

        void nathansResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse4;

            dialog = nathanDialog5;

            dialog.DialogCompleteEvent += carasResponse5;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
        }

        void carasResponse5(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse5;

            dialog = caraDialog5;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));

            state = SceneState.Complete;
        }


        #endregion
    }
}
