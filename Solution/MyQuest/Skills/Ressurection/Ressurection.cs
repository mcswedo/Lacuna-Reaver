using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Ressurection : Skill
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

        bool targetIsDead = false; //This value is used to prevent the game from crashing. Draws when a dead target is selected.

        public Ressurection()
        {
            Name = Strings.ZA464;
            Description = Strings.ZA465;

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
                TextureName = "yellow",
                Animation = HealthRingFrames,
                Loop = false
            };

            healthRingAnimation.LoadContent(ContentPath.ToSkillTextures);
            DrawOffset = new Vector2(-100, -100);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            targetIsDead = true; //This value is used to prevent the game from crashing. Draws when a dead target is selected.
            SubtractCost(actor);
            healthRingAnimation.Play();
            if (actor.Name == "Cara")
            {
                actor.SetAnimation("HealAttack");
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
                    target.SetState(State.Normal);

                    if (target.FighterStats.Health <= 0)
                    {
                        target.FighterStats.Health = target.FighterStats.ModifiedMaxHealth / 2; //used to be by a quarter.
                        target.FighterStats.Energy = target.FighterStats.ModifiedMaxEnergy / 4;
                        target.FighterStats.Stamina = 4;
                    }
                    target.OnEndCombat(); //this removes all status effects and damage modifiers that were previously on the character.
                }
                if (actor.Name == "Cara")
                {
                    actor.SetAnimation("Idle");
                }
                isRunning = false;
                targetIsDead = false; //This value is used to prevent the game from crashing. Draws when a dead target is selected.
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (targetIsDead) //This value is used to prevent the game from crashing. Draws when a dead target is selected.
            {
                healthRingAnimation.Draw(spriteBatch, targets[0].HitLocation + DrawOffset, SpriteEffects.None);
            }
        }
    }
}
