
namespace MyQuest
{
    internal static class WillLevelUp
    {
        internal static void Level1(PCFightingCharacter character)
        {
            character.FighterStats.Level = 1;
            character.FighterStats.BaseStrength = 32;
            character.FighterStats.BaseDefense = 20;
            character.FighterStats.BaseAgility = 50;
            character.FighterStats.BaseIntelligence = 40;
            character.FighterStats.BaseWillpower = 30;

            character.FighterStats.BaseMaxEnergy = 100;
            character.FighterStats.BaseMaxHealth = 150;

            character.FighterStats.Experience = 0;
            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(1);

//            character.SkillNames.Clear();
            character.AddSkillName("ShadowStrike");
        }

        internal static void Level2(PCFightingCharacter character)
        {
            Level1(character);
            character.FighterStats.Level = 2;
            character.FighterStats.BaseStrength = 39;
            character.FighterStats.BaseDefense = 25;
            character.FighterStats.BaseAgility = 58;
            character.FighterStats.BaseIntelligence = 46;
            character.FighterStats.BaseWillpower = 35;

            character.FighterStats.BaseMaxEnergy = 125;
            character.FighterStats.BaseMaxHealth = 225;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(2);

            character.AddSkillName("WillFireBall");
        }

        internal static void Level3(PCFightingCharacter character)
        {
            Level2(character);
            character.FighterStats.Level = 3;
            character.FighterStats.BaseStrength = 44;
            character.FighterStats.BaseDefense = 30;
            character.FighterStats.BaseAgility = 70;
            character.FighterStats.BaseIntelligence = 52;
            character.FighterStats.BaseWillpower = 44;

            character.FighterStats.BaseMaxEnergy = 175;
            character.FighterStats.BaseMaxHealth = 325;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(3);

            character.AddSkillName("WillPoison");
        }

        internal static void Level4(PCFightingCharacter character)
        {
            Level3(character);
            character.FighterStats.Level = 4;
            character.FighterStats.BaseStrength = 51;
            character.FighterStats.BaseDefense = 35;
            character.FighterStats.BaseAgility = 78;
            character.FighterStats.BaseIntelligence = 58;
            character.FighterStats.BaseWillpower = 52;

            character.FighterStats.BaseMaxEnergy = 225;
            character.FighterStats.BaseMaxHealth = 425;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(4);

            character.AddSkillName("Siphon");
        }

        internal static void Level5(PCFightingCharacter character)
        {
            Level4(character);
            character.FighterStats.Level = 5;
            character.FighterStats.BaseStrength = 58;
            character.FighterStats.BaseDefense = 45;
            character.FighterStats.BaseAgility = 88;
            character.FighterStats.BaseIntelligence = 65;
            character.FighterStats.BaseWillpower = 60;

            character.FighterStats.BaseMaxEnergy = 275;
            character.FighterStats.BaseMaxHealth = 600;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(5);

            character.AddSkillName("Blind");
        }

        internal static void Level6(PCFightingCharacter character)
        {
            Level5(character);
            character.FighterStats.Level = 6;
            character.FighterStats.BaseIntelligence = 74;
            character.FighterStats.BaseWillpower = 70;
            character.FighterStats.BaseAgility = 95;
            character.FighterStats.BaseStrength = 64;
            character.FighterStats.BaseDefense = 50;

            character.FighterStats.BaseMaxEnergy = 350;
            character.FighterStats.BaseMaxHealth = 800;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(6);

        }

        internal static void Level7(PCFightingCharacter character)
        {
            Level6(character);
            character.FighterStats.Level = 7;
            character.FighterStats.BaseIntelligence = 80;
            character.FighterStats.BaseWillpower = 76;
            character.FighterStats.BaseAgility = 102;
            character.FighterStats.BaseStrength = 70;
            character.FighterStats.BaseDefense = 58;

            character.FighterStats.BaseMaxEnergy = 400;
            character.FighterStats.BaseMaxHealth = 1050; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(7);

            character.AddSkillName("Weakness");
        }

        internal static void Level8(PCFightingCharacter character)
        {
            Level7(character);
            character.FighterStats.Level = 8;
            character.FighterStats.BaseIntelligence = 86;
            character.FighterStats.BaseWillpower = 80;
            character.FighterStats.BaseAgility = 110;
            character.FighterStats.BaseStrength = 77;
            character.FighterStats.BaseDefense = 64;

            character.FighterStats.BaseMaxEnergy = 450;
            character.FighterStats.BaseMaxHealth = 1350;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(8);
        }

