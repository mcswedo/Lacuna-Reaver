using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    class Aegis : Skill
    {
        #region Frame Animations

        static readonly FrameAnimation HealthRingFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 200, 200),                
                new Rectangle(200, 0, 200, 200),                  
                new Rectangle(400, 0, 200, 200),                
                new Rectangle(600, 0, 200, 200),                  
                new Rectangle(800, 0, 200, 200),                
                new Rectangle(1000, 0, 200, 200),                  
                new Rectangle(1200, 0, 200, 200),                
                new Rectangle(1400, 0, 200, 200), 
                new Rectangle(1600, 0, 200, 200), 
            }
        };


        #endregion

        #region Combat Animations


        CombatAnimation healthRingAnimation;


        #endregion

        #region Fields

        Vector2 screenPosition;

        #endregion

        public Aegis()
        {
            Name = Strings.ZA327;
            Description = Strings.ZA316;

            MpCost = 45;
            SpCost = 3;

            SpellPower = 0;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            TargetsAll = false;
            CanTargetAllies = true;
            CanTargetEnemy = false;

            DrawOffset = new Vector2(-65, -105);

            healthRingAnimation = new CombatAnimation()
            {
                Name = "HealthRingAnimation",
                TextureName = "aegis",
                Animation = HealthRingFrames,
                Loop = false
            };

            healthRingAnimation.LoadContent(ContentPath.ToSkillTextures);
            DrawOffset = new Vector2(-100, -100);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            screenPosition = targets[0].HitLocation;

            healthRingAnimation.Play();
            actor.SetAnimation("DispelAttack");
            SoundSystem.Play(AudioCues.Heal);
        }

        public override void Update(GameTime gameTime)
        {
            healthRingAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (healthRingAnimation.IsRunning == false)
            {
                foreach (FightingCharacter target in targets)
                {
                    StatusEffect effect = new Warded(3);
                    StatusEffect effect2 = new Armored(3);

                    SetStatusEffect(actor, effect, target);
                    SetStatusEffect(actor, effect2, target);
                }

                actor.SetAnimation("Idle");
                isRunning = false;
            }
        }

        //public override void OutOfCombatActivate(FightingCharacter actor, params FightingCharacter[] targets)
        //{
        //    Debug.Assert(actor.FighterStats.Energy >= MpCost);

        //    actor.FighterStats.Energy -= MpCost;
        //    foreach (FightingCharacter target in targets)
        //    {
        //        target.FighterStats.Health += (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);
        //    }
        //}

        public override void Draw(SpriteBatch spriteBatch)
        {
            healthRingAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
