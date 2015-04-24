using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{

    class NathanPowerStrike : PowerStrike
    {

        #region Constructor


        public NathanPowerStrike()
        {
            Name = Strings.ZA460;
            Description = Strings.ZA461;

            MpCost = 10;
            SpCost = 2;

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
            DrawOffset = new Vector2(-50, -10); 
        }


        #endregion

      public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            initialPosition = actor.ScreenPosition;

            destinationPosition = targets[0].ScreenPosition + DrawOffset;

            CombatAnimation dash = actor.GetAnimation("Dash");

            state = StrikeState.Traveling;
            actor.SetAnimation("Dash");
            SoundSystem.Play(AudioCues.Swoosh);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = destinationPosition;
                        state = StrikeState.Striking;
                        actor.SetAnimation("PowerStrike");
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
                    }
                    break;

                case StrikeState.Returning:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        actor.ScreenPosition = initialPosition;
                        actor.SetAnimation("Idle");
                        isRunning = false;  
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}

