using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class CaveCrabFlameLance : FireBall
    {
        public CaveCrabFlameLance()
            : base()
        {

            Name = Strings.ZA389;
            Description = Strings.ZA390;

            MpCost = 200;
            SpCost = 2;

            SpellPower = 13.0f;
            DamageModifierValue = 0f;

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
