using System;
using Microsoft.Xna.Framework;

/// Needs commenting

namespace MyQuest
{
    internal static class GlobalSettings
    {
        /// <summary>
        /// The maximum amount of damage that can be dealt
        /// to the player when stepping on a damage tile
        /// </summary>
        //internal static float MaxMapTileDamage = 1000f;
        internal static float MaxMapTileDamage = 250f;

        internal static float musicVolume = 1.0f;         // between 0 and 1
        internal static float soundEffectsVolume = 1.0f;  // between 0 and 1

        internal static readonly Rectangle TitleSafeArea = new Rectangle(
            128,
            72,
            1024,
            576);

        internal static TimeSpan DialogueLetterDelay = TimeSpan.FromSeconds(0.045);   /// Good speed
        internal static TimeSpan DialogueShortLetterDelay = TimeSpan.FromSeconds(0.003); /// Faster speed                                                                                 
        //internal static TimeSpan DebugDialogueLetterDelay = TimeSpan.FromSeconds(0.0065);     /// debugging speed

        //internal static Color DialogueTextColor = Color.White;

        //internal static Color MenuTextColor = Color.White;

        internal static bool CharDebugInfo = false;
    }
}
