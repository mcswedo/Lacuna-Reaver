using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class TohMeyAttack : Attack
    {

        #region Frame Animations

        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0,   0,  90, 80),
            }
        };

        #endregion

        #region Fields


        CombatAnimation headButtAnimation;

        Vector2 screenPosition;

        #endregion

        public TohMeyAttack() : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA347;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-50, -50);

            headButtAnimation = new CombatAnimation()
                {
                    Name = "Headbutt",
                    TextureName = "headbutt",
                    Loop = true,
                    Animation = TravelingFrames
                };

            headButtAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ProjectileOrigin;

            destinationPosition = targets[0].HitLocation + DrawOffset;

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
                        actor.SetAnimation("Headless");
                        headButtAnimation.Play();
                        state = AttackState.Striking;
                    }

                    break;

                case AttackState.Striking:

                    headButtAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (screenPosition.X <= destinationPosition.X)
                    {
                        isRunning = false;
                        DealPhysicalDamage(actor, targets.ToArray());
                        actor.SetAnimation("Idle");
                    }

                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == AttackState.Striking)
            {
                headButtAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
            }
        }
    }
}
