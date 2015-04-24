using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class Demon2Siphon : Siphon
    {
        public Demon2Siphon()
            : base()
        {
            Name = Strings.ZA478;
            Description = Strings.ZA481;

            MpCost = 125;
            SpCost = 3;

            SpellPower = 12.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-100, -100);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}
