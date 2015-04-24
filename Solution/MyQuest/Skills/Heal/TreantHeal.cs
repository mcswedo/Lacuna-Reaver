using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class TreantHeal : Heal
    {
        public TreantHeal() : base()
        {
            Name = Strings.ZA409;
            Description = Strings.ZA412;

            MpCost = 15;
            SpCost = 3;

            SpellPower = 3.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = false;
            MapSkill = false;
            HealingSkill = true;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = false;
        }
    }
}