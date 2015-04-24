using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Warded : StatusEffect
    {
        #region Constructors

        //public Warded()
        //{
        //    Name = "Warded";
        //    //RoundDuration = 10;
        //    TurnDuration = 
        //    Probability = 1.0f;
        //    //PercentDamage = 0;
        //    //FlatDamage = 0;
        //    //PersistsOutOfCombat = false;
        //    Removable = true;
        //    //AttributeModifier = true;
        //    NegativeEffect = false;
        //    StatusEffectMessageColor = Color.NavajoWhite;
        //    //SkillName = "Mana Ward";
        //}

        /// <summary>
        /// Constructs the Regeneration status effect with a given duration
        /// </summary>
        /// <param name="duration"></param>
        public Warded(int turnDuration)
        {
            Name = "Warded";
            IconName = null;
            //RoundDuration = duration;
            TurnDuration = turnDuration;
            Probability = 1f;
            //PercentDamage = 0;
            //FlatDamage = 0;
            //PersistsOutOfCombat = false;
            Removable = true;
            //AttributeModifier = true;
            NegativeEffect = false;
            StatusEffectMessageColor = Color.NavajoWhite;
            //SkillName = "Mana Ward";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
            int deltaWillpower = (int)(target.FighterStats.ModifiedWillpower * .5);

            StatModifier statModifier = new StatModifier()
            {
                TargetStat = TargetStat.WillPower,
                ModifierValue = deltaWillpower
            };
            StatModifierList.Add(statModifier);
            target.FighterStats.AddStatModifier(statModifier);
        }
    }
}
