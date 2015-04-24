using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class EnterMageTownSceneE : Scene
    {
        Dialog dialog;
        SceneHelper moveHelper1;

        #region Dialog

        static readonly Dialog boyDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z687);

        static readonly Dialog endorDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z688);

        static readonly Dialog boyDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z689, Strings.Z690, Strings.Z691);

        static readonly Dialog endorDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z692);

        static readonly Dialog boyDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z693, Strings.Z694, Strings.Z695);

        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z696);

        static readonly Dialog endorDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z697);

        static readonly Dialog mageDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z698);

        static readonly Dialog endorDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z699, Strings.Z700, Strings.Z701);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z702); 

        #endregion


        public EnterMageTownSceneE(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            moveHelper1 = new MoveNpcCharacterHelper("BoyMage", new Point(8, 47), true, new Point(11, 56), 5.0f);
            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(moveHelper1);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("BoyMage").FaceDirection(Direction.East);
    
            Party.Singleton.CurrentMap.GetNPC("Endor").FaceDirection(Direction.West);

            dialog = boyDialog1;

            dialog.DialogCompleteEvent += endorResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "BoyMage"));
        }

        void endorResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse;

            dialog = endorDialog1;

            dialog.DialogCompleteEvent += boyResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void boyResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= boyResponse;

            dialog = boyDialog2;

            dialog.DialogCompleteEvent += endorResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "BoyMage"));
        }

        void endorResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse2;

            dialog = endorDialog2;

            dialog.DialogCompleteEvent += boyResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void boyResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= boyResponse2;

            dialog = boyDialog3;

            dialog.DialogCompleteEvent += caraResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "BoyMage"));
        }

        void caraResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse;

            dialog = caraDialog1;

            dialog.DialogCompleteEvent += endorResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void endorResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse3;

            Party.Singleton.CurrentMap.GetNPC("Endor").FaceDirection(Direction.South);

            dialog = endorDialog3;

            dialog.DialogCompleteEvent += mageResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }
        void mageResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= mageResponse;

            dialog = mageDialog1;

            dialog.DialogCompleteEvent += endorResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Ruith"));
        }
        void endorResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse4;

            dialog = endorDialog4;

            dialog.DialogCompleteEvent += caraResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }
        void caraResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse2;

            dialog = caraDialog2;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));

            state = SceneState.Complete;
        }
        #endregion
    }
}
