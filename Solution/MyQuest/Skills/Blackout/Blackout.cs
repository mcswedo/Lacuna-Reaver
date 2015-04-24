using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Blackout : Skill
    {
        #region Frame Animations

        enum FireBallState
        {
            Charging,
            Traveling,
            Impact
        }

        static readonly FrameAnimation BlindingFrames = new FrameAnimation()
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>()
            {
            
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),
                new Rectangle(200, 600, 200, 200),
                new Rectangle(400, 600, 200, 200),
                new Rectangle(600, 600, 200, 200),
                new Rectangle(800, 600, 200, 200), 
            }
        };

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

        #region Combat Animations


        CombatAnimation blackoutAnimation;
        List<CombatAnimation> fireBallAnimations;

        #endregion

        #region Fields

        Vector2 screenPosition;
        Vector2 targetPosition;
        int currentAnimation;

        FireBallState state;
        Vector2 destinationPosition;
        Vector2 velocity;

        SpriteEffects effect;

        TimeSpan delayTimer;
        bool damageApplied;

        #endregion

        public Blackout()
        {
            Name = Strings.ZA351;
            Description = Strings.ZA352;

            MpCost = 115;
            SpCost = 6;

            SpellPower = Will.Instance.FighterStats.Level;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            blackoutAnimation = new CombatAnimation()
            {
                Name = "BlindAnimation",
                TextureName = "blind",
                Animation = BlindingFrames,
                Loop = false
            };

            blackoutAnimation.LoadContent(ContentPath.ToSkillTextures);

            fireBallAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "shadowBall",
                    Loop = false,
                    Animation = ChargingFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "shadowBall",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "shadowBall",
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
            if (!damageApplied)
            {
                fireBallAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            if (state == FireBallState.Impact)
            {
                blackoutAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            }

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
                        targetPosition = targets[0].ScreenPosition + new Vector2(-65, -105);
                        blackoutAnimation.Play();
                        SoundSystem.Play(AudioCues.Cloud);
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

                        foreach (FightingCharacter target in targets)
                        {
                            if (target.State != State.Dead && SkillHit)
                            {
                                StatusEffect effect = new Blindness(3);
                                SetStatusEffect(actor, effect, target);
                            }
                        }
                        return;
                    }
                    delayTimer -= gameTime.ElapsedGameTime;
                    if (delayTimer <= TimeSpan.Zero)
                    {
                        targets[0].OnHitSoundCue = OnHitSound;
                        targets[0].HitNoiseSoundCue = HitNoise;
                        actor.CurrentAnimation.IsPaused = false;

                        if (blackoutAnimation.IsRunning == false)
                        {
                            isRunning = false;
                        }
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == FireBallState.Impact)
            {
                blackoutAnimation.Draw(spriteBatch, targetPosition, SpriteEffects.None);
            }
            if (!damageApplied)
            {
                fireBallAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset, effect);
            }
        }

        protected override void SetStatusEffect(FightingCharacter actor, StatusEffect effect, FightingCharacter target)
        {
            if (target is Boggimus || target is Chepetawa || target is Serlynx ||
                target is Arlan || target is Malticar)
            {
                CombatMessage.AddMessage("Immune", target.StatusEffectMessagePosition, .5);
            }
            else
            {
                base.SetStatusEffect(actor, effect, target);
            }
        }
    }
}
