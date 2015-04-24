using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class PoisonStrike : Skill
    {
        #region Frame Animations

        static readonly FrameAnimation poisonFrames = new FrameAnimation()
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


        #endregion

        #region Fields

        protected CombatAnimation poisonCloudAnimation;
        protected StrikeState state;

        protected Vector2 initialPosition;
        protected Vector2 destinationPosition;

        #endregion

        #region Constructor


        public PoisonStrike()
        {
            Name = Strings.ZA449;
            Description = Strings.ZA450;

            MpCost = 40;
            SpCost = 4;

            SpellPower = 1.5f;
            DamageModifierValue = .2f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-100, -100);

            poisonCloudAnimation = new CombatAnimation()
            {
                Name = "Poison",
                TextureName = "poison",
                Loop = false,
                Animation = poisonFrames
            };


            poisonCloudAnimation.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].ScreenPosition + new Vector2(-50, -10);

            CombatAnimation dash = actor.GetAnimation("Dash");

            state = StrikeState.Traveling;
            actor.SetAnimation("Dash");
            SoundSystem.Play(AudioCues.Swoosh);
        }

        public override void Update(GameTime gameTime)
        {
            if (state == StrikeState.Returning)
            {
                poisonCloudAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            switch (state)
            {
                case StrikeState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StrikeState.Striking;
                        actor.SetAnimation("PoisonStrike");
                    }
                    break;

                case StrikeState.Striking:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {                 
                        state = StrikeState.Returning;

                        actor.SetAnimation("DashReturn");
                        SoundSystem.Play(AudioCues.Swoosh);
                        DamageModifier modifier = new DamageModifier(true, DamageModifierValue);
                        actor.AddDamageModifier(modifier);
                        DealPhysicalDamage(actor, targets.ToArray());

                        foreach (FightingCharacter target in targets)
                        {
                            StatusEffect poison = new Poisoned();

                            SetStatusEffect(actor, poison, target);
                        }
                        poisonCloudAnimation.Play();
                        SoundSystem.Play(AudioCues.Cloud);
                    }
                    break;

                case StrikeState.Returning:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = initialPosition;
                        actor.SetAnimation("Idle");                              
                    }

                    if (actor.CurrentAnimation.Name == ("Idle") && poisonCloudAnimation.IsRunning == false)
                    {
                        isRunning = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == StrikeState.Returning )
            {
                if (poisonCloudAnimation.IsRunning) 
                poisonCloudAnimation.Draw(spriteBatch, targets[0].ScreenPosition + DrawOffset, SpriteEffects.None);
            }
        }

        protected override void SetStatusEffect(FightingCharacter actor, StatusEffect effect, FightingCharacter target)
        {
            if (target is Boggimus || target is Chepetawa || target is Serlynx ||
                target is Arlan || target is Malticar)
            {
                CombatMessage.AddMessage("Immune", target.StatusEffectMessagePosition, .5);
            }
            else
            {
                base.SetStatusEffect(actor, effect, target);
            }
        }
    }
}
