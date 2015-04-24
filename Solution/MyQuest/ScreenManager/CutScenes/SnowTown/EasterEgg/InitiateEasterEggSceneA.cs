using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class InitiateEasterEggSceneA : Scene
    {
        NPCMapCharacter stub = Party.Singleton.CurrentMap.GetNPC("Stub");

        public InitiateEasterEggSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            stub.FaceDirection(Direction.West);
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
            state = SceneState.Complete;
        }
    }
}
