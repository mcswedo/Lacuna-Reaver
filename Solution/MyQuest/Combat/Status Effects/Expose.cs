using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    class Expose : StatusEffect
    {
        #region Constants

        // Use these constants to balance the status effect.
        const float damageModifier = .5f;

        #endregion

        #region Constructors


        /// <summary>
        /// Constructs the Expose status effect with a given duration. Reduces defense of a character, this effect stacks.
        /// </summary>
        /// <param name="duration"></param>
        public Expose()
        {
            Name = "Exposed";
            IconName = null;
            TurnDuration = 15;
            Probability = 1f;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = false;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.Black;
            //SkillName = "Weakness";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            if (target.FighterStats.ModifiedDefense > 30) //To make sure defense doesn't go below 0.
            {
                base.OnActivateEffect(target);
                int defense = target.FighterStats.ModifiedDefense;
                int deltaDefense = -10;

                StatModifier statModifier = new StatModifier()
                {
                    TargetStat = TargetStat.Defense,
                    ModifierValue = deltaDefense
                };
                StatModifierList.Add(statModifier);
                target.FighterStats.AddStatModifier(statModifier);
            }
        }
    }
}
