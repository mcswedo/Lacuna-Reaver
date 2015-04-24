using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class LacunaRadiance : Skill
    {

        enum FireBallState
        {
            Delaying,
            Charging,
            Traveling,
            Impact
        }

        #region Fields

        List<CombatAnimation> fireBallAnimations;
        List<CombatAnimation> fireBallAnimations2;
        int currentAnimation;
        int currentAnimation2;

        List<CombatAnimation> fireBallAnimations3;
        int currentAnimation3;

        FireBallState state;
        FireBallState state2;
        FireBallState state3;

        Vector2 screenPosition;
        Vector2 destinationPosition;

        Vector2 screenPosition2;
        Vector2 screenPosition3;


        Vector2 velocity;
        Vector2 velocity2;
        Vector2 velocity3;

        SpriteEffects effect;

        int counter;

        double ticker;
        double ticker2;


        #endregion

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


        #region Constructor


        public LacunaRadiance()
        {
            Name = Strings.ZA420;
            Description = Strings.ZA421;

            MpCost = 365;
            SpCost = 10;

            SpellPower = 1.5f;
            DamageModifierValue = 4f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            fireBallAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = ChargingFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in fireBallAnimations)
            {
                anim.LoadContent(ContentPath.ToSkillTextures);
            }

            fireBallAnimations2 = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = ChargingFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in fireBallAnimations2)
            {
                anim.LoadContent(ContentPath.ToSkillTextures);
            }

            fireBallAnimations3 = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = ChargingFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "sunburst",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in fireBallAnimations3)
            {
                anim.LoadContent(ContentPath.ToSkillTextures);
            }

            DrawOffset = new Vector2(-50, -50);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            ticker = TimeSpan.FromSeconds(1.25).TotalMilliseconds;

            ticker2 = TimeSpan.FromSeconds(2.25).TotalMilliseconds;

            SubtractCost(actor);

            screenPosition = actor.ProjectileOrigin;

            screenPosition2 = actor.ProjectileOrigin + new Vector2(80, 80);

            screenPosition3 = actor.ProjectileOrigin + new Vector2(80, -80);

            destinationPosition = targets[0].HitLocation;

            effect = (destinationPosition.X < screenPosition.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            velocity =
                (destinationPosition + new Vector2(80, 80) - screenPosition) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            velocity2 =
                (destinationPosition + new Vector2(80, -80) - screenPosition2) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            velocity3 =
               (destinationPosition - screenPosition3) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            state = FireBallState.Charging;
            state2 = FireBallState.Delaying;
            state3 = FireBallState.Delaying;

            currentAnimation = 0;
            currentAnimation2 = 0;
            currentAnimation3 = 0;
            counter = 0;
            fireBallAnimations[currentAnimation].Play();
            fireBallAnimations2[currentAnimation2].IsPaused = true;
            fireBallAnimations3[currentAnimation3].IsPaused = true;

            actor.SetAnimation("Charge");
            actor.CurrentAnimation.Loop = true;
            SoundSystem.Play(AudioCues.Fireball);

        }

        public override void Update(GameTime gameTime)
        {
            fireBallAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            fireBallAnimations2[currentAnimation2].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            fireBallAnimations3[currentAnimation3].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

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

                    if (fireBallAnimations[currentAnimation].IsRunning == false)
                    {
                        if (counter < 1)
                        {
                            state = FireBallState.Charging;
                            currentAnimation = 0;

                            screenPosition = actor.ProjectileOrigin;
                            fireBallAnimations[currentAnimation].Play();
                            SoundSystem.Play(AudioCues.Fireball);
                        }
                    }

                    break;
            }

            switch (state2)
            {
                case FireBallState.Delaying:
                    if (counter < 1)
                    {
                        ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (ticker <= 0)
                        {
                            fireBallAnimations2[currentAnimation2].Play();
                            SoundSystem.Play(AudioCues.Fireball);
                            state2 = FireBallState.Charging;
                        }
                    }

                    break;

                case FireBallState.Charging:


                    if (fireBallAnimations2[currentAnimation2].IsRunning == false)
                    {
                        fireBallAnimations2[++currentAnimation2].Play();
                        state2 = FireBallState.Traveling;
                    }


                    break;

                case FireBallState.Traveling:

                    screenPosition2 += velocity2 * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (fireBallAnimations2[currentAnimation2].IsRunning == false)
                    {
                        fireBallAnimations2[++currentAnimation2].Play();

                        state2 = FireBallState.Impact;
                    }
                    break;

                case FireBallState.Impact:

                    if (fireBallAnimations2[currentAnimation2].IsRunning == false)
                    {
                        state2 = FireBallState.Charging;

                        currentAnimation2 = 0;
                        ticker = TimeSpan.FromSeconds(1.25).TotalMilliseconds;
                        screenPosition2 = actor.ProjectileOrigin + new Vector2(80, 80);
                    }

                    break;
            }

            switch (state3)
            {
                case FireBallState.Delaying:

                    ticker2 -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (ticker2 <= 0)
                    {
                        fireBallAnimations3[currentAnimation3].Play();
                        SoundSystem.Play(AudioCues.Fireball);
                        state3 = FireBallState.Charging;
                    }

                    break;

                case FireBallState.Charging:


                    if (fireBallAnimations3[currentAnimation3].IsRunning == false)
                    {
                        fireBallAnimations3[++currentAnimation3].Play();
                        state3 = FireBallState.Traveling;
                    }


                    break;

                case FireBallState.Traveling:

                    screenPosition3 += velocity3 * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (fireBallAnimations3[currentAnimation3].IsRunning == false)
                    {
                        fireBallAnimations3[++currentAnimation3].Play();

                        state3 = FireBallState.Impact;
                    }
                    break;

                case FireBallState.Impact:

                    if (fireBallAnimations3[currentAnimation3].IsRunning == false)
                    {
                        state3 = FireBallState.Charging;

                        currentAnimation3 = 0;
                        ticker2 = TimeSpan.FromSeconds(2.25).TotalMilliseconds;
                        screenPosition3 = actor.ProjectileOrigin + new Vector2(80, -80);
                        counter++;
                        if (counter > 1)
                        {

                            DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                            actor.AddDamageModifier(modifier);
                            DealPhysicalDamage(actor, targets.ToArray());

                            //StatusEffect burning = new Burning();
                            //SetStatusEffect(actor, burning, targets[0]);
                            //Debug.Assert(actor.CurrentAnimation.Name.Equals("Charge"));
                            actor.CurrentAnimation.Loop = false;
                            actor.SetAnimation("Idle");
                            isRunning = false;

                        }
                    }

                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fireBallAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset, effect);

            if (!fireBallAnimations2[currentAnimation2].IsPaused)
            {
                fireBallAnimations2[currentAnimation2].Draw(spriteBatch, screenPosition2 + DrawOffset, effect);
            }

            if (!fireBallAnimations3[currentAnimation3].IsPaused)
            {
                fireBallAnimations3[currentAnimation3].Draw(spriteBatch, screenPosition3 + DrawOffset, effect);
            }
        }
    }
}
