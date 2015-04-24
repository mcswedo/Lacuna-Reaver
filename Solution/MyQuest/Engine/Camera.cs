using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

/// Finished under current specifications

namespace MyQuest
{
    internal class Camera
    {
        static Camera singleton = new Camera();

        public static Camera Singleton
        {
            get { return singleton; }
        }

        private Camera()
        {
        }

        /// <summary>
        /// World coordinates of lower left corner of view port.
        /// </summary>
        internal Vector2 position = Vector2.Zero;
        
        TimeSpan shakeDuration;
        int shakeMagnitude;
        bool shaking;
        Matrix transform;

        Matrix mapCenteringTranslation = Matrix.Identity;

        /// <summary>
        /// This translation centers the map within the screen when the map is smaller than the screen.
        /// </summary>
        internal Matrix MapCenteringTranslation
        {
            get { return mapCenteringTranslation; }
        }

        Vector3 GetRandomShakeOffset()
        {
            return new Vector3(
                (int)((Utility.RNG.NextDouble() * 2 - 1) * shakeMagnitude),
                (int)((Utility.RNG.NextDouble() * 2 - 1) * shakeMagnitude),
                0);
        }

        internal Matrix Transformation
        {
            get { return transform; }
        }

        internal float OffsetX
        {
            get;
            set;
        }

        internal float OffsetY
        {
            get;
            set;
        }

        internal int OffsetXInt
        {
            get;
            set;
        }

        internal int OffsetYInt
        {
            get;
            set;
        }

        /// <summary>
        /// Center the camera around a specified position.
        /// </summary>
        /// <param name="targetLocation">The position to look at.</param>
        /// <param name="mapDimensionsInPixels">The dimensions of the map in pixels</param>
        /// <param name="screenResolution">The dimensions of the screen in pixels</param>
        internal void CenterOnTarget(Vector2 targetLocation, Point mapDimensionsInPixels, Rectangle screenResolution)
        {
            position.X = (int)(MathHelper.Clamp(
                targetLocation.X - screenResolution.Width / 2,
                0,
                mapDimensionsInPixels.X - screenResolution.Width) + 0.5f);

            position.Y = (int)(MathHelper.Clamp(
                targetLocation.Y - screenResolution.Height / 2,
                0,
                mapDimensionsInPixels.Y - screenResolution.Height + 0.5f) + 0.5f);

            transform = Matrix.CreateTranslation(new Vector3(-position, 0)) * mapCenteringTranslation;
            OffsetX = transform.Translation.X;
            OffsetY = transform.Translation.Y;
            OffsetXInt = (int)OffsetX;
            OffsetYInt = (int)OffsetY;
        }

        /// <summary>
        /// Translate the camera to a particular location.
        /// </summary>
        internal void SetMapCenteringTranslation(int x, int y)
        {
            mapCenteringTranslation = Matrix.CreateTranslation(x, y, 0f);
            transform = Matrix.CreateTranslation(new Vector3(-position, 0)) * mapCenteringTranslation;
            OffsetX = transform.Translation.X;
            OffsetY = transform.Translation.Y;
            OffsetXInt = (int)OffsetX;
            OffsetYInt = (int)OffsetY;
        }

        internal void Update(GameTime elapsedTime)
        {
            if (shaking)
            {
                shakeDuration -= elapsedTime.ElapsedGameTime;
                if (shakeDuration <= TimeSpan.Zero)
                {
                    shaking = false;
                    shakeDuration = TimeSpan.Zero;
                }
                Vector3 shakeOffset = GetRandomShakeOffset();
                transform = Matrix.CreateTranslation(new Vector3(-position, 0)) * mapCenteringTranslation * Matrix.CreateTranslation(shakeOffset);
            }
            else
            {
                transform = Matrix.CreateTranslation(new Vector3(-position, 0)) * mapCenteringTranslation;
            }
            OffsetX = transform.Translation.X;
            OffsetY = transform.Translation.Y;
            OffsetXInt = (int)OffsetX;
            OffsetYInt = (int)OffsetY;
        }

        /// <summary>
        /// Turn on camera shaking.
        /// </summary>
        /// <param name="shakeDuration">The duration of the shake effect</param>
        /// <param name="magnitude">The magnitude of the shake. To prevent tearing, the camera 
        /// translations are casted to ints. For that reason, a magnitude of less than 4 won't
        /// have any effect on the camera.</param>
        internal void Shake(TimeSpan shakeDuration, int magnitude)
        {
            Debug.Assert(magnitude >= 4);  // See comment. 
            this.shakeMagnitude = magnitude;
            this.shakeDuration = shakeDuration;
            shaking = true;
        }
    }
}
