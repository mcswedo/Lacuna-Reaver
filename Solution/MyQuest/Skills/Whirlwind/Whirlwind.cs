using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Whirlwind : Skill
    {
        enum WhirlwindState
        {
            Traveling,
            Impact
        }

        #region Frame Animations

        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   0, 400, 400),
                new Rectangle(400,   0, 400, 400),
                new Rectangle(800,   0, 400, 400),
                new Rectangle(1200,   0, 400, 400),
                new Rectangle(1600,   0, 400, 400),

                new Rectangle(0,   400, 400, 400),
                new Rectangle(400,   400, 400, 400),
                new Rectangle(800,   400, 400, 400),
                new Rectangle(1200,   400, 400, 400),
                new Rectangle(1600,   400, 400, 400),

                new Rectangle(0,   800, 400, 400),
                new Rectangle(400,   800, 400, 400),
            }
        };

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(800,   800, 600, 400),
                new Rectangle(1400,   800, 600, 400),
               
                new Rectangle(0,   1200, 800, 400),
                new Rectangle(800,   1200, 800, 400), 
            }
        };


        #endregion

        #region Fields


        List<CombatAnimation> whirlwindAnimations;
        int currentAnimation;

        WhirlwindState state;

        Vector2 screenPosition;
        Vector2 destinationPosition;
        Vector2 velocity;

        SpriteEffects effect;


        #endregion

        #region Constructor


        public Whirlwind()
        {
            Name = Strings.ZA504;
            Description = Strings.ZA505;

            MpCost = 20;
            SpCost = 5;

            SpellPower = 5.0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-230, -230);

            whirlwindAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "gust_attack",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impact",
                    TextureName = "gust_attack",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in whirlwindAnimations)
                anim.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ProjectileOrigin;

            destinationPosition = targets[0].ScreenPosition;

            effect = (destinationPosition.X < screenPosition.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            velocity =
                (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            state = WhirlwindState.Traveling;
            currentAnimation = 0;
            whirlwindAnimations[currentAnimation].Play();
            actor.SetAnimation("Flap");

            SoundSystem.Play(AudioCues.Wind);
        }

        public override void Update(GameTime gameTime)
        {
            whirlwindAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle");
            }

            switch (state)
            {
                case WhirlwindState.Traveling:
                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (whirlwindAnimations[currentAnimation].IsRunning == false)
                    {
                        whirlwindAnimations[++currentAnimation].Play();
                        state = WhirlwindState.Impact;
                    }
                    break;

                case WhirlwindState.Impact:
                    string OnHitSound = targets[0].OnHitSoundCue;
                    string HitNoise = targets[0].HitNoiseSoundCue;
                    if (whirlwindAnimations[currentAnimation].IsRunning == false)
                    {

                        isRunning = false;

                        targets[0].OnHitSoundCue = null;
                        targets[0].HitNoiseSoundCue = null;

                        DealMagicDamage(actor, targets.ToArray());
                    }

                    targets[0].OnHitSoundCue = OnHitSound;
                    targets[0].HitNoiseSoundCue = HitNoise;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            whirlwindAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset, effect);
        }
    }
}
