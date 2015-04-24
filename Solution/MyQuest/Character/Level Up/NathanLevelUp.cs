
using System;
namespace MyQuest
{
    internal static class NathanLevelUps
    {
        internal static void Level1(PCFightingCharacter character)
        {
            character.FighterStats.Level = 1;
            character.FighterStats.BaseStrength = 44;
            character.FighterStats.BaseDefense = 37;
            character.FighterStats.BaseAgility = 40;
            character.FighterStats.BaseIntelligence = 22;
            character.FighterStats.BaseWillpower = 22;

            character.FighterStats.BaseMaxEnergy = 50;
            character.FighterStats.BaseMaxHealth = 200;

            //bool hasRift = false;
            //foreach (String skillName in character.SkillNames)
            //{
            //    if (skillName == "Rift")
            //    {
            //        hasRift = true;
            //    }
            //}

            //character.SkillNames.Clear();
            //if (hasRift)
            //{
            //    character.AddSkillName("Rift");
            //}

            character.FighterStats.Experience = 0;
            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(1);
#if DEBUG
            character.AddSkillName("DemoPhase");
            character.AddSkillName("GamePhase");
#endif
        }

        internal static void Level2(PCFightingCharacter character)
        {
            Level1(character); 
            character.FighterStats.Level = 2;
            character.FighterStats.BaseStrength = 51;
            character.FighterStats.BaseDefense = 42;
            character.FighterStats.BaseAgility = 48;
            character.FighterStats.BaseIntelligence = 26;
            character.FighterStats.BaseWillpower = 25;

            character.FighterStats.BaseMaxEnergy = 75;
            character.FighterStats.BaseMaxHealth = 275;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(2);

            character.AddSkillName("NathanPowerStrike");
        }

        internal static void Level3(PCFightingCharacter character)
        {
            Level2(character); 
            character.FighterStats.Level = 3;
            character.FighterStats.BaseStrength = 56;
            character.FighterStats.BaseDefense = 48;
            character.FighterStats.BaseAgility = 55;
            character.FighterStats.BaseIntelligence = 29;
            character.FighterStats.BaseWillpower = 28;

            character.FighterStats.BaseMaxEnergy = 125;
            character.FighterStats.BaseMaxHealth = 400;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(3);
        }

        internal static void Level4(PCFightingCharacter character)
        {
            Level3(character); 
            character.FighterStats.Level = 4;
            character.FighterStats.BaseStrength = 63;
            character.FighterStats.BaseDefense = 54;
            character.FighterStats.BaseAgility = 60;
            character.FighterStats.BaseIntelligence = 33;
            character.FighterStats.BaseWillpower = 32;

            character.FighterStats.BaseMaxEnergy = 175;
            character.FighterStats.BaseMaxHealth = 550;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(4);

            character.AddSkillName("DoubleStrike");
        }

        internal static void Level5(PCFightingCharacter character)
        {
            Level4(character); 
            character.FighterStats.Level = 5;
            character.FighterStats.BaseStrength = 70;
            character.FighterStats.BaseDefense = 60;
            character.FighterStats.BaseAgility = 70;
            character.FighterStats.BaseIntelligence = 36;
            character.FighterStats.BaseWillpower = 37;

            character.FighterStats.BaseMaxEnergy = 225;
            character.FighterStats.BaseMaxHealth = 750;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(5);

        }

        internal static void Level6(PCFightingCharacter character)
        {
            Level5(character); 
            character.FighterStats.Level = 6;
            character.FighterStats.BaseStrength = 77;
            character.FighterStats.BaseDefense = 70;
            character.FighterStats.BaseAgility = 75;
            character.FighterStats.BaseIntelligence = 41;
            character.FighterStats.BaseWillpower = 40;

            character.FighterStats.BaseMaxEnergy = 275;
            character.FighterStats.BaseMaxHealth = 1000; //1000 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(6);

            character.AddSkillName("Focus");
        }

        internal static void Level7(PCFightingCharacter character)
        {
            Level6(character); 
            character.FighterStats.Level = 7;
            character.FighterStats.BaseStrength = 82;
            character.FighterStats.BaseDefense = 76;
            character.FighterStats.BaseAgility = 80;
            character.FighterStats.BaseIntelligence = 44;
            character.FighterStats.BaseWillpower = 43;

            character.FighterStats.BaseMaxEnergy = 325;
            character.FighterStats.BaseMaxHealth = 1300; //1300 HP Total;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(7);

        }

        internal static void Level8(PCFightingCharacter character)
        {
            Level7(character); 
            character.FighterStats.Level = 8;
            character.FighterStats.BaseStrength = 88;
            character.FighterStats.BaseDefense = 88;
            character.FighterStats.BaseAgility = 88;
            character.FighterStats.BaseIntelligence = 48;
            character.FighterStats.BaseWillpower = 49;

            character.FighterStats.BaseMaxEnergy = 375;
            character.FighterStats.BaseMaxHealth = 1650; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(8);

            character.AddSkillName("PoisonStrike");
        }

