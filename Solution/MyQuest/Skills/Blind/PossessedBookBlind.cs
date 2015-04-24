using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class PossessedBookBlind : Blind
    {

        enum PandoraState
        {
            Charging,
            Impact
        }

        static readonly FrameAnimation blindFrames = new FrameAnimation()
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


        CombatAnimation blindAnimation;

        Vector2 screenPosition;

        PandoraState state;

        #endregion

        #region Constructor

        public PossessedBookBlind() : base()
        {
            Name = Strings.ZA359;
            Description = Strings.ZA360;

            MpCost = 150;
            SpCost = 6;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;


            DrawOffset = new Vector2(-100, -100);
            blindAnimation = new CombatAnimation()
            {
                Name = "Blind",
                TextureName = "blind",
                Loop = false,
                Animation = blindFrames
            };


            blindAnimation.LoadContent(ContentPath.ToSkillTextures);
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
                        blindAnimation.Play();
                        actor.SetAnimation("Idle");
                        state = PandoraState.Impact;
                        SoundSystem.Play(AudioCues.Cloud);
                    }
                    break;

                case PandoraState.Impact:

                    blindAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    if (blindAnimation.IsRunning == false)
                    {

                        isRunning = false;

                        StatusEffect blind;

                        foreach (FightingCharacter target in targets)
                        {
                            blind = new Blindness(3); 
                            SetStatusEffect(actor, blind, target);
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
                blindAnimation.Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }
        }
    }
}
