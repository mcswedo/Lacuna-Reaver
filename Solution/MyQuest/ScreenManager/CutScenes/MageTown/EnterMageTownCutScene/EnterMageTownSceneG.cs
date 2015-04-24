using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class EnterMageTownSceneG : Scene
    {
        public EnterMageTownSceneG(Screen screen)
            : base(screen)
        {
        }

        public override void Complete()
        {
            Party.Singleton.ModifyNPC("mage_town", "BoyMage", new Point(20, 27), ModAction.Add, true);
        }
        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Complete(); 
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
