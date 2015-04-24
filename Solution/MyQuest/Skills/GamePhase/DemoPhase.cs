using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    /// <summary>
    /// This class allows a play tester to transition to a different phase of the game.
    /// 
    /// </summary>
    public class DemoPhase : Skill
    {
        public DemoPhase()
        {
            Name = "DemoPhase";
            Description = "Transition to a game demo phase.";

            MpCost = 0;
            SpCost = 0;

            SpellPower = 0;
            DamageModifierValue = 0;

            BattleSkill = false;
            MapSkill = true;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = true;
            CanTargetEnemy = false;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
