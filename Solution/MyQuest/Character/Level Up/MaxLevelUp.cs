
namespace MyQuest
{
    internal static class MaxLevelUp
    {
        internal static void Level1(PCFightingCharacter character)
        {

        }

        internal static void Level2(PCFightingCharacter character)
        {

        }

        internal static void Level3(PCFightingCharacter character)
        {

        }

        internal static void Level4(PCFightingCharacter character)
        {

        }

        internal static void Level5(PCFightingCharacter character)
        {

        }

        internal static void Level6(PCFightingCharacter character)
        {

        }

        internal static void Level7(PCFightingCharacter character)
        {

        }

        internal static void Level8(PCFightingCharacter character)
        {

        }

        internal static void Level9(PCFightingCharacter character)
        {

        }

        internal static void Level10(PCFightingCharacter character)
        {

        }

        internal static void Level11(PCFightingCharacter character)
        {

        }

        internal static void Level12(PCFightingCharacter character)
        {
            Level11(character);
            character.FighterStats.Level = 12;
            character.FighterStats.BaseIntelligence = 80;
            character.FighterStats.BaseWillpower = 80;
            character.FighterStats.BaseAgility = 90;
            character.FighterStats.BaseStrength = 100;
            character.FighterStats.BaseDefense = 85;

            character.FighterStats.BaseMaxEnergy = 250;
            character.FighterStats.BaseMaxHealth = 3000;

            character.FighterStats.Experience = 0;

            //character.SkillNames.Clear();
            character.AddSkillName("MaxPoisonStrike");
            character.AddSkillName("DarkStrike");

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(12);
        }

        internal static void Level13(PCFightingCharacter character)
        {
            Level12(character);
            character.FighterStats.Level = 13;
            character.FighterStats.BaseIntelligence = 85;
            character.FighterStats.BaseWillpower = 85;
            character.FighterStats.BaseAgility = 95;
            character.FighterStats.BaseStrength = 105;
            character.FighterStats.BaseDefense = 90;

            character.FighterStats.BaseMaxEnergy = 275;
            character.FighterStats.BaseMaxHealth = 3200;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(13);
        }

        internal static void Level14(PCFightingCharacter character)
        {
            Level13(character);
            character.FighterStats.Level = 14;
            character.FighterStats.BaseIntelligence = 90;
            character.FighterStats.BaseWillpower = 90;
            character.FighterStats.BaseAgility = 100;
            character.FighterStats.BaseStrength = 110;
            character.FighterStats.BaseDefense = 95;

            character.FighterStats.BaseMaxEnergy = 300;
            character.FighterStats.BaseMaxHealth = 3500;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(14);
        }

        internal static void Level15(PCFightingCharacter character)
        {
            Level14(character);
            character.FighterStats.Level = 15;
            character.FighterStats.BaseIntelligence = 96;
            character.FighterStats.BaseWillpower = 97;
            character.FighterStats.BaseAgility = 110;
            character.FighterStats.BaseStrength = 120;
            character.FighterStats.BaseDefense = 105;

            character.FighterStats.BaseMaxEnergy = 350;
            character.FighterStats.BaseMaxHealth = 3750;

            character.FighterStats.NextLevelXp = -1;
        }

    }
}
