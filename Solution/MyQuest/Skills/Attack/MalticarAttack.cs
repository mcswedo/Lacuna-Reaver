using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class MalticarAttack : Attack
    {
        #region Constructor

        public MalticarAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA717;

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

            DrawOffset = new Vector2(-50,0);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            //initialPosition = actor.ScreenPosition;

            //destinationPosition = targets[0].HitLocation + DrawOffset;

            CombatAnimation attack = actor.GetAnimation("Attack"); //set this as his ground pound

            //velocity =
            //    (destinationPosition - actor.ScreenPosition) / (float)TimeSpan.FromSeconds((attack.Animation.FrameDelay * attack.Animation.Frames.Count)).TotalMilliseconds;

            state = AttackState.Returning;
            actor.SetAnimation("Attack");
            SoundSystem.Play(AudioCues.EarthShatter);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case AttackState.Traveling:
                    //actor.ScreenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = AttackState.Striking; //after ground pound animation, set to returning and deal damage to character.
                        //actor.SetAnimation("Attack");
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
                    //actor.ScreenPosition -= velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        //actor.ScreenPosition = initialPosition;
                        actor.SetAnimation("Idle");
                        DealPhysicalDamage(actor, targets.ToArray());
                        isRunning = false;
                    }
                    break;
            }
        }
    }
}
