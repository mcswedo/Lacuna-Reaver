using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Weakness : Skill
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

        public Weakness()
        {
            Name = Strings.ZA500;
            Description = Strings.ZA501;

            MpCost = 15;
            SpCost = 3;

            SpellPower = 3;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-50, -55);

            energyAnimation = new CombatAnimation()
            {
                Name = "EnergyAnimation",
                TextureName = "haunted_tree_energy",
                Animation = EnergyFrames,
                Loop = false
            };

            energyAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            screenPosition = targets[0].ScreenPosition + DrawOffset;
            if (actor.Name == Party.will)
            {
                actor.SetAnimation("Glow");
            }
            else
            {
                actor.SetAnimation("Weakness");
            }
            energyAnimation.Play();
            SoundSystem.Play(AudioCues.GazeOfDespair);
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
                    StatusEffect weakened = new Weakened();
                    SetStatusEffect(actor, weakened, target);
                    if (actor.Name.Equals("Witch Doctor"))
                    {
                        DealMagicDamage(actor, targets.ToArray());
                    }
                }
                if (actor.Name == Party.will)
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

        //        }
        //    }
        //}

        public override void Draw(SpriteBatch spriteBatch)
        {
            energyAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}