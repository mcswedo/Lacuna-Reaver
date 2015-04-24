using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    class FireFlyderFlameLance : FireBall
    {
          enum FlameLanceState
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


        List<CombatAnimation> flameLanceAnimations;
        int currentAnimation;

        FlameLanceState state;

        Vector2 screenPosition;
        Vector2 destinationPosition;
        Vector2 velocity;

        SpriteEffects effect;


        #endregion

        public FireFlyderFlameLance()
            : base()
        {

            Name = Strings.ZA392;
            Description = Strings.ZA393;

            MpCost = 200;
            SpCost = 3;

            SpellPower = 15.0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
    
        
        flameLanceAnimations = new List<CombatAnimation>()
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

            foreach (CombatAnimation anim in flameLanceAnimations)
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

            state = FlameLanceState.Charging;
            currentAnimation = 0;
            actor.SetAnimation("Flap");
            flameLanceAnimations[currentAnimation].Play();
     
        }

        public override void Update(GameTime gameTime)
        {
            flameLanceAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle");
            }
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
                    if (flameLanceAnimations[currentAnimation].IsRunning == false)
                    {
                        StatusEffect burning = new Burning();
                        isRunning = false;

                        DealMagicDamage(actor, targets.ToArray());
                        if (SkillHit)
                        {
                            SetStatusEffect(actor, burning, targets[0]);
                        }
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
