using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class FlyderWhirlwind : Whirlwind
    { 
        public FlyderWhirlwind() : base()
        {
            Name = Strings.ZA504;
            Description = Strings.ZA505;

            MpCost = 25;
            SpCost = 8;

            SpellPower = 2.3f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-230, -230);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}
