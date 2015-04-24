using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyQuest
{
    /// <summary>
    /// Modifies the damage dealt by a FightingCharacter.
    /// </summary>
    public class DamageModifier
    {
        bool isPositive;

        /// <summary>
        /// If the modifier is positive this is true, if it is negative, it is false
        /// </summary>
        public bool IsPositive
        {
            get { return isPositive; }
            set { isPositive = value; }
        }

        float modifierValue;

        /// <summary>
        /// Represents the positive or negative modifier applied to damage calculations
        /// </summary>
        public float ModifierValue
        {
            get { return modifierValue; }
            set { modifierValue = value; }
        }


        public DamageModifier(bool isPositive, float modifierValue)
        {
            this.isPositive = isPositive;

            if (modifierValue < 0)
            {
                throw new Exception("modifierValue can not be less than 0.");
            }

            if (this.IsPositive == false)
            {
                if (modifierValue > 1)
                {
                    throw new Exception("modiferValue can not be greater than 1 for negative damage modifiers.");
                }
            }

            this.modifierValue = modifierValue;
        }
    }
}
