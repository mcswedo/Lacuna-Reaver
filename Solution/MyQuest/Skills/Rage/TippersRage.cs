using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{

    public class TippersRage : Skill
    {

        #region Constructor

        //This should never be able to be used and is only here to have the knowledge of this possibility if the Tippers is analyzed.
        public TippersRage()
        {
            Name = Strings.ZA654;
            Description = Strings.ZA655;

            MpCost = 100000;
            SpCost = 999; 

            SpellPower = 0;
            DamageModifierValue = 0;
                        
            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;
            GrantsStatusEffect = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = false;
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
