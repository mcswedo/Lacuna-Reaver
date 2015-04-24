using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Immolate : Skill
    {
        enum ImmolateState
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


        List<CombatAnimation> immolateAnimations;
        int currentAnimation;

        ImmolateState state;

        Vector2 screenPosition;
        Vector2 destinationPosition;
        Vector2 velocity;

        SpriteEffects effect;


        #endregion

        #region Constructor


        public Immolate()
        {
            Name = Strings.ZA415;
            Description = Strings.ZA416;

            MpCost = 120;
            SpCost = 4;

            SpellPower = 11.5f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            immolateAnimations = new List<CombatAnimation>()
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
                    Name = "Impact",
                    TextureName = "FireBall",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in immolateAnimations)
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

            state = ImmolateState.Charging;
            currentAnimation = 0;
            actor.SetAnimation("Open");
            immolateAnimations[currentAnimation].Play();
            SoundSystem.Play(AudioCues.Fireball);
        }

        public override void Update(GameTime gameTime)
        {
            immolateAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle");
            }
            switch (state)
            {
                case ImmolateState.Charging:
                    if (immolateAnimations[currentAnimation].IsRunning == false)
                    {
                        immolateAnimations[++currentAnimation].Play();
                        state = ImmolateState.Traveling;
                    }
                    break;

                case ImmolateState.Traveling:
                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (immolateAnimations[currentAnimation].IsRunning == false)
                    {
                        immolateAnimations[++currentAnimation].Play();
                        state = ImmolateState.Impact;
                    }
                    break;

                case ImmolateState.Impact:
                    if (immolateAnimations[currentAnimation].IsRunning == false)
                    {
                        StatusEffect burning = new Burning();
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;

                        DealMagicDamage(actor, targets.ToArray());
                        SetStatusEffect(actor, burning, targets[0]);

                        if (targets[0].FighterStats.Stamina >= 1)
                        {
                            targets[0].FighterStats.Stamina -= 1;
                        }
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            immolateAnimations[currentAnimation].Draw(spriteBatch, screenPosition, effect);
        }
    }
}
