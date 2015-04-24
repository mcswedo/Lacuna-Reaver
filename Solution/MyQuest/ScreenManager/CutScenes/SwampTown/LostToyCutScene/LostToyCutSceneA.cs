using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class LostToyCutSceneA : Scene
    {
        NPCMapCharacter UpsetBoy = Party.Singleton.CurrentMap.GetNPC("UpsetBoy");
        
        public LostToyCutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            //1

            Party.Singleton.ModifyNPC(
               Party.Singleton.CurrentMap.Name,
               "ToyMonster",
               new Point(38, 56),
               ModAction.Add,
               false);

            Party.Singleton.ModifyNPC(
               Party.Singleton.CurrentMap.Name,
               "LostToy",
               new Point(38, 54),
               ModAction.Add,
               false);

            NPCMapCharacter LostToy = Party.Singleton.CurrentMap.GetNPC("LostToy");

            NPCMapCharacter ToyMonster = Party.Singleton.CurrentMap.GetNPC("ToyMonster");

            ToyMonster.FaceDirection(Direction.South);
            ToyMonster.IdleOnly = true;

            SceneHelper nathanHelper0 = new MovePCMapCharacterHelper(
                new Point(14, 37), 
                2.8f);

            helpers.Add(nathanHelper0);

            nathanHelper0.OnCompleteEvent += new EventHandler(nathanHelper0_OnCompleteEvent);
        }

        void nathanHelper0_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.Leader.FaceDirection(Direction.South);
            //SceneHelper camera1 = new MoveCameraHelper(new Point(15, 45), 2.8f);

            SceneHelper nathanHelper = new MovePCMapCharacterHelper(
                new Point(13, 38), 
                2.8f);

            //  helpers.Add(camera1);
            helpers.Add(nathanHelper);

            nathanHelper.OnCompleteEvent += new EventHandler(nathanHelper_OnCompleteEvent);
        }

        void nathanHelper_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.Leader.FaceDirection(Direction.South);

            SceneHelper camera1 = new MoveCameraHelper(
                new Point(15, 45), 
                2.8f);

            SceneHelper upsetBoyHelper1 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(14, 39), 
                2.8f);

            helpers.Add(camera1);
            helpers.Add(upsetBoyHelper1);

            upsetBoyHelper1.OnCompleteEvent += new EventHandler(upsetBoyHelper1_OnCompleteEvent);
        }

        //2
        void upsetBoyHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper2 = new MovePCMapCharacterHelper(
                new Point(13, 39), 
                2.8f);

            //SceneHelper camera1 = new MoveCameraHelper(new Point(14, 49), 2.8f);           

            helpers.Add(nathanHelper2);
            // helpers.Add(camera1);                       

            nathanHelper2.OnCompleteEvent += new EventHandler(upsetBoyHelper2_OnCompleteEvent);
        }

        //3
        void upsetBoyHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper3 = new MovePCMapCharacterHelper(
                new Point(15, 39), 
                2.8f);

            SceneHelper upsetBoyHelper3 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(16, 40), 
                2.8f);

            // SceneHelper camera3 = new MoveCameraHelper(Party.Singleton.Leader.TilePosition, 2.8f);

            helpers.Add(nathanHelper3);
            // helpers.Add(camera3);
            helpers.Add(upsetBoyHelper3);

            upsetBoyHelper3.OnCompleteEvent += new EventHandler(upsetBoyHelper3_OnCompleteEvent);
        }

        //4
        void upsetBoyHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper4 = new MovePCMapCharacterHelper(
                new Point(16, 40), 
                2.8f);

            SceneHelper upsetBoyHelper4 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(16, 42), 
                2.8f);

            // SceneHelper camera4 = new MoveCameraHelper(Party.Singleton.Leader.TilePosition, 2.8f);

            helpers.Add(nathanHelper4);
            // helpers.Add(camera4);
            helpers.Add(upsetBoyHelper4);

            upsetBoyHelper4.OnCompleteEvent += new EventHandler(upsetBoyHelper4_OnCompleteEvent);
        }

        //5
        void upsetBoyHelper4_OnCompleteEvent(object sender, EventArgs e)
        {

            SceneHelper nathanHelper5 = new MovePCMapCharacterHelper(
                new Point(16, 43), 
                2.8f);

            SceneHelper upsetBoyHelper5 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(16, 45), 
                2.8f);

            // SceneHelper camera5 = new MoveCameraHelper(Party.Singleton.Leader.TilePosition, 2.8f);

            helpers.Add(nathanHelper5);
            //helpers.Add(camera5);
            helpers.Add(upsetBoyHelper5);

            upsetBoyHelper5.OnCompleteEvent += new EventHandler(upsetBoyHelper5_OnCompleteEvent);
        }

        //6
        void upsetBoyHelper5_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper6 = new MovePCMapCharacterHelper
                (new Point(18, 45), 
                2.8f);

            SceneHelper upsetBoyHelper6 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(19, 45), 
                2.8f);

            SceneHelper camera6 = new MoveCameraHelper(
                new Point(19, 45), 
                2.8f);

            helpers.Add(nathanHelper6);
            helpers.Add(camera6);
            helpers.Add(upsetBoyHelper6);

            upsetBoyHelper6.OnCompleteEvent += new EventHandler(upsetBoyHelper6_OnCompleteEvent);
        }

        //7        
        void upsetBoyHelper6_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper7 = new MovePCMapCharacterHelper(
                new Point(29, 45), 
                2.8f);

            SceneHelper upsetBoyHelper7 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(30, 45), 
                2.8f);

            SceneHelper camera7 = new MoveCameraHelper(
                new Point(31, 45), 
                2.8f);

            helpers.Add(nathanHelper7);
            helpers.Add(camera7);
            helpers.Add(upsetBoyHelper7);

            upsetBoyHelper7.OnCompleteEvent += new EventHandler(upsetBoyHelper7_OnCompleteEvent);
        }

        //8
        void upsetBoyHelper7_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper8 = new MovePCMapCharacterHelper(
                new Point(29, 47), 
                2.8f);

            SceneHelper upsetBoyHelper8 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(30, 48), 
                2.8f);

            //SceneHelper camera8 = new MoveCameraHelper(Party.Singleton.Leader.TilePosition, 2.8f);

            helpers.Add(nathanHelper8);
            //helpers.Add(camera8);
            helpers.Add(upsetBoyHelper8);

            upsetBoyHelper8.OnCompleteEvent += new EventHandler(upsetBoyHelper8_OnCompleteEvent);
        }

        //9
        void upsetBoyHelper8_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper9 = new MovePCMapCharacterHelper
                (new Point(30, 59), 
                2.8f);

            SceneHelper upsetBoyHelper9 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(30, 60), 
                2.8f);

            SceneHelper camera9 = new MoveCameraHelper(
                new Point(31, 59), 
                2.8f);

            helpers.Add(camera9);
            helpers.Add(nathanHelper9);

            helpers.Add(upsetBoyHelper9);

            upsetBoyHelper9.OnCompleteEvent += new EventHandler(upsetBoyHelper9_OnCompleteEvent);
        }

        //10
        void upsetBoyHelper9_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper10 = new MovePCMapCharacterHelper(
                new Point(32, 59), 
                2.8f);

            SceneHelper upsetBoyHelper10 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(33, 60), 
                2.8f);

            //SceneHelper camera10 = new MoveCameraHelper(Party.Singleton.Leader.TilePosition, 2.8f);

            helpers.Add(nathanHelper10);
            //helpers.Add(camera10);
            helpers.Add(upsetBoyHelper10);

            upsetBoyHelper10.OnCompleteEvent += new EventHandler(upsetBoyHelper10_OnCompleteEvent);
        }

        //11      
        void upsetBoyHelper10_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper nathanHelper11 = new MovePCMapCharacterHelper(
                new Point(35, 59), 
                2.8f);

            SceneHelper upsetBoyHelper10 = new MoveNpcCharacterHelper(
                "UpsetBoy", 
                new Point(37, 60), 
                2.8f);

            SceneHelper camera10 = new MoveCameraHelper(
                new Point(36, 60), 
                2.8f);

            helpers.Add(nathanHelper11);
            helpers.Add(camera10);
            helpers.Add(upsetBoyHelper10);

            upsetBoyHelper10.OnCompleteEvent += new EventHandler(upsetBoyHelper11_OnCompleteEvent);
        }

        void upsetBoyHelper11_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.Leader.FaceDirection(Direction.East);
            UpsetBoy.IdleOnly = true;
            UpsetBoy.FaceDirection(Direction.West);
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
