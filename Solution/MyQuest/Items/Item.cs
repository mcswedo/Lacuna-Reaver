using System.Collections.Generic;
namespace MyQuest
{
    /// <summary>
    /// Represents a single item
    /// </summary>
    public abstract class Item
    {
        string displayName;

        /// <summary>
        /// The visual representation of the item's name
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        string mapName;

        public string MapName
        {
            get { return mapName; }
            set { mapName = value; }
        }

        List<string> subMapNames;

        public List<string> SubMapNames
        {
            get { return subMapNames; }
            set { subMapNames = value; }
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

        float dropChance;

        public float DropChance
        {
            get { return dropChance; }
            set { dropChance = value; }
        }
    }
}
