using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class BurtleAttack : Attack
    {
        #region Constructor

        public BurtleAttack()
        {
            Name = Strings.ZA329;
            Description = Strings.ZA333;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = true;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-50,0);

        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            initialPosition = actor.ScreenPosition;

            SoundSystem.Play(AudioCues.Attack);
            actor.SetAnimation("Attack");
        }

        public override void Update(GameTime gameTime)
        {
            if (actor.CurrentAnimation.IsRunning == false)
            {
                DealPhysicalDamage(actor, targets.ToArray());
                actor.SetAnimation("Idle");
                isRunning = false;
            }
        }
    }
}
