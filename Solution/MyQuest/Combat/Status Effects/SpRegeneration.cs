using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class SpRegeneration : StatusEffect
    {
        const int bonusStaminaReward = 1;

        public SpRegeneration(int turnDuration, float probability)
        {
            Name = "SpRegeneration";
            TurnDuration = turnDuration;
            Probability = probability;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = false;
            //AttributeModifier = true;
            NegativeEffect = false;
            StatusEffectMessageColor = Color.LightGoldenrodYellow;
            //SkillName = "Defend";
        }


        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
        }
        public override void OnStartTurn(FightingCharacter target)
        {
            base.OnStartTurn(target);
            target.FighterStats.Stamina += bonusStaminaReward;
            MathHelper.Clamp(target.FighterStats.Stamina, 0, target.FighterStats.ModifiedMaxStamina);
        }
    }
}
