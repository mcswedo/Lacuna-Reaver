using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class VoodooDollDestiny : Destiny
    {
        public VoodooDollDestiny() : base()
        {
            Name = Strings.ZA363;
            Description = Strings.ZA364;

            MpCost = 300;
            SpCost = 8;

            SpellPower = 6.0f;
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