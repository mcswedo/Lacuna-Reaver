using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    class TohMeyPowerStrike : PowerStrike
    {
        public TohMeyPowerStrike()
            : base()
        {
            Name = Strings.ZA456;
            Description = Strings.ZA457;

            MpCost = 200;
            SpCost = 5;

            SpellPower = 0.0f;
            DamageModifierValue = .75f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation + DrawOffset;

            CombatAnimation dash = actor.GetAnimation("Dash");

            velocity =
                (destinationPosition - actor.ScreenPosition) / (float)TimeSpan.FromSeconds((dash.Animation.FrameDelay * dash.Animation.Frames.Count)).TotalMilliseconds;

            state = StrikeState.Traveling;
            actor.SetAnimation("Dash");
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    actor.ScreenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StrikeState.Striking;
                        actor.SetAnimation("Jump"); //Doublestrike is broken 11/10

                        SoundSystem.Play(AudioCues.Attack);
                    }
                    break;

                case StrikeState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        state = StrikeState.Returning;

                        actor.SetAnimation("Dash");

                        DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                        actor.AddDamageModifier(modifier);
                        DealPhysicalDamage(actor, targets.ToArray());

                        SoundSystem.Play(AudioCues.SwordHitShield);
                    }
                    break;

                case StrikeState.Returning:
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

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
