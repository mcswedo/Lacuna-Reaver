using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    class FeeshAttack : Attack
    {
        protected enum FeeshAttackState
        {
            Throwing,
            Traveling,
            Returning,
        }

        static readonly FrameAnimation StingerFrames = new FrameAnimation()
        {
            FrameDelay = 1,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 64, 64),
            }
        };

        List<CombatAnimation> stingerAnimations;
        FeeshAttackState attackState;
        Vector2 screenPosition;
        Vector2 stingerOriginOffset; 

        public FeeshAttack()
            : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA337;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-50, -50);

            stingerOriginOffset = new Vector2(-50, -75); 

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            stingerAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Stinger",
                    TextureName = "feesh_stinger",
                    Loop = true,
                    Animation = StingerFrames
                }
            };

            foreach (CombatAnimation anim in stingerAnimations)
            {
                anim.LoadContent(ContentPath.ToCombatCharacterTextures);
            }
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation - DrawOffset;

            screenPosition = actor.ScreenPosition; // + stingerOriginOffset;

            velocity = (destinationPosition - screenPosition);
            velocity.Normalize();
            velocity.X *= 7.5f;
            velocity.Y *= 7.5f;

            attackState = FeeshAttackState.Throwing;
            actor.SetAnimation("Throwing");
        }

        public override void Update(GameTime gameTime)
        {

            if (stingerAnimations[0].IsRunning)
            {
                stingerAnimations[0].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            switch (attackState)
            {
                case FeeshAttackState.Throwing:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        attackState = FeeshAttackState.Traveling;
                        actor.SetAnimation("ThrowPose");
                        actor.CurrentAnimation.IsPaused = true;
                        stingerAnimations[0].Play();

                        SoundSystem.Play(AudioCues.Attack);
                    }
                    break;

                case FeeshAttackState.Traveling:
                    screenPosition += velocity;
                    if (Vector2.Distance(screenPosition, destinationPosition) < velocity.Length())
                    {
                        screenPosition = destinationPosition;
                        attackState = FeeshAttackState.Returning;
                        DealPhysicalDamage(actor, targets.ToArray());
                        actor.CurrentAnimation.IsPaused = false;
                        stingerAnimations[0].IsRunning = false;
                        actor.SetAnimation("Retracting");
                        SoundSystem.Play(AudioCues.SwordHitArmor);
                    }
                    break;

                case FeeshAttackState.Returning:
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
            if (stingerAnimations[0].IsRunning)
            {
                stingerAnimations[0].Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
            }
        }
    }
}
