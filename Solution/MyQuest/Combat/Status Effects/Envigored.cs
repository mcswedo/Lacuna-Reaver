using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    class Envigored : StatusEffect
    {
        //StatModifier statModifier;

        #region Constructors

        //public Envigored()
        //{
        //    Name = "Envigored";
        //    RoundDuration = 10;
        //    Probability = 1.0f;
        //    PercentDamage = 0;
        //    FlatDamage = 0;
        //    PersistsOutOfCombat = false;
        //    Removable = true;
        //    AttributeModifier = true;
        //    NegativeEffect = false;
        //    StatusEffectMessageColor = Color.NavajoWhite;
        //    SkillName = "Vigor";
        //}

        /// <summary>
        /// Constructs the Envigored status effect with a given duration
        /// </summary>
        /// <param name="duration"></param>
        public Envigored(int turnDuration)
        {
            Name = "Envigored";
            //RoundDuration = duration;
            TurnDuration = turnDuration;
            Probability = 1f;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = true;
            NegativeEffect = false;
            StatusEffectMessageColor = Color.NavajoWhite;
            //SkillName = "Vigor";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
            int deltaStrength = (int)(target.FighterStats.ModifiedStrength * .25);
            int deltaIntelligence = (int)(target.FighterStats.ModifiedIntelligence * .25);

            StatModifier statModifier = new StatModifier()
            {
                TargetStat = TargetStat.Strength,
                ModifierValue = deltaStrength
            };
            StatModifier statModifier2 = new StatModifier()
            {
                TargetStat = TargetStat.Intelligence,
                ModifierValue = deltaIntelligence
            };
            StatModifierList.Add(statModifier);
            StatModifierList.Add(statModifier2);
            target.FighterStats.AddStatModifier(statModifier);
            target.FighterStats.AddStatModifier(statModifier2);
        }
    }
}
