using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class WillBoxGameCutSceneA : Scene
    {
        float alpha = 0;
        float deltaAlpha = .03f;

        NPCMapCharacter BoxGame1 = Party.Singleton.CurrentMap.GetNPC("BoxGame1");
        NPCMapCharacter BoxGame2 = Party.Singleton.CurrentMap.GetNPC("BoxGame2");
        NPCMapCharacter BoxGame3 = Party.Singleton.CurrentMap.GetNPC("BoxGame3");

        SceneHelper willHelper;

        SceneHelper chest1Helper0;
        SceneHelper chest2Helper0;
        SceneHelper chest3Helper0;

        SceneHelper chest1Helper1;
        SceneHelper chest2Helper1;
        SceneHelper chest3Helper1;

        SceneHelper chest1Helper2;
        SceneHelper chest2Helper2;
        SceneHelper chest3Helper2;

        SceneHelper chest1Helper3;
        SceneHelper chest2Helper3;
        SceneHelper chest3Helper3;

        SceneHelper chest1Helper4;
        SceneHelper chest2Helper4;
        SceneHelper chest3Helper4;

        SceneHelper chest1Helper5;

        SceneHelper chest2Helper6;
        SceneHelper chest3Helper6;

        public WillBoxGameCutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }
        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "EmptyBoxGame3",
                Point.Zero,
                ModAction.Remove,
                false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest1",
                new Point(7, 26),
                ModAction.Add,
                false);

            willHelper = new MovePCMapCharacterHelper(new Point(8, 27), 2.8f);
            
            helpers.Add(willHelper);

            willHelper.OnCompleteEvent += new EventHandler(chest3Helper_OnCompleteEvent);
        }


        # region Callbacks

        void chest3Helper_OnCompleteEvent(object sender, EventArgs e)
          {
              willHelper.OnCompleteEvent -= new EventHandler(chest3Helper_OnCompleteEvent);

            //remove empty chests 
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "EmptyBoxGame1",
                Point.Zero,
                ModAction.Remove,
                false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "EmptyBoxGame2",
                Point.Zero,
                ModAction.Remove,
                false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "OpenChest1",
                Point.Zero,
                ModAction.Remove,
                false);


            //add chests for game
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "BoxGame1",
                new Point(3, 26),
                ModAction.Add,
                false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "BoxGame2",
                new Point(5, 26),
                ModAction.Add,
                false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "BoxGame3",
                new Point(7, 26),
                ModAction.Add,
                false);

            chest1Helper0 = new MoveNpcCharacterHelper("BoxGame1", new Point(7, 26), 2.8f);
            chest2Helper0 = new MoveNpcCharacterHelper("BoxGame2", new Point(3, 26), 2.8f);
            chest3Helper0 = new MoveNpcCharacterHelper("BoxGame3", new Point(5, 26), 2.8f);

            helpers.Add(chest1Helper0);
            helpers.Add(chest2Helper0);
            helpers.Add(chest3Helper0);

            chest1Helper0.OnCompleteEvent += new EventHandler(chest3Helper0_OnCompleteEvent);
        }


        void chest3Helper0_OnCompleteEvent(object sender, EventArgs e)
        {
            chest3Helper0.OnCompleteEvent -= new EventHandler(chest3Helper0_OnCompleteEvent);

            chest1Helper1 = new MoveNpcCharacterHelper("BoxGame1", new Point(7, 26), 2.8f);
            chest2Helper1 = new MoveNpcCharacterHelper("BoxGame2", new Point(5, 26), 2.8f);
            chest3Helper1 = new MoveNpcCharacterHelper("BoxGame3", new Point(3, 26), 2.8f);

            helpers.Add(chest1Helper1);
            helpers.Add(chest2Helper1);
            helpers.Add(chest3Helper1);

            chest2Helper1.OnCompleteEvent += new EventHandler(chest2Helper1_OnCompleteEvent);
        }

        void chest2Helper1_OnCompleteEvent(object sender, EventArgs e)
        {
            chest2Helper1.OnCompleteEvent -= new EventHandler(chest2Helper1_OnCompleteEvent);

            chest1Helper2 = new MoveNpcCharacterHelper("BoxGame1", new Point(6, 27), 2.8f);
            chest2Helper2 = new MoveNpcCharacterHelper("BoxGame2", new Point(7, 26), 2.8f);
            chest3Helper2 = new MoveNpcCharacterHelper("BoxGame3", new Point(3, 26), 2.8f);
            
            helpers.Add(chest1Helper2);
            helpers.Add(chest2Helper2);
            helpers.Add(chest3Helper2);

            chest1Helper2.OnCompleteEvent += new EventHandler(chest1Helper2_OnCompleteEvent);
        }

        void chest1Helper2_OnCompleteEvent(object sender, EventArgs e)
        {
            chest1Helper2.OnCompleteEvent -= new EventHandler(chest1Helper2_OnCompleteEvent);

            chest1Helper3 = new MoveNpcCharacterHelper("BoxGame1", new Point(5, 26), 2.8f);

            helpers.Add(chest1Helper3);

            chest1Helper3.OnCompleteEvent += new EventHandler(chest1Helper3_OnCompleteEvent);
        }

        void chest1Helper3_OnCompleteEvent(object sender, EventArgs e)
        {
            chest1Helper3.OnCompleteEvent -= new EventHandler(chest1Helper3_OnCompleteEvent);

            chest2Helper3 = new MoveNpcCharacterHelper("BoxGame2", new Point(5, 27), 2.8f);
            chest3Helper3 = new MoveNpcCharacterHelper("BoxGame3", new Point(5, 28), 2.8f);

            helpers.Add(chest2Helper3);
            helpers.Add(chest3Helper3);

            chest3Helper3.OnCompleteEvent += new EventHandler(chest2Helper3_OnCompleteEvent);
        }

        void chest2Helper3_OnCompleteEvent(object sender, EventArgs e)
        {
            chest2Helper3.OnCompleteEvent -= new EventHandler(chest2Helper3_OnCompleteEvent);

            chest2Helper4 = new MoveNpcCharacterHelper("BoxGame2", new Point(3, 26), 2.8f);
            chest3Helper4 = new MoveNpcCharacterHelper("BoxGame3", new Point(7, 26), 2.8f);
            chest1Helper4 = new MoveNpcCharacterHelper("BoxGame1", new Point(5, 27), 2.8f);

            helpers.Add(chest2Helper4);
            helpers.Add(chest3Helper4);
            helpers.Add(chest1Helper4);

            chest3Helper4.OnCompleteEvent += new EventHandler(chest2Helper4_OnCompleteEvent);
        }

        void chest2Helper4_OnCompleteEvent(object sender, EventArgs e)
        {
            chest2Helper4.OnCompleteEvent -= new EventHandler(chest2Helper4_OnCompleteEvent);

            chest1Helper5 = new MoveNpcCharacterHelper("BoxGame1", new Point(5, 26), 2.8f);
   
            helpers.Add(chest1Helper5);

            chest1Helper5.OnCompleteEvent += new EventHandler(chest1Helper5_OnCompleteEvent);
        }

        void chest1Helper5_OnCompleteEvent(object sender, EventArgs e)
        {
            chest1Helper5.OnCompleteEvent -= new EventHandler(chest1Helper5_OnCompleteEvent);

            chest2Helper6 = new MoveNpcCharacterHelper("BoxGame2", new Point(7, 26), 2.8f);
            chest3Helper6 = new MoveNpcCharacterHelper("BoxGame3", new Point(3, 26), 2.8f);

            helpers.Add(chest2Helper6);
            helpers.Add(chest3Helper6);

            chest3Helper6.OnCompleteEvent += new EventHandler(end_OnCompleteEvent);
        }

        void end_OnCompleteEvent(object sender, EventArgs e)
        {
            chest3Helper6.OnCompleteEvent -= new EventHandler(end_OnCompleteEvent);

            NPCMapCharacter BoxGame1 = Party.Singleton.CurrentMap.GetNPC("BoxGame1");
            NPCMapCharacter BoxGame2 = Party.Singleton.CurrentMap.GetNPC("BoxGame2");
            NPCMapCharacter BoxGame3 = Party.Singleton.CurrentMap.GetNPC("BoxGame3");

            BoxGame1.IdleOnly = true;
            BoxGame2.IdleOnly = true;
            BoxGame3.IdleOnly = true;

            Complete();

            state = SceneState.Complete;
        }

        #endregion


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            alpha += deltaAlpha;

            if (alpha > 1)
            {
                alpha = 1;
                deltaAlpha = -deltaAlpha;
            }
            else if (alpha < 0)
            {
                alpha = 0;
                deltaAlpha = -deltaAlpha;
            }

            ScreenManager.Singleton.TintBackBuffer(alpha, Color.Black, spriteBatch);
        }
    }
}
