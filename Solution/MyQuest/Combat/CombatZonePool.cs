using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    /// <summary>
    /// Encapsulates every combat zone we have in the game.
    /// </summary>   
    public class CombatZonePool
    {
        #region Background Pool

        public const string castleBG = "AgoraCastleBattleBackgroundCarpet";
        public const string castle2BG = "AgoraCastleBattleBackgroundStone";
        public const string castle3BG = "AgoraCastleBattleBackgroundGrass";
        public const string castle3BGC = "AgoraCastleBattleBackgroundGrassCredits";
        public const string castle4BG = "AgoraCastleBattleBackgroundRoof";
        public const string castle4BGC = "AgoraCastleBattleBackgroundRoofCredits";
        public const string forestBG = "ForestBackground";
        public const string forestBGC = "ForestBackgroundCredits";
        public const string libraryBG = "LibraryBattleBackground";
        public const string swampBG = "SwampBattleBackground";
        public const string caveBG = "CaveBattleBackground";
        public const string caveBGC = "CaveBattleBackgroundCredits";
        public const string snowBG = "SnowBackground";
        public const string wastelandBG = "WastelandBattleBackground";
        public const string wastelandBGC = "WastelandBattleBackgroundCredits";
        public const string valleyBG = "ValleyBattleBackground";
        public const string mountainBG = "MountainBattleBackground";
        public const string lavaBG = "LavaBattleBackground";
        public const string lavaBGC = "LavaBattleBackgroundCredits";
        public const string desertBG = "DesertBattleBackground";
        public const string desertBGC = "DesertBattleBackgroundCredits";
        public const string forest2BG = "ForestBattleBackground2";
        public const string ruinsBG = "RuinsBattleBackground";
        public const string ruinsBGC = "RuinsBattleBackgroundCredits";
        public const string ruinsWallBG = "RuinsWallsBattlelBackground";
        public const string grassBG = "GrassBackground";

        #endregion

        #region Layout Pool

        //Create the order of the slots by draw order, so the slot at the top of the screen should be created first in Slot array.

        internal static readonly CombatZoneLayout oneMediumLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(854, 300))});

        internal static readonly CombatZoneLayout twoMediumLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(754, 225)),
            new Slot(SlotSize.Medium, new Vector2(854, 375))});


        internal static readonly CombatZoneLayout twoMediumLayout2 = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(854, 225)),
            new Slot(SlotSize.Medium, new Vector2(754, 400))});

        internal static readonly CombatZoneLayout threeMediumLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(754, 190)),
            new Slot(SlotSize.Medium, new Vector2(854, 290)), 
            new Slot(SlotSize.Medium, new Vector2(954, 390))});

        internal static readonly CombatZoneLayout threeMediumLayout2 = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(700, 190)),
            new Slot(SlotSize.Medium, new Vector2(854, 290)), 
            new Slot(SlotSize.Medium, new Vector2(754, 390))});

        internal static readonly CombatZoneLayout fourMediumLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(700, 250)),
            new Slot(SlotSize.Medium, new Vector2(900, 225)),
            new Slot(SlotSize.Medium, new Vector2(780, 350)),
            new Slot(SlotSize.Medium, new Vector2(954, 375))});

        internal static readonly CombatZoneLayout fourMediumLayout2 = new CombatZoneLayout(new Slot[] { 
            new Slot(SlotSize.Medium, new Vector2(754, 200)), 
            new Slot(SlotSize.Medium, new Vector2(854, 300)),
            new Slot(SlotSize.Medium, new Vector2(1000, 275)),
            new Slot(SlotSize.Medium, new Vector2(954, 400))});

        internal static readonly CombatZoneLayout fourMediumLayoutGrass = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(754, 200)),
            new Slot(SlotSize.Medium, new Vector2(1000, 235)), 
            new Slot(SlotSize.Medium, new Vector2(854, 300)),
            new Slot(SlotSize.Medium, new Vector2(954, 400))});

        internal static readonly CombatZoneLayout fiveMediumLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(900, 175)), 
            new Slot(SlotSize.Medium, new Vector2(780, 270)),
            new Slot(SlotSize.Medium, new Vector2(975, 325)),
            new Slot(SlotSize.Medium, new Vector2(800, 390)),
            new Slot(SlotSize.Medium, new Vector2(950, 415))});

        internal static readonly CombatZoneLayout fiveMediumLayoutGrass = new CombatZoneLayout(new Slot[] { 
            new Slot(SlotSize.Medium, new Vector2(780, 270)),
            new Slot(SlotSize.Medium, new Vector2(1000, 270)),
            new Slot(SlotSize.Medium, new Vector2(890, 350)),
            new Slot(SlotSize.Medium, new Vector2(780, 415)),
            new Slot(SlotSize.Medium, new Vector2(1000, 415))});

        internal static readonly CombatZoneLayout fiveMediumLayoutSwamp = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(900, 155)), 
            new Slot(SlotSize.Medium, new Vector2(780, 250)),
            new Slot(SlotSize.Medium, new Vector2(925, 270)),
            new Slot(SlotSize.Medium, new Vector2(800, 370)),
            new Slot(SlotSize.Medium, new Vector2(950, 395))});

        internal static readonly CombatZoneLayout oneLargeLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Large, new Vector2(850, 275))});

        internal static readonly CombatZoneLayout twoLargeLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Large, new Vector2(654, 150)), 
            new Slot(SlotSize.Large, new Vector2(854, 320))});

        internal static readonly CombatZoneLayout twoMediumLargeLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Large, new Vector2(804, 210)),
            new Slot(SlotSize.Medium, new Vector2(854, 330))});
        
        internal static readonly CombatZoneLayout threeMediumLargeLayout1 = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(654, 150)),
            new Slot(SlotSize.Large, new Vector2(854, 320)),
            new Slot(SlotSize.Medium, new Vector2 (654, 400))});

        internal static readonly CombatZoneLayout oneHugeLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Huge, new Vector2(804, 300))});

        #endregion

        #region Layout Collection Pool

        internal static CombatZoneLayout[] oneMediumLayoutCollection = new CombatZoneLayout[] { oneMediumLayout };
        internal static CombatZoneLayout[] twoMediumLayoutCollection = new CombatZoneLayout[] { twoMediumLayout, twoMediumLayout2 };
        internal static CombatZoneLayout[] threeMediumLayoutCollection = new CombatZoneLayout[] { threeMediumLayout, threeMediumLayout2 };
        internal static CombatZoneLayout[] fourMediumLayoutCollection = new CombatZoneLayout[] { fourMediumLayout, fourMediumLayout2 };
        internal static CombatZoneLayout[] fourMediumLayoutCollection2 = new CombatZoneLayout[] { fourMediumLayoutGrass };
        internal static CombatZoneLayout[] fiveMediumLayoutCollection = new CombatZoneLayout[] { fiveMediumLayout };
        internal static CombatZoneLayout[] fiveMediumLayoutCollection2 = new CombatZoneLayout[] { fiveMediumLayoutGrass };
        internal static CombatZoneLayout[] fiveMediumLayoutCollection3 = new CombatZoneLayout[] { fiveMediumLayoutSwamp };
        internal static CombatZoneLayout[] oneLargeLayoutCollection = new CombatZoneLayout[] { oneLargeLayout };
        internal static CombatZoneLayout[] twoLargeLayoutCollection = new CombatZoneLayout[] { twoLargeLayout };
        internal static CombatZoneLayout[] twoMediumLargeCollection = new CombatZoneLayout[] { twoMediumLargeLayout };
        internal static CombatZoneLayout[] threeMediumLargeCollection = new CombatZoneLayout[] { threeMediumLargeLayout1 };
        internal static CombatZoneLayout[] oneHugeCollection = new CombatZoneLayout[] { oneHugeLayout };

        #endregion

        //Combat Zones
        #region Empty Zone

        internal static readonly CombatZone emptyZone = new CombatZone("Empty", 0f, null, null, new CombatZoneLayout[] { }, new Monster[] { });

        #endregion

        #region Keep Zones

        internal static Monster[] keepDemonsCollection = new Monster[] { new Monster(Monster.keepImp, 1, 1), new Monster(Monster.keepDemon, 1, 1) };
        internal static Monster[] keepDemon1Collection = new Monster[] { new Monster(Monster.keepDemon, 1) };
        internal static Monster[] keepImpCollection = new Monster[] { new Monster(Monster.keepImp, 1) };

        internal static readonly CombatZone keepZoneDemons = new CombatZone("KeepZone1", 0.03f, castleBG, AudioCues.battleCue, false, twoMediumLayoutCollection, keepDemonsCollection);
        internal static readonly CombatZone keepZoneDoubleDemon1 = new CombatZone("KeepZone1", 0.03f, castleBG, AudioCues.battleCue, false, twoMediumLayoutCollection, keepDemon1Collection);
        internal static readonly CombatZone keepZoneSingleDemon1 = new CombatZone("KeepZone1", 0.03f, castleBG, AudioCues.battleCue, false, oneMediumLayoutCollection, keepDemon1Collection);
        internal static readonly CombatZone keepZoneSingleImp = new CombatZone("KeepZone1", 0.03f, castleBG, AudioCues.battleCue, false, oneMediumLayoutCollection, keepImpCollection);
            
        #endregion      

        #region Mushroom Forest Zone

        internal static Monster[] feesh2Collection = new Monster[] { new Monster(Monster.feesh, 1, 1), new Monster(Monster.feesh, 1, 1) };
        internal static Monster[] feesh1Flyder1Collection = new Monster[] { new Monster(Monster.feesh, 1, 1), new Monster(Monster.flyder, 1, 1) };
        internal static Monster[] flyder3Collection = new Monster[] { new Monster(Monster.flyder, 1, 1), new Monster(Monster.flyder, 1, 1), new Monster(Monster.flyder, 1, 1) };

        internal static readonly CombatZone feesh2Zone = new CombatZone("ForestZone1", 0.032f, forestBG, AudioCues.battleCue, twoMediumLayoutCollection, feesh2Collection);
        internal static readonly CombatZone flyder3Zone = new CombatZone("ForestZone2", 0.025f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, flyder3Collection);
        internal static readonly CombatZone feesh1Flyder1Zone = new CombatZone("ForestZone3", 0.03f, forestBG, AudioCues.battleCue, twoMediumLayoutCollection, feesh1Flyder1Collection);

        #endregion

        #region Healer's Village Bandit Fight

        internal static Monster[] bandit1Collection = new Monster[] { new Monster(Monster.bandit, 1) };

        internal static readonly CombatZone forestBanditZone = new CombatZone("ForestBanditZone", .01f, forestBG, AudioCues.minibossCue, false, threeMediumLayoutCollection, bandit1Collection);

        #endregion

        #region Mushroom Forest Scarf Bandit Fight(Quest)

        internal static Monster[] scarfBanditCollection = new Monster[] { new Monster(Monster.bandit2, 1) };

        internal static readonly CombatZone scarfBanditZone = new CombatZone("ForestBanditZone", .01f, forestBG, AudioCues.minibossCue, false, oneMediumLayoutCollection, scarfBanditCollection);

        #endregion

        #region Mushroom Forest Bandit King Fight(Quest)

        internal static Monster[] banditKingCollection = new Monster[] { new Monster(Monster.bandit2, 1, SlotSize.Medium, 2), new Monster(Monster.banditKing, 1, SlotSize.Medium, 1), new Monster(Monster.bandit2, 1, SlotSize.Medium, 2) };

        internal static readonly CombatZone banditKingZone = new CombatZone("ForestBanditZone", .01f, forestBG, AudioCues.minibossCue, false, fiveMediumLayoutCollection, banditKingCollection);

        #endregion

        #region Path to Blind Man's Town (Overworld map)

        // We will try to reuse the combat zones from mushroom forest.
        // But we need to add bandit-oriented combat zones.
        internal static readonly CombatZone forestOverworldBanditZone = new CombatZone("ForestOverworldBanditZone", .02f, forestBG, AudioCues.battleCue, fourMediumLayoutCollection, bandit1Collection);
        internal static readonly CombatZone forestOverworldfeeshZone = new CombatZone("ForestOverworldFeeshZone", 0.032f, forestBG, AudioCues.battleCue, twoMediumLayoutCollection, feesh2Collection);
        internal static readonly CombatZone forestOverworldflyderZone = new CombatZone("ForestOverworldFlyderZone", 0.025f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, flyder3Collection);
        internal static readonly CombatZone forestOverworldfeeshFlyderZone = new CombatZone("ForestOverworldFeeshFlyderZone", 0.03f, forestBG, AudioCues.battleCue, twoMediumLayoutCollection, feesh1Flyder1Collection);
        internal static readonly CombatZone ruinsOverworldBanditZone = new CombatZone("RuinsOverworldBanditZone", .02f, ruinsBG, AudioCues.battleCue, fourMediumLayoutCollection, bandit1Collection);
        internal static readonly CombatZone grassOverworldBanditZone = new CombatZone("GrassOverworldBanditZone", .02f, grassBG, AudioCues.battleCue, fourMediumLayoutCollection2, bandit1Collection);
        internal static readonly CombatZone grassOverworldfeeshZone = new CombatZone("GrassOverworldFeeshZone", 0.032f, grassBG, AudioCues.battleCue, twoMediumLayoutCollection, feesh2Collection);
        internal static readonly CombatZone grassOverworldflyderZone = new CombatZone("GrassOverworldFlyderZone", 0.025f, grassBG, AudioCues.battleCue, threeMediumLayoutCollection, flyder3Collection);
        internal static readonly CombatZone grassOverworldfeeshFlyderZone = new CombatZone("GrassOverworldFeeshFlyderZone", 0.03f, grassBG, AudioCues.battleCue, twoMediumLayoutCollection, feesh1Flyder1Collection);

        #endregion

        #region Random Dungeon 2 (mimic chest)

        internal static Monster[] feesh5Collection = new Monster[] { new Monster(Monster.feesh, 1), new Monster(Monster.feesh, 1, 1), new Monster(Monster.feesh, 1, 1), new Monster(Monster.feesh, 1, 1), new Monster(Monster.feesh, 1, 1) };

        internal static readonly CombatZone feesh5Zone = new CombatZone("Dungeon2Zone01", .01f, caveBG, AudioCues.battleCue, false, fiveMediumLayoutCollection, feesh5Collection);

        #endregion

        #region Blind Man's Cara(Quest)

        internal static Monster[] caraBandit1Collection = new Monster[] { new Monster(Monster.carasBandit, 1) };

        internal static readonly CombatZone forestCaraBanditZone = new CombatZone("ForestBanditZone", .01f, forestBG, AudioCues.minibossCue, false, oneMediumLayoutCollection, caraBandit1Collection);

        #endregion

        #region Blind Man's Forest Zone

        internal static Monster[] hauntedTree1Feesh2Collection = new Monster[] { new Monster(Monster.hauntedTree, 1, 1), new Monster(Monster.feesh, 1, 1), new Monster(Monster.hauntedTree, 1, 1) };
        internal static Monster[] hauntedTree1Feesh1Bandit1Collection = new Monster[] { new Monster(Monster.hauntedTree, 1, 1), new Monster(Monster.feesh, 1, 1), new Monster(Monster.bandit, 1, 1) };
        internal static Monster[] bandit3Collection = new Monster[] { new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.feesh, 1, 1) };
        internal static Monster[] hauntedTree1Bandit1Collection = new Monster[] { new Monster(Monster.hauntedTree, 1, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1) };
        internal static Monster[] feesh4Collection = new Monster[] { new Monster(Monster.hauntedTree, 1, 1), new Monster(Monster.hauntedTree, 1, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1) };

        internal static readonly CombatZone hauntedTree1Feesh2Zone = new CombatZone("ForestZone4", 0.032f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, hauntedTree1Feesh2Collection);
        internal static readonly CombatZone hauntedTree1Feesh1Bandit1Zone = new CombatZone("ForestZone5", 0.025f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, hauntedTree1Feesh1Bandit1Collection);
        internal static readonly CombatZone bandit3Zone = new CombatZone("ForestZone6", 0.03f, forestBG, AudioCues.battleCue, fourMediumLayoutCollection, bandit3Collection);
        internal static readonly CombatZone hauntedTree1Bandit1Zone = new CombatZone("ForestZone7", 0.025f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, hauntedTree1Bandit1Collection);
        internal static readonly CombatZone feesh4Zone = new CombatZone("ForestZone8", 0.03f, forestBG, AudioCues.battleCue, fourMediumLayoutCollection, feesh4Collection);

        #endregion

        #region Burtle Fight(Cut Scene)

        internal static Monster[] bossBurtleCollection = new Monster[] { new Monster(Monster.burtle, 1, SlotSize.Large) };

        internal static readonly CombatZone forestBurtleZone = new CombatZone("ForestBurtleZone1", 0.03f, caveBG, AudioCues.minibossCue, false, oneLargeLayoutCollection, bossBurtleCollection);

        #endregion

        #region Path to Mage Town (Overworld map)

        internal static Monster[] desertBandit3Collection = new Monster[] { new Monster(Monster.desertBandit, 1, 3)};

        // This will be a mixture of enemies from blind man's forest and desert bandit.
        internal static readonly CombatZone overworldMageTownForestHauntedTreeFeeshZone = new CombatZone("OverworldMageTownZone01", 0.032f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, hauntedTree1Feesh2Collection);
        internal static readonly CombatZone overworldMageTownForestHauntedTreeFeeshBanditZone = new CombatZone("OverworldMageTownZone03", 0.025f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, hauntedTree1Feesh1Bandit1Collection);
        internal static readonly CombatZone overworldMageTownForestHauntedTreeBanditZone = new CombatZone("OverworldMageTownZone04", 0.025f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, hauntedTree1Bandit1Collection);
        internal static readonly CombatZone overworldMageTownGrassBanditZone = new CombatZone("OverworldMageTownZone05", 0.03f, grassBG, AudioCues.battleCue, fourMediumLayoutCollection, bandit3Collection);
        internal static readonly CombatZone overworldMageTownGrassFeeshZone = new CombatZone("OverworldMageTownZone06", 0.03f, grassBG, AudioCues.battleCue, fourMediumLayoutCollection, feesh4Collection);
        internal static readonly CombatZone overworldMageTownDesertBanditZone = new CombatZone("OverworldMageTownZone07", 0.02f, desertBG, AudioCues.battleCue, threeMediumLayoutCollection, desertBandit3Collection);
        internal static readonly CombatZone overworldMageTownRuinsBanditZone = new CombatZone("OverworldMageTownZone08", 0.02f, ruinsBG, AudioCues.battleCue, threeMediumLayoutCollection, desertBandit3Collection);

        #endregion

        #region Random Dungeon 3 (Cut Scenes)

        internal static Monster[] bandit5Collection = new Monster[] { new Monster(Monster.bandit, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1) };
        internal static Monster[] hauntedTree3Bandit2Collection = new Monster[] { new Monster(Monster.hauntedTree, 1), new Monster(Monster.hauntedTree, 1, 1), new Monster(Monster.hauntedTree, 1, 1), new Monster(Monster.desertBandit, 1, 1), new Monster(Monster.desertBandit, 1, 1) };
        internal static Monster[] caraBandit4Collection = new Monster[] { new Monster(Monster.desertBandit, 1), new Monster(Monster.desertBandit, 1), new Monster(Monster.desertBandit, 1), new Monster(Monster.desertBandit, 1) };
        internal static Monster[] caraBandit4Ghost1Collection = new Monster[] { new Monster(Monster.desertBandit, 1), new Monster(Monster.desertBandit, 1, 1), new Monster(Monster.ghost, 1, 1), new Monster(Monster.desertBandit, 1, 1), new Monster(Monster.desertBandit, 1, 1) };
        internal static Monster[] caraBandit3Ghost2Collection = new Monster[] { new Monster(Monster.desertBandit, 1), new Monster(Monster.desertBandit, 1), new Monster(Monster.desertBandit, 1), new Monster(Monster.ghost, 1), new Monster(Monster.ghost, 1) };

        internal static readonly CombatZone dungeon3CaveBanditZone = new CombatZone("Dungeon3Zone01", 0.02f, caveBG, AudioCues.battleCue, false, fiveMediumLayoutCollection, bandit5Collection);
        internal static readonly CombatZone dungeon3CaveHauntedTreeBanditZone = new CombatZone("Dungeon3Zone02", 0.02f, caveBG, AudioCues.battleCue, false, fiveMediumLayoutCollection, hauntedTree3Bandit2Collection);
        internal static readonly CombatZone dungeon3CaveCarasBanditZone = new CombatZone("Dungeon3Zone03", 0.02f, caveBG, AudioCues.battleCue, false, fourMediumLayoutCollection, caraBandit4Collection);
        internal static readonly CombatZone dungeon3CaveBanditGhostZone = new CombatZone("Dungeon3Zone04", 0.02f, caveBG, AudioCues.battleCue, false, fiveMediumLayoutCollection, caraBandit4Ghost1Collection);
        internal static readonly CombatZone dungeon3CaveCarasBanditGhostZone = new CombatZone("Dungeon3Zone05", 0.02f, caveBG, AudioCues.battleCue, false, fiveMediumLayoutCollection, caraBandit3Ghost2Collection);

        #endregion

        #region Mage Town Will(Quest)

        internal static Monster[] negaWillCollection = new Monster[] { new Monster(Monster.negaWill, 1) };

        internal static readonly CombatZone negaWillZone = new CombatZone("NegaWillZone", .01f, libraryBG, AudioCues.minibossCue, false, oneMediumLayoutCollection, negaWillCollection);

        #endregion

        #region Possessed Library Zones

        internal static Monster[] hauntedBook2hauntedChair1Tohmey1Collection = new Monster[] { new Monster(Monster.tohmey, 1, 1), new Monster(Monster.ghost, 1, 1), new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.poisonousHauntedBook, 1, 1) };
        internal static Monster[] hauntedChair1Ghost1Collection = new Monster[] { new Monster(Monster.ghost, 1, 1), new Monster(Monster.tohmey, 1, 1), new Monster(Monster.poisonousHauntedBook, 1, 1) };
        internal static Monster[] ghost2Tohmey1Collection = new Monster[] { new Monster(Monster.ghost, 1, 1), new Monster(Monster.ghost, 1, 1), new Monster(Monster.tohmey, 1, 1) };
        internal static Monster[] tohmey1HauntedChair1Collection = new Monster[] { new Monster(Monster.tohmey, 1, 1), new Monster(Monster.poisonousHauntedBook, 1, 1), new Monster(Monster.hauntedBook, 1, 1) };
        internal static Monster[] ghost1HauntedChair1HauntedBook1Collection = new Monster[] { new Monster(Monster.ghost, 1, 1), new Monster(Monster.ghost, 1, 1), new Monster(Monster.ghost, 1, 1), new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.poisonousHauntedBook, 1, 1) };
        internal static Monster[] apprentice1HauntedBook1Collection = new Monster[] { new Monster(Monster.apprentice, 1, 1), new Monster(Monster.tohmey, 1, 1), new Monster(Monster.apprentice, 1, 1), new Monster(Monster.hauntedBook, 1, 1) };
        internal static Monster[] ghost2Apprentice1Collection = new Monster[] { new Monster(Monster.apprentice, 1, 1), new Monster(Monster.ghost, 1, 1), new Monster(Monster.ghost, 1, 1) };
        internal static Monster[] apprentice1Tohmey1HauntedChair1Collection = new Monster[] { new Monster(Monster.apprentice, 1, 1), new Monster(Monster.tohmey, 1, 1), new Monster(Monster.poisonousHauntedBook, 1, 1), new Monster(Monster.ghost, 1, 1) };
        internal static Monster[] hauntedBook5Collection = new Monster[] { new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.hauntedBook, 1, 1) };
        internal static Monster[] apprentice2Ghost1Collection = new Monster[] { new Monster(Monster.apprentice, 1, 1), new Monster(Monster.apprentice, 1, 1), new Monster(Monster.ghost, 1, 1), new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.hauntedBook, 1, 1) };

        internal static readonly CombatZone hauntedBook2hauntedChair1Zone = new CombatZone("Library01", 0.03f, libraryBG, AudioCues.battleCue, fiveMediumLayoutCollection, hauntedBook2hauntedChair1Tohmey1Collection);
        internal static readonly CombatZone hauntedChair1Ghost1Zone = new CombatZone("Library02", 0.03f, libraryBG, AudioCues.battleCue, threeMediumLayoutCollection, hauntedChair1Ghost1Collection);
        internal static readonly CombatZone ghost2Zone = new CombatZone("Library03", 0.03f, libraryBG, AudioCues.battleCue, threeMediumLayoutCollection, ghost2Tohmey1Collection);
        internal static readonly CombatZone tohmey1HauntedChair1Zone = new CombatZone("Library04", 0.03f, libraryBG, AudioCues.battleCue, fourMediumLayoutCollection, tohmey1HauntedChair1Collection);
        internal static readonly CombatZone ghost1HauntedChair1HauntedBook1Zone = new CombatZone("Library05", 0.03f, libraryBG, AudioCues.battleCue, fiveMediumLayoutCollection, ghost1HauntedChair1HauntedBook1Collection);
        internal static readonly CombatZone apprentice1HauntedBook1Zone = new CombatZone("Library06", 0.03f, libraryBG, AudioCues.battleCue, fourMediumLayoutCollection, apprentice1HauntedBook1Collection);
        internal static readonly CombatZone ghost2Apprentice1Zone = new CombatZone("Library07", 0.03f, libraryBG, AudioCues.battleCue, threeMediumLayoutCollection, ghost2Apprentice1Collection);
        internal static readonly CombatZone apprentice1Tohmey1HauntedChair1Zone = new CombatZone("Library08", 0.03f, libraryBG, AudioCues.battleCue, fourMediumLayoutCollection, apprentice1Tohmey1HauntedChair1Collection);
        internal static readonly CombatZone hauntedBook5Zone = new CombatZone("Library09", 0.03f, libraryBG, AudioCues.battleCue, fiveMediumLayoutCollection, hauntedBook5Collection);
        internal static readonly CombatZone apprentice2Ghost1Zone = new CombatZone("Library10", 0.03f, libraryBG, AudioCues.battleCue, fiveMediumLayoutCollection, apprentice2Ghost1Collection);

        #endregion

        #region Elder Mantis Fight (Cut Scene)

        internal static Monster[] elderMantisCollection = new Monster[] { new Monster(Monster.apprentice, 1, SlotSize.Medium, 1), new Monster(Monster.elderMantis, 1, SlotSize.Medium, 1), new Monster(Monster.apprentice, 1, SlotSize.Medium, 1) };

        internal static readonly CombatZone elderMantisZone = new CombatZone("ElderMantisZone", 0.020f, libraryBG, AudioCues.minibossCue, false, threeMediumLayoutCollection, elderMantisCollection);

        #endregion   

        #region Path to Swamp Town (Overworld map)

        internal static Monster[] vooDooDoll1HauntedTreeTwo1Collection = new Monster[] { new Monster(Monster.SewnBogHauntedTree, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
        internal static Monster[] witchDoctor1HauntedTreeTwo1Collection = new Monster[] { new Monster(Monster.SewnBogHauntedTree, 1, 1), new Monster(Monster.witchDoctor, 1, 1) };
        internal static Monster[] witchDoctor2VoodooDoll1Collection = new Monster[] { new Monster(Monster.witchDoctor, 1, 1), new Monster(Monster.witchDoctor, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
        internal static Monster[] hauntedTreeTwo2VoodooDoll1Collection = new Monster[] { new Monster(Monster.SewnBogHauntedTree, 1, 1), new Monster(Monster.SewnBogHauntedTree, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
        internal static Monster[] voodooDoll3Collection = new Monster[] { new Monster(Monster.voodooDoll, 1, 1), new Monster(Monster.voodooDoll, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
        internal static Monster[] voodooDoll5Collection = new Monster[] { new Monster(Monster.voodooDoll, 1, 1), new Monster(Monster.voodooDoll, 1, 1), new Monster(Monster.voodooDoll, 1, 1), new Monster(Monster.voodooDoll, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
        internal static Monster[] witchDoctor1HauntedTreeTwo1VoodooDoll1Collection = new Monster[] { new Monster(Monster.witchDoctor, 1, 1), new Monster(Monster.SewnBogHauntedTree, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
        internal static Monster[] boggimusTadpole3WitchDoctor1Collection = new Monster[] { new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.witchDoctor, 1, 1) };
        internal static Monster[] boggimusTadpole2HauntedTreeTwo1VoodooDoll1Collection = new Monster[] { new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.SewnBogHauntedTree, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
        internal static Monster[] boggimusTadpole1WitchDoctor1HauntedTreeTwo1VoodooDoll2Collection = new Monster[] { new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.witchDoctor, 1, 1), new Monster(Monster.SewnBogHauntedTree, 1, 1), new Monster(Monster.voodooDoll, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
        internal static Monster[] boggimusTadpole5Collection = new Monster[] { new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.boggimusTadpole, 1, 1) };

        // This will be a mixture of enemies from possessed library and swamp town and sewn bog.
        internal static readonly CombatZone overworldSwampGhostZone = new CombatZone("OverworldSwampTownZone01", 0.03f, swampBG, AudioCues.battleCue, threeMediumLayoutCollection, ghost2Tohmey1Collection);
        internal static readonly CombatZone overworldSwampApprenticeGhostZone = new CombatZone("OverworldSwampTownZone02", 0.03f, swampBG, AudioCues.battleCue, fiveMediumLayoutCollection, apprentice2Ghost1Collection);
        internal static readonly CombatZone overworldSwampWitchDoctorHauntedTreeZone = new CombatZone("OverworldSwampTownZone03", 0.03f, swampBG, AudioCues.battleCue, twoMediumLayoutCollection, witchDoctor1HauntedTreeTwo1Collection); // x
        internal static readonly CombatZone overworldSwampWitchDoctorVoodooDollZone = new CombatZone("OverworldSwampTownZone04", 0.03f, swampBG, AudioCues.battleCue, threeMediumLayoutCollection, witchDoctor2VoodooDoll1Collection);
        internal static readonly CombatZone overworldSwampWitchDoctorHauntedTreeVoodooDollZone = new CombatZone("OverworldSwampTownZone05", 0.03f, swampBG, AudioCues.battleCue, threeMediumLayoutCollection, witchDoctor1HauntedTreeTwo1VoodooDoll1Collection);
        internal static readonly CombatZone overworldSwampBoggimusTadpoleZone = new CombatZone("OverworldSwampTownZone06", 0.03f, swampBG, AudioCues.battleCue, fiveMediumLayoutCollection3, boggimusTadpole5Collection);
        internal static readonly CombatZone overworldSwampRuinsBoggimusTadpoleZone = new CombatZone("OverworldSwampTownZone07", 0.03f, ruinsBG, AudioCues.battleCue, fiveMediumLayoutCollection, boggimusTadpole5Collection);

        internal static readonly CombatZone overworldSwampSnowForestWitchDoctorHauntedTreeZone = new CombatZone("OverworldSwampTownZone08", 0.03f, forestBG, AudioCues.battleCue, twoMediumLayoutCollection, witchDoctor1HauntedTreeTwo1Collection);
        internal static readonly CombatZone overworldSwampSnowForestWitchDoctorVoodooDollZone = new CombatZone("OverworldSwampTownZone09", 0.03f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, witchDoctor2VoodooDoll1Collection);
        internal static readonly CombatZone overworldSwampSnowForestWitchDoctorHauntedTreeVoodooDollZone = new CombatZone("OverworldSwampTownZone10", 0.03f, forestBG, AudioCues.battleCue, threeMediumLayoutCollection, witchDoctor1HauntedTreeTwo1VoodooDoll1Collection);

        internal static readonly CombatZone overworldSwampSnowGrassWitchDoctorVoodooDollZone = new CombatZone("OverworldSwampTownZone11", 0.03f, grassBG, AudioCues.battleCue, threeMediumLayoutCollection, witchDoctor2VoodooDoll1Collection); // x
        internal static readonly CombatZone overworldSwampSnowGrassWitchDoctorHauntedTreeVoodooDollZone = new CombatZone("OverworldSwampTownZone12", 0.03f, grassBG, AudioCues.battleCue, threeMediumLayoutCollection, witchDoctor1HauntedTreeTwo1VoodooDoll1Collection);
        internal static readonly CombatZone overworldSwampSnowGrassWitchDoctorHauntedTreeZone = new CombatZone("OverworldSwampTownZone08", 0.03f, grassBG, AudioCues.battleCue, twoMediumLayoutCollection, witchDoctor1HauntedTreeTwo1Collection);
        internal static readonly CombatZone overworldSwampSnowGrassvoodooDoll5Zone = new CombatZone("OverworldSwampTownZone09", 0.03f, grassBG, AudioCues.battleCue, fiveMediumLayoutCollection2, voodooDoll5Collection);
        
        #endregion

        #region Swamp Town (Cut scene triggered zones only)

        internal static Monster[] swampMixCollection = new Monster[] { new Monster(Monster.voodooDoll, 1), new Monster(Monster.witchDoctor, 1) };

        internal static readonly CombatZone swampZone3 = new CombatZone("SwampZone13", 0.03f, swampBG, AudioCues.battleCue, twoMediumLayoutCollection, swampMixCollection);

        #endregion

        #region Sewn Bog Zones (Swamp Town)

        internal static readonly CombatZone vooDooDoll1HauntedTree1Zone = new CombatZone("SwampZone1", 0.03f, swampBG, AudioCues.battleCue, twoMediumLayoutCollection, vooDooDoll1HauntedTreeTwo1Collection);
        internal static readonly CombatZone witchDoctor1HauntedTree1Zone = new CombatZone("SwampZone2", 0.03f, swampBG, AudioCues.battleCue, twoMediumLayoutCollection, witchDoctor1HauntedTreeTwo1Collection);
        internal static readonly CombatZone witchDoctor2VoodooDoll1Zone = new CombatZone("SwampZone3", 0.03f, swampBG, AudioCues.battleCue, threeMediumLayoutCollection, witchDoctor2VoodooDoll1Collection);
        internal static readonly CombatZone hauntedTree2VoodooDoll1Zone = new CombatZone("SwampZone4", 0.03f, swampBG, AudioCues.battleCue, threeMediumLayoutCollection, hauntedTreeTwo2VoodooDoll1Collection);
        internal static readonly CombatZone voodooDoll3Zone = new CombatZone("SwampZone5", 0.03f, swampBG, AudioCues.battleCue, threeMediumLayoutCollection, voodooDoll3Collection);
        internal static readonly CombatZone witchDoctor1HauntedTree1VoodooDoll1Zone = new CombatZone("SwampZone6", 0.03f, swampBG, AudioCues.battleCue, threeMediumLayoutCollection, witchDoctor1HauntedTreeTwo1VoodooDoll1Collection);
        internal static readonly CombatZone boggimusTadpole3WitchDoctor1Zone = new CombatZone("SwampZone7", 0.03f, swampBG, AudioCues.battleCue, fourMediumLayoutCollection, boggimusTadpole3WitchDoctor1Collection);
        internal static readonly CombatZone boggimusTadpole2HauntedTree1VoodooDoll1Zone = new CombatZone("SwampZone8", 0.03f, swampBG, AudioCues.battleCue, fourMediumLayoutCollection, boggimusTadpole2HauntedTreeTwo1VoodooDoll1Collection);
        internal static readonly CombatZone boggimusTadpole1WitchDoctor1HauntedTree1VoodooDoll2Zone = new CombatZone("SwampZone9", 0.03f, swampBG, AudioCues.battleCue, fiveMediumLayoutCollection3, boggimusTadpole1WitchDoctor1HauntedTreeTwo1VoodooDoll2Collection);
        internal static readonly CombatZone boggimusTadpole5Zone = new CombatZone("SwampZone10", 0.03f, swampBG, AudioCues.battleCue, fiveMediumLayoutCollection3, boggimusTadpole5Collection);
        internal static readonly CombatZone voodooDoll5Zone = new CombatZone("SwampZone11", 0.03f, swampBG, AudioCues.battleCue, fiveMediumLayoutCollection3, voodooDoll5Collection);

        #endregion

        #region Chepetawa Fight (Cut Scene)

        internal static Monster[] chepetawaCollection = new Monster[] { new Monster(Monster.chepetawa, 1, SlotSize.Medium, 1), new Monster(Monster.boggimus, 1, SlotSize.Large, 1) };

        internal static readonly CombatZone chepetawaZone = new CombatZone("ChepetawaZone", 0.020f, swampBG, AudioCues.chepetawaBoss, false, twoMediumLargeCollection, chepetawaCollection);

        #endregion
     
        #region Path to Snow Town(Overworld map)

        internal static Monster[] iceFeesh1FrostFlyder2Collection = new Monster[] { new Monster(Monster.iceFeesh, 1, 1), new Monster(Monster.frostFlyder, 1, 1), new Monster(Monster.frostFlyder, 1, 1) };
        internal static Monster[] snowBandit1IceFeesh2AbominableSnowManCollection = new Monster[] { new Monster(Monster.snowBandit, 1, 1), new Monster(Monster.iceFeesh, 1, 1), new Monster(Monster.iceFeesh, 1, 1), new Monster(Monster.abominableSnowMan, 1, 1) };
        internal static Monster[] snowBandit1IceFeesh1IceFrostflyder3Collection = new Monster[] { new Monster(Monster.snowBandit, 1, 1), new Monster(Monster.iceFeesh, 1, 1), new Monster(Monster.frostFlyder, 1, 1), new Monster(Monster.frostFlyder, 1, 1), new Monster(Monster.frostFlyder, 1, 1) };
        internal static Monster[] polarBurtle1SnowBandit1IceFeesh1Collection = new Monster[] { new Monster(Monster.polarBurtle, 1, 1), new Monster(Monster.snowBandit, 1, 1), new Monster(Monster.iceFeesh, 1, 1) };
        internal static Monster[] abominableSnowMan1IceFeesh2Collection = new Monster[] { new Monster(Monster.abominableSnowMan, 1, 1), new Monster(Monster.iceFeesh, 1, 1), new Monster(Monster.iceFeesh, 1, 1) };
        internal static Monster[] iceFeesh2FrostFlyder1PolarBurtle1Collection = new Monster[] { new Monster(Monster.polarBurtle, 1, 1), new Monster(Monster.iceFeesh, 1, 1), new Monster(Monster.frostFlyder, 1, 1), new Monster(Monster.iceFeesh, 1, 1) };
        internal static Monster[] polarBurtle1AbominableSnowMan1FrostFlyder1Collection = new Monster[] { new Monster(Monster.polarBurtle, 1, 1), new Monster(Monster.abominableSnowMan, 1, 1), new Monster(Monster.frostFlyder, 1, 1) };
        internal static Monster[] snowBandit4Collection = new Monster[] { new Monster(Monster.snowBandit, 1, 1), new Monster(Monster.snowBandit, 1, 1), new Monster(Monster.snowBandit, 1, 1), new Monster(Monster.snowBandit, 1, 1) };
        internal static Monster[] abominableSnowMan2SnowBandit2Collection = new Monster[] { new Monster(Monster.abominableSnowMan, 1, 1), new Monster(Monster.abominableSnowMan, 1, 1), new Monster(Monster.snowBandit, 1, 1), new Monster(Monster.snowBandit, 1, 1) };

        internal static Monster[] snowIceFeeshCollection = new Monster[] { new Monster(Monster.iceFeesh, 1) };

        //This will be a mixture of enemies from sewn bog and Cave Labyrinth.
        internal static readonly CombatZone overworldSnowIceFeeshFrostFlyderZone = new CombatZone("OverworldSnowZone01", 0.03f, snowBG, AudioCues.battleCue, threeMediumLayoutCollection, iceFeesh1FrostFlyder2Collection);
        internal static readonly CombatZone overworldSnowSnowBanditIceFeeshAbominableSnowManZone = new CombatZone("OverworldSnowZone02", 0.03f, snowBG, AudioCues.battleCue, fourMediumLayoutCollection, snowBandit1IceFeesh2AbominableSnowManCollection);
        internal static readonly CombatZone overworldSnowSnowBanditIceFeeshIceFrostflyderZone = new CombatZone("OverworldSnowZone03", 0.03f, snowBG, AudioCues.battleCue, fiveMediumLayoutCollection, snowBandit1IceFeesh1IceFrostflyder3Collection);
        internal static readonly CombatZone overworldSnowPolarBurtleSnowBanditIceFeeshZone = new CombatZone("OverworldSnowZone04", 0.03f, snowBG, AudioCues.battleCue, threeMediumLargeCollection, polarBurtle1SnowBandit1IceFeesh1Collection);
        internal static readonly CombatZone overworldSnowAbominableSnowManIceFeeshZone = new CombatZone("OverworldSnowZone05", 0.03f, snowBG, AudioCues.battleCue, threeMediumLayoutCollection, abominableSnowMan1IceFeesh2Collection);
        internal static readonly CombatZone overworldSnowIceFeeshFrostFlyderPolarBurtleZone = new CombatZone("OverworldSnowZone06", 0.03f, snowBG, AudioCues.battleCue, fourMediumLayoutCollection, iceFeesh2FrostFlyder1PolarBurtle1Collection);
        internal static readonly CombatZone overworldSnowPolarBurtleAbominableSnowManFrostFlyderZone = new CombatZone("OverworldSnowZone07", 0.03f, snowBG, AudioCues.battleCue, threeMediumLayoutCollection, polarBurtle1AbominableSnowMan1FrostFlyder1Collection);
        internal static readonly CombatZone overworldSnowSnowBanditZone = new CombatZone("OverworldSnowZone08", 0.03f, snowBG, AudioCues.battleCue, fourMediumLayoutCollection, snowBandit4Collection);
        internal static readonly CombatZone overworldSnowAbominableSnowManSnowBanditZone = new CombatZone("OverworldSnowZone09", 0.03f, snowBG, AudioCues.battleCue, fourMediumLayoutCollection, abominableSnowMan2SnowBandit2Collection);
        internal static readonly CombatZone overworldSnowIceFeeshZone = new CombatZone("OverworldSnowZone10", 0.03f, snowBG, AudioCues.battleCue, threeMediumLayoutCollection, snowIceFeeshCollection);

        #endregion

        #region Cave Labyrinth Zones(Snow Town)

        internal static readonly CombatZone iceFeesh1FrostFlyder2Zone = new CombatZone("SnowZone1", 0.03f, caveBG, AudioCues.battleCue, threeMediumLayoutCollection, iceFeesh1FrostFlyder2Collection);
        internal static readonly CombatZone snowBandit1IceFeesh2AbominableSnowMan1Zone = new CombatZone("SnowZone2", 0.03f, caveBG, AudioCues.battleCue, fourMediumLayoutCollection, snowBandit1IceFeesh2AbominableSnowManCollection);
        internal static readonly CombatZone snowBandit1IceFeesh1IceFrostflyder3Zone = new CombatZone("SnowZone3", 0.03f, caveBG, AudioCues.battleCue, fiveMediumLayoutCollection, snowBandit1IceFeesh1IceFrostflyder3Collection);
        internal static readonly CombatZone polarBurtle1SnowBandit1IceFeesh1Zone = new CombatZone("SnowZone4", 0.03f, caveBG, AudioCues.battleCue, threeMediumLargeCollection, polarBurtle1SnowBandit1IceFeesh1Collection);
        internal static readonly CombatZone abominableSnowMan1IceFeesh2Zone = new CombatZone("SnowZone5", 0.03f, caveBG, AudioCues.battleCue, threeMediumLayoutCollection, abominableSnowMan1IceFeesh2Collection);
        internal static readonly CombatZone iceFeesh2FrostFlyder1PolarBurtle1Zone = new CombatZone("SnowZone6", 0.03f, caveBG, AudioCues.battleCue, fourMediumLayoutCollection, iceFeesh2FrostFlyder1PolarBurtle1Collection);
        internal static readonly CombatZone polarBurtle1AbominableSnowMan1FrostFlyder1Zone = new CombatZone("SnowZone7", 0.03f, caveBG, AudioCues.battleCue, threeMediumLayoutCollection, polarBurtle1AbominableSnowMan1FrostFlyder1Collection);
        internal static readonly CombatZone snowBandit4Zone = new CombatZone("SnowZone8", 0.03f, caveBG, AudioCues.battleCue, fourMediumLayoutCollection, snowBandit4Collection);
        internal static readonly CombatZone abominableSnowMan2SnowBandit2Zone = new CombatZone("SnowZone9", 0.03f, caveBG, AudioCues.battleCue, fourMediumLayoutCollection, abominableSnowMan2SnowBandit2Collection);
        internal static readonly CombatZone SnowZone1 = new CombatZone("SnowZone10", 0.03f, caveBG, AudioCues.battleCue, threeMediumLayoutCollection, snowIceFeeshCollection);

        #endregion

        #region Serlinx Fight (Cut Scene)

        internal static Monster[] bossSerlynxCollection = new Monster[] { new Monster(Monster.serlynx, 1, 1) };

        internal static readonly CombatZone serlynxZone = new CombatZone("SerlinxZone", 0.03f, caveBG, AudioCues.minibossCue, false, oneLargeLayoutCollection, bossSerlynxCollection);

        #endregion

        #region Forbbidden Cavern Zones

        internal static Monster[] imp1CaveCrab1FireFlyder1Collection = new Monster[] { new Monster(Monster.imp, 1, 1), new Monster(Monster.caveCrab, 1, 1), new Monster(Monster.fireFlyder, 1, 1) };
        internal static Monster[] caveCrab1Imp2DemonBandit1Collection = new Monster[] { new Monster(Monster.caveCrab, 1, 1), new Monster(Monster.imp, 1, 2), new Monster(Monster.demonBandit, 1, 1) };
        internal static Monster[] desertBandit2Crab1FireFlyder1Collection = new Monster[] { new Monster(Monster.demonBandit, 1, 2), new Monster(Monster.caveCrab, 1, 1), new Monster(Monster.fireFlyder, 1, 1) };
        internal static Monster[] caveCrab2Imp2DemonBandit1Collection = new Monster[] { new Monster(Monster.caveCrab, 1, 2), new Monster(Monster.demonBandit, 1, 1), new Monster(Monster.imp, 1, 2) };
        internal static Monster[] imp2CaveCrab1FireFlyder2Collection = new Monster[] { new Monster(Monster.imp, 1, 2), new Monster(Monster.caveCrab, 1, 1), new Monster(Monster.fireFlyder, 1, 2) };
        internal static Monster[] caveCrab2DemonBandit1Collection = new Monster[] { new Monster(Monster.caveCrab, 1, 2), new Monster(Monster.demonBandit, 1, 1) };
        internal static Monster[] fireFlyder1CaveCrab1DemonBandit2Imp1Collection = new Monster[] { new Monster(Monster.fireFlyder, 1, 1), new Monster(Monster.caveCrab, 1, 1), new Monster(Monster.demonBandit, 1, 2), new Monster(Monster.imp, 1, 1) };

        internal static Monster[] caveImpCollection = new Monster[] { new Monster(Monster.imp, 1) };
        internal static Monster[] caveDemonCollection = new Monster[] { new Monster(Monster.caveDemon1, 1), new Monster(Monster.caveDemon2, 1) };

        internal static readonly CombatZone imp1CaveCrab1FireFlyder1Zone = new CombatZone("CaveZone1", 0.03f, lavaBG, AudioCues.battleCue, threeMediumLayoutCollection, imp1CaveCrab1FireFlyder1Collection);
        internal static readonly CombatZone caveCrab1Imp2DemonBandit1Zone = new CombatZone("CaveZone2", 0.03f, lavaBG, AudioCues.battleCue, fourMediumLayoutCollection, caveCrab1Imp2DemonBandit1Collection);
        internal static readonly CombatZone demonBandit2Crab1FireFlyderZone = new CombatZone("CaveZone3", 0.03f, lavaBG, AudioCues.battleCue, fourMediumLayoutCollection, desertBandit2Crab1FireFlyder1Collection);
        internal static readonly CombatZone caveCrab2Imp2DemonBandit1Zone = new CombatZone("CaveZone4", 0.03f, lavaBG, AudioCues.battleCue, fiveMediumLayoutCollection, caveCrab2Imp2DemonBandit1Collection);
        internal static readonly CombatZone imp2CaveCrab1FireFlyder2Zone = new CombatZone("CaveZone5", 0.03f, lavaBG, AudioCues.battleCue, fiveMediumLayoutCollection, imp2CaveCrab1FireFlyder2Collection);
        internal static readonly CombatZone caveCrab2DemonBandit1Zone = new CombatZone("CaveZone6", 0.03f, lavaBG, AudioCues.battleCue, threeMediumLayoutCollection, caveCrab2DemonBandit1Collection);
        internal static readonly CombatZone fireFlyder1CaveCrab1DemonBandit2Imp1Zone = new CombatZone("CaveZone7", 0.03f, lavaBG, AudioCues.battleCue, fiveMediumLayoutCollection, fireFlyder1CaveCrab1DemonBandit2Imp1Collection);

        internal static readonly CombatZone caveZone1 = new CombatZone("CaveZone7", 0.03f, castleBG, AudioCues.battleCue, threeMediumLayoutCollection, caveImpCollection);
        internal static readonly CombatZone caveZone2 = new CombatZone("CaveZone8", 0.03f, castleBG, AudioCues.battleCue, threeMediumLayoutCollection, caveDemonCollection);
            #endregion

        #region Agora Zones (Overworld)

        internal static Monster[] agoraDemonOne2DemonThree1Collection = new Monster[] { new Monster(Monster.demon1, 1, 1), new Monster(Monster.agoraDemon3, 1, 1), new Monster(Monster.demon1, 1, 1) };
        internal static Monster[] agoraDemonThree3Collection = new Monster[] { new Monster(Monster.agoraDemon3, 1, 1), new Monster(Monster.agoraDemon3, 1, 1), new Monster(Monster.agoraDemon3, 1, 1) };
        internal static Monster[] agoraDemonTwo3Collection = new Monster[] { new Monster(Monster.agoraDemon2, 1, 1), new Monster(Monster.agoraDemon2, 1, 1), new Monster(Monster.agoraDemon2, 1, 1) };
        internal static Monster[] agoraDemonOne3Collection = new Monster[] { new Monster(Monster.demon1, 1, 1), new Monster(Monster.demon1, 1, 1), new Monster(Monster.demon1, 1, 1) };
        internal static Monster[] agoraDemon1Collection = new Monster[] { new Monster(Monster.demon1, 1) };

        internal static readonly CombatZone agoraRuinsZone1 = new CombatZone("RuinsZone1", 0.03f, ruinsBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraDemon1Collection);          
        internal static readonly CombatZone agoraRuinsWallZone1 = new CombatZone("RuinsWallZone1", 0.03f, ruinsWallBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraDemon1Collection);

        internal static readonly CombatZone agoraWastlandsZone1 = new CombatZone("WastelandsZone1", 0.03f, wastelandBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraDemonOne2DemonThree1Collection);
        internal static readonly CombatZone agoraWastlandsZone2 = new CombatZone("WastelandsZone2", 0.03f, wastelandBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraDemonThree3Collection);
        internal static readonly CombatZone agoraWastlandsZone3 = new CombatZone("WastelandsZone3", 0.03f, wastelandBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraDemonTwo3Collection);
        internal static readonly CombatZone agoraWastlandsZone4 = new CombatZone("WastelandsZone4", 0.03f, wastelandBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraDemonOne3Collection);
        internal static readonly CombatZone agoraWastlandsZone5 = new CombatZone("WastelandsZone5", 0.03f, wastelandBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraDemon1Collection);
        #endregion        

        #region Agora Castle Zones

        //Note: Agora Cave Demon is an inverted Demon 1. Agora Demon 2 is the gear.
        internal static Monster[] agoraDemonImpCollection = new Monster[] { new Monster(Monster.agoraCaveDemon1, 1, 1), new Monster(Monster.agoraCaveDemon2, 1, 1), new Monster(Monster.imp, 1, 1), new Monster(Monster.agoraDemon2, 1, 1) };
        internal static Monster[] agoraGhostDemonCollection = new Monster[] { new Monster(Monster.agoraGhost, 1, 1), new Monster(Monster.agoraElderMantis,1, 1), new Monster(Monster.agoraGhost, 1, 1), new Monster(Monster.agoraDemon2, 1, 1) };
        internal static Monster[] minibossMantisCollection = new Monster[] { new Monster(Monster.agoraElderMantis, 1, SlotSize.Medium, 1), new Monster(Monster.agoraCaveDemon1, 1, 1), new Monster(Monster.agoraCaveDemon2, 1, 1) };
        internal static Monster[] agoraGhostCollection = new Monster[] { new Monster(Monster.agoraGhost, 1, 1), new Monster(Monster.agoraElderMantis, 1, 1), new Monster(Monster.agoraTohmey, 1, 1), new Monster(Monster.agoraCaveDemon1, 1, 1) };
        internal static Monster[] agoraCrabFireFlyderCollection = new Monster[] { new Monster(Monster.fireFlyder, 1, 1), new Monster(Monster.caveCrab, 1, 1), new Monster(Monster.agoraDemon3, 1, 2) };
        internal static Monster[] agoraFireFlyderCollection = new Monster[] { new Monster(Monster.fireFlyder, 1) };
        internal static Monster[] agoraGhostFlyderCollection = new Monster[] { new Monster(Monster.fireFlyder, 1, 2), new Monster(Monster.agoraGhost, 1, 2), new Monster(Monster.agoraDemon3, 1, 1) };
        internal static Monster[] agoraHauntedTreeCollection = new Monster[] { new Monster(Monster.agoraHauntedTree, 1, 2), new Monster(Monster.agoraCaveDemon2, 1, 2) };
        internal static Monster[] agoraTohmeyCollection = new Monster[] { new Monster(Monster.agoraTohmey, 1, 2), new Monster(Monster.imp, 1, 1), new Monster(Monster.agoraDemon2, 1, 1) };
        internal static Monster[] agoraGhostTohmeyCollection = new Monster[] { new Monster(Monster.agoraGhost, 1, 1),  new Monster(Monster.agoraElderMantis, 1, 1), new Monster(Monster.agoraTohmey, 1, 1)};
        internal static Monster[] agoraDemonCollection = new Monster[] { new Monster(Monster.agoraCaveDemon1, 1, 1), new Monster(Monster.agoraCaveDemon2, 1, 1), new Monster(Monster.agoraDemon3, 1, 2), new Monster(Monster.agoraDemon2, 1, 1) };
        internal static Monster[] agoraImpCollection = new Monster[] { new Monster(Monster.imp, 1, 1), new Monster(Monster.agoraElderMantis, 1, 1), new Monster(Monster.imp, 1, 1) };

        internal static readonly CombatZone agoraDemonImpZone = new CombatZone("Agora01", 0.03f, caveBG, AudioCues.battleCue, fourMediumLayoutCollection, agoraDemonImpCollection);
        internal static readonly CombatZone agoraDemonGhostZone = new CombatZone("Agora03", 0.03f, castleBG, AudioCues.battleCue, fourMediumLayoutCollection, agoraGhostDemonCollection);
        internal static readonly CombatZone agoraMantisZone = new CombatZone("Agora04", 0.03f, castleBG, AudioCues.battleCue, threeMediumLayoutCollection, minibossMantisCollection);
        internal static readonly CombatZone agoraGhostZone = new CombatZone("Agora05", 0.03f, castleBG, AudioCues.battleCue, fourMediumLayoutCollection, agoraGhostCollection);

        // Collin's stuff:
        // underground areas
        internal static readonly CombatZone agoraCrabFireFlyderZone = new CombatZone("Agora06", 0.03f, caveBG, AudioCues.battleCue, fourMediumLayoutCollection, agoraCrabFireFlyderCollection);
        internal static readonly CombatZone agoraFireFlyderZone = new CombatZone("Agora07", 0.03f, caveBG, AudioCues.battleCue, fiveMediumLayoutCollection, agoraFireFlyderCollection);
        internal static readonly CombatZone agoraGhostFireFlyderZone = new CombatZone("Agora08", 0.03f, caveBG, AudioCues.battleCue, fiveMediumLayoutCollection, agoraGhostFlyderCollection);
        // Outside areas
        internal static readonly CombatZone agoraGhostZoneGrass = new CombatZone("Agora09", 0.03f, castle3BG, AudioCues.battleCue, fourMediumLayoutCollection, agoraGhostCollection);
        internal static readonly CombatZone agoraDemonImpZoneGrass = new CombatZone("Agora10", 0.03f, castle3BG, AudioCues.battleCue, fourMediumLayoutCollection, agoraDemonImpCollection);
        internal static readonly CombatZone agoraHauntedTreeZoneGrass = new CombatZone("Agora11", 0.03f, castle3BG, AudioCues.battleCue, fourMediumLayoutCollection, agoraHauntedTreeCollection);
        // Carpet Interior rooms
        internal static readonly CombatZone agoraTohmeyZoneCarpet = new CombatZone("Agora12", 0.03f, castleBG, AudioCues.battleCue, fourMediumLayoutCollection, agoraTohmeyCollection);
        internal static readonly CombatZone agoraGhostTohmeyZoneCarpet = new CombatZone("Agora13", 0.03f, castleBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraGhostTohmeyCollection);
        internal static readonly CombatZone agoraDemonImpZoneCarpet = new CombatZone("Agora14", 0.03f, castleBG, AudioCues.battleCue, fourMediumLayoutCollection, agoraDemonImpCollection);
        internal static readonly CombatZone agoraGhostZoneCarpet = new CombatZone("Agora15", 0.03f, castleBG, AudioCues.battleCue, fourMediumLayoutCollection, agoraGhostCollection);
        internal static readonly CombatZone agoraDemonZoneCarpet = new CombatZone("Agora16", 0.03f, castleBG, AudioCues.battleCue, fiveMediumLayoutCollection, agoraDemonCollection);
        internal static readonly CombatZone agoraImpZoneCarpet = new CombatZone("Agora14", 0.03f, castleBG, AudioCues.battleCue, threeMediumLayoutCollection, agoraImpCollection);
        // Stone rooms/ ledges
        internal static readonly CombatZone agoraTohmeyZoneStone = new CombatZone("Agora12", 0.03f, castle2BG, AudioCues.battleCue, fourMediumLayoutCollection, agoraTohmeyCollection);
        internal static readonly CombatZone agoraGhostTohmeyZoneStone = new CombatZone("Agora13", 0.03f, castle2BG, AudioCues.battleCue, threeMediumLayoutCollection, agoraGhostTohmeyCollection);
        internal static readonly CombatZone agoraDemonImpZoneStone = new CombatZone("Agora14", 0.03f, castle2BG, AudioCues.battleCue, fourMediumLayoutCollection, agoraDemonImpCollection);
        internal static readonly CombatZone agoraGhostZoneStone = new CombatZone("Agora15", 0.03f, castle2BG, AudioCues.battleCue, fourMediumLayoutCollection, agoraGhostCollection);
        internal static readonly CombatZone agoraDemonZoneStone = new CombatZone("Agora16", 0.03f, castle2BG, AudioCues.battleCue, fiveMediumLayoutCollection, agoraDemonCollection);
        internal static readonly CombatZone agoraImpZoneStone = new CombatZone("Agora14", 0.03f, castle2BG, AudioCues.battleCue, threeMediumLayoutCollection, agoraImpCollection);
        internal static readonly CombatZone agoraHauntedTreeZoneStone = new CombatZone("Agora11", 0.03f, castle2BG, AudioCues.battleCue, fourMediumLayoutCollection, agoraHauntedTreeCollection);

        #endregion

        #region Arlan Fight (Cut Scene)

        internal static Monster[] bossArlanCollection = new Monster[] { new Monster(Monster.arlan, 1, 1) };
        internal static readonly CombatZone arlanZone = new CombatZone("ArlanZone", 0.020f, castle4BG, AudioCues.finalBossPT1, false, oneMediumLayoutCollection, bossArlanCollection);

        #endregion

        #region Malticar Fight (Cut Scene)

        internal static Monster[] bossMalticarCollection = new Monster[] { new Monster(Monster.malticar, 1, 1) };
        internal static Monster[] riftMalticarCollection = new Monster[] { new Monster(Monster.finalMalticar, 1, 1) };

        internal static readonly CombatZone malticarZone = new CombatZone("MalTiCarZone01", 0.020f, castle4BG, AudioCues.finalBossPT2, false, oneLargeLayoutCollection, bossMalticarCollection);
        internal static readonly CombatZone malticarZone2 = new CombatZone("MalTiCarZone02", 0.020f, castle4BG, AudioCues.willTheme, false, oneLargeLayoutCollection, riftMalticarCollection);//This adds a malticar with 0 agility.

        #endregion
        //End Combat Zones

        #region Combat Zone Pool Constructor (Builds the zones list)
        /// <summary>
        /// The zones collection determines the combat zone indeces used in the maps.
        /// This is not needed for cut scenes.
        /// </summary>
        CombatZonePool()
        {
            #region Empty Zone

            //index 0 should be an empty zone

            zones.Add(emptyZone);  // 0
            Debug.Assert(zones.Count == 1);

            #endregion

            #region Keep Zones

            zones.Add(keepZoneDemons);  // 1
            zones.Add(emptyZone);       // 2
            zones.Add(emptyZone);       // 3
            zones.Add(emptyZone);       // 4
            zones.Add(emptyZone);       // 5
            zones.Add(emptyZone);       // 6
            zones.Add(emptyZone);       // 7
            Debug.Assert(zones.Count == 8);

            #endregion

            #region Mushroom Forest Zone
            zones.Add(feesh2Zone);         //forestZone1   // 8
            zones.Add(flyder3Zone);        //forestZone2  //  9
            zones.Add(feesh1Flyder1Zone);  //forestZone3  // 10
            zones.Add(emptyZone);                         // 11
            zones.Add(emptyZone);                         // 12
            Debug.Assert(zones.Count == 13);

            #endregion

            #region Blind Man's Forest Zone
            zones.Add(hauntedTree1Feesh2Zone);              //forestZone4  // 13
            zones.Add(hauntedTree1Feesh1Bandit1Zone);  //forestZone5  // 14
            zones.Add(bandit3Zone);                    //forestZone6  // 15
            zones.Add(hauntedTree1Bandit1Zone);        //forestZone7  // 16
            zones.Add(feesh4Zone);                          //forestZone8  // 17
            Debug.Assert(zones.Count == 18);

            #endregion

            #region Unused

            zones.Add(emptyZone); //18
            zones.Add(emptyZone); //19
            zones.Add(emptyZone); //20
            zones.Add(emptyZone); //21
            zones.Add(emptyZone); //22
            zones.Add(emptyZone); //23
            zones.Add(emptyZone); //24
            zones.Add(emptyZone); //25
            zones.Add(emptyZone); //26
            zones.Add(emptyZone); //27
            Debug.Assert(zones.Count == 28);

            #endregion

            #region Library Zones

            zones.Add(hauntedBook2hauntedChair1Zone);           //libraryZone1  // 28
            zones.Add(hauntedChair1Ghost1Zone);                 //libraryZone2  // 29
            zones.Add(ghost2Zone);                              //libraryZone3  // 30
            zones.Add(tohmey1HauntedChair1Zone);                //libraryZone4  // 31
            zones.Add(ghost1HauntedChair1HauntedBook1Zone);     //libraryZone5  // 32
            zones.Add(apprentice1HauntedBook1Zone);             //libraryZone6  // 33
            zones.Add(ghost2Apprentice1Zone);                   //libraryZone7  // 34
            zones.Add(apprentice1Tohmey1HauntedChair1Zone);     //libraryZone8  // 35
            zones.Add(hauntedBook5Zone);                        //libraryZone9  // 36
            zones.Add(apprentice2Ghost1Zone);                  //libraryZone10  // 37
            zones.Add(emptyZone);                                               // 38
            zones.Add(emptyZone);                                               // 39
            zones.Add(emptyZone);                                               // 40
            Debug.Assert(zones.Count == 41);

            #endregion

            #region Swamp Zones

            zones.Add(vooDooDoll1HauntedTree1Zone);                             // 41
            zones.Add(witchDoctor1HauntedTree1VoodooDoll1Zone);                 // 42
            zones.Add(witchDoctor2VoodooDoll1Zone);                                        // 43
            zones.Add(hauntedTree2VoodooDoll1Zone);                             // 44
            zones.Add(voodooDoll3Zone);                                         // 45
            zones.Add(witchDoctor1HauntedTree1VoodooDoll1Zone);                 // 46
            zones.Add(boggimusTadpole3WitchDoctor1Zone);                        // 47
            zones.Add(boggimusTadpole2HauntedTree1VoodooDoll1Zone);             // 48
            zones.Add(boggimusTadpole1WitchDoctor1HauntedTree1VoodooDoll2Zone); // 49
            zones.Add(boggimusTadpole5Zone);                                    // 50
            zones.Add(voodooDoll5Zone);                                         // 51
            zones.Add(emptyZone);                                               // 52
            zones.Add(emptyZone);                                               // 53
            Debug.Assert(zones.Count == 54);

            #endregion

            #region Snow Zones

            zones.Add(iceFeesh1FrostFlyder2Zone);               // 54
            zones.Add(snowBandit1IceFeesh2AbominableSnowMan1Zone);                // 55
            zones.Add(snowBandit1IceFeesh1IceFrostflyder3Zone); // 56
            zones.Add(polarBurtle1SnowBandit1IceFeesh1Zone);             // 57
            zones.Add(abominableSnowMan1IceFeesh2Zone);         // 58
            zones.Add(iceFeesh2FrostFlyder1PolarBurtle1Zone);   // 59
            zones.Add(polarBurtle1AbominableSnowMan1FrostFlyder1Zone);      // 60
            zones.Add(snowBandit4Zone);                         // 61
            zones.Add(abominableSnowMan2SnowBandit2Zone);       // 62
            zones.Add(SnowZone1);                               // 63
            zones.Add(emptyZone);                               // 64
            zones.Add(emptyZone);                               // 65
            zones.Add(emptyZone);                               // 66
            Debug.Assert(zones.Count == 67);

            #endregion

            #region Forbidden Cavern Zones

            zones.Add(imp1CaveCrab1FireFlyder1Zone);  // 67
            zones.Add(caveCrab1Imp2DemonBandit1Zone);  // 68
            zones.Add(demonBandit2Crab1FireFlyderZone);  // 69
            zones.Add(caveCrab2Imp2DemonBandit1Zone);  // 70
            zones.Add(imp2CaveCrab1FireFlyder2Zone);  // 71
            zones.Add(caveCrab2DemonBandit1Zone);  // 72
            zones.Add(fireFlyder1CaveCrab1DemonBandit2Imp1Zone);  // 73
            zones.Add(emptyZone);  // 74
            zones.Add(emptyZone);  // 75
            zones.Add(emptyZone);  // 76
            zones.Add(emptyZone);  // 77
            Debug.Assert(zones.Count == 78);

            #endregion

            #region Agora Zones

            zones.Add(agoraRuinsWallZone1);  // 78
            zones.Add(agoraRuinsZone1);  // 79
            zones.Add(agoraWastlandsZone1);  // 80
            zones.Add(agoraWastlandsZone2);  // 81
            zones.Add(agoraWastlandsZone3);  // 82
            zones.Add(agoraWastlandsZone4);  // 83
            zones.Add(agoraWastlandsZone5);  // 84
            zones.Add(emptyZone);  // 85
            zones.Add(emptyZone);  // 86
            zones.Add(emptyZone);  // 87
            Debug.Assert(zones.Count == 88);

            #endregion

            #region Agora Castle Zones
            
            //Underground
            zones.Add(agoraCrabFireFlyderZone);//88
            zones.Add(agoraFireFlyderZone);//89
            zones.Add(agoraGhostFireFlyderZone);//90

            //Maze
            zones.Add(agoraDemonGhostZone);//91
            zones.Add(agoraMantisZone);//92
            zones.Add(agoraGhostZone);//93

            //Outside grass
            zones.Add(agoraGhostZoneGrass);//94
            zones.Add(agoraDemonImpZoneGrass);//95
            zones.Add(agoraHauntedTreeZoneGrass);//96

            //Carpet
            zones.Add(agoraTohmeyZoneCarpet);//97
            zones.Add(agoraGhostTohmeyZoneCarpet);//98
            zones.Add(agoraDemonImpZoneCarpet);//99
            zones.Add(agoraDemonZoneCarpet);//100
            zones.Add(agoraGhostZoneCarpet);//101
            zones.Add(agoraImpZoneCarpet);//102

            //Stone
            zones.Add(agoraTohmeyZoneStone);//103
            zones.Add(agoraGhostTohmeyZoneStone);//104
            zones.Add(agoraDemonImpZoneStone);//105
            zones.Add(agoraDemonZoneStone);//106
            zones.Add(agoraGhostZoneStone);//107 
            zones.Add(agoraImpZoneStone);//108
            zones.Add(agoraHauntedTreeZoneStone);//109

            Debug.Assert(zones.Count == 110);

            #endregion

            #region Path to Blindman's Town
            zones.Add(forestOverworldBanditZone);//110
            zones.Add(forestOverworldfeeshZone);//111
            zones.Add(forestOverworldflyderZone);//112
            zones.Add(forestOverworldfeeshFlyderZone);//113
            zones.Add(ruinsOverworldBanditZone);//114
            zones.Add(grassOverworldBanditZone);//115
            zones.Add(grassOverworldfeeshZone);//116
            zones.Add(grassOverworldflyderZone);//117
            zones.Add(grassOverworldfeeshFlyderZone);//118
            Debug.Assert(zones.Count == 119);

            #endregion

            #region Path to Mage Town
            zones.Add(overworldMageTownForestHauntedTreeFeeshZone);//119
            zones.Add(overworldMageTownForestHauntedTreeFeeshBanditZone);//120
            zones.Add(overworldMageTownForestHauntedTreeBanditZone);//121
            zones.Add(overworldMageTownGrassBanditZone);//122
            zones.Add(overworldMageTownGrassFeeshZone);//123
            zones.Add(overworldMageTownRuinsBanditZone);//124
            zones.Add(overworldMageTownDesertBanditZone);//125
            Debug.Assert(zones.Count == 126);

            #endregion

            #region Path to Swamp Town
            zones.Add(overworldSwampGhostZone);//126
            zones.Add(overworldSwampApprenticeGhostZone);//127
            zones.Add(overworldSwampWitchDoctorHauntedTreeZone);//128
            zones.Add(overworldSwampWitchDoctorVoodooDollZone);//129
            zones.Add(overworldSwampWitchDoctorHauntedTreeVoodooDollZone);//130
            zones.Add(overworldSwampBoggimusTadpoleZone);//131
            zones.Add(overworldSwampRuinsBoggimusTadpoleZone);//132
            Debug.Assert(zones.Count == 133);
            #endregion

            #region Path to Snow Town
            zones.Add(overworldSnowIceFeeshFrostFlyderZone);//133
            zones.Add(overworldSnowSnowBanditIceFeeshAbominableSnowManZone);//134
            zones.Add(overworldSnowSnowBanditIceFeeshIceFrostflyderZone);//135
            zones.Add(overworldSnowPolarBurtleSnowBanditIceFeeshZone);//136
            zones.Add(overworldSnowAbominableSnowManIceFeeshZone);//137
            zones.Add(overworldSnowIceFeeshFrostFlyderPolarBurtleZone);//138
            zones.Add(overworldSnowPolarBurtleAbominableSnowManFrostFlyderZone);//139
            zones.Add(overworldSnowSnowBanditZone);//140
            zones.Add(overworldSnowAbominableSnowManSnowBanditZone);//141
            zones.Add(overworldSnowIceFeeshZone);//142

            zones.Add(overworldSwampSnowForestWitchDoctorHauntedTreeZone);//143
            zones.Add(overworldSwampSnowForestWitchDoctorVoodooDollZone);//144
            zones.Add(overworldSwampSnowForestWitchDoctorHauntedTreeVoodooDollZone);//145
            zones.Add(overworldSwampSnowGrassWitchDoctorVoodooDollZone);//146
            zones.Add(overworldSwampSnowGrassWitchDoctorHauntedTreeVoodooDollZone);//147
            zones.Add(overworldSwampSnowGrassWitchDoctorHauntedTreeZone);//148
            zones.Add(overworldSwampSnowGrassvoodooDoll5Zone);//149

            Debug.Assert(zones.Count == 150);
            #endregion
        }

        #endregion

        #region Methods

        static CombatZonePool singleton = new CombatZonePool();

        public static CombatZonePool Singleton
        {
            get { return singleton; }
        }

        /// <summary>
        /// The list of combat zones identified by index
        /// </summary>
        List<CombatZone> zones = new List<CombatZone>();

        public CombatZone[] Zones
        {
            get { return zones.ToArray(); }
        }

        /// <summary>
        /// Attempts to retrieve a zone from the pool
        /// </summary>
        /// <param name="index">The index of the zone within the pool.
        /// This index is stored within the map itself.</param>
        /// <returns>A combat zone or the emptyZone if the index is out of range</returns>
        public CombatZone GetZone(int index)
        {
            try
            {
                return zones[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Zone " + index + " does not exist.");
            }

        }

        public short ToIndex(string zoneName)
        {
            for (int i = 0; i < zones.Count; ++i)
            {
                if (zones[i].ZoneName == zoneName)
                {
                    return (short)i;
                }
            }

            //return -1;
            throw new Exception(zoneName + " is not in the zones list.");
        }

        #endregion
    }
}
