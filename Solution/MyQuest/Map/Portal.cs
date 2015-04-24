using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

/// Prototype in use

namespace MyQuest
{
    public class Portal
    {
        string destinationMap;

        /// <summary>
        /// The name of the map this portal leads to.
        /// </summary>
        public string DestinationMap
        {
            get { return destinationMap; }
            set { destinationMap = value; }
        }


        Point destinationPosition;

        /// <summary>
        /// The tile position within the destination map that this portal leads to.
        /// </summary>
        public Point DestinationPosition
        {
            get { return destinationPosition; }
            set { destinationPosition = value; }
        }


        Point position;

        /// <summary>
        /// The tile position of this portal within the host map.
        /// </summary>
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }


        string soundCueName;

        [ContentSerializer(Optional = true)]
        public string SoundCueName
        {
            get { return soundCueName; }
            set { soundCueName = value; }
        }


        Direction? destinationDirection = null;

        [ContentSerializer(Optional = true)]
        public Direction? DestinationDirection
        {
            get { return destinationDirection; }
            set { destinationDirection = value; }
        }

        /// <summary>
        /// Used by the editor
        /// </summary>
        /// <returns>The string representation of a Portal</returns>
        public override string ToString()
        {
            return destinationMap.PadRight(40) + destinationPosition.ToString();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
