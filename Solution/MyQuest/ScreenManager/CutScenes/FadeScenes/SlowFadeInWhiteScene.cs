using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class SlowFadeInWhiteScene : Scene
    {
        float alpha;

        public SlowFadeInWhiteScene(Screen screen)
            : base(screen)
        {
            alpha = 1;
        }

        public override void LoadContent(ContentManager content)
        {    
        }

        public override void Initialize()
        {
            alpha = 1;

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "HealersBlacksmith",
                new Point(20, 3),
                ModAction.Remove,
                true); //injured arlan in finalbattlecutscenescreen
        }

        public override void Update(GameTime gameTime)
        {
            alpha -= .01f;

            if (alpha <= 0)
            {
                alpha = 0;
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
 