        internal static void Level9(PCFightingCharacter character)
        {
            Level8(character); 
            character.FighterStats.Level = 9;
            character.FighterStats.BaseStrength = 97;
            character.FighterStats.BaseDefense = 94;
            character.FighterStats.BaseAgility = 94;
            character.FighterStats.BaseIntelligence = 53;
            character.FighterStats.BaseWillpower = 52;

            character.FighterStats.BaseMaxEnergy = 425;
            character.FighterStats.BaseMaxHealth = 2050; //2050 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(9);
        }

        internal static void Level10(PCFightingCharacter character)
        {
            Level9(character); 
            character.FighterStats.Level = 10;
            character.FighterStats.BaseStrength = 102;
            character.FighterStats.BaseDefense = 104;
            character.FighterStats.BaseAgility = 100;
            character.FighterStats.BaseIntelligence = 56;
            character.FighterStats.BaseWillpower = 57;

            character.FighterStats.BaseMaxEnergy = 475;
            character.FighterStats.BaseMaxHealth = 2500;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(10);

            character.AddSkillName("ParalyzingStrike");
        }

        internal static void Level11(PCFightingCharacter character)
        {
            Level10(character); 
            character.FighterStats.Level = 11;
            character.FighterStats.BaseStrength = 108;
            character.FighterStats.BaseDefense = 110;
            character.FighterStats.BaseAgility = 112;
            character.FighterStats.BaseIntelligence = 59;
            character.FighterStats.BaseWillpower = 59;

            character.FighterStats.BaseMaxEnergy = 525;
            character.FighterStats.BaseMaxHealth = 3000;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(11);

        }

        internal static void Level12(PCFightingCharacter character)
        {
            Level11(character); 
            character.FighterStats.Level = 12;
            character.FighterStats.BaseStrength = 115;
            character.FighterStats.BaseDefense = 122;
            character.FighterStats.BaseAgility = 120;
            character.FighterStats.BaseIntelligence = 63;
            character.FighterStats.BaseWillpower = 65;

            character.FighterStats.BaseMaxEnergy = 575;
            character.FighterStats.BaseMaxHealth = 3550;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(12);

            character.AddSkillName("SwordWave");
        }

        internal static void Level13(PCFightingCharacter character)
        {
            Level12(character); 
            character.FighterStats.Level = 13;
            character.FighterStats.BaseStrength = 122;
            character.FighterStats.BaseDefense = 130;
            character.FighterStats.BaseAgility = 127;
            character.FighterStats.BaseIntelligence = 67;
            character.FighterStats.BaseWillpower = 69;

            character.FighterStats.BaseMaxEnergy = 625;
            character.FighterStats.BaseMaxHealth = 4150; //4150 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(13);

        }

        internal static void Level14(PCFightingCharacter character)
        {
            Level13(character); 
            character.FighterStats.Level = 14;
            character.FighterStats.BaseStrength = 128;
            character.FighterStats.BaseDefense = 137;
            character.FighterStats.BaseAgility = 133;
            character.FighterStats.BaseIntelligence = 70;
            character.FighterStats.BaseWillpower = 73;

            character.FighterStats.BaseMaxEnergy = 675;
            character.FighterStats.BaseMaxHealth = 4800;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(14);

            character.AddSkillName("SlashAll");
        }

        internal static void Level15(PCFightingCharacter character)
        {
            Level14(character); 
            character.FighterStats.Level = 15;
            character.FighterStats.BaseStrength = 134;
            character.FighterStats.BaseDefense = 144;
            character.FighterStats.BaseAgility = 136;
            character.FighterStats.BaseIntelligence = 73;
            character.FighterStats.BaseWillpower = 78;

            character.FighterStats.BaseMaxEnergy = 725;
            character.FighterStats.BaseMaxHealth = 5500;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(15);

        }

        internal static void Level16(PCFightingCharacter character)
        {
            Level15(character); 
            character.FighterStats.Level = 16;
            character.FighterStats.BaseStrength = 139;
            character.FighterStats.BaseDefense = 156;
            character.FighterStats.BaseAgility = 148;
            character.FighterStats.BaseIntelligence = 77;
            character.FighterStats.BaseWillpower = 84;

            character.FighterStats.BaseMaxEnergy = 775;
            character.FighterStats.BaseMaxHealth = 6250;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(16);

            character.AddSkillName("BladeStorm");
        }

