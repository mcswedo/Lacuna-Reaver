using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class NathanAttack : Attack
    {
        #region Constructor

        public NathanAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA344;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            
            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
            DrawOffset = new Vector2(-50, -10);

        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation + DrawOffset;

            CombatAnimation dash = actor.GetAnimation("Dash");

            state = AttackState.Traveling;
            actor.SetAnimation("Dash");
            SoundSystem.Play(AudioCues.Swoosh);
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
                        state = AttackState.Returning;

                        actor.SetAnimation("DashReturn");
                        SoundSystem.Play(AudioCues.Swoosh);

                        DealPhysicalDamage(actor, targets.ToArray());
                      
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
