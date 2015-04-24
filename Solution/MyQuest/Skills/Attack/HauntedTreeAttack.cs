using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class HauntedTreeAttack : Attack
    {
        #region Frame Animations

        static readonly FrameAnimation RootFrames = new FrameAnimation()
        {
            FrameDelay = 0.15,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 200, 200),                
                new Rectangle(200, 0, 200, 200),  
                new Rectangle(400, 0, 200, 200),     
                new Rectangle(600, 0, 200, 200),     
               
                new Rectangle(0, 200, 200, 200),                
                new Rectangle(200, 200, 200, 200),  
                new Rectangle(400, 200, 200, 200),     
                new Rectangle(600, 200, 200, 200),  
       
                new Rectangle(0, 400, 200, 200),                
                new Rectangle(200, 400, 200, 200),  
                new Rectangle(400, 400, 200, 200),     
                new Rectangle(600, 400, 200, 200),  
            }
        };


        #endregion

        #region Fields

        Vector2 screenPosition;
        CombatAnimation rootAnimation;

        #endregion

        public HauntedTreeAttack() : base()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA341;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 4.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            DrawOffset = new Vector2(-110, -75);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            rootAnimation = new CombatAnimation()
            {
                Name = "RootAnimation",
                TextureName = "haunted_tree_roots",
                Animation = RootFrames,
                Loop = false
            };

            rootAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            screenPosition = targets[0].ScreenPosition;

            state = AttackState.Traveling;

            rootAnimation.Play();
            actor.SetAnimation("Attack");
        }

        public override void Update(GameTime gameTime)
        {
            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle");
            }

            rootAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);


            if (rootAnimation.IsRunning == false)
            {
                DealPhysicalDamage(actor, targets.ToArray());

                SoundSystem.Play(AudioCues.SwordHitArmor);

                isRunning = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            rootAnimation.Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
        }
    }
}