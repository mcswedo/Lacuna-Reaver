using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyQuest
{
    public static class CharacterLevelUp
    {
        public delegate void OnLevelUp(PCFightingCharacter character);

        /*
         * Note: currentLevel is one less than the level that the user sees.
         * So, internal level numbers start at 0 rather than 1.
         */
        public static void LevelCharacter(PCFightingCharacter character, int currentLevel)
        {
            if (character.FighterStats.Experience >= character.FighterStats.NextLevelXp)
            {
                character.FighterStats.ExcessExperience = character.FighterStats.Experience - character.FighterStats.NextLevelXp;
            }

            switch (character.Name)
            {
                case Party.nathan:
                    nathanLevelUps[currentLevel](character);
                    break;
                case Party.will:
                    willLevelUps[currentLevel](character);
                    break;
                case Party.cara:
                    caraLevelUps[currentLevel](character);
                    break;
                case Party.max:
                    maxLevelUps[currentLevel](character);
                    break;
                case Party.sid:
                    sidLevelUps[currentLevel](character);
                    break;
            }

            character.FighterStats.Health = character.FighterStats.ModifiedMaxHealth;
            character.FighterStats.Energy = character.FighterStats.ModifiedMaxEnergy;
            character.FighterStats.Experience = character.FighterStats.ExcessExperience;
            character.FighterStats.Stamina = character.FighterStats.ModifiedMaxStamina;

            if (character.FighterStats.Experience >= character.FighterStats.NextLevelXp)
            {
                character.FighterStats.Experience = character.FighterStats.NextLevelXp - 1;
            }

            if (character.FighterStats.IsMaxLevel)
            {
                character.FighterStats.Experience = 0;
            }

            character.FighterStats.ReapplyStatModifiers();
        }
        
        public static void LevelCharacter(PCFightingCharacter character)
        {
            LevelCharacter(character, character.FighterStats.Level);
        }

        static List<OnLevelUp> nathanLevelUps = new List<OnLevelUp>()
        {
            NathanLevelUps.Level1,
            NathanLevelUps.Level2,
            NathanLevelUps.Level3,
            NathanLevelUps.Level4,
            NathanLevelUps.Level5,
            NathanLevelUps.Level6,
            NathanLevelUps.Level7,
            NathanLevelUps.Level8,
            NathanLevelUps.Level9,
            NathanLevelUps.Level10,
            NathanLevelUps.Level11,
            NathanLevelUps.Level12,
            NathanLevelUps.Level13,
            NathanLevelUps.Level14,
            NathanLevelUps.Level15,
            NathanLevelUps.Level16,
            NathanLevelUps.Level17,
            NathanLevelUps.Level18,
            NathanLevelUps.Level19,
            NathanLevelUps.Level20,
            NathanLevelUps.Level21,
            NathanLevelUps.Level22,
            NathanLevelUps.Level23,
            NathanLevelUps.Level24,
            NathanLevelUps.Level25
        };

        static List<OnLevelUp> caraLevelUps = new List<OnLevelUp>()
        {
            CaraLevelUp.Level1,
            CaraLevelUp.Level2,
            CaraLevelUp.Level3,
            CaraLevelUp.Level4,
            CaraLevelUp.Level5,
            CaraLevelUp.Level6,
            CaraLevelUp.Level7,
            CaraLevelUp.Level8,
            CaraLevelUp.Level9,
            CaraLevelUp.Level10,
            CaraLevelUp.Level11,
            CaraLevelUp.Level12,
            CaraLevelUp.Level13,
            CaraLevelUp.Level14,
            CaraLevelUp.Level15,
            CaraLevelUp.Level16,
            CaraLevelUp.Level17,
            CaraLevelUp.Level18,
            CaraLevelUp.Level19,
            CaraLevelUp.Level20,
            CaraLevelUp.Level21,
            CaraLevelUp.Level22,
            CaraLevelUp.Level23,
            CaraLevelUp.Level24,
            CaraLevelUp.Level25
        };

        static List<OnLevelUp> willLevelUps = new List<OnLevelUp>()
        {
            WillLevelUp.Level1,
            WillLevelUp.Level2,
            WillLevelUp.Level3,
            WillLevelUp.Level4,
            WillLevelUp.Level5,
            WillLevelUp.Level6,
            WillLevelUp.Level7,
            WillLevelUp.Level8,
            WillLevelUp.Level9,
            WillLevelUp.Level10,
            WillLevelUp.Level11,
            WillLevelUp.Level12,
            WillLevelUp.Level13,
            WillLevelUp.Level14,
            WillLevelUp.Level15,
            WillLevelUp.Level16,
            WillLevelUp.Level17,
            WillLevelUp.Level18,
            WillLevelUp.Level19,
            WillLevelUp.Level20,
            WillLevelUp.Level21,
            WillLevelUp.Level22,
            WillLevelUp.Level23,
            WillLevelUp.Level24,
            WillLevelUp.Level25
        };

        static List<OnLevelUp> maxLevelUps = new List<OnLevelUp>()
        {
            MaxLevelUp.Level1,
            MaxLevelUp.Level2,
            MaxLevelUp.Level3,
            MaxLevelUp.Level4,
            MaxLevelUp.Level5,
            MaxLevelUp.Level6,
            MaxLevelUp.Level7,
            MaxLevelUp.Level8,
            MaxLevelUp.Level9,
            MaxLevelUp.Level10,
            MaxLevelUp.Level11,
            MaxLevelUp.Level12,
            MaxLevelUp.Level13,
            MaxLevelUp.Level14,
            MaxLevelUp.Level15
        };

        static List<OnLevelUp> sidLevelUps = new List<OnLevelUp>()
        {
            SidLevelUp.Level1,
            SidLevelUp.Level2,
            SidLevelUp.Level3,
            SidLevelUp.Level4,
            SidLevelUp.Level5,
            SidLevelUp.Level6,
            SidLevelUp.Level7,
            SidLevelUp.Level8,
            SidLevelUp.Level9,
            SidLevelUp.Level10,
            SidLevelUp.Level11,
            SidLevelUp.Level12,
            SidLevelUp.Level13,
            SidLevelUp.Level14,
            SidLevelUp.Level15
        };
    }

}
