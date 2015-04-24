using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class UnholyRevelations : Skill
    {
        enum UnholyRevelationsState
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
                new Rectangle(0, 0, 400, 200),
                new Rectangle(400, 0, 400, 200),
                new Rectangle(800, 0, 400, 200),
            }
        };

        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 200, 400, 200),
                new Rectangle(400, 200, 400, 200),
                new Rectangle(800, 200, 400, 200),

                new Rectangle(0, 400, 400, 200),
                new Rectangle(400, 400, 400, 200),
                new Rectangle(800, 400, 400, 200),

                new Rectangle(0,  600, 400, 200),
            }
        };

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(400, 600, 400, 200),
                new Rectangle(800, 600, 400, 200),

                new Rectangle(0, 800, 400, 200),
                new Rectangle(400, 800, 400, 200),
                new Rectangle(800, 800, 400, 200),
            }
        };


        #endregion

        #region Fields


        List<CombatAnimation> unholyRevelationsAnimations;
        int currentAnimation;

        UnholyRevelationsState state;

        Vector2 screenPosition;
        Vector2 destinationPosition;
        Vector2 velocity;

        #endregion

        #region Constructor


        public UnholyRevelations()
        {
            Name = Strings.ZA492;
            Description = Strings.ZA493;

            MpCost = 20;
            SpCost = 3;

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

            DrawOffset = new Vector2(-180, -100);

            unholyRevelationsAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Charging",
                    TextureName = "revelation",
                    Loop = false,
                    Animation = ChargingFrames
                },
                new CombatAnimation()
                {
                    Name = "Traveling1",
                    TextureName = "revelation",
                    Loop = false,
                    Animation = TravelingFrames
                },
                new CombatAnimation()
                {
                    Name = "Impaact",
                    TextureName = "revelation",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in unholyRevelationsAnimations)
                anim.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ProjectileOrigin + new Vector2(90,-30);

            destinationPosition = targets[0].HitLocation;

            velocity =
                (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds((TravelingFrames.FrameDelay * TravelingFrames.Frames.Count)).TotalMilliseconds;

            state = UnholyRevelationsState.Charging;
            currentAnimation = 0;
            unholyRevelationsAnimations[currentAnimation].Play();
            actor.SetAnimation("RevelationAttack");
            //actor.CurrentAnimation.IsPaused = true;
        }

        public override void Update(GameTime gameTime)
        {
            unholyRevelationsAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            switch (state)
            {
                case UnholyRevelationsState.Charging:
                    if (unholyRevelationsAnimations[currentAnimation].IsRunning == false)
                    {
                        unholyRevelationsAnimations[++currentAnimation].Play();
                        state = UnholyRevelationsState.Traveling;
                    }
                    break;

                case UnholyRevelationsState.Traveling:
                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (unholyRevelationsAnimations[currentAnimation].IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                        unholyRevelationsAnimations[++currentAnimation].Play();
                        state = UnholyRevelationsState.Impact;
                    }
                    break;

                case UnholyRevelationsState.Impact:
                    if (unholyRevelationsAnimations[currentAnimation].IsRunning == false)
                    {
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;
                        if (actor is AgoraElderMantis)
                        {
                            SpellPower = 19.7f;
                        }
                        DealMagicDamage(actor, targets.ToArray());
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            unholyRevelationsAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
