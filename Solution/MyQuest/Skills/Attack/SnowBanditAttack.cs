using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class SnowBanditAttack : Attack
    {
        public SnowBanditAttack()
            : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA605;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            DrawOffset = new Vector2(60, 0); 
            destinationPosition = targets[0].HitLocation + DrawOffset;

            CombatAnimation dash = actor.GetAnimation("Dash");

            state = AttackState.Traveling;
            actor.SetAnimation("Dash");
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case AttackState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = AttackState.Striking;
                        actor.SetAnimation("Attack");

                        SoundSystem.Play(AudioCues.Attack);
                    }
                    break;

                case AttackState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        StatusEffect expose = new Expose();
                        state = AttackState.Returning;

                        actor.SetAnimation("Dash");
                        int targetsHealthBeforeDamage = targets[0].FighterStats.Health;

                        DealPhysicalDamage(actor, targets.ToArray());
                        if (SkillHit) //Add it if the attack lands correctly(Didn't miss).
                        {
                            SetStatusEffect(actor, expose, targets[0]);
                            //targets[0].AddStatusEffect(expose); // Every Attack reduces the targets armor.
                        }
                        //this.DamageModifierValue += .2f; //The bandit gets stronger with every attack.                

                        SoundSystem.Play(AudioCues.SwordHitArmor);
                    }
                    break;

                case AttackState.Returning:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = initialPosition;
                        actor.SetAnimation("Idle");
                        isRunning = false;
                    }
                    break;
            }
        }
    }
}
