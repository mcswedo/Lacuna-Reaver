using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    class Demon1ShadowStrike : Skill
    {
        enum ShadowStrikeState
        {
            Charging,
            Traveling,
            Impact
        }
        #region Frame Animations


        static readonly FrameAnimation ChargingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 102, 102),
                new Rectangle(102, 0, 102, 102),
                new Rectangle(204, 0, 102, 102),
                new Rectangle(306, 0, 102, 102),
                new Rectangle(408, 0, 102, 102),
                new Rectangle(510, 0, 102, 102)
            }
        };

        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 102, 102, 102),
                new Rectangle(102, 102, 102, 102),
                new Rectangle(204, 102, 102, 102),
                new Rectangle(306, 102, 102, 102),
                new Rectangle(408, 102, 102, 102),
                new Rectangle(510, 102, 102, 102)
            }
        };

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 204, 102, 102),
                new Rectangle(102, 204, 102, 102),
                new Rectangle(204, 204, 102, 102),
                new Rectangle(306, 204, 102, 102),
                new Rectangle(408, 204, 102, 102),
                new Rectangle(510, 204, 102, 102)
            }
        };


        #endregion

        #region Fields


        List<CombatAnimation> shadowStrikeAnimations;
        int currentAnimation;

        ShadowStrikeState state;

        protected Vector2 screenPosition;
        protected Vector2 destinationPosition;
        protected Vector2 velocity;

        SpriteEffects effect;

        TimeSpan delayTimer;
        bool damageApplied;

        #endregion
        public Demon1ShadowStrike()
            : base()
        {
            Name = Strings.ZA470;
            Description = Strings.ZA474;

            MpCost = 500;
            SpCost = 5;

            SpellPower = 18.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            shadowStrikeAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "shadowBall",
                    Loop = false,
                    Animation = ChargingFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "shadowBall",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "shadowBall",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };
     
            foreach (CombatAnimation anim in shadowStrikeAnimations)
                anim.LoadContent(ContentPath.ToSkillTextures);
        }
        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ProjectileOrigin;

            destinationPosition =
                targets[0].ScreenPosition +
                new Vector2(
                    targets[0].CurrentAnimation.Animation.Frames[0].Width / 2 - ImpactFrames.Frames[0].Width / 2 - 40 - 20,
                    targets[0].CurrentAnimation.Animation.Frames[0].Height - 325 + 40);

            effect = (destinationPosition.X < screenPosition.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            velocity =
                (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            state = ShadowStrikeState.Charging;
            currentAnimation = 0;
            shadowStrikeAnimations[currentAnimation].Play();
            damageApplied = false;
            actor.CurrentAnimation.IsPaused = true;

            SoundSystem.Play(AudioCues.Fireball);
        }

        public override void Update(GameTime gameTime)
        {
            shadowStrikeAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            switch (state)
            {
                case ShadowStrikeState.Charging:
                    if (shadowStrikeAnimations[currentAnimation].IsRunning == false)
                    {
                        shadowStrikeAnimations[++currentAnimation].Play();
                        state = ShadowStrikeState.Traveling;
                    }
                    break;

                case ShadowStrikeState.Traveling:
                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (shadowStrikeAnimations[currentAnimation].IsRunning == false)
                    {
                        shadowStrikeAnimations[++currentAnimation].Play();
                        state = ShadowStrikeState.Impact;
                    }
                    break;

                case ShadowStrikeState.Impact:
                    if (!damageApplied)
                    {
                        DealMagicDamage(actor, targets.ToArray());
                        delayTimer = TimeSpan.FromSeconds(.28);
                        damageApplied = true;
                        return;
                    }
                    delayTimer -= gameTime.ElapsedGameTime;
                    if (delayTimer <= TimeSpan.Zero)
                    {
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            shadowStrikeAnimations[currentAnimation].Draw(spriteBatch, screenPosition, effect);
        }
    }
}

