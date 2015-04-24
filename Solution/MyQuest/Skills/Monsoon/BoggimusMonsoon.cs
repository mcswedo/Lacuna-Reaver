using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class BoggimusMonsoon : Monsoon
    {
        public BoggimusMonsoon()
            : base()
        {
            Name = Strings.ZA427;
            Description = Strings.ZA428;

            MpCost = 20;
            SpCost = 3;

            SpellPower = 12f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

           // DrawOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}