using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class PaperMachete : Skill
    {
        enum PaperMacheteState
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


        List<CombatAnimation> paperMacheteAnimations;
        int currentAnimation;

        PaperMacheteState state;

        Vector2 screenPosition;
        Vector2 destinationPosition;
        Vector2 velocity;

        SpriteEffects effect;


        #endregion

        #region Constructor


        public PaperMachete()
        {
            Name = "Paper Machete";
            Description = "Fires a storm of razor sharp paper bits";

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

            paperMacheteAnimations = new List<CombatAnimation>()
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
                    Name = "Impaact",
                    TextureName = "FireBall",
                    Loop = false,
                    Animation = ImpactFrames
                }
            };

            foreach (CombatAnimation anim in paperMacheteAnimations)
                anim.LoadContent(ContentPath.ToSkillTextures);
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

            state = PaperMacheteState.Charging;
            currentAnimation = 0;
            paperMacheteAnimations[currentAnimation].Play();
            actor.CurrentAnimation.IsPaused = true;
        }

        public override void Update(GameTime gameTime)
        {
            paperMacheteAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            switch (state)
            {
                case PaperMacheteState.Charging:
                    if (paperMacheteAnimations[currentAnimation].IsRunning == false)
                    {
                        paperMacheteAnimations[++currentAnimation].Play();
                        state = PaperMacheteState.Traveling;
                    }
                    break;

                case PaperMacheteState.Traveling:
                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (paperMacheteAnimations[currentAnimation].IsRunning == false)
                    {
                        paperMacheteAnimations[++currentAnimation].Play();
                        state = PaperMacheteState.Impact;
                    }
                    break;

                case PaperMacheteState.Impact:
                    if (paperMacheteAnimations[currentAnimation].IsRunning == false)
                    {
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;

                        DealMagicDamage(actor, targets.ToArray());
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            paperMacheteAnimations[currentAnimation].Draw(spriteBatch, screenPosition, effect);
        }
    }
}
