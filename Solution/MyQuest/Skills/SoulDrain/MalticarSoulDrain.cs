using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    enum MalticarSiphonState
    {
        Draining,
        Chargeing
    }

    public class MalticarSiphon : Siphon
    {

        #region Frame Animations


        static readonly FrameAnimation ChargeFrames = new FrameAnimation()
        {
            FrameDelay = .075,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 400, 400),
                new Rectangle(400, 0, 400, 400),
                new Rectangle(800, 0, 400, 400),
                new Rectangle(1200, 0, 400, 400),
                new Rectangle(1600, 0, 400, 400),

                new Rectangle(0, 400, 400, 400),
                new Rectangle(400, 400, 400, 400),
                new Rectangle(800, 400, 400, 400),
                new Rectangle(1200, 400, 400, 400),
                new Rectangle(1600, 400, 400, 400),

                new Rectangle(0, 800, 400, 400),
                new Rectangle(400, 800, 400, 400),
                new Rectangle(800, 790, 400, 400),
                new Rectangle(1200, 800, 400, 400),
                new Rectangle(1600, 800, 400, 400),

                new Rectangle(0, 1200, 400, 400),
                new Rectangle(393, 1200, 400, 400),
                new Rectangle(800, 1200, 400, 400),
                new Rectangle(1200, 1200, 400, 400),
                new Rectangle(1600, 1200, 400, 400),

                new Rectangle(0, 1600, 400, 400),
                new Rectangle(400, 1600, 400, 400),
                new Rectangle(800, 1600, 400, 400),
                new Rectangle(1200, 1600, 400, 400),
                new Rectangle(1600, 1600, 400, 400)
            }
        };

        #endregion

        #region Fields


        List<CombatAnimation> siphonAnimations;
        int currentAnimation;

        MalticarSiphonState state;

        Vector2 screenPosition;

        #endregion

        #region Constants

        readonly Vector2 commonDrawOffset = new Vector2(-50, -50);

        #endregion

        #region Constructor


        public MalticarSiphon()
        {
            Name = Strings.ZA525;//If the name changes, be sure to check Arlans AI, if somebody else does this, email Kyle to do this!
            Description = Strings.ZA526;

            MpCost = 100;
            SpCost = 3;

            SpellPower = 33f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
            DrawOffset = new Vector2(-175, -150);

            siphonAnimations = new List<CombatAnimation>()
            {
                new CombatAnimation()
                {
                    Name = "SoulDrain",
                    TextureName = "soul_drain",
                    Loop = false,
                    Animation = ChargeFrames
                }
            };

            //siphonAnimations = new List<CombatAnimation>()
            //{
            //    new CombatAnimation()
            //    {
            //        Name = "Draining",
            //        TextureName = "dark",
            //        Loop = false,
            //        Animation = DrainFrames
            //    },
            //    new CombatAnimation()
            //    {
            //        Name = "Chargeing",
            //        TextureName = "red",
            //        Loop = false,
            //        Animation = ChargeFrames
            //    }
            //};

            foreach (CombatAnimation anim in siphonAnimations)
                anim.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            //SubtractCost(actor);

            screenPosition = targets[0].HitLocation;
            //screenPosition2 = screenPosition + new Vector2(-50, -50);

            state = MalticarSiphonState.Chargeing;
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
                case MalticarSiphonState.Draining:
                    if (siphonAnimations[currentAnimation].IsRunning == false)
                    {
                        //siphonAnimations[++currentAnimation].Play();
                        //SoundSystem.Play(AudioCues.Siphon);
                        state = MalticarSiphonState.Chargeing;
                    }
                    break;

                //case MalticarSiphonState.Waiting:
                //    screenPosition = screenPosition2;
                //    state = MalticarSiphonState.Chargeing;
                //    break;

                case MalticarSiphonState.Chargeing:                    
                    if (siphonAnimations[currentAnimation].IsRunning == false)
                    {
                        SoundSystem.Play(AudioCues.Siphon);
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

                        if (SkillHit)
                        {
                            //Make the target weakened and Focus Arlan.
                            StatusEffect weakened = new Weakened(1f);
                            SetStatusEffect(actor, weakened, targets[0]);
                            StatusEffect focused = new Focused(1f);
                            SetStatusEffect(actor, focused, actor);

                            if (targets[0].FighterStats.Stamina >= 2)
                            {
                                targets[0].FighterStats.Stamina -= 2;
                            }

                            //This logic is used as a clamp.
                            else
                            {
                                targets[0].FighterStats.Stamina = 0; 
                            }

                            actor.FighterStats.Stamina += 2;
                            MathHelper.Clamp(actor.FighterStats.Stamina, 0, actor.FighterStats.ModifiedMaxStamina); // If it goes above the max stamina, bring it back to max stamina. Shouldn't happen if skill cost is 2.

                            float rawHealValue = (int)CombatCalculations.RawMagicalDamage(actor.FighterStats, SpellPower);
                            float modifiedHealValue = (int)CombatCalculations.ModifiedDamage(actor.DamageModifiers, rawHealValue);
                            int finalHealValue = CombatCalculations.MagicalDefense(targets[0].FighterStats, modifiedHealValue);
                            actor.FighterStats.AddHealth((int)finalHealValue);
                            CombatMessage.AddMessage(finalHealValue.ToString(), actor.DamageMessagePosition, Color.Green, .5);
                        }
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int currentFrame = siphonAnimations[currentAnimation].GetCurrentFrame;

            if (currentFrame == 12)
            {
                siphonAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset + new Vector2(0, -10), SpriteEffects.None);
            }
            else if (currentFrame == 16)
            {
                siphonAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset + new Vector2(-7, 0), SpriteEffects.None);
            }
            else
            {
                siphonAnimations[currentAnimation].Draw(spriteBatch, screenPosition + DrawOffset, SpriteEffects.None);
            }
        }
    }
}

