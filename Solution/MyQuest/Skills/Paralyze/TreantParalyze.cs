using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class TreantParalyze : Paralyze
    {
        public TreantParalyze() : base()
        {
            Name = "Stun-Sap";
            Description = "The treant sprays brownish orange sap from its injured torso.";

            MpCost = 25;
            SpCost = 6;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            DrawOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}