using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class MultiFireBall : Skill
    {
        List<FireBall> fireBalls;

        public MultiFireBall()
        {
            Name = Strings.ZA506;
            Description = Strings.ZA507;

            MpCost = 30;
            SpCost = 6;

            SpellPower = 3;
            DamageModifierValue = 0;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;
        }

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            fireBalls = new List<FireBall>();

            for (int i = 0; i < targets.Length; ++i)
            {
                FireBall fireball = new FireBall();
                fireball.Activate(actor, targets[i]);

                fireBalls.Add(fireball);
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = fireBalls.Count - 1; i >= 0; --i)
            {
                fireBalls[i].Update(gameTime);

                if (fireBalls[i].IsRunning == false)
                {
                    fireBalls.RemoveAt(i);
                }
            }

            if (fireBalls.Count == 0)
                isRunning = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (FireBall fireBall in fireBalls)
                fireBall.Draw(spriteBatch);
        }
    }
}
