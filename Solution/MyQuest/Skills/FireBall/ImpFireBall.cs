using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    class ImpFireBall : FireBall
    {
        #region Frame Animations

        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 150, 150),
                new Rectangle(150, 0, 150, 150),
                new Rectangle(300, 0, 150, 150),
                new Rectangle(450, 0, 150, 150),
                new Rectangle(600, 0, 150, 150),
                new Rectangle(750, 0, 150, 150)
            }
        };

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 150, 150, 150),
                new Rectangle(150, 150, 150, 150),
                new Rectangle(300, 150, 150, 150),
                new Rectangle(450, 150, 150, 150),
                new Rectangle(600, 150, 150, 150),
                new Rectangle(750, 150, 150, 150)
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

        TimeSpan delayTimer;
        bool damageApplied;


        #endregion

        public ImpFireBall()
            : base()
        {

            Name = Strings.ZA378;
            Description = Strings.ZA382;

            MpCost = 500;
            SpCost = 2;

            SpellPower = 0.0f;
            DamageModifierValue = 2.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-75, -75);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            fireBallAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "LavaBall",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "LavaBall",
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

            //SubtractCost(actor);

            screenPosition = actor.ProjectileOrigin;

            destinationPosition = targets[0].HitLocation;

            effect = (destinationPosition.X < screenPosition.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            velocity =
                (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            state = FireBallState.Traveling;
            currentAnimation = 0;
            fireBallAnimations[currentAnimation].Play();
            damageApplied = false;
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
                    string OnHitSound = targets[0].OnHitSoundCue;
                    string HitNoise = targets[0].HitNoiseSoundCue;

                    if (!damageApplied)
                    {
                        StatusEffect burning = new Burning();
                        targets[0].OnHitSoundCue = null;
                        targets[0].HitNoiseSoundCue = null;
                        delayTimer = TimeSpan.FromSeconds(.28);
                        damageApplied = true;

                        DealPhysicalDamage(actor, targets.ToArray());
                        if(SkillHit)
                        {
                            SetStatusEffect(actor, burning, targets[0]);
                        }
                        return;
                    }
                    delayTimer -= gameTime.ElapsedGameTime;
                    if (delayTimer <= TimeSpan.Zero)
                    {
                        targets[0].OnHitSoundCue = OnHitSound;
                        targets[0].HitNoiseSoundCue = HitNoise;
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;
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
