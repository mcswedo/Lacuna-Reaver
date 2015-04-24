using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class ArrivedAtCastleSceneA : Scene
    {
        public ArrivedAtCastleSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.CurrentMap.MusicCueName = AudioCues.agoraCastle;
            state = SceneState.Complete;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}
