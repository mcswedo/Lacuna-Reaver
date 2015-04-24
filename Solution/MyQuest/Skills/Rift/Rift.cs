using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    /// <summary>
    /// Instantly travel to a previously visited town.
    /// 
    /// This and related code needs to be redone to construct list from prevously visited towns.
    /// 
    /// This code still needs a lot of work to improve readability.
    /// </summary>
    public class Rift : Skill
    {
        public class RiftDestination
        {
            string name;
            string mapName;
            Point tilePosition;

            // Parameterless constructor for serialization.
            public RiftDestination()
            {
            }

            public RiftDestination(string destinationName, string mapName, Point tilePosition)
            {
                this.name = destinationName;
                this.mapName = mapName;
                this.tilePosition = tilePosition;
            }

            public RiftDestination(string mapName, Point tilePosition)
            {
                this.name = "";
                this.mapName = mapName;
                this.tilePosition = tilePosition;
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public string MapName
            {
                get { return mapName; }
                set { mapName = value; }
            }

            public Point TilePosition
            {
                get { return tilePosition; }
                set { tilePosition = value; }
            }
        }

        //public const int agoraStartIndex = 6;

        public static RiftDestination[] elathiaDestinations = 
        {
            // Rift destination when user gets rift skill. (Maybe this needs to be determined through achievement system.)
            new RiftDestination("Mushroom Hollow", Maps.healersVillage, new Point(22, 13)),              // 0
            new RiftDestination("Tamarel", Maps.blindMansTown, new Point(24, 16)),                       // 1
            new RiftDestination("Celindar", Maps.mageTown, new Point(7, 45)),                            // 2
            new RiftDestination("Chapaka", Maps.swampVillage, new Point(30, 67)),                        // 3
            new RiftDestination("Snowy Ridge", Maps.snowTown, new Point(17, 26)),                        // 4
            new RiftDestination("Zella's Cabin", Maps.snowTownOutskirtsBlacksmith, new Point(6, 11)),   // 5
        };

        public static RiftDestination[] agoraDestinations = 
        {
            new RiftDestination("Refugee Camp", Maps.agoraRefugeeArea, new Point(3, 30)),                 // 0
            new RiftDestination("Agora Castle", Maps.agoraCastleCourtyard, new Point(12, 22)),           // 1

            /*
            new RiftDestination("Swamp Village", "swamp_village", new Point(30, 67)),
            new RiftDestination("Portal direction test", "possessed_library_1ground", new Point(17, 11)),
            new RiftDestination("Game Start", Maps.keepf0, new Point(6, 34)),
            new RiftDestination("Ellaethia", "overworld", new Point(70, 95)),
            new RiftDestination("Agora", "overworld", new Point(71, 129)),
            new RiftDestination("Forbidden Cavern", "forbidden_cavern_high", new Point(1, 3)),
            new RiftDestination("Cave Labyrinth", "cave_labyrinth", new Point(93, 100)),
            new RiftDestination("Cave Labyrinth, End", "cave_labyrinth", new Point(48, 53)),
            new RiftDestination("Mage Town", "mage_town", new Point(7, 45)),
            new RiftDestination("Snow Town", "snow_town", new Point(17, 26)),
            new RiftDestination("Blind Man's Forest", "blind_mans_forest_1", new Point(14, 38)),
            new RiftDestination("Blind Man's Forest End", "blind_mans_forest_4", new Point(22, 5)),
            new RiftDestination("Possessed Library, 1st level", "possessed_library_1ground", new Point(9, 20)),
            new RiftDestination("Possessed Library, 2nd level", "possessed_library_2ground", new Point(3, 20)),
            new RiftDestination("Possessed library, 5th level", "possessed_library_5", new Point(34, 3)),
            new RiftDestination("5th Floor Keep", Maps.keepf5, new Point(5, 12)),          
            new RiftDestination("Sewn Bog 1", "sewnBog_1", new Point(12, 11)),
            new RiftDestination("Sewn Bog 7", "sewnBog_7", new Point(21, 1)),
            new RiftDestination("Sewn Bog Boss", "sewn_bog_boss", new Point(4, 3)),
            new RiftDestination("Combat Test Keep", Maps.mapCombatTestKeep, new Point(5,9)),
            new RiftDestination("Combat Test Forest", Maps.mapCombatTestForest, new Point(5,8)),
            new RiftDestination("Combat Test Cave", Maps.mapCombatTestCave, new Point(6,13)),
            new RiftDestination("Combat Test Library", Maps.mapCombatTestLibrary, new Point(5,8)),
            new RiftDestination("Combat Test Agora", Maps.mapCombatTestAgora, new Point(5,8)),
            new RiftDestination("Keep room 2", Maps.keepf2, new Point(5,2)),
            new RiftDestination("Keep room 3", Maps.keepf3, new Point(5,12)),
 */
        };

        [NonSerialized]
        int destinationIndex;

        public Rift()
        {
            Name = Strings.ZA466;
            Description = Strings.ZA467;

            MpCost = 15;
            SpCost = 5;

            SpellPower = 0;
            DamageModifierValue = 0;

            BattleSkill = false;
            MapSkill = true;
            HealingSkill = false;
            MagicSkill = false;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = true;
            CanTargetEnemy = false;
        }

        public void SetDestinationByIndex(int destinationIndex)
        {
            this.destinationIndex = destinationIndex;
        }

        public override void Update(GameTime gameTime)
        {   
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void OutOfCombatActivate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            // Deduct cost.
            actor.FighterStats.Energy -= MpCost;

            // Perform travel.
            ScreenManager.Singleton.ExitAllScreensAboveTileMapScreen();
            if (Party.Singleton.GameState.InAgora)
            {
                ScreenManager.Singleton.AddScreen(new RiftTransitionScreen(agoraDestinations[destinationIndex]));
            }
            else
            {
                ScreenManager.Singleton.AddScreen(new RiftTransitionScreen(elathiaDestinations[destinationIndex]));
            }
            SoundSystem.Play(AudioCues.Rift);
        }
    }
}
