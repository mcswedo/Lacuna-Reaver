using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class PartyRejoinScene : Scene
    {
        
        public PartyRejoinScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
                      "Cara", Party.Singleton.Leader.TilePosition, 1.5f);

            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(moveHelper1);
       
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC("possessed_library_5", "Cara", Point.Zero, ModAction.Remove, true);
            SceneHelper moveHelper2 = new MoveNpcCharacterHelper(
             "Will", Party.Singleton.Leader.TilePosition, 1.5f);
            moveHelper2.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
            helpers.Add(moveHelper2);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC("possessed_library_5", "Will", Point.Zero, ModAction.Remove, true);
            state = SceneState.Complete;
        }
    }
}