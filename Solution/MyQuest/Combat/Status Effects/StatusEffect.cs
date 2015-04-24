using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    public abstract class StatusEffect
    {
        #region Properties

        string name;

        /// <summary>
        /// The name of the status effect
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string iconName;

        /// <summary>
        /// The name of the icon for the status effect
        /// </summary>
        public string IconName
        {
            get { return iconName; }
            set { iconName = value; }
        }

        Texture2D icon;

        /// <summary>
        /// The icon for the status effect
        /// </summary>
        public Texture2D Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        //int roundDuration;

        /// <summary>
        /// The number of combat rounds this effect persists
        /// </summary>
        //public int RoundDuration
        //{
        //    get { return roundDuration; }
        //    set { roundDuration = value; }
        //}

        int turnDuration = 0;

        public int TurnDuration
        {
            get { return turnDuration; }
            set { turnDuration = value; }
        }

        int turnsRemaining = 0;

        public int TurnsRemaining
        {
            get { return turnsRemaining; }
            set { turnsRemaining = value; }
        }

        float probability;

        /// <summary>
        /// The probability that this effect will be applied to a target
        /// </summary>
        public float Probability
        {
            get { return probability; }
            set {
                    if (value > 1 || value < 0)
                    {
                        throw new Exception("Probability must be between 0 and 1");
                    }
                
                    probability = value; 
                }
        }

        //float percentDamage;

        ///// <summary>
        ///// How many hit points this effect consumes on the target in a round by percentage
        ///// </summary>
        //public float PercentDamage
        //{
        //    get { return percentDamage; }
        //    set {
        //            if (value > 1 || value < 0)
        //            {
        //                throw new Exception("Percent Damage must be between 0 and 1");
        //            }

        //            percentDamage = value;
        //        }
        //}

        //int flatDamage;

        ///// <summary>
        ///// How many hit points this effect consumes on the target in a round by integer value
        ///// </summary>
        //public int FlatDamage
        //{
        //    get { return flatDamage; }
        //    set { flatDamage = value; }
        //}

        //bool persistsOutOfCombat;

        ///// <summary>
        ///// True if the status effect is maintained outside of combat
        ///// </summary>
        //public bool PersistsOutOfCombat
        //{
        //    get { return persistsOutOfCombat; }
        //    set { persistsOutOfCombat = value; }
        //}

        bool removable;

        /// <summary>
        /// True if the status effect can be removed
        /// </summary>
        public bool Removable
        {
            get { return removable; }
            set { removable = value; }
        }

        //bool attributeModifier;

        ///// <summary>
        ///// True if the status effect modifies the fighting character's attributes, false if not
        ///// </summary>
        //public bool AttributeModifier
        //{
        //    get { return attributeModifier; }
        //    set { attributeModifier = value; }
        //}

        bool negativeEffect;

        /// <summary>
        /// True if the status effect is harmful to the character, false if not.
        /// </summary>
        public bool NegativeEffect
        {
            get { return negativeEffect; }
            set { negativeEffect = value; }
        }

        //string skillName;

        ///// <summary>
        ///// The name of the skill that grants this status effect.
        ///// </summary>
        //public string SkillName
        //{
        //    get { return skillName; }
        //    set { skillName = value; }
        //}

        Color statusEffectMessageColor;

        public Color StatusEffectMessageColor
        {
            get { return statusEffectMessageColor; }
            set { statusEffectMessageColor = value; }
        }

        protected List<StatModifier> StatModifierList = new List<StatModifier>();

        #endregion

        #region Methods

        public virtual void LoadContent(ContentManager content)
        {
            if (IconName != null)
                Icon = content.Load<Texture2D>(ContentPath.ToPortraits + IconName);                
        }

        /// <summary>
        /// Called at the end of every round of combat.
        /// </summary>
        /// <param name="target"></param>
        //public virtual void ResolveEffect(FightingCharacter target)
        //{
        //    if (--RoundDuration <= 0)
        //    {
        //        target.RemoveStatusEffect(this);
        //    }
        //}

        public virtual void OnActivateEffect(FightingCharacter target)
        {
            turnsRemaining = turnDuration;
        }

        public virtual void OnStartTurn(FightingCharacter target)
        {
            turnsRemaining--;
            if (turnsRemaining == 0)
            {
                foreach (StatModifier statModifier in StatModifierList)
                {
                    target.FighterStats.RemoveStatModifier(statModifier);
                }
                StatModifierList.Clear();
                target.RemoveStatusEffect(this);
            }
        }

        public virtual void OnEndCombat(FightingCharacter target)
        {
            foreach (StatModifier statModifier in StatModifierList)
            {
                target.FighterStats.RemoveStatModifier(statModifier);
            }
            StatModifierList.Clear();
            target.RemoveStatusEffect(this);
        }

        public virtual void OnDeath(FightingCharacter target)
        {
            foreach (StatModifier statModifier in StatModifierList)
            {
                target.FighterStats.RemoveStatModifier(statModifier);
            } 
            StatModifierList.Clear();
            target.RemoveStatusEffect(this);
        }

        #endregion
    }
}
