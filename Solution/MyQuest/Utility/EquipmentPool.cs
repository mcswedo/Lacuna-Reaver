using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public static class EquipmentPool
    {
        static Dictionary<string, Equipment> pool;

        /// <summary>
        /// Preload all of the items we use during the game.
        /// </summary>
        public static void Initialize()
        {
            pool = new Dictionary<string, Equipment>();

            //AddEquipment(new PlainSword());
            //AddEquipment(new PlainBook());
            //AddEquipment(new PlainScythe());
        }

        public static Equipment RequestEquipment(Type equipmentType)
        {
            return RequestEquipment(equipmentType.Name);
        }

        /// <summary>
        /// Request a reference to a piece of Equipment
        /// </summary>
        /// <param name="contentName">The name of the item being requested</param>
        /// <returns>A reference to the item</returns>
        public static Equipment RequestEquipment(string equipmentClassName)
        {
            Debug.Assert(pool != null);

            Equipment equipment;

            try
            {
                equipment = pool[equipmentClassName];
            }
            catch (KeyNotFoundException)
            {
                equipment = Utility.CreateInstanceFromName<Equipment>("MyQuest", equipmentClassName) as Equipment;
                AddEquipment(equipment);
            }

            return equipment;
        }

        static void AddEquipment(Equipment equipment)
        {
            pool.Add(equipment.ClassName, equipment);
        }
    }
}
