using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    public class CombatZoneLayout
    {
        List<FightingCharacter> npcCombatants = new List<FightingCharacter>();

        Slot[] slots;

        public Slot[] Slots
        {
            get { return slots; }
            set { slots = value; }
        }

        public CombatZoneLayout(Slot[] slots)
        {
            this.slots = slots;            
        }

        public int NumberOfSlots
        {
            get { return slots.Length; }
        }

        public List<FightingCharacter> SelectMonsters(Monster [] combatZoneMonsters)
        {
            npcCombatants.Clear();
            List<NPCFightingCharacter> mediumSlotMonsters = new List<NPCFightingCharacter>();
            List<NPCFightingCharacter> largeSlotMonsters = new List<NPCFightingCharacter>();
            List<NPCFightingCharacter> hugeSlotMonsters = new List<NPCFightingCharacter>();

            // Fill our lists with monsters that have been given an explicit ammount to add into combat.
            foreach (Monster monster in combatZoneMonsters)
            {
                if (monster.NumberOfOccurrences > 0)
                {
                    for (int i = 0; i < monster.NumberOfOccurrences; i++)
                    {
                        NPCFightingCharacter npc = Utility.CreateInstanceFromName<NPCFightingCharacter>(
                            this.GetType().Namespace, monster.Name);

                        if (monster.Size == SlotSize.Medium)
                        {
                            mediumSlotMonsters.Add(npc);
                        }
                        else if (monster.Size == SlotSize.Large)
                        {
                            largeSlotMonsters.Add(npc);
                        }
                        else
                        {
                            hugeSlotMonsters.Add(npc);
                        }
                    }
                }
            }

            // We can not have more monsters than we have slots.
            Debug.Assert((mediumSlotMonsters.Count + largeSlotMonsters.Count + hugeSlotMonsters.Count) <= slots.Length);

            foreach (Slot slot in Slots)
            {
                if (slot.Size == SlotSize.Medium)
                {
                    if (mediumSlotMonsters.Count > 0)
                    {
                        NPCFightingCharacter npc = mediumSlotMonsters[0];
                        npc.ScreenPosition = slot.Center;
                        npcCombatants.Add(npc);

                        mediumSlotMonsters.RemoveAt(0);
                    }
                    else
                    {
                        fillSlotWithRandomMonster(slot, combatZoneMonsters);
                    }
                }
                else if (slot.Size == SlotSize.Large)
                {
                    if (largeSlotMonsters.Count > 0)
                    {
                        NPCFightingCharacter npc = largeSlotMonsters[0];
                        npc.ScreenPosition = slot.Center;
                        npcCombatants.Add(npc);

                        largeSlotMonsters.RemoveAt(0);
                    }
                    else
                    {
                        fillSlotWithRandomMonster(slot, combatZoneMonsters);
                    }
                }
                else
                {
                    if (hugeSlotMonsters.Count > 0)
                    {
                        NPCFightingCharacter npc = hugeSlotMonsters[0];
                        npc.ScreenPosition = slot.Center;
                        npcCombatants.Add(npc);

                        hugeSlotMonsters.RemoveAt(0);
                    }
                    else
                    {
                        fillSlotWithRandomMonster(slot, combatZoneMonsters);
                    }
                }
            }
            Debug.Assert(npcCombatants.Count == NumberOfSlots);
            return npcCombatants;
        }

        void fillSlotWithRandomMonster(Slot slot, Monster[] monsters)
        {
            List<Monster> candidateMonsters = new List<Monster>();

            //Find all monsters in our monsters array that fit in the current slot
            foreach (Monster monster in monsters)
            {
                if (monster.Size <= slot.Size)
                {
                    //If a monster's weight is greater than 1 we should have a better chance of selecting them,
                    //so we want to add multiples of the same monster for a better chance to choose it.
                    for (int i = 0; i < monster.Weight; i++)
                    {
                        candidateMonsters.Add(monster);
                    }
                }
            }

            int index = Utility.RNG.Next(0, candidateMonsters.Count);

            NPCFightingCharacter npc = Utility.CreateInstanceFromName<NPCFightingCharacter>(
                this.GetType().Namespace, candidateMonsters[index].Name);

            npc.ScreenPosition = slot.Center;
            npcCombatants.Add(npc);
        }
    }
}
