using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Regenerate : Skill
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

        public Regenerate()
        {
            Name = Strings.ZA462;
            Description = Strings.ZA463;

            MpCost = 15;
            SpCost = 3;

            SpellPower = 3;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = true;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = true;
            CanTargetEnemy = false;

            healthRingAnimation = new CombatAnimation()
            {
                Name = "HealthRingAnimation",
                TextureName = "green",
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
            healthRingAnimation.Play();
            if (actor.Name == "Cara")
            {
                actor.SetAnimation("RegenAttack");
            }
            SoundSystem.Play(AudioCues.Heal);
        }

        public override void Update(GameTime gameTime)
        {
            healthRingAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (healthRingAnimation.IsRunning == false)
            {
                foreach (FightingCharacter target in targets)
                {
                    StatusEffect regeneration = new Regeneration(5, 1.0f, 1/16f);
                    SetStatusEffect(actor, regeneration, target);
                }
                if (actor.Name == "Cara")
                {
                    actor.SetAnimation("Idle");
                }
                isRunning = false;
            }
        }

        //public override void OutOfCombatActivate(FightingCharacter actor, params FightingCharacter[] targets)
        //{
        //    //Debug.Assert(actor.Stats.Energy >= MpCost);

        //    if (actor.FighterStats.Energy >= MpCost)
        //    {
        //        actor.FighterStats.Energy -= MpCost;
        //        foreach (FightingCharacter target in targets)
        //        {
        //            target.FighterStats.Health += (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);
        //        }
        //    }
        //}

        public override void Draw(SpriteBatch spriteBatch)
        {
            healthRingAnimation.Draw(spriteBatch, targets[0].HitLocation + DrawOffset, SpriteEffects.None);
        }
    }
}
