using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class GamePhaseSelectionScreen : Screen
    {
        private class PhaseTransition
        {
            public delegate void TransitionFunctionDelegate(bool isFinalDestination);
            public string name;
            public TransitionFunctionDelegate transitionFunction;

            public PhaseTransition(string name, TransitionFunctionDelegate transitionFunction)
            {
                this.name = name;
                this.transitionFunction = transitionFunction;
            }
        }

        #region Positions and Fields

        static readonly Vector2 screenLocation = new Vector2(375, 80);
        static readonly Vector2 townNameLocation = new Vector2(screenLocation.X + 45, screenLocation.Y + 20);
        static readonly Vector2 upArrowLocation = new Vector2(screenLocation.X + 303, screenLocation.Y + 20);
        static readonly Vector2 downArrowLocation = new Vector2(screenLocation.X + 303, screenLocation.Y + 170);

        const int maxTownDisplay = 7;

        List<PhaseTransition> phaseTransitions = new List<PhaseTransition>();
        Rift.RiftDestination riftDestination;

        PCFightingCharacter nathan;
        PCFightingCharacter cara;
        PCFightingCharacter will;

        #endregion

        #region Transitions

        // The first transition point has no preceeding transition point, and so it does not call TransitionTo<Something>
        // All transitions other than the first must invoke the TransitionTo function for the preceeding transition.
        // This creates a chain of transitions that run.

        private void TransitionToHealerVillage(bool isFinalDestination)
        {
            // Start new game.
            LoadingPartyScreen.Singleton.IsNewGame = true;
            ScreenManager.ExitAllScreens();
            Party.Singleton.Initialize(true);
            ScreenManager.AddScreen(TileMapScreen.Instance);

            // Remove Sid and Max.
            Party.Singleton.RemoveAllFightingCharacters();
            Party.Singleton.GameState.Inventory.Items.Clear();
            nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            Party.Singleton.GameState.Fighters[0].UnequipAccessory(0);
            Party.Singleton.GameState.Fighters[0].UnequipAccessory(1);
            Party.Singleton.GameState.Fighters[0].UnequipAccessory(2);
            Party.Singleton.GameState.Fighters[0].WeaponClassName = null;
            Party.Singleton.GameState.Fighters[0].ArmorClassName = null;
            Party.Singleton.GameState.Fighters[0].EquipWeapon(EquipmentPool.RequestEquipment("PlainSword"));
            Party.Singleton.GameState.Fighters[0].EquipArmor(EquipmentPool.RequestEquipment("Armor"));
            //Item accessory = ItemPool.RequestItem("RingOfJustice");
            //Party.Singleton.GameState.Fighters[0].EquipAccessory(accessory as Accessory, 0);

            Party.Singleton.AddAchievement(TrappedGirlsController.freedAchievement);  // Side quest; needed for later game.
            Party.Singleton.AddAchievement(MayorsHouseCutSceneScreen.achievement);
            Party.Singleton.AddAchievement(NathanFoundCutSceneScreen.achievement);
           
            if (isFinalDestination)
            {
                nathan.SetLevel(1);
                Party.Singleton.GameState.Fighters[0].LevelUpReEquip();
                riftDestination = new Rift.RiftDestination(Maps.healersVillageMayorsHouseF2, new Point(0, 5));
            }
        }

        private void TransitionToHealersVillageBlacksmith(bool isFinalDestination)
        {
            TransitionToHealerVillage(false);
            new MushroomForestChest1Controller().Complete();  // Gives ring of justice.
            new MushroomForestChest6Controller().Complete();  //Gives another ring of justice

            new Mushroom1Controller().Complete();
            new Mushroom2Controller().Complete();
            new Mushroom3Controller().Complete();
            new Mushroom3Controller().Complete();


            // Talk to old lady.
            new OldLadysController().CompleteMushroomCollectionQuest();

            // Talk to blacksmith.
            new HealersBlacksmithsController().CompleteTurnInSwordHiltAchievement();

            // Talk to inn keeper to sleep.
            new HealersInnClerksController().CompleteNextDayAchievement();
     
            if (isFinalDestination)
            {
                nathan.SetLevel(3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.healersVillageBlacksmith, new Point(10, 6));
            }
        }

        private void TransitionToBlindMansTown(bool isFinalDestination)
        {
            TransitionToHealersVillageBlacksmith(false);
            // May need to add achievemnt for entering this map for the first time for the rift skill.

            new HealersBlacksmithsController().CompleteRecievedSwordHiltAchievement();
            new HealersBlacksmithCutSceneScreen().CompleteForTesting();
            new HealersBattleCutSceneScreen().CompleteForTesting();
            new HealerJoinsCutSceneScreen().CompleteForTesting();
            cara = Party.Singleton.GetFightingCharacter(Party.cara);
            nathan.EquipWeapon(EquipmentPool.RequestEquipment("AdvancedSword"));
            nathan.EquipArmor(EquipmentPool.RequestEquipment("Armor"));
            Equipment book = EquipmentPool.RequestEquipment("PlainBook");
            Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(book);
            cara.EquipArmor(EquipmentPool.RequestEquipment("ClothArmor"));
            Party.Singleton.GameState.Gold = 700;

            // We might need to apply map changes here to.
            // I have an idea about how to do this efficiently using cut scene screens to eliminate duplicate logic.
            // Kyle, talk to me about this.

       
            if (isFinalDestination)
            {
                nathan.SetLevel(6);
                cara.SetLevel(5);
                Party.Singleton.GameState.Inventory.AddItem(typeof(SmallHealthPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.blindMansTown, new Point(24, 16));
            }
        }
        private void TransitionToBlindMansForestEnd(bool isFinalDestination)
        {
            TransitionToBlindMansTown(false);

            //new BlindMansForestChest1Controller().Complete();
            //new BlindMansForestChest3Controller().Complete();
            //new BlindMansForestChest7Controller().Complete();
            //new BlindMansForestChest8Controller().Complete();

            new MansionOwnersCutSceneScreen().CompleteForTesting();
            new RoyalFamilyCutSceneScreen().CompleteForTesting();
            new StolenItemCutSceneScreen().CompleteForTesting();

            if (isFinalDestination)
            {
                nathan.SetLevel(6);
                cara.SetLevel(5);
                Party.Singleton.GameState.Inventory.AddItem(typeof(SmallHealthPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.blindMansForest4, new Point(22, 5));
            }
        }
        private void TransitionToBurtleBossFight(bool isFinalDestination)
        {
            TransitionToBlindMansForestEnd(false);
  
      
            new WillsBlindedCutSceneScreen().CompleteForTesting();
            new WillsWanderCutSceneScreen().CompleteForTesting();

            if (isFinalDestination)
            {
                nathan.SetLevel(8);
                cara.SetLevel(7); 
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.blindMansForest5, new Point(9, 2));
            }
        }

        private void TransitionToPathToMageTown(bool isFinalDestination)
        {
            TransitionToBurtleBossFight(false);

            
            new WillsBurtleInitiateCutSceneScreen().CompleteForTesting();
            new CapturedWillCutSceneScreen().CompleteForTesting();
            new WillsBlacksmithsController().CompleteRepairedScytheAchievement();

            will = Party.Singleton.GetFightingCharacter(Party.will);

            if (isFinalDestination)
            {
                nathan.SetLevel(8);
                cara.SetLevel(8);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.blindMansTown, new Point(43, 29));
            }
        }
        private void TransitionToBackToHealersVillage(bool isFinalDestination)
        {
            TransitionToPathToMageTown(false);
           
            new SeedMerchantsController().Complete();
            Party.Singleton.GameState.Inventory.AddItem(typeof(FarmersSpade), 1);
            if (isFinalDestination)
            {
                nathan.SetLevel(8);
                cara.SetLevel(8);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.healersVillage, new Point(8, 8));
            }

        }
        private void TransitionToPossessedLibraryEntrance(bool isFinalDestination)
        {
            TransitionToBackToHealersVillage(false);

            new JanniesController().CompleteAchievement();

            new TippersController().CompleteAchievement(); 

            new TippersController().CompleteMapMod(); 

            new WillEntersMageTownCutSceneScreen().CompleteForTesting();
            new EnterMageTownCutSceneScreen().CompleteForTesting();

            if (isFinalDestination)
            {
                nathan.SetLevel(12);
                cara.SetLevel(12);
                will.SetLevel(12);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.mageTown, new Point(14, 18));
            }
        }

        private void TransitionToPossessedLibraryFl3(bool isFinalDestination)
        {
            TransitionToPossessedLibraryEntrance(false);

            //new Library3Chest1Controller().Complete();
            //new Library3Chest2Controller().Complete();
            //new Library3Chest3Controller().Complete();
            //new Library3Chest4Controller().Complete();
            //new Library3Chest5Controller().Complete();

            if (isFinalDestination)
            {
                nathan.SetLevel(12);
                cara.SetLevel(12);
                will.SetLevel(12);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.possessedLibrary3, new Point(10, 8));
            }
        }


        private void TransitionToPossessedLibraryFl4(bool isFinalDestination)
        {
            TransitionToPossessedLibraryFl3(false);

            if (isFinalDestination)
            {
                nathan.SetLevel(12);
                cara.SetLevel(12);
                will.SetLevel(12);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.possessedLibrary4ground, new Point(10, 8));
            }
        }
        private void TransitionToPossessedLibraryFl5(bool isFinalDestination)
        {
            TransitionToPossessedLibraryFl4(false);

            if (isFinalDestination)
            {
                nathan.SetLevel(12);
                cara.SetLevel(12);
                will.SetLevel(12);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.possessedLibrary5, new Point(34, 18));
            }
        }
        private void TransitionToElderMantisBossFight(bool isFinalDestination)
        {
            TransitionToPossessedLibraryFl3(false);

            //new Library1Chest1Controller().Complete();
            //new Library1Chest2Controller().Complete();
            //new Library1Chest3Controller().Complete();
            //new Library2Chest2Controller().Complete();
            //new Library2Chest3Controller().Complete();
            //new Library2Chest4Controller().Complete();
            //new Library4Chest1Controller().Complete();
            //new Library4Chest2Controller().Complete();
            //new Library4Chest3Controller().Complete();
            //new Library5Chest1Controller().Complete();

            new ArlansStudyCutSceneScreen().CompleteForTesting();

            if (isFinalDestination)
            {
                nathan.SetLevel(14);
                cara.SetLevel(14);
                will.SetLevel(14);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);
                riftDestination = new Rift.RiftDestination(Maps.possessedLibrary5, new Point(34, 3));
            }
        }

        private void TransitionToEndorRiftScene(bool isFinalDestination)
        {
            TransitionToElderMantisBossFight(false);

            new LibraryBossCutSceneScreen().CompleteForTesting();

            if (isFinalDestination)
            {
                nathan.SetLevel(15);
                cara.SetLevel(15);
                will.SetLevel(15);
                riftDestination = new Rift.RiftDestination(Maps.possessedLibrary5, new Point(30, 3));
            }
        }

        private void TransitionToJustGotRift(bool isFinalDestination)
        {
            TransitionToEndorRiftScene(false);
            new EndorsStudyCutSceneScreen().CompleteForTesting();
          
            Party.Singleton.GameState.InAgora = false;
            //for (int i = 0; i < 6; i++)
            //{
            //    Nathan.Instance.unlockedElathiaRiftDestinations.Add(i);
            //}
            if (isFinalDestination)
            {
                nathan.SetLevel(15);
                cara.SetLevel(15);
                will.SetLevel(15);
                riftDestination = Rift.elathiaDestinations[2];
            }

            
        }

        private void TransitionToPathToSnowAndSwampTown(bool isFinalDestination)
        {
            TransitionToJustGotRift(false);

            new EndorsStudyCutSceneScreen().CompleteForTesting(); 

            if (isFinalDestination)
            {
                nathan.SetLevel(15);
                cara.SetLevel(15);
                will.SetLevel(15);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 3);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 3);               
                riftDestination = new Rift.RiftDestination(Maps.mageTown, new Point(10, 57));
            }
        }

   
        private void TransitionToSwampTown(bool isFinalDestination)
        {
            TransitionToPathToSnowAndSwampTown(false);

            if (isFinalDestination)
            {
                nathan.SetLevel(16);
                cara.SetLevel(16);
                will.SetLevel(16);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination(Maps.swampVillage, new Point(30, 67));
            }
        }

        private void TransitionToEntranceToSewnBog(bool isFinalDestination)
        {
            TransitionToSwampTown(false);

            new SwampQueenCutSceneScreen().CompleteForTesting();

            if (isFinalDestination)
            {
                nathan.SetLevel(16);
                cara.SetLevel(16);
                will.SetLevel(16);
                riftDestination = new Rift.RiftDestination(Maps.sewnBog1, new Point(1, 1));
            }
        }

        private void TransitionToChepetawaBossFight(bool isFinalDestination)
        {
            TransitionToEntranceToSewnBog(false);

            //new SewnBogChest1Controller().Complete();
            //new SewnBogChest2Controller().Complete();
            //new SewnBogChest3Controller().Complete();
            //new SewnBogChest4Controller().Complete();
            //new SewnBogChest5Controller().Complete();
            //new SewnBogChest6Controller().Complete();
            //new SewnBogChest7Controller().Complete();
            //new SewnBogChest8Controller().Complete();
            //new SewnBogChest9Controller().Complete();
            //new SewnBogChest10Controller().Complete();
            //new SewnBogChest11Controller().Complete();
            //new SewnBogChest12Controller().Complete();
            //new SewnBogChest13Controller().Complete();      

            if (isFinalDestination)
            {
                nathan.SetLevel(18);
                cara.SetLevel(18);
                will.SetLevel(18);
                riftDestination = new Rift.RiftDestination(Maps.sewnBog7, new Point(44, 27));
            }
        }

        private void TransitionToSavedSwampTown(bool isFinalDestination)
        {
            TransitionToChepetawaBossFight(false);

            new ChepetawaCutSceneScreen().CompleteForTesting();
            new BackToQueenCutSceneScreen().CompleteForTesting();

            if (isFinalDestination)
            {
                nathan.SetLevel(19);
                cara.SetLevel(19);
                will.SetLevel(19);
                riftDestination = new Rift.RiftDestination(Maps.swampVillage, new Point(30, 8));
            }
        }

        private void TransitionToSnowTown(bool isFinalDestination)
        {
            TransitionToSavedSwampTown(false);

            Nathan.Instance.unlockedElathiaRiftDestinations.Add(4);

            if (isFinalDestination)
            {
                nathan.SetLevel(19);
                cara.SetLevel(19);
                will.SetLevel(19);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination(Maps.snowTown, new Point(17, 20));
            }

        }

        private void TransitionToPathToCaveLabyrinth(bool isFinalDestination)
        {
            TransitionToSnowTown(false);

            if (isFinalDestination)
            {
                nathan.SetLevel(19);
                cara.SetLevel(19);
                will.SetLevel(19);
                riftDestination = new Rift.RiftDestination(Maps.overworld, new Point(20, 60));
            }

        }

        private void TransitionToCaveLabyrinth(bool isFinalDestination)
        {
            TransitionToPathToCaveLabyrinth(false);

            new InsideHermitHouseCutSceneScreen().CompleteForTesting();
            new HermitHouseAttackedCutSceneScreen().CompleteForTesting(); 
            new EnterLabyrinthCutSceneScreen().CompleteForTesting();

            Nathan.Instance.unlockedElathiaRiftDestinations.Add(5);

            if (isFinalDestination)
            {
                nathan.SetLevel(20);
                cara.SetLevel(20);
                will.SetLevel(20);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination(Maps.caveLabyrinth, new Point(93, 98));
            }
        }

        private void TransitionToSerlinxBossFight(bool isFinalDestination)
        {
            TransitionToCaveLabyrinth(false);

            if (isFinalDestination)
            {
                nathan.SetLevel(20);
                cara.SetLevel(20);
                will.SetLevel(20);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination(Maps.caveLabyrinth, new Point(48, 49));
            }
        }

        private void TransitionToBackToEndor(bool isFinalDestination)
        {
            TransitionToSerlinxBossFight(false);

            new LabyrinthBossCutSceneScreen().CompleteForTesting();
            new BackToHermitHouseCutSceneScreen().CompleteForTesting();
         

            if (isFinalDestination)
            {
                nathan.SetLevel(21);
                cara.SetLevel(21);
                will.SetLevel(21);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination(Maps.mageTown, new Point(10, 57));
            }
        }

        private void TransitionToEntranceToSouthForest(bool isFinalDestination)
        {
            TransitionToBackToEndor(false);

            new EndorsController().CompleteAchievement();
            new EndorsController().CompleteMapMod();

            if (isFinalDestination)
            {
                nathan.SetLevel(21);
                cara.SetLevel(21);
                will.SetLevel(21);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination("overworld", new Point(57, 104));
            }
        }

        private void TransitionToPathToAgoraCastle(bool isFinalDestination)
        {
            TransitionToEntranceToSouthForest(false);

            new EndorsController().CompleteAchievement();
            new EndorsController().CompleteMapMod();

            Party.Singleton.GameState.InAgora = true;

            if (isFinalDestination)
            {
                nathan.SetLevel(21);
                cara.SetLevel(21);
                will.SetLevel(21);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination("overworld", new Point(92, 120));
            }
        }


        private void TransitionToEntranceToAgoraInn(bool isFinalDestination)
        {
            TransitionToPathToAgoraCastle(false);

            new EndorsController().CompleteAchievement();
            new EndorsController().CompleteMapMod();
            Party.Singleton.GameState.Gold = 1000000;
            if (isFinalDestination)
            {
                nathan.SetLevel(21);
                cara.SetLevel(21);
                will.SetLevel(21);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination("overworld", new Point(34, 136));
            }
        }

        private void TransitionToEntranceToAgoraCastle(bool isFinalDestination)
        {
            TransitionToEntranceToAgoraInn(false);

            new EndorsController().CompleteAchievement();
            new EndorsController().CompleteMapMod();

            if (isFinalDestination)
            {
                nathan.SetLevel(21);
                cara.SetLevel(21);
                will.SetLevel(21);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination("overworld", new Point(19, 143));
            }
        }

        private void TransitionToFinalBossFight(bool isFinalDestination)
        {
            TransitionToEntranceToAgoraCastle(false);

            new EndorsController().CompleteAchievement();
            new EndorsController().CompleteMapMod();

            Nathan.Instance.unlockedAgoraRiftDestinations.Add(1);

            if (isFinalDestination)
            {
                nathan.SetLevel(21);
                cara.SetLevel(21);
                will.SetLevel(21);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 1);
                riftDestination = new Rift.RiftDestination("agora_castle_rooftop", new Point(20, 9));
            }
        }

        #endregion

        #region Input

        int currentSelection;
        int firstDisplayTown = 0;

        double fastScrollDelay = 0.1;
        double fastScrollInitialPause = 0.4;
        double ticker = 0.0;
        bool fastScrolling = false;

        #endregion

        #region Graphics

        Texture2D background;
        //Texture2D pointer;
        Texture2D upArrow;
        Texture2D downArrow;

        #endregion

        #region Initialization

        public GamePhaseSelectionScreen()
        {
            phaseTransitions.Add(new PhaseTransition("Just got Rift", TransitionToJustGotRift));
            phaseTransitions.Add(new PhaseTransition("Healer's Village", TransitionToHealerVillage));
            phaseTransitions.Add(new PhaseTransition("Healer's Village Blacksmith", TransitionToHealersVillageBlacksmith));
            phaseTransitions.Add(new PhaseTransition("Blind Man's Town", TransitionToBlindMansTown));
            phaseTransitions.Add(new PhaseTransition("Blind Man's Forest End", TransitionToBlindMansForestEnd));
            phaseTransitions.Add(new PhaseTransition("Burtle Boss Fight", TransitionToBurtleBossFight));
            phaseTransitions.Add(new PhaseTransition("Path to Mage Town", TransitionToPathToMageTown));
            phaseTransitions.Add(new PhaseTransition("Back To Healers Village", TransitionToBackToHealersVillage));
            phaseTransitions.Add(new PhaseTransition("Possessed Library Entrance", TransitionToPossessedLibraryEntrance));
            phaseTransitions.Add(new PhaseTransition("Possessed Library Floor 3", TransitionToPossessedLibraryFl3));
            phaseTransitions.Add(new PhaseTransition("Possessed Library Floor 4", TransitionToPossessedLibraryFl4));
            phaseTransitions.Add(new PhaseTransition("Possessed Library Floor 5", TransitionToPossessedLibraryFl5));
            phaseTransitions.Add(new PhaseTransition("Elder Mantis Boss Fight", TransitionToElderMantisBossFight));
            phaseTransitions.Add(new PhaseTransition("Endor Rift Scene", TransitionToEndorRiftScene));
            phaseTransitions.Add(new PhaseTransition("Path to Snow/Swamp Town", TransitionToPathToSnowAndSwampTown));
            phaseTransitions.Add(new PhaseTransition("Swamp Town", TransitionToSwampTown));
            phaseTransitions.Add(new PhaseTransition("Entrance To Sewn Bog", TransitionToEntranceToSewnBog));
            phaseTransitions.Add(new PhaseTransition("Chepetawa Boss Fight", TransitionToChepetawaBossFight));
            phaseTransitions.Add(new PhaseTransition("Saved Swamp Town", TransitionToSavedSwampTown));
            phaseTransitions.Add(new PhaseTransition("Snow Town", TransitionToSnowTown));
            phaseTransitions.Add(new PhaseTransition("Path to Cave Labyrinth", TransitionToPathToCaveLabyrinth));
            phaseTransitions.Add(new PhaseTransition("Cave Labyrinth", TransitionToCaveLabyrinth));
            phaseTransitions.Add(new PhaseTransition("Serlinx Boss Fight", TransitionToSerlinxBossFight));
            phaseTransitions.Add(new PhaseTransition("Back To Endor", TransitionToBackToEndor));
            phaseTransitions.Add(new PhaseTransition("Enctrance South Forest", TransitionToEntranceToSouthForest));
            phaseTransitions.Add(new PhaseTransition("Path To Agora Castle", TransitionToPathToAgoraCastle));
            phaseTransitions.Add(new PhaseTransition("Entrance To Agora Inn", TransitionToEntranceToAgoraInn));
            phaseTransitions.Add(new PhaseTransition("Entrance Agora Castle", TransitionToEntranceToAgoraCastle));
            phaseTransitions.Add(new PhaseTransition("Final Boss Fight", TransitionToFinalBossFight));
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Instruction_box");
            //pointer = content.Load<Texture2D>(interfaceTextureFolder + "Small_arrow");
            upArrow = content.Load<Texture2D>(interfaceTextureFolder + "Up_arrow");
            downArrow = content.Load<Texture2D>(interfaceTextureFolder + "Down_arrow");
        }

        #endregion

        #region Update Logic

        public override void HandleInput(GameTime gameTime)
        {
            UpdateFastScrolling(gameTime);

            if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();

                SoundSystem.Play(AudioCues.menuDeny);
            }
            else if (InputState.IsMenuDown())
            {
                AdjustPointerDown();

                SoundSystem.Play(AudioCues.menuMove);
            }
            else if (InputState.IsMenuUp())
            {
                AdjustPointerUp();

                SoundSystem.Play(AudioCues.menuMove);
            }
            else if (InputState.IsMenuSelect() && phaseTransitions.Count > 0)
            {
                phaseTransitions[currentSelection].transitionFunction(true);
                ScreenManager.Singleton.ExitAllScreensAboveTileMapScreen();
                ScreenManager.Singleton.AddScreen(new RiftTransitionScreen(riftDestination));
            }
        }

        #region Helpers


        /// <summary>
        /// Attempts to decrement the currentSelection by one.
        /// </summary>
        private void AdjustPointerUp()
        {
            currentSelection = Math.Max(currentSelection - 1, 0);
            if (currentSelection < firstDisplayTown)
            {
                firstDisplayTown = Math.Max(firstDisplayTown - 1, 0);
            }
        }


        /// <summary>
        /// Attempts to increment the currentSelection by one
        /// </summary>
        private void AdjustPointerDown()
        {
            currentSelection = Math.Min(currentSelection + 1, phaseTransitions.Count - 1);
            if (currentSelection - firstDisplayTown >= maxTownDisplay)
            {
                firstDisplayTown = Math.Min(firstDisplayTown + 1, phaseTransitions.Count - maxTownDisplay);
            }
        }


        /// <summary>
        /// Controls the Fast Scrolling effect when up or down is held.
        /// </summary>
        private void UpdateFastScrolling(GameTime gameTime)
        {
            if (!InputState.IsFastScrollUp() &&
                !InputState.IsFastScrollDown())
            {
                fastScrolling = false;
                ticker = 0;
            }
            else
            {
                ticker += gameTime.ElapsedGameTime.TotalSeconds;
                if (!fastScrolling && ticker > fastScrollInitialPause)
                {
                    fastScrolling = true;
                    ticker = 0;
                }
                else if (fastScrolling && ticker > fastScrollDelay)
                {
                    if (InputState.IsFastScrollDown())
                    {
                        AdjustPointerDown();
                        ticker = 0;
                    }
                    else if (InputState.IsFastScrollUp())
                    {
                        AdjustPointerUp();
                        ticker = 0;
                    }
                }
            }
        }


        #endregion


        #endregion

        #region Render


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color whiteColor = Color.White * TransitionAlpha;
            Color redColor = Color.Red * TransitionAlpha;

            int lastDisplayTown = Math.Min(phaseTransitions.Count, firstDisplayTown + maxTownDisplay);

            spriteBatch.Draw(background, screenLocation, null, whiteColor, 0.0f, Vector2.Zero, 1.25f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(upArrow, upArrowLocation, null,
                firstDisplayTown > 0 ? whiteColor : redColor,
                0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(downArrow, downArrowLocation, null,
                lastDisplayTown < phaseTransitions.Count ? whiteColor : redColor,
                0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            RenderTownNames(spriteBatch);
        }


        private void RenderTownNames(SpriteBatch spriteBatch)
        {
            Color color = Color.White * TransitionAlpha;

            Vector2 printNameLocation = townNameLocation;
            printNameLocation.X -= 5;

            int lastDisplayTown = Math.Min(phaseTransitions.Count, firstDisplayTown + maxTownDisplay);

            for (int i = firstDisplayTown; i < lastDisplayTown; ++i)
            {
                if (i == currentSelection)
                {
                    Vector2 position = printNameLocation + new Vector2(-17, 4);
                    spriteBatch.Draw(ScreenManager.PointerTexture, position, null, color, 0, Vector2.Zero, ScreenManager.SmallArrowScale, SpriteEffects.None, 0);
                }
                spriteBatch.DrawString(Fonts.MenuItem2, phaseTransitions[i].name, printNameLocation, Fonts.MenuItemColor * TransitionAlpha);
                printNameLocation.Y += Fonts.MenuItem2.LineSpacing;
            }
        }

        #endregion
    }
}
