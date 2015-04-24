using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyQuest
{
    public class FighterStats
    {
        public FighterStats()
        {
        }
        public FighterStats(FighterStats other) // This is a copy constructor
        {
            baseMaxStamina = other.baseMaxStamina;
            baseMaxHealth = other.baseMaxHealth;
            baseMaxEnergy = other.baseMaxEnergy;
            health = other.health;
            stamina = other.stamina;
            energy = other.energy;

            baseStrength = other.baseStrength;
            baseDefense = other.baseDefense;
            baseIntelligence = other.baseIntelligence;
            baseWillpower = other.baseWillpower;
            baseAgility = other.baseAgility;

            experience = other.experience;
            excessExperience = other.excessExperience;
            gold = other.gold;
            initiative = other.initiative;
            nextLevelXp = other.nextLevelXp;
            maxLevel = other.maxLevel;
            level = other.level;

            modifiedMaxStamina = other.modifiedMaxStamina;
            modifiedMaxHealth = other.modifiedMaxHealth;
            modifiedMaxEnergy = other.modifiedMaxEnergy;

            modifiedStrength = other.modifiedStrength;
            modifiedDefense = other.modifiedDefense;
            modifiedIntelligence = other.modifiedIntelligence;
            modifiedWillpower = other.modifiedWillpower;
            modifiedAgility = other.modifiedAgility;
        }

        //Max Stamina
        int baseMaxStamina;
        int modifiedMaxStamina;

        public int BaseMaxStamina
        {
            get { return baseMaxStamina; }
            set { baseMaxStamina = value; ReapplyStatModifiers(); }
        }

        [XmlIgnore]
        public int ModifiedMaxStamina
        {
            get { return modifiedMaxStamina; }
        }

        // Max Health
        int baseMaxHealth;
        int modifiedMaxHealth;

        public int BaseMaxHealth
        {
            get { return baseMaxHealth; }
            set { baseMaxHealth = value; ReapplyStatModifiers(); }
        }

        [XmlIgnore]
        public int ModifiedMaxHealth
        {
            get { return modifiedMaxHealth; }
        }

        int baseMaxEnergy;
        int modifiedMaxEnergy;

        public int BaseMaxEnergy
        {
            get { return baseMaxEnergy; }
            set { baseMaxEnergy = value; ReapplyStatModifiers(); }
        }

        [XmlIgnore]
        public int ModifiedMaxEnergy
        {
            get { return modifiedMaxEnergy; }
        }

        int health;

        //[XmlIgnore]
        public int Health
        {
            get { return health; }
            set
            {
                health = value; //Was the line after in revision 1600.
                //health = (int)MathHelper.Clamp(value, 0, ModifiedMaxHealth);
            }
        }

        public void AddHealth(int deltaHealth)
        {
            health += deltaHealth;
            health = (int)MathHelper.Clamp(health, 0, modifiedMaxHealth);
        }

        //Energy
        int energy;

        //[XmlIgnore]
        public int Energy
        {
            get { return energy; }
            set
            {
                energy = value;
                //energy = (int)MathHelper.Clamp(value, 0, modifiedMaxEnergy);
            }
        }

        public void AddEnergy(int deltaEnergy)
        {
            energy += deltaEnergy;
            energy = (int)MathHelper.Clamp(energy, 0, modifiedMaxEnergy);
        }

        //Stamina
        int stamina;

        [XmlIgnore]
        public int Stamina
        {
            get { return stamina; }
            set
            {
                stamina = (int)MathHelper.Clamp(value, 0, modifiedMaxStamina);
            }
        }

        //Is Max Level
        [XmlIgnore]
        public bool IsMaxLevel
        {
            get { return level == maxLevel; }
        }

        //Strength
        int baseStrength;
        int modifiedStrength;

        public int BaseStrength
        {
            get { return baseStrength; }
            set { baseStrength = value; }//ReapplyStatModifiers(); }
        }

        [XmlIgnore]
        public int ModifiedStrength
        {
            get { return modifiedStrength; }
        }

        //Defense
        int baseDefense;
        int modifiedDefense;

        public int BaseDefense
        {
            get { return baseDefense; }
            set { baseDefense = value; }//ReapplyStatModifiers(); }
        }

        [XmlIgnore]
        public int ModifiedDefense
        {
            get { return modifiedDefense; }
        }

        //Intelligence
        int baseIntelligence;
        int modifiedIntelligence;

        public int BaseIntelligence
        {
            get { return baseIntelligence; }
            set { baseIntelligence = value; }//ReapplyStatModifiers(); }
        }

        [XmlIgnore]
        public int ModifiedIntelligence
        {
            get { return modifiedIntelligence; }
        }

        //Willpower
        int baseWillpower;
        int modifiedWillpower;

        public int BaseWillpower
        {
            get { return baseWillpower; }
            set { baseWillpower = value; }//ReapplyStatModifiers(); }
        }

        [XmlIgnore]
        public int ModifiedWillpower
        {
            get { return modifiedWillpower; }
        }

        //Agility
        int baseAgility;
        int modifiedAgility;

        public int BaseAgility
        {
            get { return baseAgility; }
            set { baseAgility = value; }//ReapplyStatModifiers(); }
        }

        [XmlIgnore]
        public int ModifiedAgility
        {
            get { return modifiedAgility; }
        }

        //Level
        int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        //Experience
        int experience;
        int excessExperience;

        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        public int ExcessExperience
        {
            get { return excessExperience; }
            set { excessExperience = value; }
        }

        //Gold
        int gold;

        public int Gold //Gold dropped in battles.
        {
            get { return gold; }
            set { gold = value; }
        }

        //Initiative
        int initiative;

        [XmlIgnore]
        public int Initiative
        {
            get { return initiative; }
        }

        //Next Level XP
        int nextLevelXp;
        int maxLevel;

        public int NextLevelXp
        {
            get { return nextLevelXp; }
            set { nextLevelXp = value; }
        }

        public int MaxLevel
        {
            get { return maxLevel; }
            set { maxLevel = value; }
        }

        [XmlIgnore]
        public List<StatModifier> statModifiers = new List<StatModifier>();

        public void AddStatModifier(StatModifier statModifier)
        {
            statModifiers.Add(statModifier);
            ReapplyStatModifiers();
        }

        public void RemoveStatModifier(StatModifier statModifier)
        {
            statModifiers.Remove(statModifier);
            ReapplyStatModifiers();
        }

        public void ReapplyStatModifiers()
        {
            modifiedMaxHealth = baseMaxHealth;
            modifiedMaxEnergy = baseMaxEnergy;
            modifiedMaxStamina = baseMaxStamina;
            modifiedStrength = baseStrength;
            modifiedDefense = baseDefense;
            modifiedIntelligence = baseIntelligence;
            modifiedWillpower = baseWillpower;
            modifiedAgility = baseAgility;
            initiative = CombatCalculations.Initiative(this);
            foreach (StatModifier statModifier in statModifiers)
            {
                ApplyStatModifier(statModifier);
            }
            health = (int)MathHelper.Clamp(health, 0, ModifiedMaxHealth);
            energy = (int)MathHelper.Clamp(energy, 0, ModifiedMaxEnergy);
            stamina = (int)MathHelper.Clamp(stamina, 0, ModifiedMaxStamina);
        }

        public void RefillStamina()
        {
            ReapplyStatModifiers();
            stamina = modifiedMaxStamina;
        }

        void ApplyStatModifier(StatModifier statModifier)
        {
            switch (statModifier.TargetStat)
            {
                case TargetStat.Agility:
                    modifiedAgility += (int)statModifier.ModifierValue;
                    break;
                case TargetStat.Defense:
                    modifiedDefense += (int)statModifier.ModifierValue;
                    break;
                case TargetStat.Intelligence:
                    modifiedIntelligence += (int)statModifier.ModifierValue;
                    break;
                case TargetStat.MaxEnergy:
                    modifiedMaxEnergy += (int)statModifier.ModifierValue;
                    break;
                case TargetStat.MaxHealth:
                    modifiedMaxHealth += (int)statModifier.ModifierValue;
                    break;
                case TargetStat.Strength:
                    modifiedStrength += (int)statModifier.ModifierValue;
                    break;
                case TargetStat.WillPower:
                    modifiedWillpower += (int)statModifier.ModifierValue;
                    break;
                case TargetStat.Stamina:
                    modifiedMaxStamina += (int)statModifier.ModifierValue;
                    break;
            }
        }
    }
}