        internal static void Level17(PCFightingCharacter character)
        {
            Level16(character); 
            character.FighterStats.Level = 17;
            character.FighterStats.BaseStrength = 146;
            character.FighterStats.BaseDefense = 168;
            character.FighterStats.BaseAgility = 160;
            character.FighterStats.BaseIntelligence = 81;
            character.FighterStats.BaseWillpower = 90;

            character.FighterStats.BaseMaxEnergy = 825;
            character.FighterStats.BaseMaxHealth = 7050;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(17);
        }

        internal static void Level18(PCFightingCharacter character)
        {
            Level17(character); 
            character.FighterStats.Level = 18;
            character.FighterStats.BaseStrength = 156;
            character.FighterStats.BaseDefense = 180;
            character.FighterStats.BaseAgility = 172;
            character.FighterStats.BaseIntelligence = 86;
            character.FighterStats.BaseWillpower = 96;

            character.FighterStats.BaseMaxEnergy = 875;
            character.FighterStats.BaseMaxHealth = 7900; //7900 HP Total

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(18);

            character.AddSkillName("HurricaneSlash");
        }

        internal static void Level19(PCFightingCharacter character)
        {
            Level18(character); 
            character.FighterStats.Level = 19;
            character.FighterStats.BaseStrength = 163;
            character.FighterStats.BaseDefense = 192;
            character.FighterStats.BaseAgility = 184;
            character.FighterStats.BaseIntelligence = 90;
            character.FighterStats.BaseWillpower = 102;

            character.FighterStats.BaseMaxEnergy = 925;
            character.FighterStats.BaseMaxHealth = 8800;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(19);
        }

        internal static void Level20(PCFightingCharacter character)
        {
            Level19(character); 
            character.FighterStats.Level = 20;
            character.FighterStats.BaseStrength = 173;
            character.FighterStats.BaseDefense = 204;
            character.FighterStats.BaseAgility = 196;
            character.FighterStats.BaseIntelligence = 100;
            character.FighterStats.BaseWillpower = 108;

            character.FighterStats.BaseMaxEnergy = 1000; 
            character.FighterStats.BaseMaxHealth = 9800; 

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(20);

            character.AddSkillName("LacunaRadiance");
        }

        internal static void Level21(PCFightingCharacter character)
        {
            Level20(character);
            character.FighterStats.Level = 21;
            character.FighterStats.BaseStrength = 180;
            character.FighterStats.BaseDefense = 210;
            character.FighterStats.BaseAgility = 202;
            character.FighterStats.BaseIntelligence = 105;
            character.FighterStats.BaseWillpower = 113;

            character.FighterStats.BaseMaxEnergy = 1200; //1100;
            character.FighterStats.BaseMaxHealth = 11000;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(22);
        }

        internal static void Level22(PCFightingCharacter character)
        {
            Level21(character);
            character.FighterStats.Level = 22;
            character.FighterStats.BaseStrength = 188;
            character.FighterStats.BaseDefense = 217;
            character.FighterStats.BaseAgility = 208;
            character.FighterStats.BaseIntelligence = 112;
            character.FighterStats.BaseWillpower = 118;

            character.FighterStats.BaseMaxEnergy = 1400; //1200;
            character.FighterStats.BaseMaxHealth = 13000;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(23);
        }

        internal static void Level23(PCFightingCharacter character)
        {
            Level22(character);
            character.FighterStats.Level = 23;
            character.FighterStats.BaseStrength = 195;
            character.FighterStats.BaseDefense = 222;
            character.FighterStats.BaseAgility = 215;
            character.FighterStats.BaseIntelligence = 120;
            character.FighterStats.BaseWillpower = 128;

            character.FighterStats.BaseMaxEnergy = 1600; //1300;
            character.FighterStats.BaseMaxHealth = 15000;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(24);
        }

        internal static void Level24(PCFightingCharacter character)
        {
            Level23(character);
            character.FighterStats.Level = 24;
            character.FighterStats.BaseStrength = 202;
            character.FighterStats.BaseDefense = 227;
            character.FighterStats.BaseAgility = 220;
            character.FighterStats.BaseIntelligence = 128;
            character.FighterStats.BaseWillpower = 136;

            character.FighterStats.BaseMaxEnergy = 1800; //1400;
            character.FighterStats.BaseMaxHealth = 17500;

            character.FighterStats.NextLevelXp = CombatCalculations.GetNextLevelExperience(25);
        }

        internal static void Level25(PCFightingCharacter character)
        {
            Level24(character);
            character.FighterStats.Level = 25;
            character.FighterStats.BaseStrength = 212;
            character.FighterStats.BaseDefense = 235;
            character.FighterStats.BaseAgility = 230;
            character.FighterStats.BaseIntelligence = 135;
            character.FighterStats.BaseWillpower = 144;

            character.FighterStats.BaseMaxEnergy = 2000; //1500;
            character.FighterStats.BaseMaxHealth = 20000;

            character.FighterStats.NextLevelXp = -1;
        }
    }
}
