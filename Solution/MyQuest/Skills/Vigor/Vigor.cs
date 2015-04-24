using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Vigor : Skill
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

        #region Fields


        CombatAnimation healthRingAnimation;
 
        Vector2 screenPosition;



        #endregion

        #region Constructor


        public Vigor()
        {
            Name = Strings.ZA494;
            Description = Strings.ZA495;

            MpCost = 20;
            SpCost = 4;

            SpellPower = 0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            TargetsAll = false;
            CanTargetAllies = true;
            CanTargetEnemy = false;

            healthRingAnimation = new CombatAnimation()
            {
                Name = "HealthRingAnimation",
                TextureName = "red",
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

            SoundSystem.Play(AudioCues.Analyze);

            if (actor.Name == "Cara")
            {
                actor.SetAnimation("HealAttack");
            }
        }

        public override void Update(GameTime gameTime)
        {
            healthRingAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);


            if (healthRingAnimation.IsRunning == false)
            {
                actor.CurrentAnimation.IsPaused = false;
                isRunning = false;

                foreach (FightingCharacter target in targets)
                {
                    StatusEffect envigored = new Envigored(3);

                    SetStatusEffect(actor, envigored, target);
                }

                if (actor.Name == "Cara")
                {
                    actor.SetAnimation("Idle");
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            healthRingAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
