using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyQuest
{
    public static class CombatCalculations
    {
        public static float RawPhysicalDamage(FighterStats stats)
        {
            float damage;
            float strength = (float) stats.ModifiedStrength;
            float level = (float) stats.Level;

            damage = strength + (((strength + level) / 16f) * ((strength * level) / 16f));

            return damage;
        }

        public static float RawMagicalDamage(FighterStats stats, float spellpower)
        {
            float damage;
            int intelligence = stats.ModifiedIntelligence;
            int level = stats.Level;

            damage = (intelligence + level) * spellpower;

            return damage; 
        }

        public static float ModifiedDamage(List<DamageModifier> modifiers, float damage)
        {
            foreach (DamageModifier modifier in modifiers)
            {
                if (modifier.IsPositive)
                {
                    damage *= (1 + modifier.ModifierValue);
                }
                else
                {
                    damage *= (1 - modifier.ModifierValue);
                }
            }
            //Vary damage to make combat damage slightly random.
            damage *= (float)(0.9 + Utility.RNG.NextDouble() * 0.2);

            return damage;
        }

        public static int PhysicalDefense(FighterStats stats, float damage)
        {
            float finalDamage;
            float defense = stats.ModifiedDefense;

            //Set a defense cap so that damage isn't negative.
            if (defense > 450)
            {
                defense = 450;
            }

            finalDamage = damage * ((500 - defense) / 500);

            return (int)(finalDamage + 0.5f);
        }

        public static int MagicalDefense(FighterStats stats, float damage)
        {
            float finalDamage;
            float willpower = stats.ModifiedWillpower;

            finalDamage = damage * ((500 - willpower) / 500);

            return (int)(finalDamage + 0.5f);
        }

        public static int Initiative(FighterStats stats)
        {
            float initiative = 0;
            int agility = stats.ModifiedAgility;
            int level = stats.Level;

            if (level > 0)
            {
                initiative = (agility / 2) + Utility.RNG.Next(1, ((agility / 3) + level));
            }

            return (int)(initiative + 0.5f);
        }

        public static bool RunSuccessful(int avgCharacterLevel, int avgEnemyLevel, int partySize)
        {
            int escapeProbability = 50 - (10 * (avgEnemyLevel - avgCharacterLevel));
            bool escaped = false;

            if (Utility.RNG.Next(1, 100) <= escapeProbability)
                escaped = true;

            return escaped;
        }

        public static bool PhysicalCrit(FighterStats stats)
        {
            bool critical = false;
            int strength = stats.ModifiedStrength;
            float critChance = 5 + (strength / 50);

            if (Utility.RNG.Next(1, 100) <= critChance)
                critical = true;

            return critical;
        }

        public static bool MagicalCrit(FighterStats stats)
        {
            bool critical = false;
            int intelligence = stats.ModifiedIntelligence;
            float critChance = 5 + (intelligence / 50);

            if (Utility.RNG.Next(1, 100) <= critChance)
                critical = true;

            return critical;
        }

        public static int GetNextLevelExperience(int level)
        {
            int nextLevelXp = 0;

            for (int i = 0; i < level; i++)
            {
                nextLevelXp = (int)(50 * (Math.Pow(level, 2.5) + 1));
            }

            return nextLevelXp;
        }
    }
}
