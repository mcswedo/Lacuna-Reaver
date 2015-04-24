using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class WillEntersMageTownSceneB : Scene
    {
        Portal portal;
        NPCMapCharacter will = Party.Singleton.CurrentMap.GetNPC("Will");
        NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC("Cara");

        #region Dialog

        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z734);

        static readonly Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z735);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z736);

        static readonly Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z737, Strings.Z738);

        static readonly Dialog willDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z739);

        #endregion

        public WillEntersMageTownSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(caraDialog1, DialogScreen.Location.BottomLeft, "Cara"));

            SceneHelper willHelper1 = new MoveNpcCharacterHelper("Will", new Point(8,9), 2.8f);
            SceneHelper caraHelper1 = new MoveNpcCharacterHelper("Cara", new Point(7, 9), 2.8f);
            SceneHelper nathanHelper1 = new MovePCMapCharacterHelper(new Point(5, 9), 2.8f);

            helpers.Add(willHelper1);
            helpers.Add(caraHelper1);
            helpers.Add(nathanHelper1);

            Party.Singleton.Leader.IsCastingShadow = false;
            Party.Singleton.CurrentMap.GetNPC("Will").IsCastingShadow = false;
            Party.Singleton.CurrentMap.GetNPC("Cara").IsCastingShadow = false;

            willHelper1.OnCompleteEvent += new EventHandler(willHelper1_OnCompleteEvent);
        }

        void willHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(willDialog1, DialogScreen.Location.BottomRight, "Will"));

            SceneHelper willHelper2 = new MoveNpcCharacterHelper("Will", new Point(8, 10), 2.8f);
            SceneHelper caraHelper2 = new MoveNpcCharacterHelper("Cara", new Point(8, 9), 2.8f);
            SceneHelper nathanHelper2 = new MovePCMapCharacterHelper(new Point(6, 9), 2.8f);

            helpers.Add(willHelper2);
            helpers.Add(caraHelper2);
            helpers.Add(nathanHelper2);

            willHelper2.OnCompleteEvent += new EventHandler(willHelper2_OnCompleteEvent);
        }

        void willHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper willHelper3 = new MoveNpcCharacterHelper("Will", new Point(8, 12), 2.8f);
            SceneHelper caraHelper3 = new MoveNpcCharacterHelper("Cara", new Point(8, 11), 2.8f);
            SceneHelper nathanHelper3 = new MovePCMapCharacterHelper(new Point(8, 9), 2.8f);

            helpers.Add(willHelper3);
            helpers.Add(caraHelper3);
            helpers.Add(nathanHelper3);

            willHelper3.OnCompleteEvent += new EventHandler(willHelper3_OnCompleteEvent);
        }

        void willHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper willHelper4 = new MoveNpcCharacterHelper("Will", new Point(2, 15), 2.8f);
            SceneHelper caraHelper4 = new MoveNpcCharacterHelper("Cara", new Point(3, 15), 2.8f);
            SceneHelper nathanHelper4 = new MovePCMapCharacterHelper(new Point(5, 15), 2.8f);

            helpers.Add(willHelper4);
            helpers.Add(caraHelper4);
            helpers.Add(nathanHelper4);

            willHelper4.OnCompleteEvent += new EventHandler(willHelper4_OnCompleteEvent);
        }

        void willHelper4_OnCompleteEvent(object sender, EventArgs e)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(caraDialog2, DialogScreen.Location.TopLeft, "Cara"));

            SceneHelper willHelper5 = new MoveNpcCharacterHelper("Will", new Point(2, 14), 2.8f);
            SceneHelper caraHelper5 = new MoveNpcCharacterHelper("Cara", new Point(2, 15), 2.8f);
            SceneHelper nathanHelper5 = new MovePCMapCharacterHelper(new Point(4, 15), 2.8f);

            helpers.Add(willHelper5);
            helpers.Add(caraHelper5);
            helpers.Add(nathanHelper5);

            willHelper5.OnCompleteEvent += new EventHandler(willHelper5_OnCompleteEvent);
        }

        void willHelper5_OnCompleteEvent(object sender, EventArgs e)
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(willDialog2, DialogScreen.Location.TopRight, "Will"));

            SceneHelper willHelper6 = new MoveNpcCharacterHelper("Will", new Point(2, 12), 2.8f);
            SceneHelper caraHelper6 = new MoveNpcCharacterHelper("Cara", new Point(2, 13), 2.8f);
            SceneHelper nathanHelper6 = new MovePCMapCharacterHelper(new Point(2, 15), 2.8f);

            helpers.Add(willHelper6);
            helpers.Add(caraHelper6);
            helpers.Add(nathanHelper6);

            willHelper6.OnCompleteEvent += new EventHandler(willHelper6_OnCompleteEvent);
        }

        void willHelper6_OnCompleteEvent(object sender, EventArgs e)
        {
            MoveCameraHelper cameraHelper = new MoveCameraHelper(new Point(2, 10), 2.8f);

            SceneHelper willHelper7 = new MoveNpcCharacterHelper("Will", new Point(2, 7), 2.8f);
            SceneHelper caraHelper7 = new MoveNpcCharacterHelper("Cara", new Point(2, 8), 2.8f);
            SceneHelper nathanHelper7 = new MovePCMapCharacterHelper(new Point(2, 10), 2.8f);

            helpers.Add(willHelper7);
            helpers.Add(caraHelper7);
            helpers.Add(nathanHelper7);
            helpers.Add(cameraHelper);
        
            willHelper7.OnCompleteEvent += new EventHandler(willHelper7_OnCompleteEvent);
        }

        void willHelper7_OnCompleteEvent(object sender, EventArgs e)
        {
            MoveCameraHelper cameraHelper = new MoveCameraHelper(Party.Singleton.Leader.TilePosition, 2.8f);
            ScreenManager.Singleton.AddScreen(new DialogScreen(willDialog3, DialogScreen.Location.TopRight, "Will"));

            SceneHelper willHelper8 = new MoveNpcCharacterHelper("Will", new Point(5, 7), 2.8f);
            SceneHelper caraHelper8 = new MoveNpcCharacterHelper("Cara", new Point(4, 7), 2.8f);
            SceneHelper nathanHelper8 = new MovePCMapCharacterHelper(new Point(2, 7), 2.8f);

            helpers.Add(willHelper8);
            helpers.Add(caraHelper8);
            helpers.Add(nathanHelper8);
            helpers.Add(cameraHelper);

            willHelper8.OnCompleteEvent += new EventHandler(willHelper8_OnCompleteEvent);
        }

        void willHelper8_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper willHelper9 = new MoveNpcCharacterHelper("Will", new Point(5, 6), 2.8f);
            SceneHelper caraHelper9 = new MoveNpcCharacterHelper("Cara", new Point(5, 7), 2.8f);
            SceneHelper nathanHelper9 = new MovePCMapCharacterHelper(new Point(3, 7), 2.8f);

            helpers.Add(willHelper9);
            helpers.Add(caraHelper9);
            helpers.Add(nathanHelper9);

            willHelper9.OnCompleteEvent += new EventHandler(willHelper9_OnCompleteEvent);
        }

        void willHelper9_OnCompleteEvent(object sender, EventArgs e)
        {

            Party.Singleton.CurrentMap.GetNPC("Will").IsCastingShadow = true;

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                Party.will,
                Point.Zero,
                ModAction.Remove,
                false);

            SceneHelper caraHelper10 = new MoveNpcCharacterHelper("Cara", new Point(5, 6), 2.8f);
            SceneHelper nathanHelper10 = new MovePCMapCharacterHelper(new Point(4, 7), 2.8f);

            helpers.Add(caraHelper10);
            helpers.Add(nathanHelper10);

            caraHelper10.OnCompleteEvent += new EventHandler(caraHelper10_OnCompleteEvent);
        }

        void caraHelper10_OnCompleteEvent(object sender, EventArgs e)
        {

            Party.Singleton.CurrentMap.GetNPC("Cara").IsCastingShadow = true;

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                Party.cara,
                Point.Zero,
                ModAction.Remove,
                false);

            SceneHelper nathanHelper11 = new MovePCMapCharacterHelper(new Point(4, 7), 2.8f);

            helpers.Add(nathanHelper11);

            nathanHelper11.OnCompleteEvent += new EventHandler(nathanHelper11_OnCompleteEvent);
        }


        void nathanHelper11_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper12 = new MovePCMapCharacterHelper(new Point(5, 7), 2.8f);

            helpers.Add(nathanHelper12);

            nathanHelper12.OnCompleteEvent += new EventHandler(nathanHelper12_OnCompleteEvent);
        }

        void nathanHelper12_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper13 = new MovePCMapCharacterHelper(new Point(5, 6), 2.8f);
            portal = new Portal { DestinationMap = "mage_town", DestinationPosition = new Point(12, 58), Position = Point.Zero };

            helpers.Add(nathanHelper13);

            nathanHelper13.OnCompleteEvent += new EventHandler(nathanHelper13_OnCompleteEvent);
        }

        void nathanHelper13_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.PortalToMap(portal);
            Party.Singleton.Leader.IsCastingShadow = true;

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "EnterMageTownCutSceneScreen"));
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