//Delete whats below!

//namespace MyQuest
//{
//    class SoulDrain : Skill
//    {
//        protected enum AttackState
//        {
//            Traveling,
//            Striking,
//            Returning,
//        }

//        #region Fields


//        protected AttackState state;

//        protected Vector2 initialPosition;
//        protected Vector2 destinationPosition;
//        protected Vector2 velocity;


//        #endregion

//        #region Constructor

//        public SoulDrain()
//        {  
//            MpCost = 0;
//            SpCost = 0;

//            SpellPower = 0;
//            DamageModifierValue = 0f;

//            BattleSkill = true;
//            MapSkill = false;
//            HealingSkill = false;
//            MagicSkill = false;
//            IsBasicAttack = true;

//            TargetsAll = false;
//            CanTargetAllies = false;
//            CanTargetEnemy = true;

//            DrawOffset = new Vector2(-50,0);

//        }


//        #endregion

//        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
//        {
//            base.Activate(actor, targets);

//            SubtractCost(actor);

//            initialPosition = actor.ScreenPosition;

//            destinationPosition = targets[0].HitLocation + DrawOffset;

//            CombatAnimation dash = actor.GetAnimation("Dash");

//            velocity =
//                (destinationPosition - actor.ScreenPosition) / (float)TimeSpan.FromSeconds((dash.Animation.FrameDelay * dash.Animation.Frames.Count)).TotalMilliseconds;

//            state = AttackState.Traveling;
//            actor.SetAnimation("Dash");
//        }

//        public override void Update(GameTime gameTime)
//        {
//            switch (state)
//            {
//                case AttackState.Traveling:
//                    actor.ScreenPosition += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
//                    if (actor.CurrentAnimation.IsRunning == false)
//                    {
//                        state = AttackState.Striking;
//                        actor.SetAnimation("Attack");

//                        SoundSystem.Play(AudioCues.Attack);
//                    }
//                    break;

//                case AttackState.Striking:
//                    if (actor.CurrentAnimation.IsRunning == false)
//                    {
//                        state = AttackState.Returning;

//                        actor.SetAnimation("Dash");

//                        DealMagicDamage(actor, targets.ToArray());

//                        SoundSystem.Play(AudioCues.SwordHitArmor);
//                    }
//                    break;

//                case AttackState.Returning:
//                    actor.ScreenPosition -= velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
//                    if (Vector2.Distance(actor.ScreenPosition, initialPosition) < velocity.Length())
//                    {
//                        actor.ScreenPosition = initialPosition;
//                        actor.SetAnimation("Idle");
//                        isRunning = false;
//                    }
//                    break;
//            }
//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//        }
//    }
//}
