using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class BoggimusAttack : Attack
    {
        public BoggimusAttack() : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA331;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            actor.SetAnimation("Attack");
        }

        public override void Update(GameTime gameTime)
        {

            if (actor.CurrentAnimation.IsRunning == false)
            {
                if (!actor.HasStatusEffect("Rage"))
                {
                    actor.SetAnimation("Idle");
                }
                else
                {
                    actor.SetAnimation("RageIdle");
                }
                DealPhysicalDamage(actor, targets.ToArray());
                isRunning = false;
            }
        }
    }
}
