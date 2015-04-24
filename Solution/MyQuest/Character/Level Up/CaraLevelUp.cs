
namespace MyQuest
{

    static class CaraLevelUp
    {
        internal static void Level1(PCFightingCharacter character)
        {
            character.FighterStats.Level = 1;
            character.FighterStats.BaseIntelligence = 44;
            character.FighterStats.BaseWillpower = 37;
            character.FighterStats.BaseAgility = 40;
            character.FighterStats.BaseStrength = 22;
            character.FighterStats.BaseDefense = 22;

            character.FighterStats.BaseMaxEnergy = 125;
            character.FighterStats.BaseMaxHealth = 120;

            //character.SkillNames.Clear();

            character.FighterStats.Experience = 0;
            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(1);

            character.AddSkillName("Heal");
        }

        internal static void Level2(PCFightingCharacter character)
        {
            Level1(character);
            character.FighterStats.Level = 2;
            character.FighterStats.BaseIntelligence = 51;
            character.FighterStats.BaseWillpower = 42;
            character.FighterStats.BaseAgility = 48;
            character.FighterStats.BaseStrength = 25;
            character.FighterStats.BaseDefense = 25;

            character.FighterStats.BaseMaxEnergy = 150;
            character.FighterStats.BaseMaxHealth = 160;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(2);

            character.AddSkillName("EarthShatter");
        }

        internal static void Level3(PCFightingCharacter character)
        {
            Level2(character);
            character.FighterStats.Level = 3;
            character.FighterStats.BaseIntelligence = 56;
            character.FighterStats.BaseWillpower = 48;
            character.FighterStats.BaseAgility = 55;
            character.FighterStats.BaseStrength = 28;
            character.FighterStats.BaseDefense = 28;

            character.FighterStats.BaseMaxEnergy = 225;
            character.FighterStats.BaseMaxHealth = 220;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(3);

            character.AddSkillName("Analyze");
        }

        internal static void Level4(PCFightingCharacter character)
        {
            Level3(character);
            character.FighterStats.Level = 4;
            character.FighterStats.BaseIntelligence = 63;
            character.FighterStats.BaseWillpower = 54;
            character.FighterStats.BaseAgility = 60;
            character.FighterStats.BaseStrength = 32;
            character.FighterStats.BaseDefense = 31;

            character.FighterStats.BaseMaxEnergy = 300;
            character.FighterStats.BaseMaxHealth = 300;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(4);

            character.AddSkillName("Vigor");
        }

        internal static void Level5(PCFightingCharacter character)
        {
            Level4(character);
            character.FighterStats.Level = 5;
            character.FighterStats.BaseIntelligence = 70;
            character.FighterStats.BaseWillpower = 60;
            character.FighterStats.BaseAgility = 65;
            character.FighterStats.BaseStrength = 35;
            character.FighterStats.BaseDefense = 36;

            character.FighterStats.BaseMaxEnergy = 325; //300 MP
            character.FighterStats.BaseMaxHealth = 420; //420 HP

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(5);

            character.AddSkillName("GroupHeal"); 
        }

        internal static void Level6(PCFightingCharacter character)
        {
            Level5(character);
            character.FighterStats.Level = 6;
            character.FighterStats.BaseIntelligence = 80;
            character.FighterStats.BaseWillpower = 74;
            character.FighterStats.BaseAgility = 70;
            character.FighterStats.BaseStrength = 40;
            character.FighterStats.BaseDefense = 39;

            character.FighterStats.BaseMaxEnergy = 400;
            character.FighterStats.BaseMaxHealth = 600; //600 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(6);

            character.AddSkillName("Shield");
        }

        internal static void Level7(PCFightingCharacter character)
        {
            Level6(character);
            character.FighterStats.Level = 7;
            character.FighterStats.BaseIntelligence = 85;
            character.FighterStats.BaseWillpower = 80;
            character.FighterStats.BaseAgility = 73;
            character.FighterStats.BaseStrength = 43;
            character.FighterStats.BaseDefense = 42;

            character.FighterStats.BaseMaxEnergy = 450;
            character.FighterStats.BaseMaxHealth = 800; //800 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(7);

            character.AddSkillName("ManaWard");
        }

        internal static void Level8(PCFightingCharacter character)
        {
            Level7(character);
            character.FighterStats.Level = 8;
            character.FighterStats.BaseIntelligence = 90;
            character.FighterStats.BaseWillpower = 92;
            character.FighterStats.BaseAgility = 80;
            character.FighterStats.BaseStrength = 46;
            character.FighterStats.BaseDefense = 48;

            character.FighterStats.BaseMaxEnergy = 500;
            character.FighterStats.BaseMaxHealth = 1050; //1050 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(8);

            character.AddSkillName("Dispel");
        }

