using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class WillEntersMageTownSceneA : Scene
    {
        Portal portal;
        NPCMapCharacter will = Party.Singleton.CurrentMap.GetNPC("Will");

        #region Dialog

        static readonly Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z727);

        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z728);

        static readonly Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z729);

        static readonly Dialog willDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z730);

        static readonly Dialog willDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z731);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z732);

        static readonly Dialog caraDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z733);

        static readonly Dialog nathanDialog1 = new Dialog(DialogPrompt.NeedsClose, "?");

        #endregion

        public WillEntersMageTownSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper nathanHelper1 = new MovePCMapCharacterHelper(new Point(5,16), 2.8f);


            nathanHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(nathanHelper1);

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
            Party.Singleton.Leader.FaceDirection(Direction.North);

            SceneHelper caraHelper = new MoveNpcCharacterHelper(
                "Cara",
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                false,
                new Point(Party.Singleton.Leader.TilePosition.X - 2, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            SceneHelper willHelper = new MoveNpcCharacterHelper(
                "Will",
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                false,
                new Point(Party.Singleton.Leader.TilePosition.X + 2, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            caraHelper.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);

            helpers.Add(caraHelper);
            helpers.Add(willHelper);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);
            Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.North);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog1, DialogScreen.Location.TopRight, "Will"));
            willDialog1.DialogCompleteEvent += willDialog;
        }

        void willDialog(object sender, EventArgs e)
        {
            willDialog1.DialogCompleteEvent -= willDialog;


            ScreenManager.Singleton.AddScreen(
               new DialogScreen(caraDialog1, DialogScreen.Location.TopLeft, "Cara"));
            caraDialog1.DialogCompleteEvent += moveWill1;
        }

        void moveWill1(object sender, EventArgs e)
        {
            caraDialog1.DialogCompleteEvent -= moveWill1;

            SceneHelper willHelper2 = new MoveNpcCharacterHelper("Will", new Point(6 , 14), 1.7f);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog2, DialogScreen.Location.TopRight, "Will"));

            helpers.Add(willHelper2);

            willHelper2.OnCompleteEvent += new EventHandler(willHelper);
        }

        void willHelper(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.East);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog3, DialogScreen.Location.TopRight, "Will"));

            willDialog3.DialogCompleteEvent += willHelper2_OnCompleteEvent;
        }

        void willHelper2_OnCompleteEvent(object sender, PartyResponseEventArgs e)
        {

            SceneHelper willHelper3 = new MoveNpcCharacterHelper("Will", new Point(4, 14), 1.7f);
            helpers.Add(willHelper3);

            willHelper3.OnCompleteEvent += new EventHandler(willHelper3_OnCompleteEvent);
        }

        void willHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper willHelper4 = new MoveNpcCharacterHelper("Will", new Point(5, 14), 1.7f);
            helpers.Add(willHelper4);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog4, DialogScreen.Location.TopRight, "Will"));

            willHelper4.OnCompleteEvent += new EventHandler(willHelper4_OnCompleteEvent);
        }

        void willHelper4_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper willHelper5 = new MoveNpcCharacterHelper("Will", new Point(5, 13), 1.7f);
            helpers.Add(willHelper5);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(caraDialog2, DialogScreen.Location.TopLeft, "Cara"));

            willHelper5.OnCompleteEvent += new EventHandler(willHelper5_OnCompleteEvent);
        }

        void willHelper5_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Will",
                Point.Zero,
                ModAction.Remove,
                true);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(caraDialog3, DialogScreen.Location.TopLeft, "Cara"));

            caraDialog3.DialogCompleteEvent += MoveCara;
        }

        void MoveCara(object sender, EventArgs e)
        {
            SceneHelper caraHelper2 = new MoveNpcCharacterHelper("Cara", new Point(5, 14), 4f);
            helpers.Add(caraHelper2);

            caraHelper2.OnCompleteEvent += new EventHandler(caraHelper2_OnCompleteEvent);
        }

        void caraHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper caraHelper3 = new MoveNpcCharacterHelper(Party.cara, new Point(5, 13), 4f);
            helpers.Add(caraHelper3);

            caraHelper3.OnCompleteEvent += new EventHandler(caraHelper3_OnCompleteEvent);
        }

        void caraHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Cara",
                Point.Zero,
                ModAction.Remove,
                true);

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(nathanDialog1, DialogScreen.Location.TopLeft, "Nathan"));

            nathanDialog1.DialogCompleteEvent += MoveNathan;
        }

        void MoveNathan(object sender, EventArgs e)
        {
            SceneHelper nathanHelper2 = new MovePCMapCharacterHelper(new Point(5,13), 2.8f);
            helpers.Add(nathanHelper2);

            portal = new Portal { DestinationMap = "mage_town_transition", DestinationPosition = new Point(5, 14), Position = Point.Zero };
            nathanHelper2.OnCompleteEvent += new EventHandler(nathanHelper2_OnCompleteEvent);
        }

        void nathanHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.PortalToMap(portal);
            state = SceneState.Complete;
        }

        #endregion


    }
}
