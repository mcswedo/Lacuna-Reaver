using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class NathanFoundCutSceneA : Scene
    {
        #region Fields

        Dialog dialog;
        MoveNpcCharacterHelper moveHelper1 = null;
        MoveNpcCharacterHelper moveHelper2 = null;

        #endregion

        #region Dialog

        static readonly Dialog mayorDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z537);

        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z538);

        static readonly Dialog mayorDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z539);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z540, Strings.Z541);

        static readonly Dialog mayorDialog3 = new Dialog(DialogPrompt.NeedsClose, "... ",". . . . . . . . ",Strings.Z542, Strings.Z543);

        #endregion

        public NathanFoundCutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MoveCameraHelper cameraHelper = new MoveCameraHelper(new Point(26,26), 9999f);
            dialog = mayorDialog1;

            moveHelper1 = new MoveNpcCharacterHelper(
             "Cara",
             new Point(27, 26),
             2.4f);

            moveHelper2 = new MoveNpcCharacterHelper(
             "Mayor",
             new Point(25, 26),
             2.4f);

            helpers.Add(moveHelper1);
            helpers.Add(moveHelper2);
            helpers.Add(cameraHelper);

            moveHelper1.OnCompleteEvent += CaraDialog1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Mayor"));
        }

        void CaraDialog1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraDialog1;
            dialog = caraDialog1;
            NPCMapCharacter mayor = Party.Singleton.CurrentMap.GetNPC("Mayor");
            NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC("Cara");

            mayor.FaceDirection(Direction.East);
            cara.FaceDirection(Direction.West);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
            dialog.DialogCompleteEvent += MayorDialog2;
        }

        void MayorDialog2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= MayorDialog2;
            dialog = mayorDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Mayor"));
            dialog.DialogCompleteEvent += CaraDialog2;
        }

        void CaraDialog2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraDialog2;
            dialog = caraDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
            dialog.DialogCompleteEvent += MayorDialog3;
        }

        void MayorDialog3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= MayorDialog3;
            dialog = mayorDialog3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Mayor"));
            dialog.DialogCompleteEvent += EscortNathan;
        }

        void EscortNathan(object sender, EventArgs e)
        {
           MoveNpcCharacterHelper moveHelper3 = new MoveNpcCharacterHelper(
             "Cara",
             new Point(14, 26),
             2.4f);

           MoveNpcCharacterHelper moveHelper4 = new MoveNpcCharacterHelper(
             "Mayor",
             new Point(12, 26),
             2.4f);

           MoveNpcCharacterHelper moveHelper5 = new MoveNpcCharacterHelper(
             "NathanInjured",
             new Point(13, 26),
             2.4f);

            helpers.Add(moveHelper3);
            helpers.Add(moveHelper4);
            helpers.Add(moveHelper5);

            moveHelper3.OnCompleteEvent += EndScene;
        }

        void EndScene(object sender, EventArgs e)
        {
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
