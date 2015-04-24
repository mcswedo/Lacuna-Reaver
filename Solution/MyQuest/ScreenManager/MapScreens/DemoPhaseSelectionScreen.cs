using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class DemoPhaseSelectionScreen : Screen
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

        private void TransitionToKeepF5(bool isFinalDestination)
        {
          //  nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
         //   sid = Party.Singleton.AddFightingCharacter(Party.nathan);
        //    max = Party.Singleton.AddFightingCharacter(Party.nathan);
            if (isFinalDestination)
            {
                riftDestination = new Rift.RiftDestination(Maps.keepf5, new Point(5, 11));
            }
        }

        private void TransitionToMushroomHollow(bool isFinalDestination)
        {
            TransitionToKeepF5(false);

            Party.Singleton.RemoveAllFightingCharacters();
            Party.Singleton.GameState.Inventory.Items.Clear();

            nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            Party.Singleton.GameState.Fighters[0].UnequipAccessory(0);
            Party.Singleton.GameState.Fighters[0].UnequipAccessory(1);
            Party.Singleton.GameState.Fighters[0].UnequipAccessory(2);
            Party.Singleton.GameState.Fighters[0].WeaponClassName = null;
            Party.Singleton.GameState.Fighters[0].ArmorClassName = null;
            Party.Singleton.GameState.Fighters[0].EquipWeapon(EquipmentPool.RequestEquipment("PlainSword"));
     
            if (isFinalDestination)
            {
                nathan.SetLevel(1);
                Party.Singleton.GameState.Fighters[0].LevelUpReEquip();
                riftDestination = new Rift.RiftDestination(Maps.healersVillageMayorsHouseF2, new Point(2, 5));
            }
        }

        private void TransitionToTamarel(bool isFinalDestination)
        {
            TransitionToMushroomHollow(false);
            // May need to add achievemnt for entering this map for the first time for the rift skill.

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
        private void TransitionToCelindar(bool isFinalDestination)
        {
            TransitionToTamarel(false);

            new WillEntersMageTownCutSceneScreen().CompleteForTesting();
            new EnterMageTownCutSceneScreen().CompleteForTesting();

            Party.Singleton.AddFightingCharacter(Party.will);

            will = Party.Singleton.GetFightingCharacter(Party.will);

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

        private void TransitionToElderMantisBossFight(bool isFinalDestination)
        {
            TransitionToCelindar(false);

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

        public DemoPhaseSelectionScreen()
        {
            phaseTransitions.Add(new PhaseTransition("KeepF5", TransitionToKeepF5));
            phaseTransitions.Add(new PhaseTransition("Mushroom Hollow", TransitionToMushroomHollow));
            phaseTransitions.Add(new PhaseTransition("Tamarel", TransitionToTamarel));
            phaseTransitions.Add(new PhaseTransition("Celindar", TransitionToCelindar));
            phaseTransitions.Add(new PhaseTransition("Elder Mantis Boss Fight", TransitionToElderMantisBossFight));          
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
