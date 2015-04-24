using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class Armored : StatusEffect
    {
        #region Constructors

        //public Armored()
        //{
        //    Name = "Armored";
        //    //RoundDuration = 10;
        //    TurnDuration = 10;
        //    Probability = 1.0f;
        //    //PercentDamage = 0;
        //    //FlatDamage = 0;
        //    //PersistsOutOfCombat = false;
        //    Removable = true;
        //    //AttributeModifier = true;
        //    NegativeEffect = false;
        //    StatusEffectMessageColor = Color.NavajoWhite;
        //    //SkillName = "Shield";
        //}

        /// <summary>
        /// Constructs the Armored status effect with a given duration
        /// </summary>
        /// <param name="duration"></param>
        public Armored(int turnDuration)
        {
            Name = "Armored";
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
            //SkillName = "Shield";
        }

        #endregion

        public override void OnActivateEffect(FightingCharacter target)
        {
            base.OnActivateEffect(target);
            int defense = target.FighterStats.ModifiedDefense;
            int deltaDefense = (int)(defense * 1.5);

            StatModifier statModifier = new StatModifier()
            {
                TargetStat = TargetStat.Defense,
                ModifierValue = deltaDefense
            };

            StatModifierList.Add(statModifier);
            target.FighterStats.AddStatModifier(statModifier);
        }

    }
}
