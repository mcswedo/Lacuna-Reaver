using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class Lightning : Skill
    {
        #region Frame Animations

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 400, 400),
                new Rectangle(400, 0, 400, 400),
                new Rectangle(800, 0, 400, 400),
                new Rectangle(1200, 0, 400, 400),
                new Rectangle(1600, 0, 400, 400),
                new Rectangle(0, 400, 400, 400),
                new Rectangle(400, 400, 400, 400),
                new Rectangle(800, 400, 400, 400),
                new Rectangle(1200, 400, 400, 400),
                new Rectangle(1600, 400, 400, 400),
            }
        };


        #endregion

        #region Fields

        TimeSpan delayTimer;
        bool damageApplied;

        CombatAnimation lightningAnimation;
        Vector2 destinationPosition;

        #endregion

        #region Constructor


        public Lightning()
        {
            Name = Strings.ZA422;
            Description = Strings.ZA424;

            MpCost = 120;
            SpCost = 7;

            SpellPower = Cara.Instance.FighterStats.Level + 3f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
         //   DrawOffset = new Vector2(-400, -400);
            lightningAnimation = new CombatAnimation()
                {
                    Name = "Impact",
                    TextureName = "cara_lightning",
                    Loop = false,
                    Animation = ImpactFrames,
                    DrawOffset = new Vector2(-220, -300)
                };

                lightningAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation;

            damageApplied = false;

            lightningAnimation.Play();
            if (actor.Name == "Cara")
            {
                actor.SetAnimation("EarthAttack");
            }
            if (actor.Name == "Agora Demon 3")
            {
                actor.SetAnimation("Attack");
            }
            SoundSystem.Play(AudioCues.ScreenFlashCrack);
        }

        public override void Update(GameTime gameTime)
        {
            Debug.Assert(isRunning);
            if (lightningAnimation.IsRunning)
            {
                lightningAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                return;
            }
            if (!damageApplied)
            {
                DealMagicDamage(actor, targets.ToArray());
                if (targets[0].State != State.Invulnerable && SkillHit)
                {
                    StatusEffect paralyzed = new Paralyzed(2);
                    SetStatusEffect(actor, paralyzed, targets[0]);
                }
                delayTimer = TimeSpan.FromSeconds(.28);
                damageApplied = true;
                return;
            }
            delayTimer -= gameTime.ElapsedGameTime;
            if (delayTimer <= TimeSpan.Zero)
            {
                isRunning = false;
                if (actor.Name == "Cara" || actor.Name == "Agora Demon 3")
                {
                    actor.SetAnimation("Idle");
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (lightningAnimation.IsRunning)
            {
                lightningAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
        }

        protected override void SetStatusEffect(FightingCharacter actor, StatusEffect effect, FightingCharacter target)
        {
            if (target is Boggimus || target is Chepetawa || target is Serlynx ||
                target is Arlan || target is Malticar)
            {
                CombatMessage.AddMessage("Immune", target.StatusEffectMessagePosition, .5);
            }
            else
            {
                base.SetStatusEffect(actor, effect, target);
            }
        }
    }
}
