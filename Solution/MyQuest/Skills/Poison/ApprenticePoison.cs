using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    class ApprenticePoison : Poison
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

         public ApprenticePoison() : base()
        {

            Name = Strings.ZA443;
            Description = Strings.ZA444;

            MpCost = 70;
            SpCost = 3;//8;

            SpellPower = 5.0f;//4.0f;
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

            fireBallAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "apprenticePoison",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "apprenticePoison",
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

                         DealMagicDamage(actor, targets.ToArray());

                         SoundSystem.Play(AudioCues.Cloud);

                         foreach (FightingCharacter target in targets)
                         {
                             StatusEffect poisoned = new Poisoned();

                             SetStatusEffect(actor, poisoned, target);
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
