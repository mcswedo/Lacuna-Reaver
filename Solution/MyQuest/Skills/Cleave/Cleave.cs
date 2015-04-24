using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Cleave : Skill
    {
        static readonly FrameAnimation impaleFrames = new FrameAnimation()
        {
            FrameDelay = 0.07,
            Frames = new List<Rectangle>()
            {
            
                new Rectangle(0, 0, 150, 200),
                new Rectangle(150, 0, 150, 200),
                new Rectangle(300, 0, 150, 200),
                new Rectangle(450, 0, 150, 200),
                new Rectangle(600, 0, 150, 200),
                new Rectangle(750, 0, 150, 200),
                new Rectangle(900, 0, 150, 200),
                new Rectangle(1050, 0, 300, 200),
                new Rectangle(1350, 0, 150, 200),
                new Rectangle(1500, 0, 150, 200),
                new Rectangle(1650, 0, 150, 200),

                new Rectangle(0, 200, 150, 200),
                new Rectangle(150, 200, 150, 200),
                new Rectangle(300, 200, 150, 200),
                new Rectangle(450, 200, 150, 200),
                new Rectangle(600, 200, 150, 200),
                new Rectangle(750, 200, 150, 200),
                new Rectangle(900, 200, 150, 200),
                new Rectangle(1050, 200, 150, 200),
                new Rectangle(1200, 200, 150, 200),
                new Rectangle(1350, 200, 150, 200),
                new Rectangle(1500, 200, 150, 200),
                new Rectangle(1650, 200, 150, 200)
            }
        };

        #region Fields

        StrikeState state;

        CombatAnimation impaleAnimation;
        Vector2 destinationPosition;
        int targetsHit;
        int random;

        List<int> previousTargetHit = new List<int>();

        #endregion

        #region Constructor


        public Cleave()
        {
            Name = Strings.ZA387;
            Description = Strings.ZA388;

            MpCost = 40;
            SpCost = 3;

            SpellPower = 1.0f;
            DamageModifierValue = 0.0f; //If this is changed, re add the modifier in the striking case!!!

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-62, -75);

            impaleAnimation = new CombatAnimation()
            {
                Name = "ImpaleAnimation",
                TextureName = "impale",
                Animation = impaleFrames,
                Loop = false
            };
            impaleAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            if (targets.Count() == 3)
            {
                random = Utility.RNG.Next(3);
            }
            else if (targets.Count() == 2)
            {
                random = Utility.RNG.Next(2);
            }
            else
            {
                random = 0;
            }
            previousTargetHit.Add(random);

            destinationPosition = targets[random].HitLocation + DrawOffset;

            targetsHit = 0;
            state = StrikeState.Striking;
            actor.CurrentAnimation.IsPaused = true;
            impaleAnimation.Play();
            SoundSystem.Play(AudioCues.Swoosh);

        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Striking:
                    impaleAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (impaleAnimation.IsRunning == false)
                    {
                        SoundSystem.Play(AudioCues.Attack);
                        DealPhysicalDamage(actor, targets[random]);
                        targetsHit++;
                        if (targetsHit <= 3)
                        {
                            if (targets.Count() == 3)
                            {
                                random = Utility.RNG.Next(3);
                            }
                            else if (targets.Count() == 2)
                            {
                                random = Utility.RNG.Next(2);
                            }
                            else
                            {
                                random = 0;
                            }
                            //DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                            //actor.AddDamageModifier(modifier);
                            switch (random)
                            {
                                case 0:
                                    {
                                        foreach (int timesHit in previousTargetHit)
                                        {
                                            if (timesHit == 0)
                                            {
                                                //Give diminished effect if you've already hit this character.
                                                DamageModifier negativeModifier = new DamageModifier(false, .25f);
                                                actor.AddDamageModifier(negativeModifier);
                                            }
                                        }
                                        previousTargetHit.Add(random);

                                        destinationPosition = targets[0].HitLocation + DrawOffset;
                                        impaleAnimation.Play();

                                        if (previousTargetHit.Count == 4) //remove the list after attack is complete.
                                        {
                                            previousTargetHit.Clear();
                                        }
                                    }
                                    break;
                                case 1:
                                    {
                                        foreach (int timesHit in previousTargetHit)
                                        {
                                            if(timesHit == 1)
                                            {
                                                DamageModifier negativeModifier = new DamageModifier(false, .25f);
                                                actor.AddDamageModifier(negativeModifier);
                                            }
                                        }
                                        previousTargetHit.Add(random);

                                        destinationPosition = targets[1].HitLocation + DrawOffset;
                                        impaleAnimation.Play();

                                        if (previousTargetHit.Count == 4) //remove the list after attack is complete.
                                        {
                                            previousTargetHit.Clear();
                                        }
                                    }
                                    break;
                                case 2:
                                    {
                                        foreach (int timesHit in previousTargetHit)
                                        {
                                            if (timesHit == 2)
                                            {
                                                DamageModifier negativeModifier = new DamageModifier(false, .25f);
                                                actor.AddDamageModifier(negativeModifier);
                                            }
                                        }
                                        previousTargetHit.Add(random);

                                        destinationPosition = targets[2].HitLocation + DrawOffset;
                                        impaleAnimation.Play();

                                        if (previousTargetHit.Count == 4) //remove the list after attack is complete.
                                        {
                                            previousTargetHit.Clear();
                                        }
                                    }
                                    break;
                            }
                        }

                        else
                        {
                            state = StrikeState.Returning;
                        }
                    }
                    break;

                case StrikeState.Returning:
                    {
                        actor.CurrentAnimation.IsPaused = false;
                        isRunning = false;
                        break;
                    }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int currentFrame = impaleAnimation.GetCurrentFrame;
            if (state == StrikeState.Traveling || state == StrikeState.Striking)
            {
                if (currentFrame != 7)
                {
                    impaleAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
                }
                else
                {
                    impaleAnimation.Draw(spriteBatch, destinationPosition + new Vector2(-75, 0), SpriteEffects.None); //to shift it in the right place for the big frame.
                }
            }            
        }
    }
}
