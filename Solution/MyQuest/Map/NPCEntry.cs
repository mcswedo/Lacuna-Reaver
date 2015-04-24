using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

/// Finished

namespace MyQuest
{
    /// <summary>
    /// Represents an NPC entry in a map. This data is serialized with the
    /// map and used to populate the npc list on loading
    /// </summary>
    public class NPCEntry
    {
        string assetName;

        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }


        Point spawnPosition;

        public Point SpawnPosition
        {
            get { return spawnPosition; }
            set { spawnPosition = value; }
        }


        Direction spawnDirection;

        public Direction SpawnDirection
        {
            get { return spawnDirection; }
            set { spawnDirection = value; }
        }

        bool changeIdle;

        [ContentSerializerIgnore]
        public bool ChangeIdle
        {
            get { return changeIdle; }
            set { changeIdle = value; }
        }

        bool idleOnly;
        [ContentSerializerIgnore]
        public bool IdleOnly
        {
            get { return idleOnly; }
            set { idleOnly = value; }
        }
    }
}
