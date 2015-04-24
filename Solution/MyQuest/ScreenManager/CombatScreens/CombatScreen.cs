//#define TESTING

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

/// Use a "fade screen to black" effect for transitions
/// I think Damage and Status Modifiers are applied and updated at the start of every character's turn
/// Analyze where/when/how those things are applied 
///     How: are StatusModifiers strictly percent based (if not, class needs extra data)

namespace MyQuest
{
    enum CombatState
    {
        Delaying,
        Processing,
        Complete
    }

    class CombatScreen : Screen
    {
        internal const int MaxWeight = 6;
        const int iconSpacing = 50;
        //int aliveCombatants;

        static Vector2 hudPosition = new Vector2(128,520);

        public static Vector2 HudPosition
        {
            get { return hudPosition; }
        }

        #region Graphics


        Texture2D background;
        Texture2D staminaBar;
        Texture2D hpMpBar;
        Texture2D battleHud;

        //Viewport initiativeViewport;
        Viewport defaultViewport;

        GraphicsDevice device;

        Vector2 menuPosition;
        Vector2 topCombatMenuItemPosition;

        Queue<FightingCharacter> initiativeDrawOrder;

        public static Texture2D turnIndicator;
        public static Rectangle turnIndicatorSourceRectangle = new Rectangle(0, 80, 250, 20);
        public static Rectangle enemyTargetIndicatorSourceRectangle = new Rectangle(0, 0, 250, 20);
        public static Rectangle itemTargetIndicatorSourceRectangle = new Rectangle(0, 50, 250, 20);

        #endregion

        #region Combat


        CombatZone combatZone;

        /// <summary>
        /// List of FightingCharacters sorted by initiative
        /// </summary>
        List<FightingCharacter> fighters;

        /// <summary>
        /// List of PC characters sorted by screen position
        /// </summary>
        List<FightingCharacter> pcFighters;

        /// <summary>
        /// List of NPC characters sorted by screen position
        /// </summary>
        List<FightingCharacter> npcFighters;

        int currentFighter;

        static readonly TimeSpan TurnDelay = TimeSpan.FromSeconds(0.8);

        TimeSpan delayTimer;

        CombatState state;


        #endregion

        #region Initialization

        public CombatScreen(CombatZone combatZone)
        {
            this.combatZone = combatZone;

            fighters = new List<FightingCharacter>();
            npcFighters = new List<FightingCharacter>();
            pcFighters = new List<FightingCharacter>();

            menuPosition = new Vector2(135, 475);

            AddNPCFighters();
            AddPCFighters();
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.15);
            TransitionOffTime = TimeSpan.FromSeconds(5.0);

            device = GameLoop.Instance.GraphicsDevice;
            defaultViewport = device.Viewport;

            //MusicSystem.Pause();

            if (!string.IsNullOrEmpty(combatZone.CueName))
            {
                MusicSystem.Play(combatZone.CueName);
            }

            foreach (FightingCharacter fighter in fighters)
            {
                fighter.Initialize();
            }

            initiativeDrawOrder = new Queue<FightingCharacter>(fighters);

            delayTimer = TurnDelay;
            state = CombatState.Delaying;

            TurnExecutor.Singleton.Initialize(fighters, pcFighters, npcFighters, combatZone.RunningEnabled);
            foreach (FightingCharacter combatant in pcFighters)
            {               
                combatant.SetState(State.Normal);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + combatZone.BackgroundTexture);
            staminaBar = content.Load<Texture2D>(interfaceTextureFolder + "Stamina_Bar");
            battleHud = content.Load<Texture2D>(backgroundTextureFolder + "battleHud");
            hpMpBar = content.Load<Texture2D>(interfaceTextureFolder + "HpManaBar");
            turnIndicator = content.Load<Texture2D>(interfaceTextureFolder + "turn_indicator");

            //TargetDisplayManager.Singleton.LoadContent(content);

            /// Our PCFighters are persistent so we only call load for the npcs
            foreach (FightingCharacter npc in npcFighters)
            {
                npc.LoadContent();
            }
        }


