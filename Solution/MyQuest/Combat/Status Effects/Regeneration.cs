using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Regeneration : StatusEffect
    {
        float percentHealed = 0f;
        #region Constructors

        /// <summary>
        /// Constructs the Regeneration status effect with a given duration, probability of effect and healing ratio
        /// </summary>
        /// <param name="turnDuration"></param>
        /// <param name="probability"></param>
        /// <param name="percentHealed"></param>
        public Regeneration(int turnDuration, float probability, float percentHealed)
        {
            Name = "Regeneration";
            IconName = "regen_icon";
            //RoundDuration = duration;
            TurnDuration = turnDuration;
            Probability = probability;
            this.percentHealed = percentHealed;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            NegativeEffect = false;
            StatusEffectMessageColor = Color.LimeGreen;
            //SkillName = "Regenerate";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
        }

        public override void OnStartTurn(FightingCharacter target)
        {
            base.OnStartTurn(target);
            int deltaHealth = (int)(target.FighterStats.ModifiedMaxHealth * percentHealed);
            target.FighterStats.AddHealth(deltaHealth);

            //target.FighterStats.Health += deltaHealth;

            target.DisplayHealing(deltaHealth);
        }
    }
}
