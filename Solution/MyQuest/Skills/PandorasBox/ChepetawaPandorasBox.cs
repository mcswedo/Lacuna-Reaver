using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class ChepetawaPandorasBox : PandorasBox
    {
        public ChepetawaPandorasBox() : base()
        {
            Name = Strings.ZA432;
            Description = Strings.ZA433;

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;
            GrantsStatusEffect = true;

            DrawOffset = new Vector2(-50, -50);

            //TargetsAll = false;
            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}