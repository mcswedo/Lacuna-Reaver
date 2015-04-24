using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Invulnerable : StatusEffect
    {
        #region Constructors
        /// <summary>
        /// Constructs the Regeneration status effect with a given duration
        /// </summary>
        /// <param name="duration"></param>
        public Invulnerable(int turnDuration)
        {
            Name = "Invulnerable";
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
            //SkillName = "Invulnerability";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);

            target.SetState(State.Invulnerable);
        }

        public override void OnStartTurn(FightingCharacter target)
        {
            base.OnStartTurn(target);
            if (TurnsRemaining == 0)
            {
                target.SetState(State.Normal);
            }
        }

        public override void OnEndCombat(FightingCharacter target)
        {
            base.OnEndCombat(target);
            target.SetState(State.Normal);
        }
    }
}
