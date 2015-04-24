using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{  
    /// <summary>
    /// Defines a zone of combat.
    /// </summary>
    public class CombatZone
    {
        #region Fields


        string zoneName;

        /// <summary>
        /// The name of this combat zone.
        /// </summary>
        public string ZoneName
        {
            get { return zoneName; }
        }


        CombatZoneLayout [] layouts;

        public CombatZoneLayout[] Layouts
        {
            get { return layouts; }
            set { layouts = value; }
        }

        float probability;

        /// <summary>
        /// The probability of that entering this zone will trigger combat.
        /// </summary>
        public float Probability
        {
            get { return probability; }
        }


        string backgroundTexture;

        /// <summary>
        /// The background texture for this zone
        /// </summary>
        public string BackgroundTexture
        {
            get { return backgroundTexture; }
        }


        string cueName;

        /// <summary>
        /// The music cue for this zone
        /// </summary>
        public string CueName
        {
            get { return cueName; }
        }


        // This was made public so that test code can set the AIType on the monsters.
        public List<Monster> monsters = new List<Monster>();

        /// <summary>
        /// A list of named monsters which may appear in this zone.
        /// </summary>
        public Monster[] Monsters
        {
            get { return monsters.ToArray(); }
        }

        bool runningEnabled = true;

        /// <summary>
        /// True if the player can run from battles in this zone.
        /// </summary>
        public bool RunningEnabled
        {
            get { return runningEnabled; }
            set { runningEnabled = value; }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Construct a new CombatZone
        /// </summary>
        /// <param name="zoneName">The name of the zone</param>
        /// <param name="probability">The probability that this zone will trigger combat</param>
        /// <param name="backgroundTexture">The background texture for this zone</param>
        /// <param name="cueName">The music cue to play during combat</param>
        /// <param name="monsters">The named monsters that may appear in this zone</param>
        public CombatZone(
            string zoneName,
            float probability,
            string backgroundTexture,
            string cueName,
            CombatZoneLayout[] layouts,
            Monster[] monsters)
        {
            Debug.Assert(zoneName != null, "zoneName null");
            Debug.Assert(monsters != null, "monsters null in zone " + zoneName);
            Debug.Assert(zoneName.Equals("Empty") || monsters.Count() > 0, "Monster count zero for combat zone " + zoneName);
            this.zoneName = zoneName;
            this.probability = probability;
            this.backgroundTexture = backgroundTexture;
            this.cueName = cueName;

            this.layouts = layouts;

            if (monsters != null)
            {
                this.monsters.AddRange(monsters);
            }

            this.runningEnabled = true;
        }

        public CombatZone(
            string zoneName,
            float probability,
            string backgroundTexture,
            string cueName,
            bool runningEnabled,
            CombatZoneLayout[] layouts,
            Monster[] monsters)
        {
            Debug.Assert(zoneName != null, "zoneName null");
            Debug.Assert(monsters != null, "monsters null in zone " + zoneName);
            Debug.Assert(zoneName.Equals("Empty") || monsters.Count() > 0, "Monster count zero for combat zone " + zoneName);
            this.zoneName = zoneName;
            this.probability = probability;
            this.backgroundTexture = backgroundTexture;
            this.cueName = cueName;

            this.layouts = layouts;

            if (monsters != null)
            {
                this.monsters.AddRange(monsters);
            }

            this.runningEnabled = runningEnabled;
        }

        public CombatZone(
            string zoneName,
            float probability,
            string backgroundTexture,
            string cueName,
            Monster[] monsters)
        {
            Debug.Assert(zoneName != null, "zoneName null");
            Debug.Assert(monsters != null, "monsters null in zone " + zoneName);
            Debug.Assert(zoneName.Equals("Empty") || monsters.Count() > 0, "Monster count zero for combat zone " + zoneName);
            this.zoneName = zoneName;
            this.probability = probability;
            this.backgroundTexture = backgroundTexture;
            this.cueName = cueName;

            this.layouts = new CombatZoneLayout[] { 
                new CombatZoneLayout(new Slot[] { new Slot(SlotSize.Medium, new Vector2(650, 200)) }), 
                new CombatZoneLayout(new Slot[] { new Slot(SlotSize.Large, new Vector2(650, 200)) }), 
                new CombatZoneLayout(new Slot[] { new Slot(SlotSize.Huge, new Vector2(650, 200)) }) };

            if (monsters != null)
            {
                this.monsters.AddRange(monsters);
            }

            this.runningEnabled = true;
        }

        public CombatZone(
            string zoneName,
            float probability,
            string backgroundTexture,
            string cueName,
            Monster monster)
       {
            this.zoneName = zoneName;
            this.probability = probability;
            this.backgroundTexture = backgroundTexture;
            this.cueName = cueName;

            this.layouts = new CombatZoneLayout[] { 
                new CombatZoneLayout(new Slot[] { new Slot(SlotSize.Medium, new Vector2(650, 200)) }), 
                new CombatZoneLayout(new Slot[] { new Slot(SlotSize.Large, new Vector2(650, 200)) }), 
                new CombatZoneLayout(new Slot[] { new Slot(SlotSize.Huge, new Vector2(650, 200)) }) };

            if (monster != null)
            {
                this.monsters.Add(monster);
            }

            this.runningEnabled = true;
        }

        #endregion

        /// <summary>
        /// Used by the Editor
        /// </summary>
        public override string ToString()
        {
            return zoneName;
        }

        public CombatZoneLayout RandomlySelectLayout()
        {
            int index = Utility.RNG.Next(0, layouts.Length);

            if (layouts[index] == null)
            {
                throw new Exception("The layout at the index can not be null");
            }

            return layouts[index];
        }
    }
}
