using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyQuest
{
    public class TileMapScreen : Screen
    {
        public Texture2D DialogBubble  // Loaded in this class, but used by NPCMapCharacter.Draw().
        {
            get;
            private set;
        }

        public NPCMapCharacter NpcWithDialogBubble  // When set, it will be drawn.
        {
            private get;
            set;
        } 

        bool isCollisionDisplay = false;  // For debugging
        bool isDamageDisplay = false;     // For debugging
        bool isPortalDisplay = false;     // For debugging

        bool isCombatActive = true;
        bool isDamagedByTile = false;
        int damage;
        int newDamageTileCounter = 0;
        double secondsSinceLastCombat = 0;

        static readonly TimeSpan DamageOverlayTimer = TimeSpan.FromSeconds(0.4);
        TimeSpan damageOverlayTimer;

        static TileMapScreen instance = new TileMapScreen();

        public static TileMapScreen Instance
        {
            get { return instance; }
        }

        private TileMapScreen()
        {
        }

        public static void Reset()
        {
            instance.newDamageTileCounter = 0;
            instance.secondsSinceLastCombat = 0;
            instance.isCombatActive = true;
            instance.isCutScenePlaying = false;
        }

        bool isCutScenePlaying = false;  // When true, newTileReachedEvent does nothing.
        public bool IsCutScenePlaying
        {
            set { isCutScenePlaying = value; }
        }

        public override void LoadContent(ContentManager content)
        {
            DialogBubble = content.Load<Texture2D>(interfaceTextureFolder + "Dialog_Bubble");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            if (WillHandleUserInput)
            {
                Party.Singleton.CurrentMap.Update(gameTime);
                Party.Singleton.Leader.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

                secondsSinceLastCombat += gameTime.ElapsedGameTime.TotalSeconds;

                foreach (NPCMapCharacter npc in Party.Singleton.CurrentMap.NPC)
                {
                    if (Party.Singleton.Leader.BoundingBox.Intersects(npc.BoundingBox))
                    {
                        npc.CollidingWithPlayer = true;
                        if (npc.IdleOnly && npc.ControllerName != "NullController")
                        {
                            Party.Singleton.Leader.CollideWithNpc(npc, Party.Singleton.CurrentMap); // We're turning off collision with npc's.
                        }
                    }
                    else
                    {
                        npc.CollidingWithPlayer = false;
                    }

                    if ((npc.BoundingBox.Intersects(Party.Singleton.Leader.InteractionBox) ||
                        npc.BoundingBox.Intersects(Party.Singleton.Leader.BoundingBox))
                        && npc.ControllerName != "NullController")
                    {
                        npc.CanInteract = true;
                    }
                    else
                    {
                        npc.CanInteract = false;
                    }
                }

                Camera.Singleton.CenterOnTarget(
                    Party.Singleton.Leader.WorldPosition,
                    Party.Singleton.CurrentMap.DimensionsInPixels,
                    ScreenManager.Singleton.ScreenResolution);
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
#if DEBUG
            TestingStuff();
#endif
            if (InputState.IsOpenStatusScreen())
            {
                ScreenManager.AddScreen(new StatusScreen());
            }
            else if (InputState.IsViewMap())
            {
                if (Party.Singleton.CurrentMap.Name == "overworld")
                {
                    if (Party.Singleton.Leader.TilePosition.Y > 115)
                    {
                        if (Party.Singleton.GameState.Inventory.containsMapItem("agora"))
                        {
                            ExitAfterTransition();
                            ScreenManager.AddScreen(new ViewMapScreen());
                        }
                        else
                        {
                            SoundSystem.Play(AudioCues.menuDeny);
                        }
                    }

                    else
                    {
                        if (Party.Singleton.GameState.Inventory.containsMapItem("ellaethia"))
                        {
                            ExitAfterTransition();
                            ScreenManager.AddScreen(new ViewMapScreen());
                        }
                        else
                        {
                            SoundSystem.Play(AudioCues.menuDeny);
                        }
                    }
                }
                else
                {
                    if (Party.Singleton.GameState.Inventory.containsMapItem(Party.Singleton.CurrentMap.Name) || Party.Singleton.GameState.Inventory.containsSubMap(Party.Singleton.CurrentMap.Name))
                    {
                        ExitAfterTransition();
                        ScreenManager.AddScreen(new ViewMapScreen());
                    }
                    else
                    {
                        SoundSystem.Play(AudioCues.menuDeny);
                    }
                }
            }
            else if (InputState.IsPartyInteract())
            {
                TryTriggerNPC();
            }

            //Camera.Singleton.CenterOnTarget(
            //    Party.Singleton.Leader.WorldPosition,
            //    Party.Singleton.CurrentMap.DimensionsInPixels,
            //    ScreenManager.Singleton.ScreenResolution); //causes a bug in viewMap screen.
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;
            Map map = Party.Singleton.CurrentMap;

            GameLoop.Instance.BeginTileMapDraw();
            map.AltDrawGroundLayer(spriteBatch);

            map.AltDrawForeGroundLayer(spriteBatch);

            Party.Singleton.Leader.AltDrawShadow(spriteBatch, new Vector2(8, 44));

            GameLoop.Instance.RestoreNormalDraw();

            map.AltDrawNPCCharacters(spriteBatch);

//            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);


            //Party.Singleton.Leader.AltDraw(spriteBatch);

            if (isDamagedByTile && damageOverlayTimer > TimeSpan.Zero)
            {
                damageOverlayTimer -= gameTime.ElapsedGameTime;
                Party.Singleton.Leader.AltDraw(spriteBatch, Color.Red);
            }
            else
            {
                damageOverlayTimer = DamageOverlayTimer;
                Party.Singleton.Leader.AltDraw(spriteBatch);
                isDamagedByTile = false;
            }

            GameLoop.Instance.BeginTileMapDraw();

            map.AltDrawFringeLayer(spriteBatch);

            GameLoop.Instance.RestoreNormalDraw();


            if (NpcWithDialogBubble != null)
            {
//                spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);
                NpcWithDialogBubble.AltDrawDialogBubble(spriteBatch);
//                spriteBatch.End();
                NpcWithDialogBubble = null;
            }


            if (isCollisionDisplay)
            {
                GameLoop.Instance.BeginTileMapDraw();
                map.AltDrawCollisionLayer(spriteBatch);
                GameLoop.Instance.RestoreNormalDraw();

//                spriteBatch.Begin();
//                spriteBatch.DrawString(Fonts.MenuItem2, "Leader at: " + Party.Singleton.Leader.TilePosition.ToString(), new Vector2(100, 100), Fonts.MenuItemColor * TransitionAlpha);
//                spriteBatch.End();
            }

            if (isPortalDisplay)
            {
                GameLoop.Instance.BeginTileMapDraw();
                map.AltDrawPortals(spriteBatch);
                GameLoop.Instance.RestoreNormalDraw();
            }

            if (isDamageDisplay)
            {
                GameLoop.Instance.BeginTileMapDraw();
                map.AltDrawDamageLayer(spriteBatch);
                GameLoop.Instance.RestoreNormalDraw();
            }

            //ScreenManager.TintBackBuffer(damageAlpha, Color.Red); //GW: experimenting with showing damage taken by a tile
        }
   
        bool TryTriggerNPC()
        {
            NPCMapCharacter npc = null;

            foreach (NPCMapCharacter mapNPC in Party.Singleton.CurrentMap.NPC)
            {
                if (mapNPC.BoundingBox.Intersects(Party.Singleton.Leader.InteractionBox) || mapNPC.BoundingBox.Intersects(Party.Singleton.Leader.BoundingBox))
                {
                    //if (!mapNPC.CollidingWithPlayer)
                    //{
                        npc = mapNPC;
                        break;
                    //}
                }
            }

            if (npc == null)
            {
                return false;
            }

            Direction faceDirection = Direction.South;
            string leaderDirection = Party.Singleton.Leader.Direction.ToString();

            if (leaderDirection.Contains("West"))
            {
                faceDirection = Direction.East;
            }
            else if (leaderDirection.Contains("East"))
            {
                faceDirection = Direction.West;
            }
            else if (leaderDirection == "South")
            {
                faceDirection = Direction.North;
            }

            npc.FaceDirection(faceDirection);
            npc.Interact();

            return true;
        }

        #region New Tile Event

        public void Leader_newTileReachedEvent(object sender, System.EventArgs e)
        {
            if (isCutScenePlaying)
            {
                return;
            }
            damage = (int)(Party.Singleton.CurrentMap.GetDamageValue(Party.Singleton.Leader.TilePosition) * GlobalSettings.MaxMapTileDamage);
          
            if (damage > 0)
            {
                newDamageTileCounter++;
                //damageAlpha = 1 - (float)Party.Singleton.GameState.Fighters[0].Stats.Health /  //GW: experimenting with showing damage taken by a tile
                //                  (float)Party.Singleton.GameState.Fighters[0].Stats.MaxHealth;

                // play a sound effect?
                // provide a visual cue? (i.e. screen flashes red)
                // display the damage being dealt to the party like we would in combat?
                foreach (PCFightingCharacter character in Party.Singleton.GameState.Fighters)
                {
                    if (character.State != State.Dead)
                    {
                        character.FighterStats.Health -= damage;

                        if (newDamageTileCounter == 1)
                        {
                            SoundSystem.Play(AudioCues.Fireball);
                            isDamagedByTile = true;
                        }

                        if (newDamageTileCounter >= 5)
                        {
                            newDamageTileCounter = 0;
                        }

                        if (character.FighterStats.Health <= 0)
                        {
                            character.FighterStats.Health = 1;
                        }
                    }
                }
            }

            if (TryTriggerPortal())
            {
                TryTriggerCutScene();
            }
            else if (TryTriggerCutScene())
            {
            }
            else if (isCombatActive)
            {
                TryTriggerCombat();
            }
        }

        bool TryTriggerCombat()
        {
            if (secondsSinceLastCombat < 7)
            {
                return false;
            }

            CombatZone zone = CombatZonePool.Singleton.GetZone(
                Party.Singleton.CurrentMap.GetCombatId(Party.Singleton.Leader.TilePosition));

            if (zone != CombatZonePool.emptyZone)
            {
                if ((float)Utility.RNG.NextDouble() < zone.Probability)
                {
                    //ScreenManager.AddScreen(new CombatScreen(zone));
                    ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
                    //ScreenState = MyQuest.ScreenState.Hidden;
                    secondsSinceLastCombat = 0;
                    return true;
                }

            }
            return false;
        }

       public bool TryTriggerCutScene()
        {
            CutSceneEntry entry = Party.Singleton.CurrentMap.GetCutSceneAt(Party.Singleton.Leader.TilePosition);

            if (entry == null)
                return false;

            CutSceneScreen screen = Utility.CreateInstanceFromName<CutSceneScreen>(
                this.GetType().Namespace,
                entry.AssetName);

            if (screen.CanPlay())
            {
                ScreenManager.AddScreen(screen);
                return true;
            }

            return false;
        }      

        bool TryTriggerPortal()
        {
            Portal portal = Party.Singleton.CurrentMap.GetPortalAt(Party.Singleton.Leader.TilePosition);

            if (portal == null)
                return false;

            Party.Singleton.ActivatePortalCutScene(portal);

            return true;
        }

        #endregion

        #region Test code

        void TestingStuff()
        {
            if (InputState.Temp_IsSaveGame())
            {
                Party.Singleton.SaveGameState(Party.saveFileName);
            }

            if (InputState.Temp_IsToggleCollisionDisplay())
            {
                isCollisionDisplay = !isCollisionDisplay;
                isDamageDisplay = !isDamageDisplay;
                GlobalSettings.CharDebugInfo = !GlobalSettings.CharDebugInfo;
            }

            if (InputState.Temp_IsTogglePortalDisplay())
            {
                isPortalDisplay = !isPortalDisplay;
            }

            if (InputState.Temp_IsToggleCombatActive())
            {
                isCombatActive = !isCombatActive;
            }

            if (InputState.Temp_IsDebugTextMode())
            {
                if (GlobalSettings.DialogueLetterDelay == TimeSpan.FromSeconds(0.045))
                    GlobalSettings.DialogueLetterDelay = TimeSpan.Zero;
                else
                    GlobalSettings.DialogueLetterDelay = TimeSpan.FromSeconds(0.045);
            }

            if (InputState.Temp_IsTest())
            {
                Test();
            }

            //if (InputState.IsStockAndDisplayItemShopScreen())
            //{
            //    List<SaleItem> testStock = new List<SaleItem>()
            //    {
            //        new SaleItem(typeof(SmallHealthPotion), 10),
            //        new SaleItem(typeof(MediumHealthPotion), 20),
            //        new SaleItem(typeof(LargeHealthPotion), 30),

            //        new SaleItem(typeof(SmallEnergyPotion), 20),
            //        new SaleItem(typeof(MediumEnergyPotion), 30),
            //        new SaleItem(typeof(LargeEnergyPotion), 40)
            //    };

            //    ScreenManager.AddScreen(new ItemShopScreen(testStock));
            //}
            //if (InputState.IsHelpScreen())
            //{
            //    ScreenManager.AddScreen(new HelpScreen());
            //}
            if (InputState.Temp_IsDebugInfoKeyPress())
            {
                MessageOverlay.AddMessage("Map: " + Party.Singleton.CurrentMap.Name, 5);
                MessageOverlay.AddMessage("Tile Pos: (" + Party.Singleton.Leader.TilePosition.X + ", " + Party.Singleton.Leader.TilePosition.Y + ")", 5);
            }
        }


        /**
         * Put any thing you want in here.  Trigger this code in the tile map screen by pressing '0'.
         */
        void Test()
        {
#if DEBUG
            //GabrielTest();
            //TurnerTest();
            //AdamTest();
            //MattTest();
            //TheoTest();
            //JaredTest();
            KyleTest();
            //GerrenTest();
            //FinalBattleTest(); 
            //GlenTest();
            //VideoTest();
#endif
        }

        int testIncrementor = 0;
        void GerrenTest()
        {
            Party.Singleton.RemoveAllFightingCharacters();

            //PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
           
            PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            
           PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);

           PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
            
            will.SetLevel(25);
            cara.SetLevel(25);
            nathan.SetLevel(25);
  
            //Party.Singleton.GameState.Inventory.AddItem(typeof(LargeHealthPotion), 20);

            Monster[] monsters = new Monster[] { new Monster(Monster.ghost, 1, 1)};
            CombatZone zone = new CombatZone("", 0f, CombatZonePool.swampBG, AudioCues.battleCue, CombatZonePool.oneLargeLayoutCollection, monsters);
            
            ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
            //ScreenManager.Singleton.AddScreen(new CombatScreen(CombatZonePool.chepetawaZone));
           
        }
        void FinalBattleTest()
        {
            Party.Singleton.RemoveAllFightingCharacters();

            PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);

            nathan.SetLevel(25);
            TurnExecutor.Singleton.FinalBattle = true;

            Nathan.Instance.SkillNames.Clear();
            Nathan.Instance.AddSkillName("BattleRift");
        
            ScreenManager.AddScreen(new EnterCombatTransitionScreen(CombatZonePool.malticarZone2, ScreenState));        

        }
        void KyleTest()
        {
            if (testIncrementor == 0)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
                PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
                nathan.SetLevel(4);
                Equipment sword = EquipmentPool.RequestEquipment("PlainSword");
                Equipment armor = EquipmentPool.RequestEquipment("PlainArmor"); //Advanced Armor 12+
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                Item accessory1 = ItemPool.RequestItem(typeof(RingOfJustice));
                Nathan.Instance.EquipAccessory(accessory1 as Accessory, 0);
                Item accessory2 = ItemPool.RequestItem(typeof(RingOfJustice));
                Nathan.Instance.EquipAccessory(accessory2 as Accessory, 1);
                //Item accessory3 = ItemPool.RequestItem(typeof(NathansGift));
                //Nathan.Instance.EquipAccessory(accessory3 as Accessory, 2);
                Party.Singleton.AddAchievement(HealersBlacksmithsController.receivedSwordAchievement);
                cara.SetLevel(25);
                Equipment sword1 = EquipmentPool.RequestEquipment("PlainBook"); //AdvancedBook 10+
                Equipment armor1 = EquipmentPool.RequestEquipment("ClothArmor"); //WoolArmor 12+
                Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor1);
                Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(sword1);
                Item accessory4 = ItemPool.RequestItem(typeof(TranslucentRing));
                Cara.Instance.EquipAccessory(accessory4 as Accessory, 0);
                Item accessory5 = ItemPool.RequestItem(typeof(LapizLazuliRing));
                Cara.Instance.EquipAccessory(accessory5 as Accessory, 1);
                Item accessory6 = ItemPool.RequestItem(typeof(RingOfTheSages));  //Pearl Band at 15+
                Cara.Instance.EquipAccessory(accessory6 as Accessory, 2);
                will.SetLevel(25);
                Equipment sword2 = EquipmentPool.RequestEquipment("PlainScythe");
                Equipment armor2 = EquipmentPool.RequestEquipment("LeatherArmor"); //RawHideArmor 12+
                Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(armor2);
                Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(sword2);
                Item accessory7 = ItemPool.RequestItem(typeof(ShadowStrikeRing));
                Will.Instance.EquipAccessory(accessory7 as Accessory, 0);
                Item accessory8 = ItemPool.RequestItem(typeof(AmuletOfInsight));
                Will.Instance.EquipAccessory(accessory8 as Accessory, 1);
                Item accessory9 = ItemPool.RequestItem(typeof(PearlBand));  //Nathans Gift at 15+
                Will.Instance.EquipAccessory(accessory9 as Accessory, 2);

                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 2);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 2);
                Party.Singleton.GameState.Inventory.AddItem(typeof(SmallHealthPotion), 6);
                Party.Singleton.GameState.Inventory.AddItem(typeof(SmallEnergyPotion), 7);

                Party.Singleton.ModifyNPC(
                      Maps.snowTownBlacksmithInterior,
                      "MasterBlacksmith",
                       new Point(6, 3),
                       Direction.South,
                       true,
                       ModAction.Add,
                       true);   

                //will.AddStatusEffect(new Weakened());
                //nathan.AddStatusEffect(new Weakened());
                //nathan.FighterStats.Health = 1;
                //will.FighterStats.Health = 0;
                //nathan.FighterStats.Health = 0;
                //will.FighterStats.BaseAgility = 450;
                //Will.Instance.FighterStats.Experience = 155000;
                testIncrementor++;
            }

            //else if(testIncrementor == 1)
            //{
            //    testIncrementor++;

            //    //Monster[] monsters = new Monster[] { new Monster(Monster.malticar, 1, SlotSize.Large, 1) };
            //    //CombatZone zone = new CombatZone("", 0f, CombatZonePool.swampBG, AudioCues.chepetawaBoss, CombatZonePool.twoMediumLargeCollection, monsters);
            //    //ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));

            //    //Monster[] monsters = new Monster[] { new Monster(Monster.feesh, 1, SlotSize.Medium, 1), new Monster(Monster.feesh, 1, SlotSize.Medium, 1), new Monster(Monster.feesh, 1, SlotSize.Medium, 1), new Monster(Monster.feesh, 1, SlotSize.Medium, 1), new Monster(Monster.feesh, 1, SlotSize.Medium, 1) };

            //    //CombatZone malticarZone = new CombatZone("MalTiCarZone01", 0.020f, CombatZonePool.castle4BG, AudioCues.finalBossPT2, false, CombatZonePool.oneLargeLayoutCollection, monsters);
            //    //CombatZone zone = new CombatZone("TESTABC", 0f, CombatZonePool.forestBG, AudioCues.battleCue, CombatZonePool.fiveMediumLayoutCollection, monsters);
            //    //ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
            //    //Party.Singleton.GameState.Gold = 2000;
            //}
            if (testIncrementor == 1)
            {
                Party.Singleton.GameState.Inventory.AddItem(typeof(BloodMetalOre), 1);
                Party.Singleton.GameState.Inventory.AddItem(typeof(StarfireOre), 1);
                Portal portal;
                portal = new Portal { DestinationMap = Maps.snowTownBlacksmithInterior, DestinationPosition = new Point(8, 5), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
                testIncrementor++;
            }
            else
            {
                Portal portal;
                portal = new Portal { DestinationMap = Maps.healersVillage, DestinationPosition = new Point(15, 7), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
                //Party.Singleton.ModifyMapLayer(Maps.caveLabyrinth, Layer.Collision, new Point(21, 1), 1, true);

            }
        }

        void TheoTest()
        {

            Party.Singleton.RemoveAllFightingCharacters();
            PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
            PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
            nathan.SetLevel(18);
            cara.SetLevel(18);
            will.SetLevel(18);
            Party.Singleton.GameState.Inventory.AddItem(typeof(LargeHealthPotion),20);

            Monster[] monsters = new Monster[] { new Monster(Monster.agoraDemon2, 1, 1), new Monster(Monster.agoraDemon2, 1, 1), new Monster(Monster.agoraDemon2, 1, 1) };
            CombatZone zone = new CombatZone("", 0f, CombatZonePool.swampBG, AudioCues.battleCue, CombatZonePool.threeMediumLargeCollection, monsters);
            ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
           // ScreenManager.AddScreen(new EnterCombatTransitionScreen(CombatZonePool.agoraRuinsWallZone1, ScreenState));
        }

        void MattTest()
        {
            Party.Singleton.RemoveAllFightingCharacters();
            PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
            PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
            nathan.SetLevel(7);
            cara.SetLevel(6);
            will.SetLevel(6);
            Party.Singleton.GameState.Inventory.AddItem(typeof(LargeHealthPotion),20);

            Monster[] monsters = new Monster[] { new Monster(Monster.hauntedTree, 1) };
            //Monster[] monsters = new Monster[] { new Monster(Monster.feesh,1,2) };
            CombatZone zone = new CombatZone("", 0f, CombatZonePool.forest2BG, AudioCues.battleCue, CombatZonePool.twoMediumLargeCollection, monsters);
            ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));

           // ScreenManager.AddScreen(new RiftTransitionScreen(new Rift.RiftDestination("5th Floor Keep", Maps.keepf5, new Point(5, 12))));
        }

        void TurnerTest()
        {
            //Party.Singleton.GameState.Inventory.AddItem(typeof(RingOfJustice), 1);

            //Party.Singleton.AddLogEntry("someplace far far away", "Feddie", "werq ewrqwe rqwe qwdsrerqwe rqwer qwer qwer qwer qwer wqe rqwer wer weqr weqr wer wer wer.");
            //Party.Singleton.AddLogEntry("someplace less far away", "Feddie", "werq ewrqwe rqwe qwrerqwe rqwer qwefr qwer qwer qwer wqe rqwer wer weqr weqr wer wer wer.");
            //Party.Singleton.AddLogEntry("someplace near", "Feddie", "werq ewrqwe rqwe qwrerqwe rqwer qwer qwer qwer qwer wqef rqwer wer weqr weqr wer wer wer.");
            //Party.Singleton.AddLogEntry("someplace very near", "Feddie", "werq ewrqwe rq2we qwrerqwe rqwer qwer qwer qwer qwer wqe rqwer wer weqr weqr wer wer wer.");

            
            //// Combat test
            //Party.Singleton.RemoveAllFightingCharacters();
            //PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            //nathan.SetLevel(1);
            ////ScreenManager.AddScreen(new EnterCombatTransitionScreen(CombatZonePool.keepZoneDemons, ScreenState));

            //Monster[] monsters = new Monster[] { new Monster(Monster.feesh, 1) };
            //CombatZone zone = new CombatZone("", 0f, CombatZonePool.forestBG, AudioCues.battleCue, CombatZonePool.oneLargeLayoutCollection, monsters);
            //ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));

            Party.Singleton.AddAchievement(WillDiesSceneA.willDiedAchievement);
            ScreenManager.AddScreen(new EndingScreen());            
        }

        void GabrielTest()
        {
            //Always Defend by pressing p

            Party.Singleton.RemoveAllFightingCharacters();
            /*PCFightingCharacter max = Party.Singleton.AddFightingCharacter(Party.max);
            max.SetLevel(12);
            
            
            PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
            cara.SetLevel(12);
            
            PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            nathan.SetLevel(12);
            
            PCFightingCharacter sid = Party.Singleton.AddFightingCharacter(Party.sid);
            sid.SetLevel(12);
            */
            PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
            will.SetLevel(17);
            // will at lvl 16  breaks skill menu

            Monster monster = new Monster(Monster.chepetawa, 1);
            Monster[] testMonsters = new Monster[] { monster };
            //CombatZone zone = new CombatZone("Test Zone", 0f, CombatZonePool.forestBG, AudioCues.battleCue, CombatZonePool.threeMediumLayoutCollection, testMonsters);
            CombatZone zone = new CombatZone("Test Zone", 0f, CombatZonePool.forestBG, AudioCues.battleCue, CombatZonePool.threeMediumLayoutCollection, testMonsters);
            ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
        }

        void AdamTest()
        {
            Party.Singleton.RemoveAllFightingCharacters();
            PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            //PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
            //PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
            nathan.SetLevel(16);
            //cara.SetLevel(16);
            //will.SetLevel(16);

            CombatZoneLayout threeMediumLayout = new CombatZoneLayout(new Slot[] {
            new Slot(SlotSize.Medium, new Vector2(754, 150)),
            new Slot(SlotSize.Medium, new Vector2(854, 250)), 
            new Slot(SlotSize.Medium, new Vector2(954, 350))});

            CombatZoneLayout[] layoutCollection = new CombatZoneLayout[] { threeMediumLayout };

            Monster[] monsterCollection = new Monster[] { new Monster("HauntedBook", 1, 3) };

            CombatZone zone = new CombatZone("SwampZone1", 0.3f, "SwampBattleBackground", AudioCues.battleCue, layoutCollection, monsterCollection);

            ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
        }

        void JaredTest()
        {
            Party.Singleton.RemoveAllFightingCharacters();
            PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
            //PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
            nathan.SetLevel(16);
            cara.SetLevel(16);
            //will.SetLevel(16);

            Monster[] monsterCollection = new Monster[] { new Monster("HauntedTree", 1) };

            CombatZone zone = new CombatZone("SwampZone1", 0.3f, "SwampBattleBackground", AudioCues.battleCue, CombatZonePool.threeMediumLayoutCollection, monsterCollection);

            ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
        }

        void GlenTest()
        {
            Monster[] monsters = new Monster[] { new Monster(Monster.ghost, 1, 1), new Monster(Monster.poisonousHauntedBook, 1, 1), new Monster(Monster.ghost, 1, 1), new Monster(Monster.hauntedBook, 1, 1), new Monster(Monster.ghost, 1, 1) };
            CombatZone zone = new CombatZone("Library05", 0.03f, CombatZonePool.libraryBG, AudioCues.battleCue, CombatZonePool.fiveMediumLayoutCollection, monsters);
            ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
        }

        void VideoTest()
        {
            testIncrementor++;
            Portal portal;
            if (testIncrementor == 1)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                nathan.SetLevel(13);
                PCFightingCharacter sid = Party.Singleton.AddFightingCharacter(Party.sid);
                sid.SetLevel(13);
                PCFightingCharacter max = Party.Singleton.AddFightingCharacter(Party.max);
                max.SetLevel(13);
                Equipment sword = EquipmentPool.RequestEquipment("PlainSword");
                Equipment armor = EquipmentPool.RequestEquipment("Armor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                portal = new Portal { DestinationMap = "keepf3", DestinationPosition = new Point(5, 10), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }

            if (testIncrementor == 2)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
                nathan.SetLevel(4);
                Equipment sword = EquipmentPool.RequestEquipment("AdvancedSword");
                Equipment armor = EquipmentPool.RequestEquipment("AdvancedArmor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                cara.SetLevel(3);
                Equipment sword1 = EquipmentPool.RequestEquipment("PlainBook");
                Equipment armor1 = EquipmentPool.RequestEquipment("ClothArmor");
                Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor1);
                Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(sword1);
                portal = new Portal { DestinationMap = "blind_mans_town", DestinationPosition = new Point(24, 15), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }

            if (testIncrementor == 3)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
                nathan.SetLevel(5);
                Equipment sword = EquipmentPool.RequestEquipment("AdvancedSword");
                Equipment armor = EquipmentPool.RequestEquipment("AdvancedArmor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                cara.SetLevel(4);
                Equipment sword1 = EquipmentPool.RequestEquipment("PlainBook");
                Equipment armor1 = EquipmentPool.RequestEquipment("ClothArmor");
                Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor1);
                Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(sword1);
                portal = new Portal { DestinationMap = "blind_mans_forest_4", DestinationPosition = new Point(21, 4), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }

            if (testIncrementor == 4)
            {
                portal = new Portal { DestinationMap = "possessed_library_4ground", DestinationPosition = new Point(8, 6), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }

            if (testIncrementor == 5)
            {
                portal = new Portal { DestinationMap = "swamp_village", DestinationPosition = new Point(29, 21), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }

            if (testIncrementor == 6)
            {
                portal = new Portal { DestinationMap = "sewnBog_7", DestinationPosition = new Point(44, 27), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }

            if (testIncrementor == 7)
            {
                portal = new Portal { DestinationMap = "snow_town", DestinationPosition = new Point(17, 24), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }
            if (testIncrementor == 8)
            {
                portal = new Portal { DestinationMap = "mage_town", DestinationPosition = new Point(14, 27), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }
            if (testIncrementor == 9)
            {
                portal = new Portal { DestinationMap = "cave_labyrinth", DestinationPosition = new Point(48, 58), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }
            if (testIncrementor == 10)
            {
                portal = new Portal { DestinationMap = "forbidden_cavern_high", DestinationPosition = new Point(1, 3), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }
            if (testIncrementor == 11)
            {
                portal = new Portal { DestinationMap = "agora_castle_courtyard", DestinationPosition = new Point(11, 17), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
            }
            if (testIncrementor == 12)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
                PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
                nathan.SetLevel(12);
                Equipment sword = EquipmentPool.RequestEquipment("ExpertsSword");
                Equipment armor = EquipmentPool.RequestEquipment("ExpertsArmor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                cara.SetLevel(11);
                Equipment sword1 = EquipmentPool.RequestEquipment("ExpertsBook");
                Equipment armor1 = EquipmentPool.RequestEquipment("WoolArmor");
                Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor1);
                Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(sword1);
                will.SetLevel(11);
                Equipment sword2 = EquipmentPool.RequestEquipment("ExpertsScythe");
                Equipment armor2 = EquipmentPool.RequestEquipment("TannedLeatherArmor");
                Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(armor2);
                Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(sword2);

                Monster[] monsters = new Monster[] { new Monster(Monster.witchDoctor, 1, 1), new Monster(Monster.boggimusTadpole, 1, 1), new Monster(Monster.voodooDoll, 1, 1) };
                CombatZone zone = new CombatZone("", 0f, CombatZonePool.swampBG, AudioCues.battleCue, CombatZonePool.threeMediumLayoutCollection, monsters);
                ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
            }
            if (testIncrementor == 13)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
                PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
                nathan.SetLevel(9);
                Equipment sword = EquipmentPool.RequestEquipment("ExpertsSword");
                Equipment armor = EquipmentPool.RequestEquipment("ExpertsArmor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                cara.SetLevel(8);
                Equipment sword1 = EquipmentPool.RequestEquipment("AdvancedBook");
                Equipment armor1 = EquipmentPool.RequestEquipment("CottonArmor");
                Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor1);
                Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(sword1);
                will.SetLevel(8);
                Equipment sword2 = EquipmentPool.RequestEquipment("AdvancedScythe");
                Equipment armor2 = EquipmentPool.RequestEquipment("RawhideLeatherArmor");
                Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(armor2);
                Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(sword2);

                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumHealthPotion), 2);
                Party.Singleton.GameState.Inventory.AddItem(typeof(MediumEnergyPotion), 5);
                Party.Singleton.GameState.Inventory.AddItem(typeof(SmallHealthPotion), 5);

                Monster[] monsters = new Monster[] { new Monster(Monster.ghost, 1, 1), new Monster(Monster.apprentice, 1, 1), new Monster(Monster.apprentice, 1, 1) };
                CombatZone zone = new CombatZone("", 0f, CombatZonePool.libraryBG, AudioCues.battleCue, CombatZonePool.threeMediumLayoutCollection, monsters);
                ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
            }
            if (testIncrementor == 14)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                nathan.SetLevel(3);
                Equipment sword = EquipmentPool.RequestEquipment("PlainSword");
                Equipment armor = EquipmentPool.RequestEquipment("Armor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);

                Monster[] monsters = new Monster[] { new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.bandit, 1, 1) };
                CombatZone zone = new CombatZone("", 0f, CombatZonePool.forestBG, AudioCues.battleCue, CombatZonePool.threeMediumLayoutCollection, monsters);
                ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
            }
            if (testIncrementor == 15)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
                nathan.SetLevel(5);
                Equipment sword = EquipmentPool.RequestEquipment("AdvancedSword");
                Equipment armor = EquipmentPool.RequestEquipment("AdvancedArmor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                cara.SetLevel(4);
                Equipment sword1 = EquipmentPool.RequestEquipment("PlainBook");
                Equipment armor1 = EquipmentPool.RequestEquipment("ClothArmor");
                Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor1);
                Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(sword1);

                Monster[] monsters = new Monster[] { new Monster(Monster.hauntedTree, 1, 1), new Monster(Monster.bandit, 1, 1), new Monster(Monster.flyder, 1, 1) };
                CombatZone zone = new CombatZone("", 0f, CombatZonePool.forestBG, AudioCues.battleCue, CombatZonePool.threeMediumLayoutCollection, monsters);
                ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
            }
            if (testIncrementor == 16)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
                PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
                nathan.SetLevel(6);
                Equipment sword = EquipmentPool.RequestEquipment("AdvancedSword");
                Equipment armor = EquipmentPool.RequestEquipment("AdvancedArmor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                cara.SetLevel(5);
                Equipment sword1 = EquipmentPool.RequestEquipment("AdvancedBook");
                Equipment armor1 = EquipmentPool.RequestEquipment("CottonArmor");
                Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor1);
                Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(sword1);
                will.SetLevel(5);
                Equipment sword2 = EquipmentPool.RequestEquipment("PlainScythe");
                Equipment armor2 = EquipmentPool.RequestEquipment("LeatherArmor");
                Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(armor2);
                Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(sword2);

                Monster[] monsters = new Monster[] { new Monster(Monster.burtle, 1, 1) };
                CombatZone zone = new CombatZone("", 0f, CombatZonePool.forestBG, AudioCues.battleCue, CombatZonePool.oneLargeLayoutCollection, monsters);
                ScreenManager.AddScreen(new EnterCombatTransitionScreen(zone, ScreenState));
            }
            if (testIncrementor == 17)
            {
                Party.Singleton.RemoveAllFightingCharacters();
                PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
                PCFightingCharacter cara = Party.Singleton.AddFightingCharacter(Party.cara);
                PCFightingCharacter will = Party.Singleton.AddFightingCharacter(Party.will);
                nathan.SetLevel(14);
                Equipment sword = EquipmentPool.RequestEquipment("ExpertsSword");
                Equipment armor = EquipmentPool.RequestEquipment("ExpertsArmor");
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipArmor(armor);
                Party.Singleton.GetFightingCharacter(Party.nathan).EquipWeapon(sword);
                cara.SetLevel(14);
                Equipment sword1 = EquipmentPool.RequestEquipment("ExpertsBook");
                Equipment armor1 = EquipmentPool.RequestEquipment("WoolArmor");
                Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(armor1);
                Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(sword1);
                will.SetLevel(13);
                Equipment sword2 = EquipmentPool.RequestEquipment("ExpertsScythe");
                Equipment armor2 = EquipmentPool.RequestEquipment("TannedLeatherArmor");
                Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(armor2);
                Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(sword2);

                ScreenManager.Singleton.AddScreen(new CombatScreen(CombatZonePool.chepetawaZone));
            }

        }

        #endregion
    }
}