        internal static void Level9(PCFightingCharacter character)
        {
            Level8(character);
            character.FighterStats.Level = 9;
            character.FighterStats.BaseIntelligence = 92;
            character.FighterStats.BaseWillpower = 86;
            character.FighterStats.BaseAgility = 116;
            character.FighterStats.BaseStrength = 82;
            character.FighterStats.BaseDefense = 70;

            character.FighterStats.BaseMaxEnergy = 475;
            character.FighterStats.BaseMaxHealth = 1700; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(9);

        }

        internal static void Level10(PCFightingCharacter character)
        {
            Level9(character);
            character.FighterStats.Level = 10;
            character.FighterStats.BaseIntelligence = 97;
            character.FighterStats.BaseWillpower = 90;
            character.FighterStats.BaseAgility = 125;
            character.FighterStats.BaseStrength = 88;
            character.FighterStats.BaseDefense = 77;

            character.FighterStats.BaseMaxEnergy = 550;
            character.FighterStats.BaseMaxHealth = 2100; //1700 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(10);

            character.AddSkillName("Paralyze");
        }

        internal static void Level11(PCFightingCharacter character)
        {
            Level10(character);
            character.FighterStats.Level = 11;
            character.FighterStats.BaseIntelligence = 105;
            character.FighterStats.BaseWillpower = 96;
            character.FighterStats.BaseAgility = 135;
            character.FighterStats.BaseStrength = 95;
            character.FighterStats.BaseDefense = 85;

            character.FighterStats.BaseMaxEnergy = 625;
            character.FighterStats.BaseMaxHealth = 2500; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(11);

            character.AddSkillName("FlameStrike");
        }

        internal static void Level12(PCFightingCharacter character)
        {
            Level11(character);
            character.FighterStats.Level = 12;
            character.FighterStats.BaseIntelligence = 112;
            character.FighterStats.BaseWillpower = 102;
            character.FighterStats.BaseAgility = 144;
            character.FighterStats.BaseStrength = 101;
            character.FighterStats.BaseDefense = 92;

            character.FighterStats.BaseMaxEnergy = 700;
            character.FighterStats.BaseMaxHealth = 3000; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(12);

            character.AddSkillName("Plague");
        }

        internal static void Level13(PCFightingCharacter character)
        {
            Level12(character);
            character.FighterStats.Level = 13;
            character.FighterStats.BaseIntelligence = 118;
            character.FighterStats.BaseWillpower = 108;
            character.FighterStats.BaseAgility = 151;
            character.FighterStats.BaseStrength = 106;
            character.FighterStats.BaseDefense = 96;

            character.FighterStats.BaseMaxEnergy = 750;
            character.FighterStats.BaseMaxHealth = 3800; //3300 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(13);
        }

        internal static void Level14(PCFightingCharacter character)
        {
            Level13(character);
            character.FighterStats.Level = 14;
            character.FighterStats.BaseIntelligence = 124;
            character.FighterStats.BaseWillpower = 114;
            character.FighterStats.BaseAgility = 160;
            character.FighterStats.BaseStrength = 112;
            character.FighterStats.BaseDefense = 102;

            character.FighterStats.BaseMaxEnergy = 825;
            character.FighterStats.BaseMaxHealth = 4650; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(14);

            // Will learns this skill from his side quest in mage town.
            //character.AddSkillName("ShadowBlast");
        }

        internal static void Level15(PCFightingCharacter character)
        {
            Level14(character);
            character.FighterStats.Level = 15;
            character.FighterStats.BaseIntelligence = 130;
            character.FighterStats.BaseWillpower = 120;
            character.FighterStats.BaseAgility = 170;
            character.FighterStats.BaseStrength = 118;
            character.FighterStats.BaseDefense = 108;

            character.FighterStats.BaseMaxEnergy = 900; 
            character.FighterStats.BaseMaxHealth = 5000; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(15);
        }

        internal static void Level16(PCFightingCharacter character)
        {
            Level15(character);
            character.FighterStats.Level = 16;
            character.FighterStats.BaseIntelligence = 135;
            character.FighterStats.BaseWillpower = 125;
            character.FighterStats.BaseAgility = 182;
            character.FighterStats.BaseStrength = 122;
            character.FighterStats.BaseDefense = 112;

            character.FighterStats.BaseMaxEnergy = 1000;
            character.FighterStats.BaseMaxHealth = 5800; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(16);

            character.AddSkillName("Blackout");
        }

        internal static void Level17(PCFightingCharacter character)
        {
            Level16(character);
            character.FighterStats.Level = 17;
            character.FighterStats.BaseIntelligence = 141;
            character.FighterStats.BaseWillpower = 131;
            character.FighterStats.BaseAgility = 192;
            character.FighterStats.BaseStrength = 127;
            character.FighterStats.BaseDefense = 117;

            character.FighterStats.BaseMaxEnergy = 1100;
            character.FighterStats.BaseMaxHealth = 6500;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(17);
        }

