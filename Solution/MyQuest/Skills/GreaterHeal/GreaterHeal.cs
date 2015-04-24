using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class GreaterHeal : Skill
    {
        #region Frame Animations

        static readonly FrameAnimation HealthRingFrames = new FrameAnimation()
        {
            FrameDelay = 0.075,
            Frames = new List<Rectangle>()
            {
                new Rectangle(400, 100, 100, 100), 

                new Rectangle(0, 200, 100, 100), 
                new Rectangle(100, 200, 100, 100), 
                new Rectangle(200, 200, 100, 100), 
                new Rectangle(300, 200, 100, 100), 
                new Rectangle(400, 200, 100, 100), 

                new Rectangle(0, 300, 100, 100), 
                new Rectangle(100, 300, 100, 100), 
                new Rectangle(200, 300, 100, 100), 
                new Rectangle(300, 300, 100, 100), 
                new Rectangle(400, 300, 100, 100), 

                new Rectangle(0, 400, 100, 100), 
                new Rectangle(100, 400, 100, 100), 
                new Rectangle(200, 400, 100, 100), 
                new Rectangle(300, 400, 100, 100), 
                new Rectangle(400, 400, 100, 100), 

       
            }
        };



        #endregion

        #region Combat Animations


        CombatAnimation healthRingAnimation;


        #endregion

        #region Fields

        Vector2 screenPosition;

        #endregion

        public GreaterHeal()
        {
            Name = Strings.ZA403;
            Description = Strings.ZA404;

            MpCost = 15;
            SpCost = 4;

            SpellPower = Cara.Instance.FighterStats.Level + 3;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = true;
            HealingSkill = true;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = true;
            CanTargetEnemy = false;

            DrawOffset = new Vector2(-50, -50);

            healthRingAnimation = new CombatAnimation()
            {
                Name = "HealthRingAnimation",
                TextureName = "cara_greater_heal",
                Animation = HealthRingFrames,
                Loop = false
            };

            healthRingAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            screenPosition = targets[0].HitLocation;

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
                int healing = (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);
                targets[0].FighterStats.AddHealth(healing);

                CombatMessage.AddMessage(healing.ToString(), targets[0].DamageMessagePosition, Color.Green, .5);

                if (actor.Name == "Cara")
                {
                    actor.SetAnimation("Idle");
                }
                isRunning = false;
            }
        }

        public override void OutOfCombatActivate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            bool aTargetWasHealed = false;
            foreach (FightingCharacter target in targets)
            {
                int targetCurrentHealth = target.FighterStats.Health;
                int healing = (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);
                target.FighterStats.AddHealth(healing);

                if (targetCurrentHealth < target.FighterStats.Health)
                {
                    aTargetWasHealed = true;
                }
            }

            if (aTargetWasHealed)
            {
                actor.FighterStats.Energy -= MpCost;
                SoundSystem.Play(AudioCues.Heal);
            }
            else
            {
                SoundSystem.Play(AudioCues.menuDeny);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            healthRingAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
