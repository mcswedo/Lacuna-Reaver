using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class SwordWave : Skill
    {
        #region Fields

        StrikeState state;

        Vector2 initialPosition;
        Vector2 destinationPosition;
        Vector2 velocity;
        CombatAnimation projectileAnimation;
        CombatAnimation burstAnimation;
        Vector2 screenPosition;

        #endregion

        static readonly FrameAnimation WaveFrames = new FrameAnimation()
        {
            FrameDelay = .5,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 361, 222)
            }
        };

        static readonly FrameAnimation BurstFrames = new FrameAnimation()
        {
            FrameDelay = .05,
            Frames = new List<Rectangle>()
            {
                new Rectangle(768, 256, 256, 256),  
                new Rectangle(512, 256, 256, 256),
                new Rectangle(256, 256, 256, 256),
                new Rectangle(0,   256, 256, 256),
            
                new Rectangle(768, 0, 256, 256),
                new Rectangle(512, 0, 256, 256),
                new Rectangle(256, 0, 256, 256),
                new Rectangle(0, 0, 256, 256)         
            }
        };

        #region Constructor


        public SwordWave()
        {
            Name = Strings.ZA490;
            Description = Strings.ZA491;

            MpCost = 120;
            SpCost = 8;

            SpellPower = 1.5f;
            DamageModifierValue = 2f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            projectileAnimation = new CombatAnimation()
            {
                Name = "WaveProjectile",
                TextureName = "wave",
                Loop = false,
                Animation = WaveFrames,
                DrawOffset = new Vector2(-100,-111)
            };

            burstAnimation = new CombatAnimation()
            {
                Name = "Burst",
                TextureName = "burst",
                Loop = false,
                Animation = BurstFrames,
                DrawOffset = new Vector2(0, -128)
            };

            projectileAnimation.LoadContent(ContentPath.ToSkillTextures);
            burstAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            destinationPosition.X = actor.ScreenPosition.X;
            destinationPosition.Y = targets[0].ScreenPosition.Y;

            screenPosition = actor.ScreenPosition;

            initialPosition = actor.ScreenPosition;

            CombatAnimation dash = actor.GetAnimation("Dash");

            state = StrikeState.Traveling;
            actor.SetAnimation("Dash");
            SoundSystem.Play(AudioCues.Swoosh);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StrikeState.Striking;
                        actor.SetAnimation("Slash");
                        projectileAnimation.Play();
                
                    }
                    break;

                case StrikeState.Striking:
                    {
                        if (actor.CurrentAnimation.IsRunning == false)
                        {                          
                            state = StrikeState.Returning;

                            screenPosition = actor.ScreenPosition;                          

                            velocity =
                               (targets[0].HitLocation - screenPosition) / (float)TimeSpan.FromSeconds((WaveFrames.FrameDelay * WaveFrames.Frames.Count)).TotalMilliseconds;

                            SoundSystem.Play(AudioCues.Fireball);

                            actor.SetAnimation("Dash");
                            SoundSystem.Play(AudioCues.Swoosh);
                           
                        }
                        
                        break;
                    }

                case StrikeState.Returning:
                    {
                        if (actor.CurrentAnimation.IsRunning == false)
                        {
                            actor.ScreenPosition = initialPosition;
                            actor.SetAnimation("Idle");
                            if (projectileAnimation.IsRunning == false)
                            {
                                burstAnimation.Play();
                                state = StrikeState.DoubleStriking;  
                            }
                         
                        }

                        break;
                    }

                case StrikeState.DoubleStriking:
                    {
                        if (burstAnimation.IsRunning == false)
                        {
                            DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                            actor.AddDamageModifier(modifier);
                            DealPhysicalDamage(actor, targets.ToArray());

                            SoundSystem.Play(AudioCues.SwordHitArmor);
                            isRunning = false;

                        }

                        break;
                    }
            }

            if (state == StrikeState.Striking || state == StrikeState.Returning)
            {
                screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                projectileAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            }

            if (state == StrikeState.DoubleStriking)
            {
                burstAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }
          
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if ( state == StrikeState.Returning)
            {
                projectileAnimation.Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }

            if (state == StrikeState.DoubleStriking)
            {
                burstAnimation.Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }
        }
    }
}
