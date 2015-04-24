using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ChepetawaSceneB : Scene
    {
        NPCMapCharacter will;
        NPCMapCharacter cara;

        Dialog dialog; 

        #region Dialog

        static readonly Dialog chepetawaDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z863);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z864);

        static readonly Dialog chepetawaDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z865, Strings.Z866);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z867);

        static readonly Dialog chepetawaDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z868, Strings.Z869, Strings.Z870);

        static readonly Dialog willDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z871);

        static readonly Dialog chepetawaDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z872);

        static readonly Dialog chepetawaDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z873);
          
        #endregion

        public ChepetawaSceneB(Screen screen)
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

            dialog = chepetawaDialog;

            dialog.DialogCompleteEvent += carasResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Chepetawa"));

        }

        void carasResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse;

            dialog = caraDialog;

            dialog.DialogCompleteEvent += chepetawasResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void chepetawasResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= chepetawasResponse;

            dialog = chepetawaDialog2;

            dialog.DialogCompleteEvent += carasResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Chepetawa"));
        }

        void carasResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse2;

            dialog = caraDialog2;

            dialog.DialogCompleteEvent += chepetawasResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void chepetawasResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= chepetawasResponse2;

            dialog = chepetawaDialog3;

            dialog.DialogCompleteEvent += willsResponse; 
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Chepetawa"));
        }

        void willsResponse(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= willsResponse;

            dialog = willDialog4;

            dialog.DialogCompleteEvent += chepetawasResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void chepetawasResponse3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= chepetawasResponse3;

            dialog = chepetawaDialog4;

            dialog.DialogCompleteEvent += chepetawaSummons;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Chepetawa"));
        }

        void chepetawaSummons(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= chepetawaSummons;

            dialog = chepetawaDialog5;

            Party.Singleton.CurrentMap.GetNPC("Chepetawa").FaceDirection(Direction.West);

            Party.Singleton.CurrentMap.ResetSpawnDirection("Chepetawa", Direction.West);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Chepetawa"));

            state = SceneState.Complete;
        }
      
        #endregion
    }
}