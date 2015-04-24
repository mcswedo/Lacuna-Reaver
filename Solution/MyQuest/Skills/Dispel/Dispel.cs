using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Dispel : Skill
    {
        #region Frame Animations


        static readonly FrameAnimation HealthRingFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>()
            {
                new Rectangle(1600, 0, 200, 200), 
                new Rectangle(1400, 0, 200, 200),
                new Rectangle(1200, 0, 200, 200),  
                new Rectangle(1000, 0, 200, 200), 
                new Rectangle(800, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),   
                new Rectangle(200, 0, 200, 200),  
                new Rectangle(0, 0, 200, 200)                                                                                    
            }
        };
       

        #endregion

        #region Fields

        CombatAnimation healthRingAnimation;

        Vector2 screenPosition;
  


        #endregion

        #region Constructor


        public Dispel()
        {
            Name = Strings.ZA370;
            Description = Strings.ZA371;

            MpCost = 20;
            SpCost = 2;

            SpellPower = 0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = false;

            TargetsAll = false;
            CanTargetAllies = true;
            CanTargetEnemy = true;

            healthRingAnimation = new CombatAnimation()
            {
                Name = "HealthRingAnimation",
                TextureName = "purple",
                Animation = HealthRingFrames,
                Loop = false
            };

            healthRingAnimation.LoadContent(ContentPath.ToSkillTextures);
            DrawOffset = new Vector2(-100, -100);
        }



        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = targets[0].HitLocation;

            healthRingAnimation.Play();

            actor.SetAnimation("DispelAttack");
            SoundSystem.Play(AudioCues.Analyze);
        }

        public override void Update(GameTime gameTime)
        {
            healthRingAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (healthRingAnimation.IsRunning == false)
            {
                actor.CurrentAnimation.IsPaused = false;
                actor.SetAnimation("Idle");
                isRunning = false;
                if (targets[0].HasDestiny())
                {
                    targets[0].RemoveDestiny();
                }
                targets[0].RemoveAllNegativeStatusEffects();
                targets[0].RemoveAllNegativeDamageModifiers();

                if (targets[0].Blind)
                {
                    targets[0].Blind = false;
                }
            }
           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            healthRingAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
