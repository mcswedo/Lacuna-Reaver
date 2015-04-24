  using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    /// <summary>
    /// Heals all members of your party.
    /// </summary>
    public class GroupHeal : Skill
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

        #endregion

        #region Fields

        CombatAnimation healthRingAnimation;
        StrikeState state;
        int targetsHit;
        Vector2 destinationPosition;

        #endregion

        public GroupHeal()
        {
            Name = Strings.ZA407;
            Description = Strings.ZA408;

            MpCost = 30;
            SpCost = 5;

            SpellPower = Cara.Instance.FighterStats.Level;//7
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = true;
            HealingSkill = true;
            MagicSkill = true;
            IsBasicAttack = false; 

            TargetsAll = true;
            CanTargetAllies = true;
            CanTargetEnemy = false;

            DrawOffset = new Vector2(-50, -50);

            healthRingAnimation = new CombatAnimation()
            {
                Name = "HealthRingAnimation",
                TextureName = "cara_heal",
                Animation = HealthRingFrames,
                Loop = false
            };

            healthRingAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation + DrawOffset;

            targetsHit = 0;

            state = StrikeState.Traveling;
            healthRingAnimation.Play();
            if (actor.Name == "Cara")
            {
                actor.SetAnimation("HealAttack");
            }
            SoundSystem.Play(AudioCues.Heal);
        }

        public override void Update(GameTime gameTime)
        {          
            switch (state)
            {
                case StrikeState.Traveling:
                    healthRingAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (healthRingAnimation.IsRunning == false)
                    {                      
                        state = StrikeState.Striking;
                    }
                    break;

                case StrikeState.Striking:
                        targetsHit++;
                        if (targetsHit < targets.Count)
                        {
                            destinationPosition = targets[targetsHit].HitLocation + DrawOffset;
                            healthRingAnimation.Play();
                            SoundSystem.Play(AudioCues.Heal);
                            state = StrikeState.Traveling;
                        }

                        else
                        {
                            foreach (FightingCharacter target in targets)
                            {
                                int healing = (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);
                                target.FighterStats.AddHealth(healing);
                                CombatMessage.AddMessage(healing.ToString(), target.DamageMessagePosition, Color.Green, .5);
                            }
                            if (actor.Name == "Cara")
                            {
                                actor.SetAnimation("Idle");
                            }
                            isRunning = false;
                        }
                  
                    break;
            }
        }

        public override void OutOfCombatActivate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            bool aTargetWasHealed = false;
            //actor.FighterStats.Energy -= MpCost;

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
            if (state == StrikeState.Traveling)
            {
                healthRingAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
        }
    }
}
