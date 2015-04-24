using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class EnterMageTownSceneB : Scene
    {
        NPCMapCharacter will;
        NPCMapCharacter cara;

        #region dialog

        static readonly Dialog mage1Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z631);

        static readonly Dialog will1Dialog = new Dialog(DialogPrompt.NeedsClose, "???");

        static readonly Dialog mage2Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z632);

        static readonly Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z633);

        static readonly Dialog mageDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z634);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z635);

        static readonly Dialog mageDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z636,  Strings.Z637);

        static readonly Dialog willDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z638);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z639);

        static readonly Dialog willDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z640, Strings.Z641);

        static readonly Dialog mageDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z642, Strings.Z643);

        static readonly Dialog willDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z644);
          
        #endregion

        public EnterMageTownSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {          
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
              Party.will,
              new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
              false,
              new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y),
              1.7f);

            will = Party.Singleton.CurrentMap.GetNPC(Party.will);

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
            SceneHelper moveHelper2 = new MoveNpcCharacterHelper(
             Party.cara,
             new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
             false,
             new Point(Party.Singleton.Leader.TilePosition.X - 1, Party.Singleton.Leader.TilePosition.Y),
             1.7f);

            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);

            moveHelper2.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
            will.FaceDirection(Direction.East);
            helpers.Add(moveHelper2);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {      
            cara.FaceDirection(Direction.North);
            mage1Dialog.DialogCompleteEvent += willsResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(mage1Dialog, DialogScreen.Location.TopRight, "Ruith"));

        }
        void willsResponse(object sender, PartyResponseEventArgs e)
        {
            mage1Dialog.DialogCompleteEvent -= willsResponse;

            will1Dialog.DialogCompleteEvent += magesResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(will1Dialog, DialogScreen.Location.TopLeft, "Will"));
        }
        void magesResponse(object sender, PartyResponseEventArgs e)
        {
            will1Dialog.DialogCompleteEvent -= magesResponse;

            mage2Dialog.DialogCompleteEvent += willsResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(mage2Dialog, DialogScreen.Location.TopRight, "Ruith"));
        }
        void willsResponse2(object sender, PartyResponseEventArgs e)
        {
            mage2Dialog.DialogCompleteEvent -= willsResponse2;

            willDialog2.DialogCompleteEvent += mageResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog2, DialogScreen.Location.TopLeft, "Will"));
        }
        void mageResponse2(object sender, PartyResponseEventArgs e)
        {
            willDialog2.DialogCompleteEvent -= mageResponse2;

            mageDialog3.DialogCompleteEvent += caraResponse; 
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(mageDialog3, DialogScreen.Location.TopRight, "Ruith"));
        }

        void caraResponse(object sender, PartyResponseEventArgs e)
        {
            mageDialog3.DialogCompleteEvent -= caraResponse;

            caraDialog.DialogCompleteEvent += mageResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(caraDialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void mageResponse3(object sender, PartyResponseEventArgs e)
        {
            caraDialog.DialogCompleteEvent -= mageResponse3;

            mageDialog4.DialogCompleteEvent += willResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(mageDialog4, DialogScreen.Location.TopRight, "Ruith"));
        }

        void willResponse3(object sender, PartyResponseEventArgs e)
        {
            mageDialog4.DialogCompleteEvent -= willResponse3;

            willDialog3.DialogCompleteEvent += caraResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog3, DialogScreen.Location.TopLeft, "Will"));
        }

        void caraResponse2(object sender, PartyResponseEventArgs e)
        {
            willDialog3.DialogCompleteEvent -= caraResponse2;

            caraDialog2.DialogCompleteEvent += willResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(caraDialog2, DialogScreen.Location.TopLeft, "Cara"));
        }

        void willResponse4(object sender, PartyResponseEventArgs e)
        {
            caraDialog2.DialogCompleteEvent -= willResponse4;

            willDialog4.DialogCompleteEvent += mageResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog4, DialogScreen.Location.TopLeft, "Will"));
        }

        void mageResponse4(object sender, PartyResponseEventArgs e)
        {
            willDialog4.DialogCompleteEvent -= mageResponse4;

            mageDialog5.DialogCompleteEvent += willResponse5;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(mageDialog5, DialogScreen.Location.TopRight, "Ruith"));
        }

        void willResponse5(object sender, PartyResponseEventArgs e)
        {
            mageDialog5.DialogCompleteEvent -= willResponse5;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog5, DialogScreen.Location.TopLeft, "Will"));
            state = SceneState.Complete;   
        }

        #endregion
    }
}