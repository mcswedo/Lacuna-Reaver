using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class EnterMageTownSceneC : Scene
    {
        public EnterMageTownSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
                  "Lydia", new Point(14, 55), 5.0f);

            SceneHelper moveHelper2 = new MoveNpcCharacterHelper(
                  "Burlam", new Point(11, 55), 5.0f);

            moveHelper2.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
            helpers.Add(moveHelper1);
            helpers.Add(moveHelper2);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Lydia").FaceDirection(Direction.South);
            Party.Singleton.CurrentMap.GetNPC("Burlam").FaceDirection(Direction.South);
            Party.Singleton.CurrentMap.ResetSpawnDirection("Lydia", Direction.South);
            Party.Singleton.CurrentMap.ResetSpawnDirection("Burlam", Direction.South);
            Party.Singleton.CurrentMap.ResetSpawnDirection("Will", Direction.East);
            state = SceneState.Complete;
        }
    }
}
