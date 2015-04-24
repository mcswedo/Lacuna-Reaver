using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ChepetawaSceneA : Scene
    {
        public ChepetawaSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {          
            SceneHelper moveHelper1 = new MovePCMapCharacterHelper(new Point(4, 3));

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
            state = SceneState.Complete;
        }
    }
}