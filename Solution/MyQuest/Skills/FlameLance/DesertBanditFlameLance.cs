using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class DesertBanditFlameLance : FireBall
    {
        public DesertBanditFlameLance()
            : base()
        {

            Name = Strings.ZA389;
            Description = Strings.ZA391;

            MpCost = 200;
            SpCost = 5;

            SpellPower = 3.5f;
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
