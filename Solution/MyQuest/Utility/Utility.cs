using System;
using System.Xml;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

/// Needs commenting

namespace MyQuest
{
    public static class Utility
    {
        public static Random RNG = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Converts world coordinates to tile coordinates
        /// </summary>
        public static Point ToTileCoordinates(Vector2 worldCoordinates, Point tileDimensions)
        {
            int wx = worldCoordinates.X < 0 ? -1 : (int)(worldCoordinates.X / tileDimensions.X);
            int wy = worldCoordinates.Y < 0 ? -1 : (int)(worldCoordinates.Y / tileDimensions.Y);

            return new Point(wx, wy);
        }

        /// <summary>
        /// Converts tilw coordinates to world coordinates
        /// </summary>
        public static Vector2 ToWorldCoordinates(Point tileCoordinates, Point tileDimensions)
        {
            return new Vector2(
                tileCoordinates.X * tileDimensions.X,
                tileCoordinates.Y * tileDimensions.Y);
        }

        public static Point GetMapPositionFromDirection(Point currentTilePosition, Direction direction)
        {
            Point destinationPosition = currentTilePosition;

            if (direction == Direction.North)
            {
                destinationPosition.Y -= 1;
            }
            else if (direction == Direction.East)
            {
                destinationPosition.X += 1;
            }
            else if (direction == Direction.South)
            {
                destinationPosition.Y += 1;
            }
            else
            {
                destinationPosition.X -= 1;
            }

            return destinationPosition;
        }

        /// <summary>
        /// Generates a path from the start position to the end position
        /// </summary>
        /// <param name="start">The start position in tile coordinates</param>
        /// <param name="end">The end position in tile coordinates</param>
        /// <returns>A list of directions defining the path</returns>
        /// <remarks>Generates a naive path (does not take collision into account)</remarks>
        public static List<Direction> GetPathTo(Point start, Point end)
        {
            List<Direction> path = new List<Direction>();

            MakePath(start, end, path);

            return path;
        }

        /// <summary>
        /// Recursively generates a path from one tile to the next
        /// </summary>
        static void MakePath(Point start, Point end, List<Direction> path)
        {
            if (start.Y < end.Y)
            {
                path.Add(Direction.South);
                MakePath(new Point(start.X, start.Y + 1), end, path);
            }
            else 
                if (start.Y > end.Y)
                {
                    path.Add(Direction.North);
                    MakePath(new Point(start.X, start.Y - 1), end, path);
                }
                else
                {
                    if (start.X > end.X)
                    {
                        path.Add(Direction.West);
                        MakePath(new Point(start.X - 1, start.Y), end, path);
                    }
                    else 
                        if (start.X < end.X)
                        {
                            path.Add(Direction.East);
                            MakePath(new Point(start.X + 1, start.Y), end, path);
                        }
                        else
                        {
                            return;
                        }      
                }
        }

        /// <summary>
        /// Simple method for filling an array with a default value
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="layer">The array to fill</param>
        /// <param name="value">The value to use</param>
        public static void InitializeArray<T>(T[] array, T value)
        {
            for (int i = 0; i < array.Length; ++i)
                array[i] = value;
        }

        /// <summary>
        /// Creates an object from its name
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="nameSpace">The namespace where the type is defined</param>
        /// <param name="className">The name of the object</param>
        public static T CreateInstanceFromName<T>(string nameSpace, string className)
        {
            Type type = Type.GetType(nameSpace + "." + className);

            if (type == null)
            {
                 throw new Exception("The following class does not exist: " + nameSpace + "." + className);
            }

            return (T)Activator.CreateInstance(type);
        }

        internal static bool DirectionsAreOpposing(Direction a, Direction b)
        {
            return (a == Direction.North && b == Direction.South ||
                    a == Direction.East && b == Direction.West ||
                    a == Direction.South && b == Direction.North ||
                    a == Direction.West && b == Direction.East);
        }
    }
}