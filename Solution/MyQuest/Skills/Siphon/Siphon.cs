using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    enum SiphonState
    {
        Draining,
        Chargeing
    }

    public class Siphon : Skill
    {

        #region Frame Animations


        static readonly FrameAnimation ChargeFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
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

        static readonly FrameAnimation DrainFrames = new FrameAnimation()
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


        List<CombatAnimation> siphonAnimations;
        int currentAnimation;

        SiphonState state;

        Vector2 screenPosition;

        #endregion

        #region Constants

        readonly Vector2 commonDrawOffset = new Vector2(-50, -50);

        #endregion

        #region Constructor


        public Siphon()
        {
            Name = Strings.ZA478;
            Description = Strings.ZA479;

            MpCost = 20;
            SpCost = 5;
          
            if (Will.Instance.FighterStats.Level <= 20)
            {
                SpellPower = Will.Instance.FighterStats.Level + 1;//5.5f;
            }
            else
            {
                SpellPower = Will.Instance.FighterStats.Level - 1;
            }
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
            DrawOffset = new Vector2(-100, -100);

            siphonAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "Draining",
                    TextureName = "dark",
                    Loop = false,
                    Animation = DrainFrames
                },
                new CombatAnimation()
                {
                    Name = "Chargeing",
                    TextureName = "red",
                    Loop = false,
                    Animation = ChargeFrames
                }
            };

            foreach (CombatAnimation anim in siphonAnimations)
                anim.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = targets[0].HitLocation;

            state = SiphonState.Draining;
            currentAnimation = 0;
            siphonAnimations[currentAnimation].Play();
            if (actor.Name == Party.will)
            {
                actor.SetAnimation("Glow");
            }
            else
            {
                actor.CurrentAnimation.IsPaused = true;
            }
            SoundSystem.Play(AudioCues.Siphon);
        }

        public override void Update(GameTime gameTime)
        {
            int targetCurrentHealth = targets[0].FighterStats.Health;
            siphonAnimations[currentAnimation].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            switch (state)
            {
                case SiphonState.Draining:
                    if (siphonAnimations[currentAnimation].IsRunning == false)
                    {
                        siphonAnimations[++currentAnimation].Play();
                        SoundSystem.Play(AudioCues.Siphon);
                        state = SiphonState.Chargeing;
                    }
                    break;

                case SiphonState.Chargeing:
                    screenPosition = actor.HitLocation;
                    if (siphonAnimations[currentAnimation].IsRunning == false)
                    {
                        if (actor.Name == Party.will)
                        {
                            actor.SetAnimation("Idle");
                        }
                        else
                        {
                            actor.CurrentAnimation.IsPaused = false;
                        }
                        isRunning = false;

                        DealMagicDamage(actor, targets.ToArray());

                        if(targets[0].FighterStats.Health < targetCurrentHealth)
                        {
                        float rawHealValue = (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower) / 3;
                        float healValue = (int)CombatCalculations.ModifiedDamage(actor.DamageModifiers, rawHealValue);
                        actor.FighterStats.AddHealth((int)healValue);
                        CombatMessage.AddMessage(healValue.ToString(), actor.DamageMessagePosition, Color.Green, .5);
                        }
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            siphonAnimations[currentAnimation].Draw(spriteBatch, screenPosition+DrawOffset, SpriteEffects.None);
        }
    }
}
