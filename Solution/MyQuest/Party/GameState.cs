using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    /// <summary>
    /// This class stores the state of the game
    /// </summary>
    public class GameState
    {
        string partyLeader;

        public string PartyLeader
        {
            get { return partyLeader; }
            set { partyLeader = value; }
        }

        List<PCFightingCharacter> fighters;

        public List<PCFightingCharacter> Fighters
        {
            get { return fighters; }
            set { fighters = value; }
        }

        bool inAgora = false;
        public bool InAgora
        {
            get { return inAgora; }
            set { inAgora = value; }
        }

        //public void CopyPartyInstances()
        //{
        //    Nathan.Instance = (Nathan)Fighters[0];
        //    if (Fighters.Count >= 2)
        //    {
        //        Cara.Instance = (Cara)Fighters[1];
        //    }
        //    if (Fighters.Count >= 3)
        //    {
        //        Will.Instance = (Will)Fighters[2];
        //    }
            
        //}

        List<string> partyAchievements;

        public List<string> PartyAchievements
        {
            get { return partyAchievements; }
            set { partyAchievements = value; }
        }

        Inventory inventory; 

        public Inventory Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        List<LogEntry> log;

        public List<LogEntry> Log
        {
            get { return log; }
            set { log = value; }
        }

        int gold;

        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        List<string> visitedTowns;

        public List<string> VisitedTowns
        {
            get { return visitedTowns; }
            set { visitedTowns = value; }  // Used only by serialization.
        }

        string lastMap;

        public string LastMap
        {
            get { return lastMap; }
            set { lastMap = value; }
        }

        Vector2 lastPartyPosition;

        public Vector2 LastPartyPosition
        {
            get { return lastPartyPosition; }
            set { lastPartyPosition = value; }
        }

        Direction lastPartyDirection;

        public Direction LastPartyDirection
        {
            get { return lastPartyDirection; }
            set { lastPartyDirection = value; }
        }

        SortedList<LayerModification> layerModifications;

        public SortedList<LayerModification> LayerModifications
        {
            get { return layerModifications; }
            set { layerModifications = value; }
        }

        SortedList<NPCModification> npcModifications;

        public SortedList<NPCModification> NPCModifications
        {
            get { return npcModifications; }
            set { npcModifications = value; }
        }

        public static GameState Default()
        {
            GameState gameState = new GameState();

            //MyContentManager.Clear();

            gameState.layerModifications = new SortedList<LayerModification>();
            gameState.npcModifications = new SortedList<NPCModification>();
            gameState.fighters = new List<PCFightingCharacter>();
            gameState.partyAchievements = new List<string>();
            gameState.inventory = new Inventory();
            gameState.log = new List<LogEntry>();
            gameState.visitedTowns = new List<string>();

            gameState.LastMap = "keepf0";
            gameState.LastPartyPosition = Utility.ToWorldCoordinates(new Point(6, 34), new Point(64, 64));

            gameState.PartyLeader = Party.nathan;

            gameState.fighters.Add(Nathan.Instance);
            gameState.fighters[0].SetLevel(12);

            gameState.fighters.Add(Sid.Instance);
            gameState.fighters[1].SetLevel(12);

            gameState.fighters.Add(Max.Instance);
            gameState.fighters[2].SetLevel(12);

            // Nathan
            gameState.fighters[0].EquipArmor(EquipmentPool.RequestEquipment("Armor"));
            gameState.fighters[0].EquipWeapon(EquipmentPool.RequestEquipment("PlainSword"));

            // Sid
            gameState.fighters[1].EquipArmor(EquipmentPool.RequestEquipment("Armor"));
            gameState.fighters[1].EquipWeapon(EquipmentPool.RequestEquipment("PlainSword"));

            // Max
            gameState.fighters[2].EquipArmor(EquipmentPool.RequestEquipment("Armor"));
            gameState.fighters[2].EquipWeapon(EquipmentPool.RequestEquipment("PlainSword"));

            gameState.Gold = 0;
         
            // Preload to eliminate delay at beginning of keep.  THis doesn't seem to work.
            MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "Friend1");
            MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "Friend2");
            MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "CharacterShadow");
            MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "will_standing");
            MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "will_walking_sprite_sheet");
       
            //SizeTest(gameState);
              
            return gameState;
        }

        /// <summary>
        /// Just testing the size of our save files. As the player
        /// progresses through the game, the save file will grow
        /// and grow but it looks like ~120K will probably be as
        /// large as it gets.
        /// </summary>
        //private static void SizeTest(GameState gs)
        //{
        //    gs.log.Add(new LogEntry("Agora", "Old man", "Insert dialog here"));
        //    gs.log.Add(new LogEntry("Healer Village", "Old woman", "Insert dialog here"));
        //    gs.log.Add(new LogEntry("Mage Town", "Blind man", "Insert dialog here"));

        //    /// We'll probably cap the conversation log at 50 entries
        //    for (int i = 0; i < 12; ++i)
        //        gs.log.Add(new LogEntry("Keepf1", "Bill", "BEGINA short piece of dialogA short piece of dialogA short piece of dialogA short piece of dialogA short piece of dialogA short piece of dialogA short piece of dialogA short piece of dialogA short piece of dialogA short piece of dialogEND"));

        //    gs.log.Add(new LogEntry("The End", "The End", "Really it's the end"));

        //    /// 15 maps with 10 layer mods each -> 150 entries
        //    for (int i = 0; i < 15; ++i)
        //    {
        //        List<LayerModification> mods = new List<LayerModification>();

        //        for (int j = 0; j < 10; ++j)
        //        {
        //            int layer = Utility.RNG.Next((int)Layer.MonsterZone);
        //            mods.Add(new LayerModification((Layer)layer, new Point(0, 0), (float)Utility.RNG.NextDouble()));
        //        }

        //        gs.layerModifications.Add("MyQuestRegularMap" + i.ToString(), mods);
        //    }
        //}
    }
}