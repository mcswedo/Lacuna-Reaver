using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class BanditPowerStrike : PowerStrike
    {
        public BanditPowerStrike() : base()
        {
            Name = Strings.ZA458;
            Description = Strings.ZA459;

            MpCost = 200;
            SpCost = 5;

            SpellPower = 0.0f;
            DamageModifierValue = 0.4f;

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
