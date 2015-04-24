using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class ArlanFireBall : FireBall
    {

        #region Frame Animations


        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   0, 100, 100),
                new Rectangle(100,   0, 100, 100),
                new Rectangle(200,   0, 100, 100),
                new Rectangle(300,   0, 100, 100),
                new Rectangle(400,   0, 100, 100),
                new Rectangle(500,   0, 100, 100),
                new Rectangle(600,   0, 100, 100),
                new Rectangle(700,   0, 100, 100)
            }
        };

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   0, 100, 100),
                new Rectangle(100,   0, 100, 100),
                
                new Rectangle(0,   100, 100, 100),
                new Rectangle(100, 100, 100, 100),
                new Rectangle(200, 100, 100, 100),
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

        #region Constructor

        public ArlanFireBall()
        {
            Name = Strings.ZA378; //This is fire bolt, not fire ball. Works the same though.
            Description = Strings.ZA380;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 26;//4.0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            fireBallAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "arlanProjectile",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "arlanProjectile",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in fireBallAnimations)
            {
                anim.LoadContent(ContentPath.ToSkillTextures);
            }
        }

        #endregion
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
            actor.SetAnimation("Idle");

            SoundSystem.Play(AudioCues.Fireball);

            actor.SetAnimation("Attack");
            
        }

        public override void Update(GameTime gameTime)
        {
            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle"); //if attack animation ends before fireball animation
            }

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
                        actor.SetAnimation("Idle"); //if fireball animation ends before attack animation
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;

                        DealMagicDamage(actor, targets.ToArray());
                        if (actor.FighterStats.Stamina <= 5)
                        {
                            actor.FighterStats.Stamina += 5;
                        }
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
