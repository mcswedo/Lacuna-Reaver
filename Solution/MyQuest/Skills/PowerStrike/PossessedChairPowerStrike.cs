using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class PossessedChairPowerStrike : Attack
    {
        public PossessedChairPowerStrike()
            : base()
        {
            Name = "Jump";
            Description = "The chair flies into the air and slams down on its target.";

            MpCost = 100;
            SpCost = 5;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}
