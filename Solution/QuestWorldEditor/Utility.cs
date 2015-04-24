using System;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate; 

namespace QuestWorldEditor
{
    /// <summary>
    /// Static class containing utility functions for the Editor
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// Perform a deep Copy of the object via XNA's IntermediateSerializer.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        /// <remarks>This routine serializes the source to a temporary xml file and
        /// then deserializes that file so it is very very slow and inefficient!! The
        /// upside is that if the objects you're cloning are already setup for
        /// XNA serialization (which ours are) you don't have to mess with the
        /// ICloneable interfaces or Serializable dependencies</remarks>
        internal static T Clone<T>(T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            T clone;
            using (XmlWriter writer = XmlWriter.Create("tempzd57fg8u.xml"))
            {
                /// Serialize the source to a temporary xml file
                IntermediateSerializer.Serialize(writer, source, null);
                writer.Close();

                using (XmlReader reader = XmlReader.Create("tempzd57fg8u.xml"))
                {
                    /// Deserialize the temporary xml file into the clone
                    clone = (T)IntermediateSerializer.Deserialize<T>(reader, null);
                    reader.Close();
                }
            }

            /// Delete the temp file
            File.Delete("tempzd57fg8u.xml");

            return clone;
        }

        /// <summary>
        /// Simple method for filling an array with a default value
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="layer">The array to fill</param>
        /// <param name="value">The value to use</param>
        internal static void InitializeArray<T>(T[] array, T value)
        {
            for (int i = 0; i < array.Length; ++i)
                array[i] = value;
        }

        /// <summary>
        /// Helper for appending a file extension to an image filename.
        /// </summary>
        /// <param name="path">The absolute path to the file</param>
        internal static void AppendImageFileExtension(ref string path)
        {
            if (File.Exists(path + ".bmp"))
                path += ".bmp";
            else if (File.Exists(path + ".png"))
                path += ".png";
            else if (File.Exists(path + ".jpeg"))
                path += ".jpeg";
            else if (File.Exists(path + ".jpg"))
                path += ".jpg";
            else if (File.Exists(path + ".tga"))
                path += ".tga";
        }
    }
}
