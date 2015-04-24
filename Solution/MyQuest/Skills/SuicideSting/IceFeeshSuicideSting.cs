using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class IceFeeshSuicideSting : SuicideSting
    {
        public IceFeeshSuicideSting() : base()
        {
            Name = Strings.ZA488;
            Description = Strings.ZA489;

            MpCost = 0;
            SpCost = 10;

            SpellPower = 15.0f;
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