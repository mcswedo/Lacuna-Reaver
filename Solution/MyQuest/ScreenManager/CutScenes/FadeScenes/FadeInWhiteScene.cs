using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class FadeInWhiteScene : Scene
    {
        float alpha;

        public FadeInWhiteScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {    
        }

        public override void Initialize()
        {
            alpha = 1;
        }

        public override void Update(GameTime gameTime)
        {
            alpha -= .1f;

            if (alpha <= 0)
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
            //ScreenManager.Singleton.TintBackBuffer(alpha, Color.LightSkyBlue, spriteBatch); 
        }
    }
}
 