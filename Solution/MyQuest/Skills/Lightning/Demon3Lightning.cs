using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Demon3Lightning : Lightning
    {
        public Demon3Lightning()
            : base()
        {
            Name = Strings.ZA422;
            Description = Strings.ZA423;

            MpCost = 500;
            SpCost = 4;

            SpellPower = 30.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}