        internal static void Level9(PCFightingCharacter character)
        {
            Level8(character);
            character.FighterStats.Level = 9;
            character.FighterStats.BaseIntelligence = 99;
            character.FighterStats.BaseWillpower = 98;
            character.FighterStats.BaseAgility = 83;
            character.FighterStats.BaseStrength = 51;
            character.FighterStats.BaseDefense = 52;

            character.FighterStats.BaseMaxEnergy = 550;
            character.FighterStats.BaseMaxHealth = 1350; //1350 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(9);
        }

        internal static void Level10(PCFightingCharacter character)
        {
            Level9(character);
            character.FighterStats.Level = 10;
            character.FighterStats.BaseIntelligence = 106;
            character.FighterStats.BaseWillpower = 108;
            character.FighterStats.BaseAgility = 89;
            character.FighterStats.BaseStrength = 54;
            character.FighterStats.BaseDefense = 57;

            character.FighterStats.BaseMaxEnergy = 650;
            character.FighterStats.BaseMaxHealth = 1700; //1700 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(10);

           character.AddSkillName("GreaterHeal"); 
        }

        internal static void Level11(PCFightingCharacter character)
        {
            Level10(character);
            character.FighterStats.Level = 11;
            character.FighterStats.BaseIntelligence = 112;
            character.FighterStats.BaseWillpower = 114;
            character.FighterStats.BaseAgility = 95;
            character.FighterStats.BaseStrength = 57;
            character.FighterStats.BaseDefense = 60;

            character.FighterStats.BaseMaxEnergy = 750;
            character.FighterStats.BaseMaxHealth = 2100; //2100 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(11);

            character.AddSkillName("Regenerate");
        }

        internal static void Level12(PCFightingCharacter character)
        {
            Level11(character);
            character.FighterStats.Level = 12;
            character.FighterStats.BaseIntelligence = 119;
            character.FighterStats.BaseWillpower = 126;
            character.FighterStats.BaseAgility = 103;
            character.FighterStats.BaseStrength = 61;
            character.FighterStats.BaseDefense = 66;

            character.FighterStats.BaseMaxEnergy = 850;
            character.FighterStats.BaseMaxHealth = 2650; //2650 Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(12);

            character.AddSkillName("Ressurection");
        }

        internal static void Level13(PCFightingCharacter character)
        {
            Level12(character);
            character.FighterStats.Level = 13;
            character.FighterStats.BaseIntelligence = 126;
            character.FighterStats.BaseWillpower = 134;
            character.FighterStats.BaseAgility = 110;
            character.FighterStats.BaseStrength = 65;
            character.FighterStats.BaseDefense = 70;

            character.FighterStats.BaseMaxEnergy = 925;
            character.FighterStats.BaseMaxHealth = 3300; //3300 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(13);
        }

        internal static void Level14(PCFightingCharacter character)
        {
            Level13(character);
            character.FighterStats.Level = 14;
            character.FighterStats.BaseIntelligence = 132;
            character.FighterStats.BaseWillpower = 141;
            character.FighterStats.BaseAgility = 115;
            character.FighterStats.BaseStrength = 68;
            character.FighterStats.BaseDefense = 75;

            character.FighterStats.BaseMaxEnergy = 1000;
            character.FighterStats.BaseMaxHealth = 4000; //4000 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(14);

            character.AddSkillName("Aegis"); 
        }

        internal static void Level15(PCFightingCharacter character)
        {
            Level14(character);
            character.FighterStats.Level = 15;
            character.FighterStats.BaseIntelligence = 138;
            character.FighterStats.BaseWillpower = 148;
            character.FighterStats.BaseAgility = 118;
            character.FighterStats.BaseStrength = 71;
            character.FighterStats.BaseDefense = 80;

            character.FighterStats.BaseMaxEnergy = 1100;
            character.FighterStats.BaseMaxHealth = 4600; //4600 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(15);
        }

        internal static void Level16(PCFightingCharacter character)
        {
            Level15(character);
            character.FighterStats.Level = 16;
            character.FighterStats.BaseIntelligence = 143;
            character.FighterStats.BaseWillpower = 160;
            character.FighterStats.BaseAgility = 125;
            character.FighterStats.BaseStrength = 74;
            character.FighterStats.BaseDefense = 86;

            character.FighterStats.BaseMaxEnergy = 1200;
            character.FighterStats.BaseMaxHealth = 5250; //5250 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(16);

            character.AddSkillName("Lightning");
        }

