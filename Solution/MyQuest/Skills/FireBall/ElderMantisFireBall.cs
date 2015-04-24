using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    class ElderMantisFireBall : FireBall
    {
        #region Frame Animations


        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   0, 200, 300),
                new Rectangle(200,   0, 200, 300),
                new Rectangle(400,   0, 200, 300),
                new Rectangle(600,   0, 200, 300),
                new Rectangle(800,   0, 200, 300),
            }
        };

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   300, 200, 300),
                new Rectangle(200,   300, 200, 300),
                new Rectangle(400,   300, 200, 300),
                new Rectangle(600,   300, 200, 300),
                new Rectangle(800,   300, 200, 300),
            }
        };
        #endregion

        #region Fields


        List<CombatAnimation> fireBallAnimations;
        int currentAnimation;

        FireBallState state;

        Vector2 screenPosition;
        Vector2 destinationPosition;
        Vector2 velocity;

        SpriteEffects effect;


        #endregion

        public ElderMantisFireBall() : base()
        {
            Name = Strings.ZA378;
            Description = Strings.ZA381;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 7.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-100, -145);

            TargetsAll = false;
            CanTargetAllies = false;

            CanTargetEnemy = true; fireBallAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "elder_mantis_projectile",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "elder_mantis_projectile",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in fireBallAnimations)
            {
                anim.LoadContent(ContentPath.ToSkillTextures);
            }
        }
        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ProjectileOrigin;

            destinationPosition = targets[0].HitLocation;

            effect = (destinationPosition.X < screenPosition.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            velocity =
                (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            actor.SetAnimation("Attack");
            state = FireBallState.Traveling;
            currentAnimation = 0;
            fireBallAnimations[currentAnimation].Play();
            actor.CurrentAnimation.IsPaused = true;

            SoundSystem.Play(AudioCues.Fireball);
        }

        public override void Update(GameTime gameTime)
        {
            fireBallAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            switch (state)
            {
                case FireBallState.Traveling:
                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (fireBallAnimations[currentAnimation].IsRunning == false)
                    {
                        fireBallAnimations[++currentAnimation].Play();
                        state = FireBallState.Impact;
                    }
                    break;

                case FireBallState.Impact:
                    if (fireBallAnimations[currentAnimation].IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;
                        if (actor is AgoraElderMantis)
                        {
                            SpellPower = 14.2f;
                        }
                        DealMagicDamage(actor, targets.ToArray());
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fireBallAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset, effect);
        }
    }
}
