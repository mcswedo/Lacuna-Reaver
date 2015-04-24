using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    /// <summary>
    /// CutScene Helper that handles camera movement
    /// </summary>
    class MoveCameraHelper : SceneHelper
    {
        Vector2 cameraVelocity;
        Vector2 destination;

        float moveSpeed;

        /// <summary>
        /// Constructs a new MoveCameraHelper
        /// </summary>
        /// <param name="pointToCenterOn">The tile position you want the camera to be centered on</param>
        /// <param name="moveSpeed">The speed at which the camera will move</param>
        public MoveCameraHelper(Point pointToCenterOn, float moveSpeed)
        {
            Vector2 currentPosition = Camera.Singleton.position;

            /// Centering the camera on the target will give us the
            /// WorldPosition that the camera must move to in order for the
            /// target to be centered within the camera.
            Camera.Singleton.CenterOnTarget(
                Utility.ToWorldCoordinates(
                    pointToCenterOn, 
                    Party.Singleton.CurrentMap.TileDimensions), 
                    Party.Singleton.CurrentMap.DimensionsInPixels,
                    ScreenManager.Singleton.ScreenResolution);

            this.moveSpeed = moveSpeed;
            this.destination = Camera.Singleton.position;
            this.cameraVelocity = destination - currentPosition;

            if (cameraVelocity.LengthSquared() >= 0.000001f)
            {
                cameraVelocity.Normalize();
            }
            else
            {
                isComplete = true;
            }

            /// We have to make sure to move the camera back to where it started
            Camera.Singleton.position = currentPosition;
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Singleton.position += cameraVelocity * moveSpeed;

            if (Vector2.Distance(destination, Camera.Singleton.position) <= moveSpeed)
            {
                Camera.Singleton.position = destination;
                isComplete = true;
            }
        }
    }
}
