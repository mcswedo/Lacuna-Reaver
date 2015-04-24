using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class BurtleShield : Shield
    {
        public BurtleShield() : base()
        {
            Name = Strings.ZA477;
            Description = Strings.ZA476;

            MpCost = 15;
            SpCost = 3;

            SpellPower = 0;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            TargetsAll = false;
            CanTargetAllies = true;
            CanTargetEnemy = false;
        }
    }
}
