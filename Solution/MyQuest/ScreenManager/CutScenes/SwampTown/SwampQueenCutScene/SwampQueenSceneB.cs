using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class SwampQueenSceneB : Scene
    {
        NPCMapCharacter will;
        NPCMapCharacter cara;

        Dialog dialog; 

        #region Dialog

        static readonly Dialog queenDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z876, Strings.Z877);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z878, Strings.Z879, Strings.Z880);

        static readonly Dialog queenDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z881, Strings.Z882, Strings.Z883);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z884);

        static readonly Dialog queenDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z885, Strings.Z886, Strings.Z887, Strings.Z888, Strings.Z889, Strings.Z890, Strings.Z891, Strings.Z892, Strings.Z893, Strings.Z894);

        static readonly Dialog caraDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z895);

        static readonly Dialog queenDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z896, Strings.Z897, Strings.ZA563, Strings.Z898, Strings.Z899);

        static readonly Dialog caraDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z900);
          
        #endregion

        public SwampQueenSceneB(Screen screen)
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
              new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y - 1),
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
             new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y + 1),
             1.7f);

            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);

            moveHelper2.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
            will.FaceDirection(Direction.North);
            helpers.Add(moveHelper2);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {      
            cara.FaceDirection(Direction.East);

            dialog = queenDialog;

            dialog.DialogCompleteEvent += carasResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "SwampQueenSick"));

        }
        void carasResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse;

            dialog = caraDialog;

            dialog.DialogCompleteEvent += queensResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }
        void queensResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= queensResponse;

            dialog = queenDialog2;

            dialog.DialogCompleteEvent += carasResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "SwampQueenSick"));
        }
        void carasResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse2;

            dialog = caraDialog2;

            dialog.DialogCompleteEvent += queensResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }
        void queensResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= queensResponse2;

            dialog = queenDialog3;

            dialog.DialogCompleteEvent += carasResponse3; 
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "SwampQueenSick"));

            Party.Singleton.AddLogEntry("Chapaka", "SwampQueen", Strings.Z896);
        }

        void carasResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse3;

            dialog = caraDialog3; 
            
            dialog.DialogCompleteEvent += queensResponse3;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void queensResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= queensResponse3;

            dialog = queenDialog4;
            dialog.DialogCompleteEvent += carasResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "SwampQueenSick"));
        }

        void carasResponse4(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse4;

            dialog = caraDialog4; 

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));

            state = SceneState.Complete;
        }
      
        #endregion
    }
}