using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Disintegrate : Skill
    {
        static readonly FrameAnimation disintegrateFrames = new FrameAnimation()
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

        #region Combat Animations

        CombatAnimation disintegrateAnimation;

        #endregion

        #region Fields

        Vector2 screenPosition;
        bool played;
        SpriteEffects effect;
        int originalMPCost = 175;
        int originalSPCost = 7;

        #endregion

        #region Constructor

        public Disintegrate()
        {
            Name = Strings.ZA365;
            Description = Strings.ZA366;


            MpCost = 175;
            SpCost = 7;


            SpellPower = Will.Instance.FighterStats.Level + 14.5f; //20.0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-65, -105);

            disintegrateAnimation = new CombatAnimation()
            {
                Name = "disintegrateAnimation",
                TextureName = "gaze",
                Animation = disintegrateFrames,
                Loop = false
            };

            disintegrateAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            if (actor is NegaWill)
            {
                MpCost = 333;
                SpCost = 4;
            }

            SubtractCost(actor);

            screenPosition = targets[0].ScreenPosition;

            if (actor is NegaWill)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effect = SpriteEffects.None;
            }

            actor.SetAnimation("ShadowBlast");
        }
     
        public override void Update(GameTime gameTime)
        {
            if (actor.GetAnimation("ShadowBlast").IsRunning == false)
            {
                actor.SetAnimation("Idle");

                if (!played)
                {
                    disintegrateAnimation.Play();
                    SoundSystem.Play(AudioCues.Cloud);
                    played = true;
                }

                disintegrateAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                if (disintegrateAnimation.IsRunning == false)
                {
                    float originalSpellPower = this.SpellPower;
                    if (actor is NegaWill)
                    {
                        SpellPower = 4;
                    }
                    DealMagicDamage(actor, targets.ToArray());
                    if (actor is NegaWill)
                    {
                        this.SpellPower = originalSpellPower;
                        this.MpCost = originalMPCost;
                        this.SpCost = originalSPCost;
                        if (SkillHit)
                        {
                            StatusEffect blind = new Blindness(2);
                            SetStatusEffect(actor, blind, targets[0]);
                        }
                    }
                    played = false; 
                    isRunning = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (played)
            {
                disintegrateAnimation.Draw(spriteBatch, screenPosition + DrawOffset, effect);
            }
        }
    }
}
