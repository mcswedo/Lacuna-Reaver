using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MyQuest
{
    class BoggimusTadpoleAttack : Attack
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


        CombatAnimation armAnimation;

        Vector2 screenPosition;

        #endregion

        public BoggimusTadpoleAttack() : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA332;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-100, -100);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            armAnimation = new CombatAnimation()
            {
                Name = "Arm",
                TextureName = "arm",
                Loop = true,
                Animation = TravelingFrame
            };

            armAnimation.LoadContent(ContentPath.ToSkillTextures);
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
                    if (actor.CurrentAnimation.GetCurrentFrame == 4) // IsRunning == false)
                    {
                        actor.CurrentAnimation.IsPaused = true;
                        //actor.SetAnimation("Throw");
                        armAnimation.Play();
                        state = AttackState.Striking;
                    }

                    break;

                case AttackState.Striking:

                    armAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (screenPosition.X <= destinationPosition.X)
                    {
                        DealPhysicalDamage(actor, targets.ToArray());

                        state = AttackState.Returning;
                    }

                    break;

                case AttackState.Returning:

                    actor.CurrentAnimation.IsPaused = false;

                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == AttackState.Striking)
            {
                armAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
            }
        }
    }
}
