using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{

    public class Focus : Skill
    {
        Vector2 initialPosition;

        #region Constructor

        public Focus()
        {
            Name = Strings.ZA397;
            Description = Strings.ZA398;

            MpCost = 12;
            SpCost = 1; 

            SpellPower = 0;
            DamageModifierValue = 0;
                        
            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = false;
        }

        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            actor.SetAnimation("Focus");

            SoundSystem.Play(AudioCues.Focus);
        }

        public override void Update(GameTime gameTime)
        {
            if (actor.CurrentAnimation.IsRunning == false)
            {
                StatusEffect focused = new Focused();
                SetStatusEffect(actor, focused, actor);

                //This logic is now set in the SetStatusEffect function.
                //if (!Nathan.Instance.HasStatusEffect("Focused"))
                //{
                //    CombatMessage.AddMessage("Failed", Nathan.Instance.StatusEffectMessagePosition, .5);
                //}
                actor.SetAnimation("Idle");
                isRunning = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
