using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class ElderMantisUnholyRevelations : UnholyRevelations
    {
        public ElderMantisUnholyRevelations() : base()
        {
            Name = "Unholy Revelations";
            Description = "The mantis summons the undead to attack its target";

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0.0f;
            DamageModifierValue = 0.0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            RenderOffset = new Vector2(-50, -50);

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }
    }
}