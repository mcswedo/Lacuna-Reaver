using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    enum StrikeState
    {
        Traveling,
        Striking,
        DoubleStriking,
        Returning
    }

    public class DoubleStrike : Skill
    {
        #region Fields


        StrikeState state;

        Vector2 initialPosition;
        Vector2 destinationPosition;

        #endregion

        #region Constructor


        public DoubleStrike()
        {
            Name = Strings.ZA372;
            Description = Strings.ZA373;

            MpCost = 10;
            SpCost = 5;

            SpellPower = 2.5f;
            DamageModifierValue = 1f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false; 

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

            state = StrikeState.Traveling;
            actor.SetAnimation("Dash");
            SoundSystem.Play(AudioCues.Swoosh);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StrikeState.Striking;
                        actor.SetAnimation("Attack");
                        SoundSystem.Play(AudioCues.SwordHitFlesh);
                     
                    }
                    break;

                case StrikeState.Striking:
                    
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StrikeState.DoubleStriking;

                        actor.SetAnimation("Attack");

                        SoundSystem.Play(AudioCues.SwordHitFlesh);
                     }
                    break;

                case StrikeState.DoubleStriking:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StrikeState.Returning;

                        actor.SetAnimation("DashReturn");

                        SoundSystem.Play(AudioCues.Swoosh);

                        DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                        actor.AddDamageModifier(modifier);
                        DealPhysicalDamage(actor, targets.ToArray());
                      
                    }
                    break;

                case StrikeState.Returning:
               
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = initialPosition;
                        actor.SetAnimation("Idle");
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
