using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace MyQuest
{
    public static class ItemPool
    {
        static Dictionary<string, Item> pool;

        /// <summary>
        /// Preload all of the items we use during the game.
        /// </summary>
        public static void Initialize()
        {
            pool = new Dictionary<string, Item>();

            AddItem(new SmallHealthPotion());
            AddItem(new MediumHealthPotion());
            AddItem(new LargeHealthPotion());
            AddItem(new HugeHealthPotion());

            AddItem(new SmallEnergyPotion());
            AddItem(new MediumEnergyPotion());
            AddItem(new LargeEnergyPotion());
            AddItem(new HugeEnergyPotion());

            AddItem(new Mushroom());

            // Level 1-5
            AddItem(new RingOfJustice());
            AddItem(new RingOfBravura()); 
            AddItem(new RingOfVeneration()); 
            AddItem(new RingOfTheSages()); 
            AddItem(new RingOfCalling());

            // Level 6-10
            AddItem(new LapizLazuliRing());
            AddItem(new PearlBand());
            AddItem(new JadeStatue());
            AddItem(new ObsidianCharm());
            AddItem(new RubyRing());

            AddItem(new NathansGift());
            AddItem(new AmuletOfInsight());

            // Level 11-15
            AddItem(new TranslucentRing());
            AddItem(new SkirmishRing());
            AddItem(new ShadowStrikeRing());
            AddItem(new DivineRing());
            AddItem(new SpikedChain());

            // Level 16-20
            AddItem(new ChainOfFlame());
            AddItem(new EarthenGloves());
            AddItem(new NatureCharm());
            AddItem(new ChainOfTheSea());
            AddItem(new ChainOfSkies());

            // Level 20-Max
            AddItem(new BattleMageRing());
            AddItem(new BattleLordsRing());
            AddItem(new BattleThiefRing());
            AddItem(new TrainingBelt());

            //Maps
            AddItem(new EllaethiaMap());
            AddItem(new HealersVillageMap());
            AddItem(new WillsForestMap());
            AddItem(new MushroomForestMap());
            AddItem(new WillsVillageMap());
            AddItem(new PossessedLibraryMap());
            AddItem(new MageTownMap());
            AddItem(new ForbiddenCavernMap());
            AddItem(new SwampVillageMap());
            AddItem(new SewnBogMap());
            AddItem(new AgoraMap());
            AddItem(new CaveLabyrinthMap());
            AddItem(new SnowTownMap());
            AddItem(new RefugeeCampMap());
        }

        public static Item RequestItem(Type itemType)
        {
            return RequestItem(itemType.Name);
        }

        /// <summary>
        /// Request a reference to an Item
        /// </summary>
        /// <param name="contentName">The name of the item being requested</param>
        /// <returns>A reference to the item</returns>
        public static Item RequestItem(string itemName)
        {
            Debug.Assert(pool != null);

            Item item;
            
            try
            {
                item = pool[itemName];
            }
            catch (KeyNotFoundException)
            {
                item = Utility.CreateInstanceFromName<Item>("MyQuest", itemName) as Item;
                AddItem(item);
            }

            return item;
        }

        static void AddItem(Item item)
        {
            // I commented out the following so there won't be unexpected crashes;
            // instead, long names will blead over the dorders of the GUI.
            //
            //if(Fonts.MenuItem2.MeasureString(item.DisplayName).X > maxDisplayNameLength)
            //{
            //    throw new Exception(item.GetType().Name + ": DisplayName exceeds "
            //                                            + maxDisplayNameLength.ToString()
            //                                            + " pixels.");
            //}

            pool.Add(item.GetType().Name, item);
        }
    }
}
