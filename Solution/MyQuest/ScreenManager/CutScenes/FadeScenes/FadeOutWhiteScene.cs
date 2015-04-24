using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    public class FadeOutWhiteScene : Scene
    {
        float alpha;     
        public FadeOutWhiteScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;
            alpha = 0;

            SoundSystem.Play(AudioCues.ScreenFlashCrack);
        }

        public override void Update(GameTime gameTime)
        {
            Debug.Assert(GameLoop.Instance.IsFixedTimeStep);

            alpha += .1f;

            if (alpha >= 1)
            {
                state = SceneState.Complete;
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GameLoop.Instance.BeginNonPremultipliedBlendStateDraw();
            ScreenManager.Singleton.TintBackBuffer(alpha, Color.White, spriteBatch);
            GameLoop.Instance.RestoreNormalDraw();
        }
    }
}
 