using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class ChepetawaMonsoon : Monsoon
    {
        public ChepetawaMonsoon() : base()
        {
            Name = Strings.ZA427;
            Description = Strings.ZA430;

            MpCost = 30;
            SpCost = 3;

            SpellPower = 4f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

           // DrawOffset = new Vector2(-50, -50);

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}