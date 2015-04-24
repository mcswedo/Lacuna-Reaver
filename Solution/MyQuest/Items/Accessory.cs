using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace MyQuest
{
    /// <summary>
    /// Represents an equipable accessory which may or may not
    /// temporarily modify a FightingCharacter's Stats. The modifiers
    /// remain active as long as the Accessory is equipped.
    /// </summary>
    public class Accessory : Item
    {
        List<StatModifier> modifiers;

        /// <summary>
        /// A list of StatModifiers to be applied by this Accessory
        /// </summary>
        public List<StatModifier> Modifiers
        {
            get { return modifiers; }
            set { modifiers = value; }
        }

        Texture2D graphic;

        public Texture2D Graphic
        {
            get { return graphic; }
            set { graphic = value; }
        }

        protected void LoadContent(string textureName)
        {
            Graphic = MyContentManager.LoadTexture(ContentPath.ToItemIcons + textureName);
        }
    }
}
