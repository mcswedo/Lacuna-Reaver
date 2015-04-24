using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// We are currently operating under the following conditions:
///     Item shops have an infinite supply of whatever items/accessories they sell
///     The player may carry as many items/accessories as he can afford

/// May require additional graphics but finished under current specifications
/// Should we allow DPad for navigation?

namespace MyQuest
{
    #region SaleItem


    /// <summary>
    /// Represents an item which can be purchased
    /// </summary>
    public class SaleItem
    {
        Type itemType;

        public Type ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }


        int price;

        public int Price
        {
            get { return price; }
        }


        public SaleItem(Type itemType, int price)
        {
            this.itemType = itemType;
            this.price = price;
        }
    }


    #endregion

    public class ItemShopScreen : Screen
    {      
        #region Positions and Data


        static readonly Vector2 screenLocation = new Vector2(375, 80);
        static readonly Vector2 itemNameLocation = new Vector2(screenLocation.X + 80, screenLocation.Y + 57);
        static readonly Vector2 itemCostLocation = new Vector2(screenLocation.X + 374, itemNameLocation.Y);
        static readonly Vector2 quantityLocation = new Vector2(screenLocation.X + 487, itemNameLocation.Y);
        static readonly Vector2 itemLocation = new Vector2(itemNameLocation.X, itemNameLocation.Y + 30);
        static readonly Vector2 descriptionLocation = new Vector2(itemLocation.X, screenLocation.Y + 335);
        static readonly Vector2 goldLocation = new Vector2(screenLocation.X + 597, descriptionLocation.Y);
        static readonly Vector2 goldTextLocation = new Vector2(goldLocation.X, goldLocation.Y + 32);

        const int maxItemDisplay = 10;

        List<SaleItem> itemStock = new List<SaleItem>();


        #endregion

        #region Graphics


        Texture2D background;
        //Texture2D pointer;
        Texture2D arrow;


        #endregion

        #region Input


        int currentSelection;
        int firstDisplayItem;

        Inventory inventory;

        double fastScrollDelay = 0.1;
        double fastScrollInitialPause = 0.4;
        double ticker = 0.0;
        bool fastScrolling = false;


        #endregion

        #region Initialization
       

        public ItemShopScreen(List<SaleItem> items)
        {
            Debug.Assert(items != null && items.Count > 0);

            //itemStock.Clear();
            itemStock.AddRange(items);
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            inventory = Party.Singleton.GameState.Inventory;
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Shop_screen");
            //pointer = content.Load<Texture2D>(interfaceTextureFolder + "Small_arrow");
            arrow = content.Load<Texture2D>(interfaceTextureFolder + "Up_arrow");
        }


        #endregion

        #region Update Logic


        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();
                return;
            }

            UpdateFastScrolling(gameTime);

            SelectItem();
        }

        #region Helpers


        /// <summary>
        /// Attempts to decrement the currentSelection by one.
        /// </summary>
        private void AdjustPointerUp()
        {
            if (currentSelection - 1 != 0)
            {
                SoundSystem.Play(AudioCues.menuMove);
            }

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
            if (currentSelection + 1 != itemStock.Count)
            {
                SoundSystem.Play(AudioCues.menuMove);
            }

            currentSelection = Math.Min(currentSelection + 1, itemStock.Count - 1);
            if (currentSelection - firstDisplayItem >= maxItemDisplay)
            {
                firstDisplayItem = Math.Min(firstDisplayItem + 1, itemStock.Count - maxItemDisplay);
            }
        }

        /// <summary>
        /// Handles logic for selecting an Item from the Inventory
        /// </summary>
        private void SelectItem()
        {
            if (InputState.IsMenuDown() || InputState.IsMoveItemDown())
            {
                AdjustPointerDown();
            }
            else if (InputState.IsMenuUp() || InputState.IsMoveItemUp())
            {
                AdjustPointerUp();
            }
            else if (InputState.IsMenuSelect())
            {
                Item item = ItemPool.RequestItem(itemStock[currentSelection].ItemType);
                PurchaseItem(itemStock[currentSelection], item);
              
            }
        }

        /// <summary>
        /// Tries to purchase an Item. If the Party does not have enough gold,
        /// the purchase doesn't happen, and we should play a buzzer sound or
        /// something to indicate that the Party can't afford the Item.
        /// </summary>
        /// <param name="contentName"></param>
        /// <param name="price"></param>
        private void PurchaseItem(SaleItem saleItem, Item item)
        {
            if (Party.Singleton.GameState.Gold >= saleItem.Price)
            {
                inventory.AddItem(saleItem.ItemType, 1, item.MapName, item.SubMapNames);
                Party.Singleton.GameState.Gold -= saleItem.Price;
                if (item.MapName != null)
                {
                    itemStock.Remove(itemStock[currentSelection]);
                    AdjustPointerUp();
                }
                SoundSystem.Play(AudioCues.ItemBuy);

            }
            else
            {
                SoundSystem.Play(AudioCues.menuDeny);//Not enough cash
            }
        }

        /// <summary>
        /// Controls the Fast Scrolling effect when up or down is held.
        /// </summary>
        /// <param name="gameTime"></param>
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

            Color color = Fonts.MenuItemColor * TransitionAlpha;

            spriteBatch.Draw(background, screenLocation, color);

            spriteBatch.DrawString(Fonts.MenuItem2, Strings.ItemShopScreen_Name, itemNameLocation, color);
            spriteBatch.DrawString(Fonts.MenuItem2, Strings.ItemShopScreen_Cost, itemCostLocation, color);
            spriteBatch.DrawString(Fonts.MenuItem2, Strings.ItemShopScreen_Owned, quantityLocation, color);

            spriteBatch.DrawString(Fonts.MenuItem2, Strings.StatusScreen_Gold, goldLocation, color);
            spriteBatch.DrawString(Fonts.MenuTitle2, Party.Singleton.GameState.Gold.ToString(),
                goldTextLocation, Fonts.MenuTitleColor * TransitionAlpha);

            RenderItemList(spriteBatch, color);
        }


        #region Helpers

        private void RenderItemList(SpriteBatch spriteBatch, Color color)
        {
            Vector2 location = itemLocation;

            int lastDisplayItem = Math.Min(itemStock.Count, firstDisplayItem + maxItemDisplay);

            for (int i = firstDisplayItem; i < lastDisplayItem; ++i)
            {
                Item item = ItemPool.RequestItem(itemStock[i].ItemType);

                string price = itemStock[i].Price.ToString();
                string owned = inventory.ItemCount(itemStock[i].ItemType).ToString();

                if (i == currentSelection)
                {
                    //spriteBatch.Draw(pointer, new Vector2(location.X - pointer.Width, location.Y), color);
                    Vector2 arrowPosition = location + new Vector2(-29, -1);
                    spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, color, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
  
                    spriteBatch.DrawString(Fonts.MenuItem2, ParseDescription(item.Description), descriptionLocation, color);
                }
                spriteBatch.DrawString(Fonts.MenuItem2, item.DisplayName, location, color);

                spriteBatch.DrawString(Fonts.MenuItem2, price, new Vector2(location.X + 300, location.Y), color);

                spriteBatch.DrawString(Fonts.MenuItem2, owned, new Vector2(location.X + 415, location.Y), color);

                location.Y += Fonts.MenuItem2.LineSpacing;
            }
        }

        private string ParseDescription(string description)
        {
            string parsedDescription = "";
            string tempLine = "";
            string textToAdd = "";
            int index = 0;
            int startIndex = index;
            while (index < description.Length)
            {
                while (description[index] != ' ')
                {
                    ++index;
                    if (index == description.Length)
                    {
                        textToAdd = description.Substring(startIndex, index - startIndex);
                        if (Fonts.MenuItem2.MeasureString(tempLine + textToAdd).X <= 450)
                        {
                            tempLine += textToAdd;
                            textToAdd = "";
                            parsedDescription += tempLine;
                        }
                        else
                        {
                            --index;
                            while (description[index] != ' ')
                            {
                                --index;
                            }
                            textToAdd = description.Substring(startIndex, index - startIndex);
                            tempLine += textToAdd;
                            parsedDescription += tempLine;
                            parsedDescription += '\n';
                            tempLine = "";
                            textToAdd = description.Substring(index, description.Length - index);
                            parsedDescription += textToAdd;
                        }
                        return parsedDescription;
                    }
                }
                textToAdd = description.Substring(startIndex, index - startIndex);
                if (Fonts.MenuItem2.MeasureString(tempLine + textToAdd).X <= 490)
                {
                    tempLine += textToAdd;
                    textToAdd = "";
                    startIndex = index++;
                }
                else
                {
                    tempLine += '\n';
                    parsedDescription += tempLine;
                    tempLine = "";
                }
            }
            return parsedDescription;
        }


        #endregion


        #endregion
    }
}
