using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Destiny : Skill
    {
        #region Frame Animations

        static readonly FrameAnimation EnergyFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 252, 195),                
                new Rectangle(252, 0, 252, 195),  
                new Rectangle(504, 0, 252, 195),    
                new Rectangle(756, 0, 252, 195),    
               
                new Rectangle(0, 195, 252, 195),                
                new Rectangle(252, 195, 252, 195),  
                new Rectangle(504, 195, 252, 195),    
                new Rectangle(756, 195, 252, 195), 
       
                new Rectangle(0, 390, 252, 195),                
                new Rectangle(252, 390, 252, 195),  
                new Rectangle(504, 390, 252, 195),    
                new Rectangle(756, 390, 252, 195), 
            }
        };


        #endregion

        #region Fields

        Vector2 screenPosition;
        CombatAnimation energyAnimation;

        #endregion

        #region Constructor


        public Destiny()
        {
            Name = Strings.ZA363;
            Description = Strings.ZA364;

            MpCost = 20;
            SpCost = 5;

            SpellPower = 0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-110, -110);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            energyAnimation = new CombatAnimation()
            {
                Name = "EnergyAnimation",
                TextureName = "destiny",
                Animation = EnergyFrames,
                Loop = false
            };

            energyAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            screenPosition = targets[0].ScreenPosition + DrawOffset;

            actor.SetAnimation("Destiny");
            energyAnimation.Play();
            SoundSystem.Play(AudioCues.Fireball);
        }

        public override void Update(GameTime gameTime)
        {
            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle");
            }

            energyAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);


            if (energyAnimation.IsRunning == false)
            {
                foreach (FightingCharacter target in targets)
                {
                    DealMagicDamage(actor, targets.ToArray());
                    if (!target.HasDestiny())
                    {
                        VoodooDoll voodooDoll = (VoodooDoll) actor;
                        StatusEffect destiny = new DestinyStatusEffect(voodooDoll);
                        SetStatusEffect(actor, destiny, target);
                        if (target.HasDestiny())
                        {
                            voodooDoll.destinyTarget = (PCFightingCharacter)target;
                        }
                    }
                }

                isRunning = false;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            energyAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
