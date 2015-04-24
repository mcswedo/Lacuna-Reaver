using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class NemesisCannon : Skill
    {

        static readonly FrameAnimation fireFrames = new FrameAnimation()
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>()
            {
            
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),
                new Rectangle(200, 600, 200, 200),
                new Rectangle(400, 600, 200, 200),
                new Rectangle(600, 600, 200, 200),
                new Rectangle(800, 600, 200, 200),               
            }
        };


        #region Fields


        CombatAnimation fireAnimation;
        StrikeState state;
        int targetsHit;
        Vector2 destinationPosition;

        #endregion

        #region Constructor

        public NemesisCannon()
        {
            Name = Strings.ZA651;
            Description = Strings.ZA652;

            MpCost = 200;
            SpCost = 7;

            SpellPower = 24f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-65, -105);

            fireAnimation = new CombatAnimation()
            {
                Name = "FireAnimation",
                TextureName = "red_cloud",
                Animation = fireFrames,
                Loop = false
            };

            fireAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);
            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation + DrawOffset;

            targetsHit = 0;

            state = StrikeState.Traveling;
            fireAnimation.Play();
            if (actor.Name == Party.will)
            {
                actor.SetAnimation("Glow");
            }
            else
            {
                actor.CurrentAnimation.IsPaused = true;
            }
            SoundSystem.Play(AudioCues.Cloud);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    fireAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (fireAnimation.IsRunning == false)
                    {                      
                        state = StrikeState.Striking;
                    }
                    break;

                case StrikeState.Striking:
                        targetsHit++;
                        if (targetsHit < targets.Count)
                        {
                            destinationPosition = targets[targetsHit].HitLocation + DrawOffset;
                            fireAnimation.Play();
                            SoundSystem.Play(AudioCues.Cloud);
                            state = StrikeState.Traveling;
                        }

                        else
                        {
                            DealMagicDamage(actor, targets.ToArray());
                            StatusEffect poison = new Poisoned();
                            foreach (FightingCharacter target in targets) //Nemesis Cannon may leave target poisoned.
                            {                                
                                SetStatusEffect(actor, poison, target);                              
                            }
                            if (actor.Name == Party.will)
                            {
                                actor.SetAnimation("Idle");
                            }
                            else
                            {
                                actor.CurrentAnimation.IsPaused = false;
                            }
                            isRunning = false;

                        }
                  
                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == StrikeState.Traveling)
            {
                fireAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
        }
    }
}
