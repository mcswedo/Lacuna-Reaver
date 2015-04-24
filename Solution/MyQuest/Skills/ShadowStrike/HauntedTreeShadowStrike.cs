using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    class HauntedTreeShadowStrike : Demon1ShadowStrike
    {
        protected enum TreeAttackState
        {
            Throwing,
            Traveling,
        }

        static readonly FrameAnimation ShadowFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>
            {
                new Rectangle(252,390, 252, 195),
                new Rectangle(504,390, 252, 195),
                /*new Rectangle(252,390, 252, 195),
                new Rectangle(504,390, 252, 195),
                new Rectangle(252,390, 252, 195),
                new Rectangle(504,390, 252, 195),
                new Rectangle(252,390, 252, 195),
                new Rectangle(504,390, 252, 195),*/
            }
        };

        List<CombatAnimation> shadowAnimations;
        TreeAttackState attackState;
        Vector2 shadowOriginOffset; 

        public HauntedTreeShadowStrike() : base()
        {
            Name = Strings.ZA472;
            Description = Strings.ZA473;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 20.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-50, -50);

            shadowOriginOffset = new Vector2(-100, -80); 

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            shadowAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "ShadowHead",
                    TextureName = "swamp_tree_haunt",
                    Loop = true,
                    Animation = ShadowFrames
                }
            };

            foreach (CombatAnimation anim in shadowAnimations)
            {
                anim.LoadContent(ContentPath.ToCombatCharacterTextures);
            }
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation + DrawOffset;

            screenPosition = actor.ScreenPosition + shadowOriginOffset;

            velocity = (destinationPosition - screenPosition);
            velocity.Normalize();
            velocity.X *= 7.5f;
            velocity.Y *= 7.5f;

            attackState = TreeAttackState.Throwing;
            actor.SetAnimation("ShadowStrike");
        }

        public override void Update(GameTime gameTime)
        {
            if (shadowAnimations[0].IsRunning)
            {
                shadowAnimations[0].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            switch (attackState)
            {
                case TreeAttackState.Throwing:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        attackState = TreeAttackState.Traveling;
                        actor.SetAnimation("Idle");
                        actor.CurrentAnimation.IsPaused = true;
                        shadowAnimations[0].Play();

                        SoundSystem.Play(AudioCues.Attack);
                    }
                    break;

                case TreeAttackState.Traveling:
                    screenPosition += velocity;
                    if (Vector2.Distance(screenPosition, destinationPosition) < velocity.Length())
                    {
                        DealPhysicalDamage(actor, targets.ToArray());
                        actor.CurrentAnimation.IsPaused = false;
                        shadowAnimations[0].IsRunning = false;
                    }

                    if (shadowAnimations[0].IsRunning == false)
                    {
                        SoundSystem.Play(AudioCues.SwordHitArmor);
                        isRunning = false;
                    }
                    break;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (shadowAnimations[0].IsRunning)
            {
                shadowAnimations[0].Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }
        }
    }
}