        internal static void Level18(PCFightingCharacter character)
        {
            Level17(character);
            character.FighterStats.Level = 18;
            character.FighterStats.BaseIntelligence = 150;
            character.FighterStats.BaseWillpower = 140;
            character.FighterStats.BaseAgility = 204;
            character.FighterStats.BaseStrength = 133;
            character.FighterStats.BaseDefense = 123;

            character.FighterStats.BaseMaxEnergy = 1200;
            character.FighterStats.BaseMaxHealth = 7600;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(18);

            character.AddSkillName("FireStorm");
        }

        internal static void Level19(PCFightingCharacter character)
        {
            Level18(character);
            character.FighterStats.Level = 19;
            character.FighterStats.BaseIntelligence = 156;
            character.FighterStats.BaseWillpower = 146;
            character.FighterStats.BaseAgility = 215;
            character.FighterStats.BaseStrength = 140;
            character.FighterStats.BaseDefense = 130;

            character.FighterStats.BaseMaxEnergy = 1350;
            character.FighterStats.BaseMaxHealth = 8100; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(19);
        }

        internal static void Level20(PCFightingCharacter character)
        {
            Level19(character);
            character.FighterStats.Level = 20;
            character.FighterStats.BaseIntelligence = 166;
            character.FighterStats.BaseWillpower = 156;
            character.FighterStats.BaseAgility = 230;
            character.FighterStats.BaseStrength = 150;
            character.FighterStats.BaseDefense = 140;

            character.FighterStats.BaseMaxEnergy = 1500;
            character.FighterStats.BaseMaxHealth = 8800;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(20);

            character.AddSkillName("Disintegrate");
        }

        internal static void Level21(PCFightingCharacter character)
        {
            Level20(character);
            character.FighterStats.Level = 21;
            character.FighterStats.BaseStrength = 158;
            character.FighterStats.BaseDefense = 150;
            character.FighterStats.BaseAgility = 245;
            character.FighterStats.BaseIntelligence = 177;
            character.FighterStats.BaseWillpower = 166;

            character.FighterStats.BaseMaxEnergy = 1800; //1650;
            character.FighterStats.BaseMaxHealth = 9600;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(22);
        }

        internal static void Level22(PCFightingCharacter character)
        {
            Level21(character);
            character.FighterStats.Level = 22;
            character.FighterStats.BaseStrength = 166;
            character.FighterStats.BaseDefense = 162;
            character.FighterStats.BaseAgility = 260;
            character.FighterStats.BaseIntelligence = 189;
            character.FighterStats.BaseWillpower = 178;

            character.FighterStats.BaseMaxEnergy = 1950; //1800;
            character.FighterStats.BaseMaxHealth = 10750;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(23);
        }

        internal static void Level23(PCFightingCharacter character)
        {
            Level22(character);
            character.FighterStats.Level = 23;
            character.FighterStats.BaseStrength = 175;
            character.FighterStats.BaseDefense = 174;
            character.FighterStats.BaseAgility = 275;
            character.FighterStats.BaseIntelligence = 200;
            character.FighterStats.BaseWillpower = 190;

            character.FighterStats.BaseMaxEnergy = 2250; //1950;
            character.FighterStats.BaseMaxHealth = 12000;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(24);
        }

        internal static void Level24(PCFightingCharacter character)
        {
            Level23(character);
            character.FighterStats.Level = 24;
            character.FighterStats.BaseStrength = 183;
            character.FighterStats.BaseDefense = 184;
            character.FighterStats.BaseAgility = 290;
            character.FighterStats.BaseIntelligence = 212;
            character.FighterStats.BaseWillpower = 202;

            character.FighterStats.BaseMaxEnergy = 2400; //2100;
            character.FighterStats.BaseMaxHealth = 13500;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(25);
        }

        internal static void Level25(PCFightingCharacter character)
        {
            Level24(character);
            character.FighterStats.Level = 25;
            character.FighterStats.BaseStrength = 190;
            character.FighterStats.BaseDefense = 200;
            character.FighterStats.BaseAgility = 305;
            character.FighterStats.BaseIntelligence = 220;
            character.FighterStats.BaseWillpower = 210;

            character.FighterStats.BaseMaxEnergy = 2700; //2500;
            character.FighterStats.BaseMaxHealth = 15000;

            character.FighterStats.NextLevelXp = -1;
        }
    }
}
