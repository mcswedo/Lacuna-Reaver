using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Blindness : StatusEffect
    {
        #region Constructors

        /// <summary>
        /// Constructs the Regeneration status effect with a given duration
        /// </summary>
        /// <param name="duration"></param>
        public Blindness(int turnDuration)
        {
            Name = "Blindness";
            IconName = "blind_icon";
            //RoundDuration = duration;
            TurnDuration = turnDuration;
            Probability = .65f;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = false;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.SlateGray;
            //SkillName = "Blind";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
            target.Blind = true;
        }

        public override void OnStartTurn(FightingCharacter target)
        {
            base.OnStartTurn(target);
            if (TurnsRemaining == 0)
            {
                target.Blind = false;
            }
        }

        public override void OnEndCombat(FightingCharacter target)
        {
            base.OnEndCombat(target);
            target.Blind = false;
        }
    }
}
