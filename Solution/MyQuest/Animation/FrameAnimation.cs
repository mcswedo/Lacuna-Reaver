using System.Collections.Generic;
using Microsoft.Xna.Framework;

/// Finished

namespace MyQuest
{
    public class FrameAnimation
    {
        List<Rectangle> frames = new List<Rectangle>();

        public List<Rectangle> Frames
        {
            get { return frames; }
            set { frames = value; }
        }


        double frameDelay;

        public double FrameDelay
        {
            get { return frameDelay; }
            set { frameDelay = value; }
        }
    }
}
