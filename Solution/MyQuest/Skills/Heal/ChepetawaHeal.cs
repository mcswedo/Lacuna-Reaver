using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class ChepetawaHeal : Heal
    {
        #region Frame Animations

        protected enum HealState
        {
            Charging,
            Ending, 
        }

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

        //Vector2 screenPosition;
        StrikeState state;
        int targetsHit;
        Vector2 destinationPosition;

        #endregion

        public ChepetawaHeal() : base()
        {
            Name = Strings.ZA409;
            Description = Strings.ZA410;

            MpCost = 15;
            SpCost = 3;
            
            SpellPower = 22.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = true;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = true;
            CanTargetAllies = true;
            CanTargetEnemy = false;

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
   
            actor.SetAnimation("Heal");
            
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
            if (actor.FighterStats.Energy >= MpCost)
            {
                actor.FighterStats.Energy -= MpCost;
                foreach (FightingCharacter target in targets)
                {
                    target.FighterStats.Health += (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);
                }
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
