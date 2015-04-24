using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    class PossessedBookPoison : Poison
    {
        enum PandoraState
        {
            Charging,
            Impact
        }

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

        #region Fields


        CombatAnimation poisonAnimation;

        Vector2 screenPosition;

        PandoraState state;

        #endregion

        #region Constructor

        public PossessedBookPoison() : base()
        {

            Name = Strings.ZA445;
            Description = Strings.ZA444;

            MpCost = 150;
            SpCost = 6;

            SpellPower = 0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-100, -100);
            poisonAnimation = new CombatAnimation()
            {
                Name = "Poison",
                TextureName = "poison",
                Loop = false,
                Animation = poisonFrames
            };


            poisonAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = targets[0].ScreenPosition + DrawOffset;

            state = PandoraState.Charging;
            actor.SetAnimation("Attack");
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case PandoraState.Charging:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        poisonAnimation.Play();
                        actor.SetAnimation("Idle");
                        SoundSystem.Play(AudioCues.Cloud);
                        state = PandoraState.Impact;
                    }
                    break;

                case PandoraState.Impact:

                    poisonAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    if (poisonAnimation.IsRunning == false)
                    {

                        isRunning = false;

                        StatusEffect poison;

                        foreach (FightingCharacter target in targets)
                        {
                            poison = new Poisoned();

                            SetStatusEffect(actor, poison, target);
                        }
                        //DealMagicDamage(actor, targets.ToArray());
                    }

                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == PandoraState.Impact)
            {
                poisonAnimation.Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }
        }
    }
}
