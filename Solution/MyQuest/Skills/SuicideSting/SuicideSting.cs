using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class SuicideSting : Attack
    {
        protected enum FrostFeeshAttackState
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
        FrostFeeshAttackState attackState;
        Vector2 screenPosition;
        Vector2 stingerOriginOffset; 

        #region Constructor


        public SuicideSting()
        {
            Name = Strings.ZA488;
            Description = Strings.ZA489;

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

            DrawOffset = new Vector2(-50, -50);
            stingerOriginOffset = new Vector2(-50, -75); 

            stingerAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Stinger",
                    TextureName = "ice_feesh_stinger",
                    Loop = true,
                    Animation = StingerFrames
                }
            };

            foreach (CombatAnimation anim in stingerAnimations)
            {
                anim.LoadContent(ContentPath.ToCombatCharacterTextures);
            }
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation - DrawOffset;

            screenPosition = actor.ScreenPosition + stingerOriginOffset;

            velocity = (destinationPosition - screenPosition);
            velocity.Normalize();
            velocity.X *= 7.5f;
            velocity.Y *= 7.5f;

            attackState = FrostFeeshAttackState.Throwing;
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
                case FrostFeeshAttackState.Throwing:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        attackState = FrostFeeshAttackState.Traveling;
                        actor.SetAnimation("ThrowPose");
                        actor.CurrentAnimation.IsPaused = true;
                        stingerAnimations[0].Play();

                        SoundSystem.Play(AudioCues.Attack);
                    }
                    break;

                case FrostFeeshAttackState.Traveling:
                    screenPosition += velocity;
                    if (Vector2.Distance(screenPosition, destinationPosition) < velocity.Length())
                    {
                        screenPosition = destinationPosition;
                        attackState = FrostFeeshAttackState.Returning;
                        //DealPhysicalDamage(actor, targets.ToArray());
                        actor.CurrentAnimation.IsPaused = false;
                        stingerAnimations[0].IsRunning = false;
                        actor.SetAnimation("Retracting");
                        SoundSystem.Play(AudioCues.SwordHitArmor);
                    }
                    break;

                case FrostFeeshAttackState.Returning:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.SetAnimation("Idle");
                        isRunning = false;

                        float oldSpellPower = this.SpellPower;
                        float modifiedSpellPower = oldSpellPower + (float)(actor.FighterStats.Health * (.003));
                        this.SpellPower = modifiedSpellPower; //The Ice Feesh's health is a factor in the damage dealt. The more health, the more damage is dealt.

                        DealMagicDamage(actor, targets.ToArray());
                        this.SpellPower = oldSpellPower;
                        actor.FighterStats.Health = 0;
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
