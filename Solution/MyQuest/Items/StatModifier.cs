using System;

namespace MyQuest
{
    /// <summary>
    /// Enum describing the Stat a particular modifier may affect
    /// </summary>
    public enum TargetStat
    {
        None,
        Health,
        MaxHealth,
        Energy,
        MaxEnergy,
        Strength,
        Defense,
        WillPower,
        Intelligence,
        Agility,
        Stamina 
    }

    /// <summary>
    /// Represents a single stat modifier which may be either temporary or permanent.
    /// </summary>
    public class StatModifier
    {
        TargetStat targetStat;

        /// <summary>
        /// The Stat this modifier targets
        /// </summary>
        public TargetStat TargetStat
        {
            get { return targetStat; }
            set { targetStat = value; }
        }


        float modifierValue;

        /// <summary>
        /// The value of the modifier
        /// </summary>
        public float ModifierValue
        {
            get { return modifierValue; }
            set { modifierValue = value; }
        }
    }
}
