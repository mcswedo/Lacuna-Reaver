using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class BladeStorm : Skill
    {
        enum StormState
        {
            Striking,
            Dashing, 
            DoubleStriking,          
            DoubleDashing,
            TripleStriking,
            TripleDashing,
            QuadStriking,
            QuadDashing,
            StrikeFinishing,
            Returning
        }


        #region Fields


        StormState state;

        Vector2 initialPosition;
        Vector2 destinationPosition;
        Vector2 reflectPosition; 


        #endregion

        #region Constructor


        public BladeStorm()
        {
            Name = Strings.ZA353;
            Description = Strings.ZA354;

            MpCost = 280;
            SpCost = 7;

            SpellPower = 1.5f;
            DamageModifierValue = 2.7f;

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
            if (targets[0].Name == "Serlynx" || targets[0].Name == "Mal'ticar")
            {
                reflectPosition = destinationPosition + new Vector2(targets[0].CurrentAnimation.Animation.Frames[0].Width/2, 0);
            }
            else
            {
                reflectPosition = destinationPosition + new Vector2(targets[0].CurrentAnimation.Animation.Frames[0].Width, 0);
            }
            CombatAnimation dash = actor.GetAnimation("Dash");

            state = StormState.Striking;
            actor.SetAnimation("Dash");
            SoundSystem.Play(AudioCues.Swoosh);
        }

        public override void Update(GameTime gameTime)
        {
            
            switch (state)
            {
                case StormState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StormState.Dashing;
                        actor.SetAnimation("Attack");
                        SoundSystem.Play(AudioCues.SwordHitFlesh);

                    }
                    break;

                case StormState.Dashing:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StormState.DoubleStriking;

                        actor.SetAnimation("DashReturn");
                        SoundSystem.Play(AudioCues.Swoosh);
                    }
                    break;

                case StormState.DoubleStriking:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = reflectPosition;
                        state = StormState.DoubleDashing;

                        actor.SetAnimation("ReflectedAttack");
                        SoundSystem.Play(AudioCues.SwordHitFlesh);
                    }
                    break;

                case StormState.DoubleDashing:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StormState.TripleStriking;

                        actor.SetAnimation("ReflectedDash");

                        SoundSystem.Play(AudioCues.Swoosh);
                    }
                    break;

                case StormState.TripleStriking:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StormState.TripleDashing;
                        actor.SetAnimation("Attack");
                        SoundSystem.Play(AudioCues.SwordHitFlesh);
                    }
                    break;

                case StormState.TripleDashing:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StormState.QuadStriking;

                        actor.SetAnimation("DashReturn");
                        SoundSystem.Play(AudioCues.Swoosh);
                    }
                    break;

                case StormState.QuadStriking:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = reflectPosition;
                        state = StormState.QuadDashing;

                        actor.SetAnimation("ReflectedAttack");
                        SoundSystem.Play(AudioCues.SwordHitFlesh);
                    }
                    break;

                case StormState.QuadDashing:

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StormState.StrikeFinishing;

                        actor.SetAnimation("ReflectedDash");

                        SoundSystem.Play(AudioCues.Swoosh);
                    }
                    break;

                case StormState.StrikeFinishing:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StormState.Returning;
                        actor.SetAnimation("Attack");
                        SoundSystem.Play(AudioCues.SwordHitFlesh);

                        DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                        actor.AddDamageModifier(modifier);
                        DealPhysicalDamage(actor, targets.ToArray());

                    }
                    break;

                case StormState.Returning:

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
