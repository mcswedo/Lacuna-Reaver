using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Poisoned : StatusEffect
    {
        float percentDamage = 1/16f;
        #region Constructors

        public Poisoned() //Bug: Breaks when Poison damage kills somebody. AS OF REVISION 1474. Has to do with active skill being null in turnexecutor.
        {
            Name = "Poisoned";
            IconName = "poison_icon";
            //RoundDuration = 10;
            TurnDuration = 5;
            Probability = .65f;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = false;
            NegativeEffect = true;
            StatusEffectMessageColor = Color.Purple;
            //SkillName = "Poison";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
        }

        public override void OnStartTurn(FightingCharacter target)
        {
            base.OnStartTurn(target);
            if (target.State != State.Invulnerable)
            {
                int damage = (int)(target.FighterStats.ModifiedMaxHealth * percentDamage);

                target.FighterStats.Health -= damage;
                target.FighterStats.Health = (int)MathHelper.Clamp(target.FighterStats.Health, 0, target.FighterStats.ModifiedMaxHealth);

                CombatMessage.AddMessage(damage.ToString(), target.DamageMessagePosition, Color.Purple, .5);
            }
            else
            {
                CombatMessage.AddMessage(0.ToString(), target.DamageMessagePosition, Color.Purple, .5);
            }
            //target.DisplayDamage(damage);
        }
    }
}
