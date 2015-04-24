using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    enum FlameLanceState
    {
        Charging,
        Traveling,
        Impact
    }

    public class FlameLance : Skill
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


        List<CombatAnimation> flameLanceAnimations;
        int currentAnimation;

        FlameLanceState state;

        Vector2 screenPosition;
        Vector2 destinationPosition;
        Vector2 velocity;

        SpriteEffects effect;

        TimeSpan delayTimer;
        bool damageApplied;


        #endregion

        #region Constructor


        public FlameLance()
        {
            Name = Strings.ZA389;
            Description = Strings.ZA394;

            MpCost = 50;
            SpCost = 5;

            SpellPower = 11.0f;//6.5f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-50, -50);

            flameLanceAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "RedFireBall",
                    Loop = false,
                    Animation = ChargingFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "RedFireBall",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impact",
                    TextureName = "RedFireBall",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in flameLanceAnimations)
            {
                anim.LoadContent(ContentPath.ToSkillTextures);
            }
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

            state = FlameLanceState.Charging;
            currentAnimation = 0;
            flameLanceAnimations[currentAnimation].Play();
            damageApplied = false;
            actor.CurrentAnimation.IsPaused = true;
            SoundSystem.Play(AudioCues.Fireball);
        }

        public override void Update(GameTime gameTime)
        {
            flameLanceAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            switch (state)
            {
                case FlameLanceState.Charging:
                    if (flameLanceAnimations[currentAnimation].IsRunning == false)
                    {
                        flameLanceAnimations[++currentAnimation].Play();
                        state = FlameLanceState.Traveling;
                    }
                    break;

                case FlameLanceState.Traveling:
                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (flameLanceAnimations[currentAnimation].IsRunning == false)
                    {
                        flameLanceAnimations[++currentAnimation].Play();
                        state = FlameLanceState.Impact;
                    }
                    break;

                case FlameLanceState.Impact:
                    if (!damageApplied)
                    {
                        DealMagicDamage(actor, targets.ToArray()); 
                        if (SkillHit && actor is CaveCrab)
                        {
                            StatusEffect burning = new Burning();
                            SetStatusEffect(actor, burning, targets[0]);
                        }
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
            flameLanceAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset, effect);
        }
    }
}
