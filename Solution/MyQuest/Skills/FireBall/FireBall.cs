using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    enum FireBallState
    {
        Charging,
        Traveling,
        Impact
    }

    public class FireBall : Skill
    {
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

        #region Constructor


        public FireBall()
        {

            fireBallAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "FireBall",
                    Loop = false,
                    Animation = ChargingFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "FireBall",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "FireBall",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in fireBallAnimations)
            {
                anim.LoadContent(ContentPath.ToSkillTextures);
            }

            DrawOffset = new Vector2(-50, -50);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ProjectileOrigin;
            
            destinationPosition = targets[0].HitLocation;

            effect = (destinationPosition.X < screenPosition.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None);            

            velocity =
                (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            state = FireBallState.Charging;
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
                case FireBallState.Charging:
                    if (fireBallAnimations[currentAnimation].IsRunning == false)
                    {
                        fireBallAnimations[++currentAnimation].Play();
                        state = FireBallState.Traveling;
                    }
                    break;

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
                        targets[0].OnHitSoundCue = null;
                        targets[0].HitNoiseSoundCue = null;
                        delayTimer = TimeSpan.FromSeconds(.28);
                        damageApplied = true;

                        DealMagicDamage(actor, targets.ToArray());
                        if (actor is CaveCrab)
                        {
                            StatusEffect burning = new Burning();
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
