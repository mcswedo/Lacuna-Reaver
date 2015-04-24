using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Defending : StatusEffect
    {
        const int bonusStaminaReward = 3;//2;

        public Defending()
        {
            Name = "Defending";
            //RoundDuration = 1;
            TurnDuration = 1;
            Probability = 1.0f;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = false;
            //AttributeModifier = true;
            NegativeEffect = false;
            StatusEffectMessageColor = Color.Yellow;
            //SkillName = "Defend";
        }


        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
            int defense = target.FighterStats.ModifiedDefense;
            int deltaDefense = (int)(defense * 0.5);

            StatModifier statModifier = new StatModifier()
            {
                TargetStat = TargetStat.Defense,
                ModifierValue = deltaDefense
            };
            StatModifierList.Add(statModifier);
            target.FighterStats.AddStatModifier(statModifier);
        }
        public override void OnStartTurn(FightingCharacter target)
        {
            base.OnStartTurn(target);
            target.FighterStats.Stamina += bonusStaminaReward;
        }
    }
}
