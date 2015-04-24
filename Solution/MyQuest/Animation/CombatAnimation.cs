using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class CombatAnimation
    {
        #region Name


        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        #endregion

        #region Graphics


        string textureName;

        public string TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }


        FrameAnimation animation;

        public FrameAnimation Animation
        {
            get { return animation; }
            set { animation = value; }
        }


        Texture2D texture;

        Vector2 drawOffset;

        public Vector2 DrawOffset
        {
            get { return drawOffset; }
            set { drawOffset = value; }
        }

        float alpha = 1;
        Color color = Color.White; 

        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        #endregion

        #region PlayBack


        bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }


        bool isPaused;

        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }


        bool loop = false;

        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }


        /// <summary>
        /// The current frame of the animation
        /// </summary>
        int currentFrame;

        public int GetCurrentFrame
        {
            get { return currentFrame; }
        }

        /// <summary>
        /// Counts the time until the next frame
        /// </summary>
        double ticker;


        #endregion

        float intensity = 1;
        bool isFluxing;
        bool isFluxUp = false;
        //public double timeModifier = 1;

        public void LoadContent(string path)
        {
            texture = MyContentManager.LoadTexture(path + textureName);
        }

        public void Play()
        {
            if (animation == null)
            {
                throw new Exception("animation is null for " + name);
            }

            currentFrame = 0;
            ticker = TimeSpan.FromSeconds(animation.FrameDelay).TotalMilliseconds;
            isRunning = true;
            isPaused = false;
        }

        public void Update(double elapsedTime)
        {
            if (isPaused)
                return;

            ticker -= elapsedTime;

            if (ticker <= 0)
            {
                ticker = TimeSpan.FromSeconds(animation.FrameDelay).TotalMilliseconds - elapsedTime;

                if (++currentFrame >= animation.Frames.Count)
                {
                    if (Loop)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        isRunning = false;
                        currentFrame = 0;
                    }
                }
            }
        }

        public void Update(double elapsedTime, double timeModifier)
        {
            if (isPaused)
                return;

            ticker -= elapsedTime * timeModifier;

            if (ticker <= 0)
            {
                ticker = TimeSpan.FromSeconds(animation.FrameDelay).TotalMilliseconds - (elapsedTime * timeModifier);

                if (++currentFrame >= animation.Frames.Count)
                {
                    if (Loop)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        isRunning = false;
                        currentFrame = 0;
                    }
                }
            }
            //timeModifier = 1;
        }

       
        public void deathFade()
        {
            if (alpha >= 0)
            {
                alpha -= .05f;
            }
        }


        public void triggerRedFlux()
        {
            isFluxing = true;
            isFluxUp = false; 
        }

        private void redFlux()
        {
            if (!isFluxUp)
            {
                intensity -= .075f;

                if (intensity <= 0)
                {
                    intensity = 0;
                    isFluxUp = true;
                }
            }
            else
            {
                intensity += .05f;
                if (intensity >= 1)
                {
                    intensity = 1;
                    isFluxing = false;
                }
            }

            color = new Color(1, intensity, intensity);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effect)
        {
            // I don't see how to guarrantee the following assertion.  DT  Please fix if you can.
            // I think there is a problem with the ice feesh animations.
            //Debug.Assert(isRunning == true);

            if (!isRunning)
            {
                currentFrame = 0;
            }

            spriteBatch.Draw(
                texture,
                position + drawOffset,
                animation.Frames[currentFrame],
                Color.White * alpha,
                0f,
                Vector2.Zero,
                1,
                effect,
                0f);

            //spriteBatch.Draw(texture, position, animation.Frames[currentFrame], Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effect, Color tintEffect)
        {
// I don't see how to guarrantee the following assertion.  DT  Please fix if you can.
            // I think there is a problem with the ice feesh animations.
            //            Debug.Assert(isRunning == true);

            if (!isRunning)
            {
                currentFrame = 0;
            }

            if (isFluxing)
            {
                if (tintEffect.ToVector3() != new Vector3(1, 1, 1))//fighter is status effected
                {
                    color = Color.Red;
                    isFluxing = false;
                }
                else
                {
                    redFlux();
                }
            }
            else
            {
                color = tintEffect;
            }
            spriteBatch.Draw(
                texture,
                position + drawOffset,
                animation.Frames[currentFrame],
                color * alpha,
                0f,
                Vector2.Zero,
                1f,
                effect,
                0f);
        }
    }
}