using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class DemonGearAttack : Attack
    {
        static readonly FrameAnimation ProjectileFrames = new FrameAnimation()
        {
            FrameDelay = .5,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 200, 200)
            }
        };

        CombatAnimation projectileAnimation;
        Vector2 screenPosition;
        public DemonGearAttack()
            : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA384;

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

            projectileAnimation = new CombatAnimation()
            {
                Name = "GearProjectile",
                TextureName = "gear_projectile",
                Loop = false,
                Animation = ProjectileFrames,
            };

            projectileAnimation.LoadContent(ContentPath.ToSkillTextures);
        
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation;

            velocity =
               (destinationPosition - screenPosition) / (float)TimeSpan.FromSeconds((ProjectileFrames.FrameDelay * ProjectileFrames.Frames.Count)).TotalMilliseconds;

            projectileAnimation.Play();
            actor.SetAnimation("Idle");
            SoundSystem.Play(AudioCues.Fireball);
        }

        public override void Update(GameTime gameTime)
        {
            screenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            projectileAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (projectileAnimation.IsRunning == false)
            {   
                DealPhysicalDamage(actor, targets.ToArray());

                SoundSystem.Play(AudioCues.SwordHitArmor);
                isRunning = false;
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            projectileAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
