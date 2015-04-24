using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MyQuest
{
    class CaraAttack : Attack
    {
        #region Frame Animations


        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,     0, 100, 100),
                new Rectangle(100,   0, 100, 100),
                new Rectangle(200,   0, 100, 100),
                new Rectangle(300,   0, 100, 100),
                new Rectangle(400,   0, 100, 100),
                
                new Rectangle(500,   0, 100, 100),
                new Rectangle(600,   0, 100, 100),
                new Rectangle(700,   0, 100, 100),
                new Rectangle(800,   0, 100, 100)
            }
        };

      
        #endregion

        Vector2 screenPosition;
        CombatAnimation projectileAnimation;
        SpriteEffects effect;

        #region Constructor

        public CaraAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA334;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(0,-50);

            projectileAnimation = new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "cara_projectile",
                    Loop = false,
                    Animation = TravelingFrames
                };

            projectileAnimation.LoadContent(ContentPath.ToSkillTextures);

        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ProjectileOrigin;

            //initialPosition = actor.ScreenPosition;

            effect = (destinationPosition.X < screenPosition.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            destinationPosition = targets[0].HitLocation + DrawOffset;

            //CombatAnimation dash = actor.GetAnimation("Dash");
            
            velocity =(destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            state = AttackState.Traveling;
            actor.SetAnimation("Attack");
            projectileAnimation.Play();
            actor.CurrentAnimation.IsPaused = true;
            SoundSystem.Play(AudioCues.Focus);
         
        }

        public override void Update(GameTime gameTime)
        {
            projectileAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            switch (state)
            {
                case AttackState.Traveling:
                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (projectileAnimation.IsRunning == false)
                    {
                        state = AttackState.Returning;
                        SoundSystem.Play(AudioCues.CaraGrunt);
                        DealPhysicalDamage(actor, targets.ToArray());
                    }
                    break;

                //case AttackState.Striking:
                //    if (actor.CurrentAnimation.IsRunning == false)
                //    {
                //        state = AttackState.Returning;

                //        actor.SetAnimation("Dash");

                //        DealPhysicalDamage(actor, targets.ToArray());

                //    }
                //    break;

                case AttackState.Returning:
                   // actor.ScreenPosition -= velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                   // if (Vector2.Distance(actor.ScreenPosition, initialPosition) < velocity.Length())
                    {
                        actor.SetAnimation("Idle");
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(state == AttackState.Traveling)
            projectileAnimation.Draw(spriteBatch, screenPosition + DrawOffset, effect);
        }
    }

}
