using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class WitchDoctorPoisonStrike : Attack
    {
        #region Frame Animations

        static readonly FrameAnimation TravelingFrame = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   0,  200, 200),
            }
        };

        #endregion

        #region Fields


        CombatAnimation spearAnimation;

        Vector2 screenPosition;

        #endregion

        public WitchDoctorPoisonStrike() : base()
        {
            Name = Strings.ZA451;
            Description = Strings.ZA452;

            MpCost = 8;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-100, -110);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            spearAnimation = new CombatAnimation()
            {
                Name = "Spear",
                TextureName = "spear",
                Loop = true,
                Animation = TravelingFrame
            };

            spearAnimation.LoadContent(ContentPath.ToSkillTextures);
        }
        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ProjectileOrigin;

            destinationPosition = targets[0].HitLocation;

            screenPosition = initialPosition;

            velocity =
                (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds(.75).TotalMilliseconds;

            actor.SetAnimation("Attack");
            state = AttackState.Traveling;
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case AttackState.Traveling:
                    if (actor.CurrentAnimation.GetCurrentFrame == 3) // IsRunning == false)
                    {
                        actor.CurrentAnimation.IsPaused = true;
                        //actor.SetAnimation("Throw");
                        spearAnimation.Play();
                        state = AttackState.Striking;
                    }

                    break;

                case AttackState.Striking:

                    spearAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (screenPosition.X <= destinationPosition.X)
                    {
                        DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                        actor.AddDamageModifier(modifier);
                        DealPhysicalDamage(actor, targets.ToArray());

                        foreach (FightingCharacter target in targets)
                        {
                            StatusEffect poison = new Poisoned();

                            SetStatusEffect(actor, poison, target);
                        }

                        state = AttackState.Returning;
                    }

                    break;

                case AttackState.Returning:

                    actor.CurrentAnimation.IsPaused = false;

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == AttackState.Striking)
            {
                spearAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
            }
        }
    }
}
