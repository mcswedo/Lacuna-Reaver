using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class PolarBurtleAttack : Attack
    {
        #region Constructor

        public PolarBurtleAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA333;

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

            DrawOffset = new Vector2(-50, 0);

        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation + DrawOffset;

            CombatAnimation dash = actor.GetAnimation("Dash");

            velocity =
                (destinationPosition - actor.ScreenPosition) / (float)TimeSpan.FromSeconds((dash.Animation.FrameDelay * dash.Animation.Frames.Count)).TotalMilliseconds;

            state = AttackState.Traveling;
            actor.SetAnimation("Dash");
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case AttackState.Traveling:
                    actor.ScreenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = AttackState.Striking;
                        actor.SetAnimation("Attack");

                        SoundSystem.Play(AudioCues.Attack);
                    }
                    break;

                case AttackState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = AttackState.Returning;

                        actor.SetAnimation("Dash");

                        DealPhysicalDamage(actor, targets.ToArray());

                        SoundSystem.Play(AudioCues.SwordHitArmor);
                    }
                    break;

                case AttackState.Returning:
                    actor.ScreenPosition -= velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (Vector2.Distance(actor.ScreenPosition, initialPosition) < velocity.Length())
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
