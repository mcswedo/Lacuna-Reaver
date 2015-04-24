using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace MyQuest
{
    public enum ModAction
    {
        Add,
        Remove,
    }

    public enum Layer
    {
        Ground,
        Foreground,
        Fringe,
        Collision,
        Damage,
        MonsterZone
    }
    
    public class Party
    {
        internal const string nathan = "Nathan";
        internal const string cara = "Cara";
        internal const string will = "Will";
        internal const string sid = "Sid";
        internal const string max = "Max";
        internal const string pccara = "PCCara";
        internal const string pcwill = "PCWill";

        private PCMapCharacter nathanMapCharacter = null;
        private PCMapCharacter caraMapCharacter = null;
        private PCMapCharacter willMapCharacter = null;

        internal const string saveFileName = "save.xml";

        #region Singleton


        static Party singleton = new Party();

        public static Party Singleton
        {
            get { return singleton; }
        }


        private Party()
        {
        }


        #endregion

        #region Party Members


        PCMapCharacter leader;

        public PCMapCharacter Leader
        {
            get { return leader; }
        }


        #endregion

        #region GameState


        GameState gameState;

        public GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        public bool CanContinue
        {
            get { return File.Exists(saveFileName); }
        }

        public void SaveGameState(string saveFileName)
        {
            gameState.LastMap = CurrentMap.Name;
            gameState.LastPartyPosition = Leader.WorldPosition;
            PartySerializer2.SaveTo(saveFileName);
        }

        #endregion

        #region Maps

        Dictionary<string, Map> mapCache;

        Map currentMap;

        public Map CurrentMap
        {
            get { return currentMap; }
        }

        List<NPCModification> tempNPCMods;
        List<LayerModification> tempLayerMods;
        
        #region Methods


        /// <summary>
        /// Helper for retrieving a map from the cache. If the map
        /// is not found, we attempt to load it from disk.
        /// </summary>
        Map GetMap(string mapName)
        {
            if (!mapCache.ContainsKey(mapName))
            {
                Map map = MyContentManager.LoadMap(mapName);
                if (map.Name != mapName)
                {
                    GameLoop.MessageBox(new IntPtr(0), "Map name does not match file name: " + mapName, "Error", 0);
                }
                ApplyModifications(map);
                map.Initialize();
                mapCache.Add(map.Name, map);         
            }
            return mapCache[mapName];
        }

        /// <summary>
        /// Helper for setting the CurrentMap
        /// </summary>
        void SetCurrentMap(string mapName)
        {
            // Save changes to current map before switching to new current map.
            if (CurrentMap != null)
            {
                foreach (NPCModification npcMod in tempNPCMods)
                {
                    ApplyNPCMod(CurrentMap, npcMod);
                }
                foreach (LayerModification layerMod in tempLayerMods)
                {
                    ApplyLayerMod(CurrentMap, layerMod);
                }
            }

            tempNPCMods.Clear();
            tempLayerMods.Clear();

            currentMap = GetMap(mapName);
            ApplyModifications(currentMap); 
            currentMap.Reset();
            CenterMapOnScreen(CurrentMap);

            // If the new map is a riftable destination, then unlock it.

            if (Party.Singleton.GameState.InAgora)
            {
                for (int i = 0; i < Rift.agoraDestinations.Length; ++i)
                {
                    Rift.RiftDestination destination = Rift.agoraDestinations[i];
                    if (destination.MapName == currentMap.Name)
                    {
                        if (!Nathan.Instance.unlockedAgoraRiftDestinations.Contains(i))
                        {
                            Nathan.Instance.unlockedAgoraRiftDestinations.Add(i);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Rift.elathiaDestinations.Length; ++i)
                {
                    Rift.RiftDestination destination = Rift.elathiaDestinations[i];
                    if (destination.MapName == currentMap.Name)
                    {
                        if (!Nathan.Instance.unlockedElathiaRiftDestinations.Contains(i))
                        {
                            Nathan.Instance.unlockedElathiaRiftDestinations.Add(i);
                        }
                    }
                }
            }

            MusicSystem.Play(CurrentMap.MusicCueName);
        }

        public void ActivatePortalCutScene(Portal portal)
        {
            if (portal.DestinationDirection.HasValue)
            {
                ScreenManager.Singleton.AddScreen(new PortalCutSceneScreen(portal));
            }
            else
            {
                PortalToMap(portal);
            }
        }

        /// <summary>
        /// Portals to the map specified by a given portal.
        /// </summary>

        public void PortalToMap(Portal portal)
        {
            SoundSystem.Play(portal.SoundCueName);

            SetCurrentMap(portal.DestinationMap);

            if (portal.DestinationPosition.X < 0)
            {
                Debug.Assert(portal.DestinationPosition.Y < 0);
                // Leave position unchanged.
            }
            else
            {
                Leader.TilePosition = portal.DestinationPosition;
                Leader.WorldPosition = Utility.ToWorldCoordinates(portal.DestinationPosition, CurrentMap.TileDimensions);
            }

            if (portal.DestinationDirection.HasValue)
            {
                Leader.Direction = portal.DestinationDirection.Value;
            }

            Camera.Singleton.CenterOnTarget(
                 Party.Singleton.Leader.WorldPosition,
                 Party.Singleton.CurrentMap.DimensionsInPixels,
                 ScreenManager.Singleton.ScreenResolution);

//            Leader.justPortaled = true;
            //CurrentMap.SetOccupancy(Leader.TilePosition, true);
        }

        void CenterMapOnScreen(Map map)
        {
            int tx = ScreenManager.Singleton.ScreenResolution.Width / 2 - map.DimensionsInPixels.X / 2;
            int ty = ScreenManager.Singleton.ScreenResolution.Height / 2 - map.DimensionsInPixels.Y / 2;
            if (tx < 0)
            {
                tx = 0;
            }
            if (ty < 0)
            {
                ty = 0;
            }
            Camera.Singleton.SetMapCenteringTranslation(tx, ty);
        }

        public void SetPartyLeaderCara()
        {
            leader = caraMapCharacter;
            leader.WorldPosition = gameState.LastPartyPosition;
            leader.Direction = gameState.LastPartyDirection;
            leader.Initialize(CurrentMap);
        }

        public void SetPartyLeaderNathan()
        {
            leader = nathanMapCharacter;
            leader.WorldPosition = gameState.LastPartyPosition;
            leader.Direction = gameState.LastPartyDirection;
            leader.Initialize(CurrentMap);
        }

        public void SetPartyLeaderWill()
        {
            leader = willMapCharacter;
            leader.WorldPosition = gameState.LastPartyPosition;
            leader.Direction = gameState.LastPartyDirection;
            leader.Initialize(CurrentMap);
        }

        #endregion


        #endregion

        #region Initialization


        /// <summary>
        /// Allow the party to initialize all data necessary for game play.
        /// This method is called once before every game starts.
        /// </summary>
        public void Initialize(bool isNewGame)
        {
            MyContentManager.ClearAllMaps();
            TurnExecutor.Singleton.FinalBattle = false; 
            if (isNewGame)
            {
                Nathan.Instance.Reset();
                Sid.Instance.Reset();
                Max.Instance.Reset();
                Cara.Instance.Reset();
                Will.Instance.Reset();
                gameState = GameState.Default();
            }

            mapCache = new Dictionary<string, Map>();
            if (gameState.Fighters.Count == 2)
            {
                Will.Instance.Reset();
            }
            else if (gameState.Fighters.Count == 1)
            {
                Cara.Instance.Reset();
                Will.Instance.Reset();
            }

            tempLayerMods = new List<LayerModification>();
            tempNPCMods = new List<NPCModification>();

            SetCurrentMap(gameState.LastMap);

            LoadParty();

            foreach (FightingCharacter fighter in Party.Singleton.GameState.Fighters)
            {
                fighter.FighterStats.ReapplyStatModifiers();
            }

            Camera.Singleton.CenterOnTarget(
                Leader.WorldPosition,
                CurrentMap.DimensionsInPixels,
                ScreenManager.Singleton.ScreenResolution);

        }

        
        /// <summary>
        /// Create and load each party member
        /// </summary>
        private void LoadParty()
        {
            EventHandler newTileReachedEventHandler = new System.EventHandler(TileMapScreen.Instance.Leader_newTileReachedEvent);
            nathanMapCharacter = MyContentManager.LoadPCMapCharacter(ContentPath.ToPCMapCharacters + nathan);
            nathanMapCharacter.LoadContent();
            nathanMapCharacter.newTileReachedEvent += newTileReachedEventHandler;

            caraMapCharacter = MyContentManager.LoadPCMapCharacter(ContentPath.ToPCMapCharacters + pccara);
            caraMapCharacter.LoadContent();
            caraMapCharacter.newTileReachedEvent += newTileReachedEventHandler;

            willMapCharacter = MyContentManager.LoadPCMapCharacter(ContentPath.ToPCMapCharacters + pcwill);
            willMapCharacter.LoadContent();
            willMapCharacter.newTileReachedEvent += newTileReachedEventHandler;

            SetPartyLeaderNathan();

            foreach (PCFightingCharacter fighter in gameState.Fighters)
            {
                fighter.LoadContent();
            }
        }


        #endregion

        #region Register Modifications


        internal void AddLogEntry(string location, string speaker, params string[] text)
        {
            LogEntry newLogEntry = new LogEntry(location, speaker, text);
            foreach (LogEntry entry in gameState.Log)
            {
                if (entry.Dialog.Equals(newLogEntry.Dialog))
                {
                    return;
                }
            }
            gameState.Log.Add(newLogEntry);
        }

        internal void ClearAllLogEntries()
        {
            gameState.Log.Clear();
        }

        internal void AddAchievement(string achievement)
        {
            if (!gameState.PartyAchievements.Contains(achievement))
                gameState.PartyAchievements.Add(achievement);
        }

        internal void ModifyMapLayer(string mapName, Layer layer, Point tilePosition, float newValue, bool permanent)
        {
            if (CurrentMap.Name != mapName && permanent == false)
                return;

            LayerModification layerMod = new LayerModification(layer, tilePosition, newValue);

            if (permanent)
            {
                if (!gameState.LayerModifications.ContainsKey(mapName))
                    gameState.LayerModifications.Add(mapName, new List<LayerModification>());

                gameState.LayerModifications[mapName].Add(layerMod);
            }
            else
            {
                float value = 1;
                int index = CurrentMap.GetIndex(tilePosition);

                switch (layer)
                {
                    case Layer.Ground:
                        value = CurrentMap.GroundLayer[index];
                        break;
                    case Layer.Foreground:
                        value = CurrentMap.ForeGroundLayer[index];
                        break;
                    case Layer.Fringe:
                        value = CurrentMap.FringeLayer[index];
                        break;
                    case Layer.Damage:
                        value = CurrentMap.DamageLayer[index];
                        break;
                    case Layer.Collision:
                        value = CurrentMap.CollisionLayer[index];
                        break;
                    case Layer.MonsterZone:
                        value = CurrentMap.CombatLayer[index];
                        break;
                    default:
                        throw new Exception("Unsupported layer mod");
                }

                tempLayerMods.Add(
                    new LayerModification(layer, tilePosition, value));
            }

            if (currentMap.Name == mapName)
            {
                ApplyLayerMod(currentMap, layerMod);
            }
        }

        internal void ModifyNPC(string mapName, string assetName, Point spawnLocation, ModAction modAction, bool permanent)
        {
            if (CurrentMap.Name != mapName && permanent == false)
                return;

            Map map = GetMap(mapName);

            NPCModification mod = new NPCModification(modAction, new NPCEntry { AssetName = assetName, SpawnPosition = spawnLocation });

            if (permanent == true)
            {
                if (!gameState.NPCModifications.ContainsKey(mapName))
                    gameState.NPCModifications.Add(mapName, new List<NPCModification>());

                gameState.NPCModifications[mapName].Add(mod);
            }
            else
            {
                NPCModification tempMod = new NPCModification(modAction, new NPCEntry { AssetName = assetName, SpawnPosition = spawnLocation });

                if (modAction == ModAction.Remove)
                {
                    tempMod.Action = ModAction.Add;
                    foreach (NPCEntry entry in map.NpcEntries)
                    {
                        if (entry.AssetName == assetName)
                        {
                            tempMod.Entry.SpawnPosition = entry.SpawnPosition;
                            break;
                        }
                    }
                }
                else
                {
                    tempMod.Action = ModAction.Remove;
                }

                tempNPCMods.Add(tempMod);
            }

            ApplyNPCMod(map, mod);

            if (map.Name == CurrentMap.Name)
            {
                map.Reset();
            }
        }

        internal void ModifyNPC(string mapName, string assetName, Point spawnLocation, Direction spawnDirection, bool idleOnly, ModAction modAction, bool permanent)
        {
            if (CurrentMap.Name != mapName && permanent == false)
                return;

            Map map = GetMap(mapName);

            NPCModification mod = new NPCModification(modAction, new NPCEntry { AssetName = assetName, SpawnPosition = spawnLocation, SpawnDirection = spawnDirection, ChangeIdle = true, IdleOnly = idleOnly});

            if (permanent == true)
            {
                if (!gameState.NPCModifications.ContainsKey(mapName))
                    gameState.NPCModifications.Add(mapName, new List<NPCModification>());

                gameState.NPCModifications[mapName].Add(mod);
            }
            else
            {
                NPCModification tempMod = new NPCModification(modAction, new NPCEntry { AssetName = assetName, SpawnPosition = spawnLocation, SpawnDirection = spawnDirection, ChangeIdle = true, IdleOnly = idleOnly });

                if (modAction == ModAction.Remove)
                {
                    tempMod.Action = ModAction.Add;
                    foreach (NPCEntry entry in map.NpcEntries)
                    {
                        if (entry.AssetName == assetName)
                        {
                            tempMod.Entry.SpawnPosition = entry.SpawnPosition;
                            tempMod.Entry.SpawnDirection = entry.SpawnDirection;
                            tempMod.Entry.ChangeIdle = entry.ChangeIdle;
                            tempMod.Entry.ChangeIdle = entry.IdleOnly;
                            break;
                        }
                    }
                }
                else
                {
                    tempMod.Action = ModAction.Remove;
                }

                tempNPCMods.Add(tempMod);
            }

            ApplyNPCMod(map, mod);

            if (map.Name == CurrentMap.Name)
            {
                map.Reset();
            }
        }

        internal void MapMusicSwitch(Map currentMap)
        {
            if (currentMap.Name == Maps.overworld && Party.Singleton.Leader.TilePosition.Y >= 110)
            {
                currentMap.MusicCueName = AudioCues.agoraOverworld;
            }

            else if (currentMap.Name == Maps.overworld && Party.Singleton.Leader.TilePosition.Y <= 109)
            {
                currentMap.MusicCueName = AudioCues.overworld;
            }
        }

        #endregion

        #region ApplyModifications


        /// <summary>
        /// Helper for applying all modifications to a given map.
        /// </summary>
        private void ApplyModifications(Map map)
        {
            ApplyLayerMods(map);
            ApplyNPCMods(map);
        }

        /// <summary>
        /// Helper for applying npc modifications to a given map.
        /// </summary>
        /// <remarks>This should be called before the map is reset</remarks>
        private void ApplyNPCMods(Map map)
        {
            List<NPCModification> npcMods;

            /// If there aren't any mods for this map
            if (!gameState.NPCModifications.TryGetValue(map.Name, out npcMods))
            {
                return;
            }

            foreach (NPCModification npcMod in npcMods)
            {
                ApplyNPCMod(map, npcMod);            
            }
        }

        /// <summary>
        /// Helper for applying a given npc modification to a given map.
        /// </summary>
        private void ApplyNPCMod(Map map, NPCModification npcMod)
        {
            if (npcMod.Action == ModAction.Add)
            {
                for (int i = 0; i < map.NpcEntries.Count; ++i)
                {
                    if (map.NpcEntries[i].AssetName == npcMod.Entry.AssetName)
                    {
                        return; 
                    }
                }
                
                map.NpcEntries.Add(npcMod.Entry);
            }
            else
            {
                for (int i = 0; i < map.NpcEntries.Count; ++i)
                {
                    if (map.NpcEntries[i].AssetName == npcMod.Entry.AssetName)
                    {
                        map.NpcEntries.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Helper for applying layer modifications to a given map.
        /// </summary>
        private void ApplyLayerMods(Map map)
        {
            List<LayerModification> modList;

            /// If there aren't any mods for this map
            if (!gameState.LayerModifications.TryGetValue(map.Name, out modList))
            {
                return;
            }

            foreach (LayerModification layerMod in modList)
            {
                ApplyLayerMod(map, layerMod);
            }
        }

        /// <summary>
        /// Helper for applying a given layer modification to a given map.
        /// </summary>
        private void ApplyLayerMod(Map map, LayerModification layerMod)
        {
            int index = map.GetIndex(layerMod.Location);

            switch (layerMod.TargetLayer)
            {
                case Layer.Collision:
                    map.CollisionLayer[index] = layerMod.NewValue;
                    break;
                case Layer.Damage:
                    map.DamageLayer[index] = layerMod.NewValue;
                    break;
                case Layer.Foreground:
                    map.ForeGroundLayer[index] = (short)layerMod.NewValue;
                    break;
                case Layer.Fringe:
                    map.FringeLayer[index] = (short)layerMod.NewValue;
                    break;
                case Layer.Ground:
                    map.GroundLayer[index] = (short)layerMod.NewValue;
                    break;
                case Layer.MonsterZone:
                    map.CombatLayer[index] = (short)layerMod.NewValue;
                    break;
            }
        }
  

        #endregion

        #region FightingCharacters

        public void RemoveFightingCharacter(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));

            for (int i = 0; i < gameState.Fighters.Count; ++i)
            {
                if (gameState.Fighters[i].Name == name)
                {
                    gameState.Fighters.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveFightingCharacter(PCFightingCharacter character)
        {
            gameState.Fighters.Remove(character);
        }

        public PCFightingCharacter AddFightingCharacter(PCFightingCharacter character)
        {
            character.LoadContent();
            gameState.Fighters.Add(character);
            return character;
        }

        public PCFightingCharacter AddFightingCharacter(string name)
        {
            //PCFightingCharacter character = Utility.CreateInstanceFromName<PCFightingCharacter>(this.GetType().Namespace, name);
            PCFightingCharacter character = null;
            if (name.Equals(Party.nathan))
            {
                character = Nathan.Instance;
            }
            else if (name.Equals(Party.cara))
            {
                character = Cara.Instance;
            }
            else if (name.Equals(Party.will))
            {
                character = Will.Instance;
            }
            else if (name.Equals(Party.sid))
            {
                character = Sid.Instance;
            }
            else if (name.Equals(Party.max))
            {
                character = Max.Instance;
            }
            else
            {
                throw new Exception("Unknown party member name.");
            }
            character.LoadContent();
            gameState.Fighters.Add(character);
            return character;
        }

        public PCFightingCharacter GetFightingCharacter(string name)
        {
            foreach (PCFightingCharacter character in gameState.Fighters)
            {
                if (character.Name == name)
                {
                    return character;
                }
            }
            return null; 
        }

        // This method is for battle test code.
        public void RemoveAllFightingCharacters()
        {
            while (gameState.Fighters.Count > 0)
            {
                gameState.Fighters.RemoveAt(0);
            }
        }
        
        #endregion

        /// <summary>
        /// Get a readonly list of the party's achievements
        /// </summary>
        public IList<string> PartyAchievements
        {
            get { return gameState.PartyAchievements; }
        }
    }
}