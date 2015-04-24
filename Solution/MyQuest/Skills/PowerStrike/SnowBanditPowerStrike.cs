using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class SnowBanditPowerStrike : PowerStrike
    {
        public SnowBanditPowerStrike()
            : base()
        {
            Name = Strings.ZA458;
            Description = Strings.ZA459;

            MpCost = 250;
            SpCost = 7;

            SpellPower = 0.0f;
            DamageModifierValue = 0.35f;

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
