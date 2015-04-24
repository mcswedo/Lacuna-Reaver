using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class FadeOutScene : Scene
    {
        const double defaultFadeSeconds = .5;
        float alpha = 0.0f;
        TimeSpan fadeDuration;
        TimeSpan fadeTimeRemaining;
        Color color;

        public FadeOutScene(Screen screen)
            : base(screen)
        {
            state = SceneState.Working;
            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;
            fadeDuration = TimeSpan.FromSeconds(defaultFadeSeconds);
            fadeTimeRemaining = fadeDuration;
            color = Color.Black;
        }

        public FadeOutScene(Screen screen, TimeSpan fadeDuration)
            : base(screen)
        {
            state = SceneState.Working;
            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;
            this.fadeDuration = fadeDuration;
            fadeTimeRemaining = fadeDuration;
            color = Color.Black;
        }

        public FadeOutScene(Screen screen, TimeSpan fadeDuration, Color color)
            : base(screen)
        {
            state = SceneState.Working;
            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;
            this.fadeDuration = fadeDuration;
            fadeTimeRemaining = fadeDuration;
            this.color = color;
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            fadeTimeRemaining -= gameTime.ElapsedGameTime;

            alpha = 1-(float)(fadeTimeRemaining.TotalSeconds / fadeDuration.TotalSeconds);
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;
                state = SceneState.Complete;
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Color c = new Color(color.R * alpha, color.G * alpha, color.B * alpha, color.A * alpha);

            ScreenManager.Singleton.TintBackBuffer(alpha, color, spriteBatch);
        }
    }
}
 