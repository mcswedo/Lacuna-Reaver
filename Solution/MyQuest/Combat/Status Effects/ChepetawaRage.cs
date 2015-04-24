using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    class ChepetawaRage : StatusEffect
    {
        #region Constructors

        public ChepetawaRage()
        {
            Name = "Rage";
            TurnDuration = 999;
            Probability = 1.0f;
            Removable = false;
            NegativeEffect = false;
            StatusEffectMessageColor = Color.Crimson;
        }


        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
            int intelligence = target.FighterStats.ModifiedIntelligence;
            int willpower = target.FighterStats.ModifiedWillpower;
            int defense = target.FighterStats.ModifiedDefense;
            int strength = target.FighterStats.ModifiedStrength;

            int deltaIntelligence = (int)(intelligence * .25 /*.5*/);
            int deltaWillPower = (int)(willpower * .23/*.5*/);
            int deltaDefense = (int)(defense * .115/*.25*/);
            int deltaStrength = (int)(strength * .12/*.25*/);

            StatModifier statModifierIntelligence = new StatModifier()
            {
                TargetStat = TargetStat.Intelligence,
                ModifierValue = deltaIntelligence
            };

            StatModifier statModifierWillPower = new StatModifier()
            {
                TargetStat = TargetStat.WillPower,
                ModifierValue = deltaWillPower
            };

            StatModifier statModifierDefense = new StatModifier()
            {
                TargetStat = TargetStat.Defense,
                ModifierValue = deltaDefense
            };

            StatModifier statModifierStrength = new StatModifier()
            {
                TargetStat = TargetStat.Strength,
                ModifierValue = deltaStrength
            };
            StatModifierList.Add(statModifierIntelligence);
            StatModifierList.Add(statModifierWillPower);
            StatModifierList.Add(statModifierDefense);
            StatModifierList.Add(statModifierStrength);

            target.FighterStats.AddStatModifier(statModifierIntelligence);
            target.FighterStats.AddStatModifier(statModifierWillPower);
            target.FighterStats.AddStatModifier(statModifierDefense);
            target.FighterStats.AddStatModifier(statModifierStrength);
        }
    }
}
