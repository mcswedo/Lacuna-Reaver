using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    class Weakened : StatusEffect
    {
        #region Constants

        // Use these constants to balance the status effect.
        const float damageModifier = .5f;

        #endregion

        #region Constructors


        /// <summary>
        /// Constructs the Weakened status effect with a given duration.
        /// </summary>
        /// <param name="duration"></param>
        public Weakened()
        {
            Name = "Weakened";
            IconName = null;
            TurnDuration = 1;
            Probability = .75f;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = false;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.NavajoWhite;
            //SkillName = "Weakness";
        }

        public Weakened(float probability)
        {
            Name = "Weakened";
            IconName = null;
            TurnDuration = 1;
            Probability = probability;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = false;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.NavajoWhite;
            //SkillName = "Weakness";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);

            DamageModifier modifier = new DamageModifier(false, damageModifier);

            //target.CurrentAnimation.Animation.FrameDelay += .03; Idea on weaken, doesn't solve problems with rest of status effects.
            target.DamageModifiers.Add(modifier);
            target.RemoveStatusEffect(this);
        }
    }
}
