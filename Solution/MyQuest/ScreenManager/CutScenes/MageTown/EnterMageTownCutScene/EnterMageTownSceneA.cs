using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class EnterMageTownSceneA : Scene
    {
        public EnterMageTownSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

            MoveCameraHelper cameraHelper = new MoveCameraHelper(new Point(12, 58), 9999f);

            MovePCMapCharacterHelper helper = new MovePCMapCharacterHelper(
              new Point(12, 57)); 

            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
                      "Ruith", new Point(13, 56), 2.0f);

            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(cameraHelper);
            helpers.Add(helper);
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
            Party.Singleton.CurrentMap.ResetSpawnDirection("Ruith", Direction.South);
            state = SceneState.Complete;        
        }

    }
}
