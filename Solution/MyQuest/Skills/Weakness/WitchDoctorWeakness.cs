using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class WitchDoctorWeakness : Weakness
    {
        public WitchDoctorWeakness() : base()
        {
            Name = Strings.ZA502;
            Description = Strings.ZA503;

            MpCost = 100;
            SpCost = 6;

            SpellPower = 3.0f;
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