using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class EnterMageTownSceneD : Scene
    {

        Dialog dialog; 

        #region dialog

        static readonly Dialog waitDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z645);

        static readonly Dialog endorDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z646, Strings.Z647, Strings.Z648);

        static readonly Dialog nathanDialog1 = new Dialog(DialogPrompt.NeedsClose, "???");

        static readonly Dialog endorDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z649);

        static readonly Dialog mageDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z650);

        static readonly Dialog endorDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z651, Strings.Z652, Strings.Z653);

        static readonly Dialog mageDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z654, Strings.Z655, Strings.Z656); 
          
        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z657);
         
        static readonly Dialog mageDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z658, Strings.Z659);

        static readonly Dialog endorDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z660, Strings.Z661, Strings.Z662);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z663, Strings.Z664);

        static readonly Dialog endorDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z665, Strings.Z666, Strings.Z667);

        static readonly Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z668, Strings.Z669, Strings.Z670, Strings.Z671);

        static readonly Dialog endorDialog6 = new Dialog(DialogPrompt.NeedsClose, Strings.Z672, Strings.Z673);

        static readonly Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z674, Strings.Z675);

        static readonly Dialog mageDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z676, Strings.Z677, Strings.Z678, Strings.Z679, Strings.Z680, Strings.Z681, Strings.Z682);

        static readonly Dialog caraDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z683);

        static readonly Dialog endorDialog7 = new Dialog(DialogPrompt.NeedsClose, Strings.Z684, Strings.Z685);

        static readonly Dialog caraDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z686);
          
        #endregion

        public EnterMageTownSceneD(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = waitDialog;
            dialog.DialogCompleteEvent += endorSpeech;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void endorSpeech(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorSpeech;

            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
            "Endor",
            new Point(12, 53),
            false,
            new Point(12, 56),
            1.7f);
            Party.Singleton.CurrentMap.ResetSpawnDirection("Endor", Direction.South);

            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(moveHelper1);

        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            dialog = endorDialog1;
            dialog.DialogCompleteEvent += nathanResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor")); 
        }
        void nathanResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= nathanResponse;

            dialog = nathanDialog1;

            dialog.DialogCompleteEvent += endorResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }
        void endorResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse;

            dialog = endorDialog2;

            dialog.DialogCompleteEvent += mageResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }
        void mageResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= mageResponse;

            dialog = mageDialog1;

            dialog.DialogCompleteEvent += endorResponse2; 
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Ruith"));
        }

        void endorResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse2;

            dialog = endorDialog3; 

            dialog.DialogCompleteEvent += mageResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void mageResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= mageResponse2;

            dialog = mageDialog2; 

            dialog.DialogCompleteEvent += caraResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Ruith"));
        }

        void caraResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse;

            dialog = caraDialog1;

            dialog.DialogCompleteEvent += mageResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void mageResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= mageResponse3;

            dialog = mageDialog3; 

            dialog.DialogCompleteEvent += endorResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Ruith"));
        }

        void endorResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse3;

            dialog = endorDialog4; 

            dialog.DialogCompleteEvent += caraResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void caraResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse2;

            dialog = caraDialog2; 
            
            dialog.DialogCompleteEvent += endorResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void endorResponse4(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse4;

            dialog = endorDialog5;

            dialog.DialogCompleteEvent += willResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));

        }

        void willResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= willResponse;

            dialog = willDialog1;

            dialog.DialogCompleteEvent += endorResponse5;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void endorResponse5(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse5;

            dialog = endorDialog6;

            dialog.DialogCompleteEvent += willResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void willResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= willResponse2;

            dialog = willDialog2;

            dialog.DialogCompleteEvent += mageResponse4; 
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void mageResponse4(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= mageResponse4;

            dialog = mageDialog4;

            dialog.DialogCompleteEvent += caraResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Ruith"));
        }
        void caraResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse3;

            dialog = caraDialog3;

            dialog.DialogCompleteEvent += endorResponse6;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void endorResponse6(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse6;

            dialog = endorDialog7;

            dialog.DialogCompleteEvent += caraResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));
        }

        void caraResponse4(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse4;

            dialog = caraDialog4;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));

            state = SceneState.Complete;
        }

        #endregion
    }
}