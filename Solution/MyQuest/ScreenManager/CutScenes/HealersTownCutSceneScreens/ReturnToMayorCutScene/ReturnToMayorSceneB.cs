using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ReturnToMayorSceneB : Scene
    {
        static readonly Dialog mayor1Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z545);

        static readonly Dialog cara1Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z546);

        static readonly Dialog mayor2Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z547,  Strings.Z548, Strings.Z549);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z550);

        static readonly Dialog cara2Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z551);

        public ReturnToMayorSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
              Party.cara,
              new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
              false,
              new Point(Party.Singleton.Leader.TilePosition.X-1, Party.Singleton.Leader.TilePosition.Y),
              1.7f);
             
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
             Party.will,
             new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
             false,
             new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y),
             1.7f);
         
            moveHelper2.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
            helpers.Add(moveHelper2);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            mayor1Dialog.DialogCompleteEvent += carasResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(mayor1Dialog, DialogScreen.Location.TopLeft, "Mayor"));

        }
        void carasResponse(object sender, PartyResponseEventArgs e)
        {
            mayor1Dialog.DialogCompleteEvent -= carasResponse;

            cara1Dialog.DialogCompleteEvent += mayorsResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(cara1Dialog, DialogScreen.Location.TopLeft, "Cara"));
        }
        void mayorsResponse(object sender, PartyResponseEventArgs e)
        {
            cara1Dialog.DialogCompleteEvent -= mayorsResponse;

            mayor2Dialog.DialogCompleteEvent += willsResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(mayor2Dialog, DialogScreen.Location.TopLeft, "Mayor"));
            Party.Singleton.AddLogEntry("Mushroom Hollow", "Mayor", mayor2Dialog.Text);
        }
        void willsResponse(object sender, PartyResponseEventArgs e)
        {
            mayor2Dialog.DialogCompleteEvent -= willsResponse;

            willDialog.DialogCompleteEvent += carasFinalResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog, DialogScreen.Location.TopLeft, "Will"));
        }
        void carasFinalResponse(object sender, PartyResponseEventArgs e)
        {
            willDialog.DialogCompleteEvent -= carasFinalResponse;

            cara2Dialog.DialogCompleteEvent += partyJoin; 
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(cara2Dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void partyJoin(object sender, PartyResponseEventArgs e)
        {
            cara2Dialog.DialogCompleteEvent -= partyJoin;

            SceneHelper moveHelper4 = new MoveNpcCharacterHelper(
             Party.cara, new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y));
           
            helpers.Add(moveHelper4);

            SceneHelper moveHelper3 = new MoveNpcCharacterHelper(
             Party.will, new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y));

            moveHelper3.OnCompleteEvent += new EventHandler(moveHelper3_OnCompleteEvent);
            helpers.Add(moveHelper3);          
        }

        void moveHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                        "healers_village_mayors_house_f1",
                        Party.cara,
                        Party.Singleton.Leader.TilePosition,
                        ModAction.Remove,
                        true);

            Party.Singleton.ModifyNPC(
                        "healers_village_mayors_house_f1",
                        Party.will,
                        Party.Singleton.Leader.TilePosition,
                        ModAction.Remove,
                        true);

            state = SceneState.Complete;
        }

        #endregion
    }
}