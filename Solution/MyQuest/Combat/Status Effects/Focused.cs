using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Focused : StatusEffect
    {
        #region Constants

        // Use these constants to balance the status effect.
        const float damageModifier = .25f;
        DamageModifier modifier = new DamageModifier(true, damageModifier);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs the focused status effect with a given duration.
        /// </summary>
        /// <param name="duration"></param>
        public Focused()
        {
            Name = "Focused";
            IconName = null;
            TurnDuration = 2;
            Probability = .85f;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = false;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.NavajoWhite;
            //SkillName = "Focus";
        }

        public Focused(float probability)
        {
            Name = "Focused";
            IconName = null;
            TurnDuration = 2;
            Probability = probability;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = false;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.NavajoWhite;
            //SkillName = "Focus";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);

            target.DamageModifiers.Add(modifier);
        }

        public override void OnStartTurn(FightingCharacter target)
        {
            base.OnStartTurn(target);
            if (TurnsRemaining == 0)
            {
                target.DamageModifiers.Remove(modifier);
                target.RemoveStatusEffect(this);
            }
        }

        public override void OnDeath(FightingCharacter target)
        {
            base.OnDeath(target);
            target.DamageModifiers.Remove(modifier);
            target.RemoveStatusEffect(this);
        }
    }
}
