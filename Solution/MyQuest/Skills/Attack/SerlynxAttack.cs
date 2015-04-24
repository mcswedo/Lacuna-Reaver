using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    class SerlynxAttack : Attack
    {
        protected enum SerlynxAttackState
        {
            Traveling,
            Striking,
            Returning
        }
        #region Frame Animations

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 600, 250),
                new Rectangle(600, 0, 600, 250),
                new Rectangle(1200, 0, 600, 250),
                new Rectangle(0, 250, 600, 250),
                new Rectangle(600, 250, 600, 250),
                new Rectangle(1200, 250, 600, 250),
                new Rectangle(0, 500, 600, 250),
                new Rectangle(600, 500, 600, 250),
                new Rectangle(1200, 500, 600, 250),
                new Rectangle(0, 750, 600, 250),
                new Rectangle(600, 750, 600, 250),
                new Rectangle(1200, 750, 600, 250),
                new Rectangle(0, 1000, 600, 250),
                new Rectangle(600, 1000, 600, 250),
                new Rectangle(1200, 1000, 600, 250),
            }
        };

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

        #region Fields

        bool damageApplied;
        SerlynxAttackState attackState;
        CombatAnimation earthShatterAnimation;
        CombatAnimation healthRingAnimation;
        Vector2 screenPosition;
        int i; 

        #endregion

        #region Constructor

        public SerlynxAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA662; //This will be added to Strings when animations are complete.

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-50,0);

            earthShatterAnimation = new CombatAnimation()
            {
                Name = "Impact",
                TextureName = "cara_earth_shatter",
                Loop = false,
                Animation = ImpactFrames,
                DrawOffset = new Vector2(-220, -150)
            };

            healthRingAnimation = new CombatAnimation()
            {
                Name = "HealthRingAnimation",
                TextureName = "cara_heal",
                Animation = HealthRingFrames,
                Loop = false
            };

            healthRingAnimation.LoadContent(ContentPath.ToSkillTextures);
            earthShatterAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {            
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ScreenPosition;

            destinationPosition = targets[0].HitLocation;

            damageApplied = false;
            actor.SetAnimation("Attack");
            SoundSystem.Play(AudioCues.Attack);
            attackState = SerlynxAttackState.Traveling;
            i = 2; 
        }

        public override void Update(GameTime gameTime)
        {
            switch (attackState)
            {
                case SerlynxAttackState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        if (i < 4)
                        {
                            actor.SetAnimation("Attack" + i);
                            i++;
                        }
                        else
                        {
                            attackState = SerlynxAttackState.Striking;
                            SoundSystem.Play(AudioCues.EarthShatter);
                            SoundSystem.Play(AudioCues.SwordHitShield);
                            earthShatterAnimation.Play();
                            i = 1;
                        }                 
                        
                    }
                                    
                    break;

                case SerlynxAttackState.Striking:

                    if (actor.CurrentAnimation.IsRunning ==false && i == 1)
                    {
                        actor.SetAnimation("AttackReturn");
                        i++; 
                    }

                    if (actor.CurrentAnimation.IsRunning == false && i == 2)
                    {
                        actor.SetAnimation("AttackReturn"+i);
                        i++;
                    }

                    if (actor.CurrentAnimation.IsRunning == false && i == 3)
                    {
                        actor.SetAnimation("Idle");
                    }

                    if (earthShatterAnimation.IsRunning)
                    {
                        earthShatterAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                        return;
                    }
                    if (!damageApplied)
                    {
                        StatusEffect expose = new Expose();
                        healthRingAnimation.Play();
                        DealPhysicalDamage(actor, targets.ToArray());
                        //actor.SetAnimation("Idle");
                        damageApplied = true;

                        if (SkillHit)
                        {
                            SetStatusEffect(actor, expose, targets[0]);
                            //targets[0].AddStatusEffect(expose); // Every Attack reduces the targets armor.    
                        }
                        if (actor.FighterStats.Stamina <= 3)
                        {
                            actor.FighterStats.Stamina += 8; //Increase Serlynx stamina so that he can use an ability next turn.
                        }
                        healthRingAnimation.Play();
                        SoundSystem.Play(AudioCues.Heal);
                        attackState = SerlynxAttackState.Returning;
                    }
                    break;

                case SerlynxAttackState.Returning:

                    healthRingAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    if (healthRingAnimation.IsRunning == false)
                    {
                        int deltaHealth = actor.FighterStats.BaseMaxHealth / 20;
                        actor.FighterStats.Health += deltaHealth; //Heal the serlynx when doing a basic attack.

                        CombatMessage.AddMessage(deltaHealth.ToString(), actor.DamageMessagePosition, Color.Green, .5);

                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (earthShatterAnimation.IsRunning)
            {
                earthShatterAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
            if (healthRingAnimation.IsRunning)
            {
                healthRingAnimation.Draw(spriteBatch, screenPosition + new Vector2(-64,-128), SpriteEffects.None);
            }
        }
    }
}
