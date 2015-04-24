using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Analyze : Skill
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

        FightingCharacter target;

        public Analyze()
        {
            Name = Strings.ZA328;
            Description = Strings.ZA317;

            MpCost = 15;
            SpCost = 1;

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

            healthRingAnimation = new CombatAnimation()
            {
                Name = "HealthRingAnimation",
                TextureName = "potion",
                Animation = HealthRingFrames,
                Loop = false
            };

            healthRingAnimation.LoadContent(ContentPath.ToSkillTextures);
            DrawOffset = new Vector2(-100, -100);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            target = targets[0];
            SubtractCost(actor);
            healthRingAnimation.Play();
            actor.SetAnimation("AnalyzeAttack"); 
            SoundSystem.Play(AudioCues.Analyze);
        }

        public override void Update(GameTime gameTime)
        {
            healthRingAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (healthRingAnimation.IsRunning == false)
            {
                ScreenManager.Singleton.AddScreen(new AnalyzeScreen(target));
                actor.SetAnimation("Idle"); 
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
            healthRingAnimation.Draw(spriteBatch, targets[0].ScreenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}