        #endregion

        #region Update Logic


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            if (!otherScreenHasFocus && state == CombatState.Complete)
            {
                ScreenManager.RemoveScreen(this);
                InvokeExitScreenHandlers();
                return;
            }
#if DEBUG
            if (InputState.Temp_IsToggleCollisionDisplay())  // F1
            {
                MessageOverlay.AddMessage("Combat Zone: " + combatZone.ZoneName, 5);
            }
#endif

            if (fighters.Count == 0)
            {
                state = CombatState.Complete;
                ScreenManager.AddScreen(new DefeatConfirmationScreen());
            }
            else
            {
                // Advance the currentFighter index.
                /// If the current character is dead, move on to the next one
                //while (fighters[currentFighter].State == State.Dead)
                //{
                //    if (++currentFighter >= fighters.Count)
                //    {
                //        currentFighter = 0;
                //    }
                //}

                foreach (FightingCharacter fighter in fighters)
                {
                    fighter.Update(gameTime);
                }

                if (!WillHandleUserInput)
                {
                    return;
                }

                CheckWinLoss();

                switch (state)
                {
                    case CombatState.Delaying:
                        {
                            delayTimer -= gameTime.ElapsedGameTime;
                            if (delayTimer <= TimeSpan.Zero)
                            {
                                TurnExecutor.Singleton.StartTurn(currentFighter);
                                // The current fighter may have just died as a result of having poison applied.
                                CheckWinLoss();
                                if (state != CombatState.Complete)
                                {
                                    state = CombatState.Processing;
                                }
                            }
                            break;
                        }
                    case CombatState.Processing:
                        {
                            TurnExecutor.Singleton.Update(gameTime);                            

                            if (TurnExecutor.Singleton.TurnComplete)
                            {
                                if (TurnExecutor.Singleton.PartyRetreatSuccessful)
                                {
                                    EndCombat();
                                    MusicSystem.Play(Party.Singleton.CurrentMap.MusicCueName);
                                    pcFighters.Clear();
                                    npcFighters.Clear();
                                    fighters.Clear();
                                    ExitAfterTransition();
                                    EndCombat();
                                    //foreach (PCFightingCharacter fighter in TurnExecutor.Singleton.PCFighters) This should never be possible, the fighters are already removed. This is done in EndCombat().
                                    //{
                                    //    if (fighter.State == State.Dead)
                                    //    {
                                    //        fighter.OnResurrection();
                                    //    }
                                    //}
                                    state = CombatState.Complete;
                                    break;
                                }

                                if (++currentFighter >= fighters.Count)
                                {
                                    currentFighter = 0;
                                    EndRound();
                                }

                                CheckDeathState();

                                while (fighters[currentFighter].State == State.Dead)
                                {
                                    if (++currentFighter >= fighters.Count)
                                    {
                                        currentFighter = 0;
                                    }
                                }


                                if (combatZone == CombatZonePool.malticarZone && fighters[0].Statistics.Round >= 25 && npcFighters[0].State == State.Dead)// currentFighter == 0)
                                {
                                    ScreenManager.Singleton.RemoveScreen(this);

                                    ScreenManager.Singleton.AddScreen(
                                        (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "WillDiesCutSceneScreen"));
                                }

                                delayTimer = TimeSpan.FromSeconds(.6);

                                state = CombatState.Delaying;
                            }
                            break;
                        }
                }
            }
        }


        /// <summary>
        /// Checks whether or not a character has died and sets their state to dead if they have
        /// </summary>
        private void CheckDeathState()
        {
            foreach (FightingCharacter fighter in fighters)
            {
                if (fighter.FighterStats.Health <= 0 && fighter.State != State.Dead)
                {
                    fighter.SetState(State.Dead);
                    fighter.FighterStats.Energy = 0;
                    fighter.FighterStats.Stamina = 0;
                }
            }
        }

        /// <summary>
        /// Checks Win/Loss conditions to see if any side of the battle has won
        /// </summary>
        private void CheckWinLoss()
        {
            int deadEnemies = 0;
            int deadPlayers = 0;

            foreach (FightingCharacter fighter in fighters)
            {
                if (fighter is NPCFightingCharacter)
                {
                    if (fighter.State == State.Dead)
                        deadEnemies++;
                }
                else
                {
                    if (fighter.State == State.Dead)
                        deadPlayers++;
                }
            }

            //check if the player won
            if (deadEnemies == npcFighters.Count)
            {
                EndCombat();
                state = CombatState.Complete;

                if (Party.Singleton.CurrentMap.Name.Equals(Maps.agoraCastleRoofTop) && Party.Singleton.Leader.TilePosition.Y <= 11)//For final battle, don't do anything.
                {
                }

                //add the victory screen
                else
                {
                    ScreenManager.AddScreen(new VictoryDelayScreen(npcFighters));
                }
                
            }
            else if (deadPlayers == pcFighters.Count)
            {
                state = CombatState.Complete;
                ScreenManager.AddScreen(new DefeatConfirmationScreen());
            }
        }

        /// <summary>
        /// Helper for performing end of round stuff
        /// </summary>
        private void EndRound()
        {
            foreach (FightingCharacter fighter in fighters)
            {
                //fighter.OnEndRound();
                fighter.Statistics.Round++;
            }
        }

        /// <summary>
        /// Helper for ending combat after the party retreats
        /// </summary>
        private void EndCombat()
        {
            /// revive characters if necessary
            foreach (FightingCharacter fighter in pcFighters)
            {
                fighter.OnEndCombat();


                //This logic has been moved to the Victory Screen to prevent the Dead PCFighters from gaining experience.
                if (fighter.State == State.Dead)
                {
                    if (TurnExecutor.Singleton.PartyRetreatSuccessful)
                    {
                        fighter.OnResurrection();
                    }
                }               
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }


        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            CombatMessage.Init(spriteBatch, Fonts.MenuItem2);

            Color color = Fonts.CombatMenuItemColor * TransitionAlpha;

            Vector2 statusWindow = new Vector2(hudPosition.X + 400, hudPosition.Y);

            spriteBatch.Draw(background, Vector2.Zero, Color.White * TransitionAlpha);

            //if (displayTitleSafe)
            //{
            //    spriteBatch.Draw(ScreenManager.BlankTexture, GlobalSettings.TitleSafeArea, Color.White * TransitionAlpha);
            //}

            spriteBatch.Draw(battleHud, hudPosition, Color.White * TransitionAlpha);

            if (!OtherScreenHasFocus)
            {
                topCombatMenuItemPosition.X = menuPosition.X + 35;
                topCombatMenuItemPosition.Y = menuPosition.Y + 55;
                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA169, topCombatMenuItemPosition, Fonts.CombatMenuItemColor * TransitionAlpha);
                topCombatMenuItemPosition.Y += Fonts.CombatMenuItem2.LineSpacing;

                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA170, topCombatMenuItemPosition, Fonts.CombatMenuItemColor * TransitionAlpha);
                topCombatMenuItemPosition.Y += Fonts.CombatMenuItem2.LineSpacing;

                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA171, topCombatMenuItemPosition, Fonts.CombatMenuItemColor * TransitionAlpha);
                topCombatMenuItemPosition.Y += Fonts.CombatMenuItem2.LineSpacing;

                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA172, topCombatMenuItemPosition, Fonts.CombatMenuItemColor * TransitionAlpha);
                topCombatMenuItemPosition.Y += Fonts.CombatMenuItem2.LineSpacing;

                if (combatZone.RunningEnabled)
                {
                    spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA173, topCombatMenuItemPosition, Fonts.CombatMenuItemColor * TransitionAlpha);
                }
            }

            RenderCharacterDetails(spriteBatch, color, statusWindow);

            if (fighters.Count > 0)
            {
                RenderFighters(spriteBatch);
                //if (TurnExecutor.Singleton.ActiveSkill != null)
                //{
                //    TurnExecutor.Singleton.ActiveSkill.Draw(spriteBatch);
                //}
                //    RenderUntargetedFighters(spriteBatch)  
                //Not created yet. This code is designed to draw combat animations on top of the target and behind
                //untargeted characters.
            }

            if (TurnExecutor.Singleton.ActiveSkill != null)
            {
                TurnExecutor.Singleton.ActiveSkill.Draw(spriteBatch);
            }

            CombatMessage.Draw(gameTime, spriteBatch);
        }

        void RenderCharacterDetails(SpriteBatch spriteBatch, Color color, Vector2 statusWindow)
        {
            int ySpacing = 10;
            float recWidth = 0.0f;

            foreach (PCFightingCharacter fighter in pcFighters)
            {
                spriteBatch.DrawString(Fonts.CombatStatus, fighter.Name, new Vector2(statusWindow.X - 5, statusWindow.Y + ySpacing), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(
                                        Fonts.CombatStatus,
                                        "HP: " + fighter.FighterStats.Health + "/" + fighter.FighterStats.ModifiedMaxHealth,
                                        new Vector2(statusWindow.X + 85, statusWindow.Y + ySpacing),
                                        Fonts.MenuItemColor * TransitionAlpha);

                recWidth = 160 * ((float)fighter.FighterStats.Health / (float)fighter.FighterStats.ModifiedMaxHealth);
                spriteBatch.Draw(
                    ScreenManager.BlankTexture,
                    new Rectangle((int)statusWindow.X + 85, (int)statusWindow.Y + ySpacing + 26, (int)recWidth + 1, 9),
                    Color.Red * TransitionAlpha);

                spriteBatch.Draw(hpMpBar, new Vector2(statusWindow.X + 85, statusWindow.Y + ySpacing + 26), color);

                spriteBatch.DrawString(
                                        Fonts.CombatStatus,
                                        "MP: " + fighter.FighterStats.Energy + "/" + fighter.FighterStats.ModifiedMaxEnergy,
                                        new Vector2(statusWindow.X + 265, statusWindow.Y + ySpacing),
                                        Fonts.MenuItemColor * TransitionAlpha);

                recWidth = 160 * ((float)fighter.FighterStats.Energy / (float)fighter.FighterStats.ModifiedMaxEnergy);

                spriteBatch.Draw(
                    ScreenManager.BlankTexture,
                    new Rectangle((int)statusWindow.X + 265, (int)statusWindow.Y + ySpacing + 26, (int)recWidth + 1, 9),
                    Color.Blue * TransitionAlpha);

                spriteBatch.Draw(hpMpBar, new Vector2(statusWindow.X + 265, statusWindow.Y + ySpacing + 26), Color.White * TransitionAlpha);

                spriteBatch.DrawString(Fonts.CombatStatus, 
                                        "SP: " + fighter.FighterStats.Stamina + "/" + fighter.FighterStats.ModifiedMaxStamina,//"/10", 
                                        new Vector2(statusWindow.X + 445, statusWindow.Y + ySpacing), 
                                        Fonts.MenuItemColor * TransitionAlpha);

                recWidth = 160 * ((float)fighter.FighterStats.Stamina / (float)fighter.FighterStats.ModifiedMaxStamina);

                spriteBatch.Draw(
                     ScreenManager.BlankTexture,
                     new Rectangle((int)statusWindow.X + 445, (int)statusWindow.Y + ySpacing + 26, (int)recWidth + 1, 9),
                     Color.Yellow * TransitionAlpha);

                spriteBatch.Draw(hpMpBar, new Vector2(statusWindow.X + 445, statusWindow.Y + ySpacing + 26), Color.White * TransitionAlpha);

                ySpacing += 50;
            }

        }

        void RenderFighters(SpriteBatch spriteBatch)
        {
            Color pcEffectTinting = Color.White;

            if (fighters[currentFighter] is PCFightingCharacter && fighters[currentFighter].CurrentAnimation.Name == "Idle")
            {
                if (fighters[currentFighter].State != State.Paralyzed && fighters[currentFighter].State != State.Dead)
                {
                    spriteBatch.Draw(turnIndicator, fighters[currentFighter].PointerPosition, turnIndicatorSourceRectangle, Color.White * TransitionAlpha);
                }
            }

            if (TurnExecutor.Singleton.NPCTargets.Count >= 0)
            {
                foreach (int i in TurnExecutor.Singleton.NPCTargets)
                {
                    FightingCharacter target = TurnExecutor.Singleton.NPCFighters[i];
                    spriteBatch.Draw(CombatScreen.turnIndicator, target.PointerPosition + new Vector2(0, 0), CombatScreen.enemyTargetIndicatorSourceRectangle, Color.White * TransitionAlpha);
                }
            }

            if (TurnExecutor.Singleton.PCTargets.Count >= 0)
            {
                foreach (int i in TurnExecutor.Singleton.PCTargets)
                {
                    FightingCharacter target = TurnExecutor.Singleton.PCFighters[i];
                    spriteBatch.Draw(CombatScreen.turnIndicator, target.PointerPosition + new Vector2(0, 0), CombatScreen.enemyTargetIndicatorSourceRectangle, Color.White * TransitionAlpha);
                }
            }

            if (TurnExecutor.Singleton.ItemTargets.Count >= 0)
            {
                foreach (int i in TurnExecutor.Singleton.ItemTargets)
                {
                    FightingCharacter target = TurnExecutor.Singleton.PCFighters[i];
                    spriteBatch.Draw(CombatScreen.turnIndicator, target.PointerPosition + new Vector2(0, -2), CombatScreen.itemTargetIndicatorSourceRectangle, Color.White * TransitionAlpha);
                }
            }

            for (int i = 0; i < pcFighters.Count; i++)
            {
                if (fighters[currentFighter].Name != pcFighters[i].Name)
                {
                    pcEffectTinting = Color.White;

                    if (pcFighters[i].State != State.Dead)
                    {
                        if (pcFighters[i].HasStatusEffect("Paralyzed")/* || pcFighters[i].HasStatusEffect("Stunned")*/)
                        {
                            //pcEffectTinting = Color.LightSteelBlue;
                            pcFighters[i].SetAnimation("Idle");
                        }
                        if (pcFighters[i].HasStatusEffect("Poisoned"))
                        {
                            //pcEffectTinting = Color.Green;
                        }
                        if (pcFighters[i].HasStatusEffect("Blindness"))
                        {
                            //pcEffectTinting = Color.BlueViolet;
                        }
                        if (pcFighters[i].HasStatusEffect("Armored"))
                        {
                            //pcEffectTinting = Color.SandyBrown;
                        }
                        if (pcFighters[i].HasStatusEffect("Envigored"))
                        {
                            //pcEffectTinting = Color.Orange;
                        }
                        if (pcFighters[i].HasStatusEffect("Invulnerable"))
                        {
                            //pcEffectTinting = Color.LightSkyBlue;
                        }
                        if (pcFighters[i].HasStatusEffect("Regeneration"))
                        {
                            //pcEffectTinting = Color.LawnGreen;
                        }
                        if (pcFighters[i].HasStatusEffect("Warded"))
                        {
                            //pcEffectTinting = Color.Blue;
                        }

                        foreach (DamageModifier modifier in pcFighters[i].DamageModifiers) //For Weakened or Focused
                        {
                            if (!modifier.IsPositive)
                            {
                                //pcEffectTinting = Color.Gray;
                            }
                            else
                            {
                                //pcEffectTinting = Color.LightGoldenrodYellow;
                            }
                        }
                    }

                    pcFighters[i].Draw(spriteBatch, pcEffectTinting);
                }
            }

            for (int i = 0; i < npcFighters.Count; i++)
            {
                {
                    Color npcEffectTinting = Color.White; //To reset the tinting on the next character check.
                    if (npcFighters[i].HasStatusEffect("Paralyzed"))
                    {
                        //npcEffectTinting = Color.LightSteelBlue;
                        npcFighters[i].SetAnimation("Idle");
                    }
                    if (npcFighters[i].HasStatusEffect("Poisoned"))
                    {
                        //npcEffectTinting = Color.Green;
                    }
                    if (npcFighters[i].HasStatusEffect("Blindness"))
                    {
                        //npcEffectTinting = Color.BlueViolet;
                    }

                    if (npcFighters[i].HasStatusEffect("Armored"))
                    {
                       // npcEffectTinting = Color.SandyBrown;
                    }
                    if (npcFighters[i].HasStatusEffect("Envigored"))
                    {
                        //npcEffectTinting = Color.Orange;
                    }
                    if (npcFighters[i].HasStatusEffect("Invulnerable"))
                    {
                        //npcEffectTinting = Color.LightSkyBlue;
                    }
                    if (npcFighters[i].HasStatusEffect("Regeneration"))
                    {
                        //npcEffectTinting = Color.LawnGreen;
                    }
                    if (npcFighters[i].HasStatusEffect("Stunned"))
                    {
                       // npcEffectTinting = Color.LightYellow;
                    }
                    if (npcFighters[i].HasStatusEffect("Warded"))
                    {
                        //npcEffectTinting = Color.Blue;
                    }
                    foreach (DamageModifier modifier in npcFighters[i].DamageModifiers) //For Weakened or Focused
                    {
                        if (!modifier.IsPositive)
                        {
                            //npcEffectTinting = Color.Gray;
                        }
                        else
                        {
                            //npcEffectTinting = Color.LightGoldenrodYellow;
                        }
                    }

                    if (npcFighters[i].HasStatusEffect("ChepetawaRage"))
                    {
                        npcEffectTinting = Color.OrangeRed;
                    }

                    if (npcFighters[i].HasStatusEffect("BurtleRage"))
                    {
                        npcEffectTinting = Color.OrangeRed;
                    }

                    if (fighters[currentFighter].HasStatusEffect("Rage"))
                    {
                        pcEffectTinting = Color.Crimson;
                    }

                    if (npcFighters[i].State == State.Dead)
                    {
                        npcFighters[i].CurrentAnimation.deathFade();
                    }

                    npcFighters[i].Draw(spriteBatch, npcEffectTinting, npcFighters[i].State == State.Dead);
                }
            }

            if (fighters[currentFighter] is PCFightingCharacter)
            {
                pcEffectTinting = Color.White;

                if (fighters[currentFighter].State != State.Dead)
                {
                    if (fighters[currentFighter].HasStatusEffect("Paralyzed"))
                    {
                        //pcEffectTinting = Color.LightSteelBlue;
                        fighters[currentFighter].SetAnimation("Idle");
                    }
                    if (fighters[currentFighter].HasStatusEffect("Poisoned"))
                    {
                        //pcEffectTinting = Color.Green;
                    }
                    if (fighters[currentFighter].HasStatusEffect("Blindness"))
                    {
                        //pcEffectTinting = Color.BlueViolet;
                    }
                    if (fighters[currentFighter].HasStatusEffect("Armored"))
                    {
                        //pcEffectTinting = Color.SandyBrown;
                    }
                    if (fighters[currentFighter].HasStatusEffect("Envigored"))
                    {
                        //pcEffectTinting = Color.Orange;
                    }
                    if (fighters[currentFighter].HasStatusEffect("Invulnerable"))
                    {
                        //pcEffectTinting = Color.LightSkyBlue;
                    }
                    if (fighters[currentFighter].HasStatusEffect("Regeneration"))
                    {
                        //pcEffectTinting = Color.LawnGreen;
                    }
                    if (fighters[currentFighter].HasStatusEffect("Stunned"))
                    {
                        //pcEffectTinting = Color.LightYellow;
                    }
                    if (fighters[currentFighter].HasStatusEffect("Warded"))
                    {
                        //pcEffectTinting = Color.Blue;
                    }
                    foreach (DamageModifier modifier in fighters[currentFighter].DamageModifiers) //For Weakened or Focused
                    {
                        if (!modifier.IsPositive)
                        {
                            //pcEffectTinting = Color.Gray;
                        }
                        else
                        {
                            //pcEffectTinting = Color.LightGoldenrodYellow;
                        }
                    }
                }

                fighters[currentFighter].Draw(spriteBatch, pcEffectTinting);
            }
        }

        #endregion

        #region Build Combatant List


        private void AddNPCFighters()
        {
            Debug.Assert(npcFighters.Count == 0);
            Debug.Assert(fighters.Count == 0);
            //npcFighters.Clear();
            //fighters.Clear();

            CombatZoneLayout layout = combatZone.RandomlySelectLayout();
            Debug.Assert(combatZone.Monsters.Length > 0, "combat zone with no monsters: " + combatZone.ZoneName);

            npcFighters = layout.SelectMonsters(combatZone.Monsters);

            foreach(NPCFightingCharacter monster in npcFighters)
            {
                InsertCombatant(monster);
            }
        }

        private void AddPCFighters()
        {
            if (Party.Singleton.GameState.Fighters.Count == 1)
            {
                Party.Singleton.GameState.Fighters[0].ScreenPosition = new Vector2(469, 272);

                pcFighters.Add(Party.Singleton.GameState.Fighters[0]);
                InsertCombatant(Party.Singleton.GameState.Fighters[0]);
            }
            else if (Party.Singleton.GameState.Fighters.Count == 2)
            {
                Party.Singleton.GameState.Fighters[0].ScreenPosition = new Vector2(128 + ((512 / 3) * 2), 205);
                Party.Singleton.GameState.Fighters[1].ScreenPosition = new Vector2(128 + (512 / 3), 338);
                
                pcFighters.Add(Party.Singleton.GameState.Fighters[0]);
                pcFighters.Add(Party.Singleton.GameState.Fighters[1]);

                InsertCombatant(Party.Singleton.GameState.Fighters[0]);
                InsertCombatant(Party.Singleton.GameState.Fighters[1]);
            }
            else
            {
                Party.Singleton.GameState.Fighters[0].ScreenPosition = new Vector2(128 + (512 / 4 * 3), 72 + 400 / 4 * 1);
                if (Party.Singleton.GameState.Fighters[1].Name.Equals(Party.cara))
                {
                    Party.Singleton.GameState.Fighters[1].ScreenPosition = new Vector2(128 + (512 / 4 * 2) - 55, (72 + 400 / 4 * 2));
                }
                else
                {
                    Party.Singleton.GameState.Fighters[1].ScreenPosition = new Vector2(128 + (512 / 4 * 2), (72 + 400 / 4 * 2) - 15);
                }
                Party.Singleton.GameState.Fighters[2].ScreenPosition = new Vector2(128 + (512 / 4 * 1), (72 + 400 / 4 * 3) - 15);
                
                pcFighters.Add(Party.Singleton.GameState.Fighters[0]);
                pcFighters.Add(Party.Singleton.GameState.Fighters[1]);
                pcFighters.Add(Party.Singleton.GameState.Fighters[2]);

                InsertCombatant(Party.Singleton.GameState.Fighters[0]);
                InsertCombatant(Party.Singleton.GameState.Fighters[1]);
                InsertCombatant(Party.Singleton.GameState.Fighters[2]);
            }
        }

        /// <summary>
        /// Inserts a given fighter in the list based on its Initiative Value
        /// </summary>
        private void InsertCombatant(FightingCharacter fighter)
        {
            fighter.FighterStats.ReapplyStatModifiers();
            int index = 0;
            while (index < fighters.Count && fighter.FighterStats.Initiative < fighters[index].FighterStats.Initiative)
            {
                ++index;
            }
            fighters.Insert(index, fighter);
        }


        #endregion
    }
}
