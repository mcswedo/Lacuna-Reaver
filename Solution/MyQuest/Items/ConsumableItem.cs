
namespace MyQuest
{
    /// <summary>
    /// Represents an item which may be consumed. When consumed, this item may
    /// have an immediate and permament effect on a FightingCharacter's Stats.
    /// </summary>
    public abstract class ConsumableItem : Item
    {
        /// <summary>
        /// Apply status modifiers to a given set of stats.
        /// </summary>
        /// <remarks>This can be overriden by derived classes to perform special modifiers</remarks>
        public abstract void Consume(FighterStats stats);
    }
}
