using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class EnterMageTownSceneF : Scene
    {
        public EnterMageTownSceneF(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {         
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
                "Burlam", new Point(7, 55), 3.0f);


            SceneHelper moveHelper2 = new MoveNpcCharacterHelper(
                 "Lydia", new Point(18, 55), 3.0f);    

            SceneHelper moveHelper3 = new MoveNpcCharacterHelper(
                 "BoyMage", new Point(7, 56), 3.0f);

            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
       
            helpers.Add(moveHelper1);
            helpers.Add(moveHelper2);
            helpers.Add(moveHelper3);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
               "Endor", new Point(12, 53), 1.0f);

            SceneHelper moveHelper2 = new MoveNpcCharacterHelper(
               "Burlam", new Point(7, 47), 3.0f);

            SceneHelper moveHelper3 = new MoveNpcCharacterHelper(
               "Lydia", new Point(18, 47), 3.0f);

            //SceneHelper moveHelper4 = new MoveNpcCharacterHelper(
            // "BoyMage", new Point(7, 47), 3.0f);

            helpers.Add(moveHelper1);
            helpers.Add(moveHelper2);
            helpers.Add(moveHelper3);
            //helpers.Add(moveHelper4);

            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper3_OnCompleteEvent);
        }

        void moveHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
        //    Party.Singleton.ModifyNPC("mage_town", "BoyMage", Point.Zero, ModAction.Remove, true);
        //    Party.Singleton.ModifyNPC("mage_town", "Lydia", Point.Zero, ModAction.Remove, true);
        //    Party.Singleton.ModifyNPC("mage_town", "Burlam", Point.Zero, ModAction.Remove, true);
              Party.Singleton.ModifyNPC("mage_town", "Endor", Point.Zero, ModAction.Remove, true);
              state = SceneState.Complete;
        }
    }
}
