using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class VoodooDollAttack : Attack
    {
        #region Frame Animations

        static readonly FrameAnimation TravelingFrame = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   0,  200, 200),
            }
        };

        #endregion

        #region Fields


        CombatAnimation needleAnimation;

        Vector2 screenPosition;

        #endregion

        public VoodooDollAttack() : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA349;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 3.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-100, -100);

            needleAnimation = new CombatAnimation()
            {
                Name = "Needle",
                TextureName = "needle",
                Loop = true,
                Animation = TravelingFrame
            };

            needleAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ProjectileOrigin;

            destinationPosition = targets[0].HitLocation;

            screenPosition = initialPosition;

            velocity =
                (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds(.75).TotalMilliseconds;

            actor.SetAnimation("Attack");
            state = AttackState.Traveling;
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case AttackState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Throw");
                        needleAnimation.Play();
                        state = AttackState.Striking;
                    }

                    break;

                case AttackState.Striking:

                    needleAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (screenPosition.X <= destinationPosition.X)
                    {
                        actor.SetAnimation("Idle");
                        isRunning = false;
                        DealPhysicalDamage(actor, targets.ToArray());
                    }

                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == AttackState.Striking)
            {
                needleAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
            }
        }
    }
}
