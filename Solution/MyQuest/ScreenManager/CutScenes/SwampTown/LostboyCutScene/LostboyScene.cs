using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class  LostboyScene : Scene
    {
        NPCMapCharacter LostBoy = Party.Singleton.CurrentMap.GetNPC("LostBoy");

        #region Dialog

        static readonly Dialog MomDialog1 = new Dialog(
           DialogPrompt.NeedsClose,
           "My Son, Thank You!");
        static readonly Dialog LostBoyDialog2 = new Dialog(
           DialogPrompt.NeedsClose,
           " You know where my mom is? Show me!");
        /*static readonly Dialog willDialog1 = new Dialog(
           DialogPrompt.NeedsClose,
           "It's always pitch black in my case! Shut up and follow me.");

        static readonly Dialog caraDialog2 = new Dialog(
           DialogPrompt.NeedsClose,
           "This feels weird. I feel like I'm turning upside down.");

        static readonly Dialog willDialog2 = new Dialog(
           DialogPrompt.NeedsClose,
           "Hey, didn't I tell you to shut up?",
           "I know what you mean though, something doesn't feel right.");

        static readonly Dialog willDialog3 = new Dialog(
           DialogPrompt.NeedsClose,
           "It's this way, follow me!");*/

        #endregion

        public  LostboyScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
           // ScreenManager.Singleton.AddScreen(new DialogScreen(omDialog1, DialogScreen.Location.BottomLeft, "Cara"));
            LostBoy.IdleOnly = false;
            ScreenManager.Singleton.AddScreen(new DialogScreen(LostBoyDialog2, DialogScreen.Location.TopLeft, NPCPool.stub));
            SceneHelper nathanHelper1 = new MovePCMapCharacterHelper(new Point(58, 57), 2.8f);
           

           // ;
            helpers.Add(nathanHelper1);

           

            nathanHelper1.OnCompleteEvent += new EventHandler(nathanHelper1_OnCompleteEvent);
        }

        void nathanHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
          
            //Party.Singleton.CurrentMap.GetNPC("LostBoy").Is = false;
            MoveCameraHelper helper = new MoveCameraHelper(new Point(42, 67 ), 2.8f);
           
            LostBoy.IdleOnly = false;
           // SceneHelper LostBoyHelper1 = new MoveNpcCharacterHelper("LostBoy", new Point(45,67), 2.8f);
            
            SceneHelper nathanHelper2 = new MovePCMapCharacterHelper(new Point(55, 57), 2.8f);
            
            
            
            helpers.Add(nathanHelper2);
            helpers.Add(helper);
            //helpers.Add(LostBoyHelper1);
            nathanHelper2.OnCompleteEvent += new EventHandler(nathanHelper2_OnCompleteEvent);
           
        
                      
        }

       void nathanHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            
            SceneHelper LostBoyHelper1 = new MoveNpcCharacterHelper("LostBoy", new Point(42, 67), 2.8f);
            SceneHelper nathanHelper3 = new MovePCMapCharacterHelper(new Point(41, 67), 2.8f);
            helpers.Add(nathanHelper3); 
           helpers.Add(LostBoyHelper1);
           nathanHelper3.OnCompleteEvent += new EventHandler(nathanHelper3_OnCompleteEvent);
        }

        void nathanHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
            
            
          
            ScreenManager.Singleton.AddScreen(new DialogScreen(MomDialog1, DialogScreen.Location.BottomRight, NPCPool.stub));
            state = SceneState.Complete; 
            //LostBoy.IdleOnly = true;
           
        }

       /* void nathanHelper4_OnCompleteEvent(object sender, EventArgs e)
        {
           
        }

     /*   void willHelper5_OnCompleteEvent(object sender, EventArgs e)
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

            
            state = SceneState.Complete;
        }

      */  public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}