using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class InCombatInventoryScreen : Screen
    {
        #region Fields


        Texture2D background;
        //Texture2D pointer;
        Texture2D staminaBar;

        const int maxItemDisplay = 5;
        int firstDisplayItem;
        int currentSelection;

        Inventory inventory;
        List<ConsumableItem> consumables;
        List<InventoryItem> inventoryItems;  // parallel list to consumables

        PCFightingCharacter fighter;


        #endregion

        #region Positions


        Vector2 menuPosition;
        Vector2 pointerPosition;
        Vector2 menuNamePosition;


        #endregion

        #region Initialization


        public InCombatInventoryScreen(PCFightingCharacter fighter, Vector2 menuPosition)
        {
            this.fighter = fighter;

            this.menuPosition = menuPosition - new Vector2(6, 4);

            pointerPosition = menuPosition + new Vector2(35, 30); ;
            menuNamePosition = menuPosition + new Vector2(35, 0);
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            IsPopup = true;

            inventory = Party.Singleton.GameState.Inventory;

            PopulateConsumables();
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "hudFrames");
            //pointer = content.Load<Texture2D>(interfaceTextureFolder + "Small_arrow");
            staminaBar = content.Load<Texture2D>(interfaceTextureFolder + "Stamina_Bar");
        }

        private void PopulateConsumables()
        {
            consumables = new List<ConsumableItem>();
            inventoryItems = new List<InventoryItem>();

            for (int i = 0; i < inventory.Items.Count; i++)
            {
                Item item = ItemPool.RequestItem(inventory.Items[i].ItemName);
                if (item is ConsumableItem)
                {
                    consumables.Add(item as ConsumableItem);
                    inventoryItems.Add(inventory.Items[i]);
                }
            }
        }


        #endregion

        #region Handle Input


        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsMenuCancel())
            {
                ScreenManager.RemoveScreen(this);
            }

            if (consumables.Count > 0)
            {
                /// If an item is chosen, we bring up the target selection screen
                /// which will set this variable if the player presses A
                if (TurnExecutor.Singleton.Action == FighterAction.Item)
                {
                    Party.Singleton.GameState.Inventory.RemoveItem(consumables[currentSelection], 1);
                    ScreenManager.RemoveScreen(this);
                    return;
                }

                if (InputState.IsMenuDown())
                {
                    AdjustPointerDown();

                    SoundSystem.Play(AudioCues.menuMove);
                }
                else if (InputState.IsMenuUp())
                {
                    AdjustPointerUp();

                    SoundSystem.Play(AudioCues.menuMove);
                }
                else if (InputState.IsMenuSelect())
                {
                    ScreenManager.AddScreen(new ItemTargetSelectionScreen(fighter, consumables[currentSelection] as ConsumableItem));

                    SoundSystem.Play(AudioCues.menuConfirm);
                }
            }
        }

        /// <summary>
        /// Attempts to decrement the currentSelection by one.
        /// </summary>
        private void AdjustPointerUp()
        {
            currentSelection = Math.Max(currentSelection - 1, 0);
            if (currentSelection < firstDisplayItem)
            {
                firstDisplayItem = Math.Max(firstDisplayItem - 1, 0);
            }
        }

        /// <summary>
        /// Attempts to increment the currentSelection by one
        /// </summary>
        private void AdjustPointerDown()
        {
            currentSelection = Math.Min(currentSelection + 1, consumables.Count - 1);
            if (currentSelection - firstDisplayItem >= maxItemDisplay)
            {
                firstDisplayItem = Math.Min(firstDisplayItem + 1, consumables.Count - maxItemDisplay);
            }
        }


        #endregion

        #region Update and Draw


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color color = Color.White * TransitionAlpha;

            spriteBatch.Draw(background, menuPosition, color);

            spriteBatch.DrawString(Fonts.MenuItem2, Strings.ZA179, menuNamePosition, Fonts.MenuItemColor * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuItem2, Strings.ZA180, new Vector2(menuNamePosition.X + 165, menuNamePosition.Y), Fonts.MenuItemColor * TransitionAlpha);

            RenderItemList(spriteBatch, color);
        }

        private void RenderItemList(SpriteBatch spriteBatch, Color color)
        {
            Vector2 location = pointerPosition;

            int lastDisplayItem = Math.Min(consumables.Count, firstDisplayItem + maxItemDisplay);

            for (int i = firstDisplayItem; i < lastDisplayItem; i++)
            {
                if (i == currentSelection)
                {
                    //spriteBatch.Draw(pointer, new Vector2(location.X - pointer.Width, location.Y), color);
                    Vector2 arrowPosition = new Vector2(location.X, location.Y) + new Vector2(-39, -1);
                    spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, color, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
                }

                spriteBatch.DrawString(Fonts.MenuItem2, consumables[i].DisplayName, location, Fonts.MenuItemColor * TransitionAlpha);

                //spriteBatch.DrawString(Fonts.MenuItem2, inventory.Items[i].Quantity.ToString(), new Vector2(location.X + 173, location.Y), Fonts.MenuItemColor * TransitionAlpha);
                spriteBatch.DrawString(Fonts.MenuItem2, inventoryItems[i].Quantity.ToString(), new Vector2(location.X + 173, location.Y), Fonts.MenuItemColor * TransitionAlpha);

                location.Y += Fonts.MenuItem2.LineSpacing;
            }
        }


        #endregion
    }
}
