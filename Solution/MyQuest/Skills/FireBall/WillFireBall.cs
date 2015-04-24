using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class WillFireBall : FireBall
    {
        public WillFireBall() : base()
        {
            
            Name = Strings.ZA383;
            Description = Strings.ZA384;

            MpCost = 10;
            SpCost = 2;

            SpellPower = Will.Instance.FighterStats.Level - 1f;//5;
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
