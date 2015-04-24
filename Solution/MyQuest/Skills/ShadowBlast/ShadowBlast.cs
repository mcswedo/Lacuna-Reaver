using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ShadowBlast : Skill
    {
        #region Fields

        Vector2 screenPosition;


        #endregion

        #region Constructor


        public ShadowBlast()
        {
            Name = Strings.ZA468;
            Description = Strings.ZA469;

            MpCost = 120;
            SpCost = 7;

            SpellPower = 9.5f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = actor.ScreenPosition + new Vector2(20, 30);

            actor.SetAnimation("ShadowBlast");
        }

        public override void Update(GameTime gameTime)
        {
            if (actor.CurrentAnimation.IsRunning == false)
            {
                actor.SetAnimation("Idle");
                DealMagicDamage(actor, targets.ToArray());
                isRunning = false;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
