using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace MyQuest
{
    /// <summary>
    /// Represents a piece of equipment which may or may not
    /// temporarily modify a FightingCharacter's Stats. The modifiers
    /// remain active as long as the Equipment is equipped.
    /// </summary>
    public abstract class Equipment
    {
        string displayName;

        /// <summary>
        /// The name of this item
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }


        public string ClassName
        {
            get { return GetType().Name; }
        }


        string description;

        /// <summary>
        /// A description of what this item is or what it does
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        List<StatModifier> modifiers;

        /// <summary>
        /// A list of StatModifiers to be applied by this Equipment
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
