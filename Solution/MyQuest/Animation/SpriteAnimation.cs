using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// Finished

namespace MyQuest
{
    public class SpriteAnimation
    {
        #region Animation Data


        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        Dictionary<string, FrameAnimation> animations = new Dictionary<string, FrameAnimation>();

        public Dictionary<string, FrameAnimation> Animations
        {
            get { return animations; }
            set { animations = value; }
        }


        FrameAnimation currentAnimation;

        /// <summary>
        /// The current frame of the current animation
        /// </summary>
        int currentFrame;

        /// <summary>
        /// Counts the time until the next frame
        /// </summary>
        double ticker;

        float alpha = 0; 

        #endregion

        #region Graphic Data


        string spriteSheetName;

        public string SpriteSheetName
        {
            get { return spriteSheetName; }
            set { spriteSheetName = value; }
        }


        Texture2D spriteSheet;


        #endregion

        #region Methods


        public void LoadContent(string path)
        {
            spriteSheet = MyContentManager.LoadTexture(path + spriteSheetName);
        }

        public void Play(string animationName)
        {
            if (!string.IsNullOrEmpty(animationName))
            {
                FrameAnimation newAnimation;

                if (!animations.TryGetValue(animationName, out newAnimation))
                {
                    throw new Exception("No animation by this name");
                }

                if (currentAnimation != newAnimation)
                {
                    currentAnimation = newAnimation;
                    currentFrame = 0;
                    ticker = TimeSpan.FromSeconds(currentAnimation.FrameDelay).TotalMilliseconds;
                }
            }
        }

        public void Update(double elapsedTime)
        {
            if (currentAnimation == null)
                return;

            ticker -= elapsedTime;

            if (ticker <= 0)
            {
                ticker = TimeSpan.FromSeconds(currentAnimation.FrameDelay).TotalMilliseconds;

                if (++currentFrame >= currentAnimation.Frames.Count)
                {
                    currentFrame = 0;
                }
            }
        }

        public void AltDraw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (currentAnimation == null)
            {
                return;
            }

            spriteBatch.Draw(
                spriteSheet,
                position + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY),
                currentAnimation.Frames[currentFrame],
                Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (currentAnimation == null)
                return;

            spriteBatch.Draw(
                spriteSheet,
                position,
                currentAnimation.Frames[currentFrame],
                Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            if (currentAnimation == null)
                return;

            spriteBatch.Draw(
                spriteSheet,
                position,
                currentAnimation.Frames[currentFrame],
                color);
        }

        public void DrawForGameMap(SpriteBatch spriteBatch, Vector2 position)
        {
            if (currentAnimation == null)
                return;

            if (alpha >= 0 && alpha <= 1)
                alpha += .01f;
            else
                alpha = 0;

            spriteBatch.Draw(
                spriteSheet,
                position,
                currentAnimation.Frames[currentFrame],
                new Color(Color.PaleGoldenrod.R, Color.PaleGoldenrod.G, Color.PaleGoldenrod.B, alpha), 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
        }

        #endregion
    }
}
