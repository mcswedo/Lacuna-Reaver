using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    class BurtleRage : StatusEffect
    {
        #region Constructors

        public BurtleRage()
        {
            Name = "Rage";
            //RoundDuration = 999;
            TurnDuration = 999;
            Probability = 1.0f;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = false;
            //AttributeModifier = true;
            NegativeEffect = false;
            StatusEffectMessageColor = Color.Crimson;
        }


        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
            int deltaStrength = (int)(target.FighterStats.BaseStrength * .25);
            int deltaDefense = -(int)(target.FighterStats.BaseDefense * .5);

            StatModifier statModifierStrength = new StatModifier()
            {
                TargetStat = TargetStat.Strength,
                ModifierValue = deltaStrength
            };

            StatModifier statModifierDefense = new StatModifier()
            {
                TargetStat = TargetStat.Defense,
                ModifierValue = deltaDefense
            };
            StatModifierList.Add(statModifierStrength);
            StatModifierList.Add(statModifierDefense);

            target.FighterStats.AddStatModifier(statModifierStrength);
            target.FighterStats.AddStatModifier(statModifierDefense);
        }
    }
}