        internal static void Level17(PCFightingCharacter character)
        {
            Level16(character);
            character.FighterStats.Level = 17;
            character.FighterStats.BaseIntelligence = 150;
            character.FighterStats.BaseWillpower = 172;
            character.FighterStats.BaseAgility = 137;
            character.FighterStats.BaseStrength = 78;
            character.FighterStats.BaseDefense = 92;

            character.FighterStats.BaseMaxEnergy = 1400;
            character.FighterStats.BaseMaxHealth = 5650; //5650 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(17);
        }

        internal static void Level18(PCFightingCharacter character)
        {
            Level17(character);
            character.FighterStats.Level = 18;
            character.FighterStats.BaseIntelligence = 160;
            character.FighterStats.BaseWillpower = 184;
            character.FighterStats.BaseAgility = 142;
            character.FighterStats.BaseStrength = 83;
            character.FighterStats.BaseDefense = 98;

            character.FighterStats.BaseMaxEnergy = 1600;
            character.FighterStats.BaseMaxHealth = 6300; //6300 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(18);

            character.AddSkillName("IceStorm");
        }

        internal static void Level19(PCFightingCharacter character)
        {
            Level18(character);
            character.FighterStats.Level = 19;
            character.FighterStats.BaseIntelligence = 167;
            character.FighterStats.BaseWillpower = 196;
            character.FighterStats.BaseAgility = 150;
            character.FighterStats.BaseStrength = 87;
            character.FighterStats.BaseDefense = 104;

            character.FighterStats.BaseMaxEnergy = 1800;
            character.FighterStats.BaseMaxHealth = 7000; //7000 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(19);
        }

        internal static void Level20(PCFightingCharacter character)
        {
            Level19(character);
            character.FighterStats.Level = 20;
            character.FighterStats.BaseIntelligence = 177;
            character.FighterStats.BaseWillpower = 208;
            character.FighterStats.BaseAgility = 160;
            character.FighterStats.BaseStrength = 92;
            character.FighterStats.BaseDefense = 110;

            character.FighterStats.BaseMaxEnergy = 2000; 
            character.FighterStats.BaseMaxHealth = 7800; //7800 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(20);

            character.AddSkillName("Invulnerability");
        }

        internal static void Level21(PCFightingCharacter character)
        {
            Level20(character);
            character.FighterStats.Level = 21;
            character.FighterStats.BaseStrength = 97;
            character.FighterStats.BaseDefense = 116;
            character.FighterStats.BaseAgility = 170;
            character.FighterStats.BaseIntelligence = 189;
            character.FighterStats.BaseWillpower = 220;

            character.FighterStats.BaseMaxEnergy = 2250;
            character.FighterStats.BaseMaxHealth = 8700;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(22);
        }

        internal static void Level22(PCFightingCharacter character)
        {
            Level21(character);
            character.FighterStats.Level = 22;
            character.FighterStats.BaseStrength = 103;
            character.FighterStats.BaseDefense = 123;
            character.FighterStats.BaseAgility = 180;
            character.FighterStats.BaseIntelligence = 202;
            character.FighterStats.BaseWillpower = 233;

            character.FighterStats.BaseMaxEnergy = 2500;
            character.FighterStats.BaseMaxHealth = 9600;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(23);
        }

        internal static void Level23(PCFightingCharacter character)
        {
            Level22(character);
            character.FighterStats.Level = 23;
            character.FighterStats.BaseStrength = 109;
            character.FighterStats.BaseDefense = 130;
            character.FighterStats.BaseAgility = 190;
            character.FighterStats.BaseIntelligence = 215;
            character.FighterStats.BaseWillpower = 246;

            character.FighterStats.BaseMaxEnergy = 2800;
            character.FighterStats.BaseMaxHealth = 10500;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(24);
        }

        internal static void Level24(PCFightingCharacter character)
        {
            Level23(character);
            character.FighterStats.Level = 24;
            character.FighterStats.BaseStrength = 114;
            character.FighterStats.BaseDefense = 138;
            character.FighterStats.BaseAgility = 200;
            character.FighterStats.BaseIntelligence = 227;
            character.FighterStats.BaseWillpower = 260;

            character.FighterStats.BaseMaxEnergy = 3200;
            character.FighterStats.BaseMaxHealth = 11500;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(25);
        }

        internal static void Level25(PCFightingCharacter character)
        {
            Level24(character);
            character.FighterStats.Level = 25;
            character.FighterStats.BaseStrength = 120;
            character.FighterStats.BaseDefense = 148;
            character.FighterStats.BaseAgility = 210;
            character.FighterStats.BaseIntelligence = 240;
            character.FighterStats.BaseWillpower = 275;

            character.FighterStats.BaseMaxEnergy = 3750;
            character.FighterStats.BaseMaxHealth = 12500;

            character.FighterStats.NextLevelXp = -1;
        }
    }
}
