using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// Needs: item icons (if we're using them), scroll box arrows
/// Also, item descriptions need to be wrapped if the text is larger in width than the description box
/// Input bug. The dpad is recognized as an item re-ordering button and as a scroll button (causes conflict)

namespace MyQuest
{
    enum ActionState
    {
        SelectItem,
        EquipAccessory,
        UnequipAccessory
    }

    public class InventoryScreen : Screen
    {
        #region Positions

        static readonly Vector2 screenLocation = new Vector2(128, 72);
        static readonly Vector2 itemNameLocation = new Vector2(screenLocation.X + 618, screenLocation.Y + 68);
        static readonly Vector2 quantityLocation = new Vector2(itemNameLocation.X + 230, itemNameLocation.Y);
        static readonly Vector2 itemLocation = new Vector2(itemNameLocation.X - 10, itemNameLocation.Y + 50);
        static readonly Vector2 descriptionLocation = new Vector2(itemLocation.X - 10, itemLocation.Y + 280);

        static readonly Vector2 equipmentSlotLocation = new Vector2(screenLocation.X + 88, screenLocation.Y + 227);
        static readonly Vector2 accessorySlotLocation = new Vector2(screenLocation.X + 320, screenLocation.Y + 227);

        static readonly Vector2 upArrowLocation = new Vector2(screenLocation.X + 939, screenLocation.Y + 60);
        static readonly Vector2 downArrowLocation = new Vector2(screenLocation.X + 939, screenLocation.Y + 325);

        static readonly Rectangle portraitLocation = new Rectangle((int)screenLocation.X + 78, (int)screenLocation.Y + 66, 144, 148);

        static readonly Vector2 helpSheetLocation = new Vector2(screenLocation.X + 65, screenLocation.Y + 345);

        const int maxItemDisplay = 9;
        const int accessorySlotCount = 3;


        #endregion

        #region Graphics


        Texture2D background;
        Texture2D helpSheet;
        //Texture2D pointer;
        Texture2D upArrow;
        Texture2D downArrow;
        Texture2D slotBox;

        string helpSheetText = "A/D: Switch Character\n"
                      + "Spacebar: Use\n"
                      + "T: Destroy\n"
                      + "Left Shift: Unequip\n";

        string helpSheetText2 = "\nQ/E: Move Item\n"
                      + "Esc: Back";


        #endregion

        #region Input


        ActionState actionState;

        int currentSelection;
        int firstDisplayItem;
        int currentPartyMember;

        int slotSelection;

        Inventory inventory;

        double fastScrollDelay = 0.1;
        double fastScrollInitialPause = 0.4;
        double ticker = 0.0;
        bool fastScrolling = false;

        #endregion

        #region Initialization


        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            inventory = Party.Singleton.GameState.Inventory;
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "InventoryScreenBackground_b");
            //pointer = content.Load<Texture2D>(interfaceTextureFolder + "Small_arrow");
            helpSheet = content.Load<Texture2D>(backgroundTextureFolder + "Instruction_box");
            upArrow = content.Load<Texture2D>(interfaceTextureFolder + "Up_arrow");
            downArrow = content.Load<Texture2D>(interfaceTextureFolder + "Down_arrow");
            slotBox = content.Load<Texture2D>(interfaceTextureFolder + "Slot_box");
        }


        #endregion

        #region Update Logic

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (actionState != ActionState.SelectItem)
            {
                SelectAccessory();
                return;
            }

            if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();
                SoundSystem.Play(AudioCues.menuDeny);
                return;
            }

            UpdateFastScrolling(gameTime);

            if (InputState.IsSwitchCharacterRight())
            {
                currentPartyMember = Math.Min(currentPartyMember + 1, Party.Singleton.GameState.Fighters.Count - 1);
            }
            else if (InputState.IsSwitchCharacterLeft())
            {
                currentPartyMember = Math.Max(currentPartyMember - 1, 0);
            }
            else if (InputState.IsUnequipPressed())
            {
                PCFightingCharacter member = Party.Singleton.GameState.Fighters[currentPartyMember];
                if (member.SlotOne != null || member.SlotTwo != null || member.SlotThree != null)
                {
                    actionState = ActionState.UnequipAccessory;

                    if (member.SlotOne != null)
                        slotSelection = 0;
                    else if (member.SlotTwo != null)
                        slotSelection = 1;
                    else
                        slotSelection = 2;
                }
            }
            else if (InputState.IsMoveItemUp())
            {
                if (inventory.Items.Count > 0 && currentSelection > 0)
                {
                    inventory.SwapItems(currentSelection, currentSelection - 1);
                    AdjustPointerUp();
                }
            }
            else if (InputState.IsMoveItemDown())
            {
                if (inventory.Items.Count > 0 && currentSelection < inventory.Items.Count - 1)
                {
                    inventory.SwapItems(currentSelection, currentSelection + 1);
                    AdjustPointerDown();
                }
            }

            //if (actionState == ActionState.SelectItem)
            //{
                if (InputState.IsDestroyPressed())
                {
                    if (inventory.Items.Count > 0)
                    {
                        Item item = ItemPool.RequestItem(inventory.Items[currentSelection].ItemName);

                        if (item is QuestItem)
                        {
                            SoundSystem.Play(AudioCues.menuDeny);
                        }
                        else
                        {
                            ScreenManager.AddScreen(new ConfirmItemDestroyScreen(currentSelection, null));
                            return;
                        }
                    }
                }
                SelectItem();
            //}
        }

        #region Helpers


        /// <summary>
        /// Attempts to decrement the currentSelection by one.
        /// </summary>
        private void AdjustPointerUp()
        {
            if (currentSelection - 1 != -1)
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
            if (currentSelection + 1 != inventory.Items.Count)
            {
                SoundSystem.Play(AudioCues.menuMove);
            }

            currentSelection = Math.Min(currentSelection + 1, inventory.Items.Count - 1);
            if (currentSelection - firstDisplayItem >= maxItemDisplay)
            {
                firstDisplayItem = Math.Min(firstDisplayItem + 1, inventory.Items.Count - maxItemDisplay);
            }
        }


        /// <summary>
        /// Handles logic for selecting an accessory
        /// </summary>
        private void SelectAccessory()
        {
            if (InputState.IsMenuDown())
            {
                if (slotSelection == 3) //Code I would like to use to view equipment
                {
                    slotSelection = 4;
                }
                else if (slotSelection == 4)
                {
                    slotSelection = 3;
                }
                else
                {
                    slotSelection = Math.Min(slotSelection + 1, accessorySlotCount - 1);
                }
            }
            else if (InputState.IsMenuUp())
            {
                if (slotSelection == 3) //Code I would like to use to view equipment
                {
                    slotSelection = 4;
                }
                else if (slotSelection == 4)
                {
                    slotSelection = 3;
                }
                else
                {
                    slotSelection = Math.Max(slotSelection - 1, 0);
                }
            }
            else if (InputState.IsMenuCancel())
            {
                actionState = ActionState.SelectItem;
            }
            else if (InputState.IsMenuLeft())
            {
                slotSelection = 3;//Code I would like to use to view equipment
            }
            else if (InputState.IsMenuRight())
            {
                slotSelection = 0;//Code I would like to use to view equipment
            }
            else if (InputState.IsMenuSelect())
            {
                PCFightingCharacter character = Party.Singleton.GameState.Fighters[currentPartyMember];

                if (slotSelection == 0 && !string.IsNullOrEmpty(character.SlotOne))
                {
                    inventory.AddItem(ItemPool.RequestItem(character.SlotOne), 1);
                }
                else if (slotSelection == 1 && !string.IsNullOrEmpty(character.SlotTwo))
                {
                    inventory.AddItem(ItemPool.RequestItem(character.SlotTwo), 1);
                }
                else if (slotSelection == 2 && !string.IsNullOrEmpty(character.SlotThree))
                {
                    inventory.AddItem(ItemPool.RequestItem(character.SlotThree), 1);
                }

                if (actionState == ActionState.UnequipAccessory)
                {
                    if (slotSelection == 3 || slotSelection == 4)
                    {
                        SoundSystem.Play(AudioCues.menuDeny);
                    }
                    else
                    {
                        UnequipAccessory();
                    }
                }
                else
                {
                    if (slotSelection == 3 || slotSelection == 4)
                    {
                        SoundSystem.Play(AudioCues.menuDeny);
                    }
                    else
                    {
                        EquipAccessory();
                    }
                }
            }
        }


        /// <summary>
        /// Handles logic for unequipping an Accessory
        /// </summary>
        private void UnequipAccessory()
        {
            Party.Singleton.GameState.Fighters[currentPartyMember].UnequipAccessory(slotSelection);

            currentSelection = inventory.Items.Count - 1;

            firstDisplayItem = Math.Max(currentSelection - maxItemDisplay + 1, 0);

            actionState = ActionState.SelectItem;
        }


        /// <summary>
        /// Handles logic for equipping an Accessory
        /// </summary>
        private void EquipAccessory()
        {
            Item item = ItemPool.RequestItem(inventory.Items[currentSelection].ItemName);

            Party.Singleton.GameState.Fighters[currentPartyMember].UnequipAccessory(slotSelection);
            Party.Singleton.GameState.Fighters[currentPartyMember].EquipAccessory(item as Accessory, slotSelection);

            if (inventory.ItemCount(item) == 1 && currentSelection == inventory.Items.Count - 1)
            {
                AdjustPointerUp();
            }

            inventory.RemoveItem(item, 1);

            actionState = ActionState.SelectItem;
        }


        /// <summary>
        /// Handles logic for selecting an Item from the Inventory
        /// </summary>
        private void SelectItem()
        {
            if (InputState.IsMenuDown())
            {
                AdjustPointerDown();
            }
            else if (InputState.IsMenuUp())
            {
                AdjustPointerUp();
            }
            else if (InputState.IsMenuSelect() && inventory.Items.Count > 0)
            {
                Item item = ItemPool.RequestItem(inventory.Items[currentSelection].ItemName);

                if (item is ConsumableItem)
                {
                    if (inventory.ItemCount(item) == 1 && currentSelection == inventory.Items.Count - 1)
                    {
                        AdjustPointerUp();
                    }

                    //If the character has full health, don't consume. Else, consume.
                    if (item is SmallHealthPotion || item is MediumHealthPotion || item is LargeHealthPotion || item is HugeHealthPotion)
                    {
                        if (Party.Singleton.GameState.Fighters[currentPartyMember].FighterStats.Health != Party.Singleton.GameState.Fighters[currentPartyMember].FighterStats.ModifiedMaxHealth)
                        {
                            Party.Singleton.GameState.Fighters[currentPartyMember].ConsumeItem(item as ConsumableItem);
                            inventory.RemoveItem(item, 1);
                        }
                        else
                        {
                            SoundSystem.Play(AudioCues.menuDeny);
                        }
                    }

                    //If the character has full mana, don't consume. Else, consume.
                    else if (item is SmallEnergyPotion || item is MediumEnergyPotion || item is LargeEnergyPotion || item is HugeEnergyPotion)
                    {
                        if (Party.Singleton.GameState.Fighters[currentPartyMember].FighterStats.Energy != Party.Singleton.GameState.Fighters[currentPartyMember].FighterStats.ModifiedMaxEnergy)
                        {
                            Party.Singleton.GameState.Fighters[currentPartyMember].ConsumeItem(item as ConsumableItem);
                            inventory.RemoveItem(item, 1);
                        }
                        else
                        {
                            SoundSystem.Play(AudioCues.menuDeny);
                        }
                    }

                    //I don't think there are any other consumable items, consume it.
                    else
                    {
                        Party.Singleton.GameState.Fighters[currentPartyMember].ConsumeItem(item as ConsumableItem);
                        inventory.RemoveItem(item, 1);
                    }
                }
                else if (item is Accessory)
                {
                    actionState = ActionState.EquipAccessory;

                    if (Party.Singleton.GameState.Fighters[currentPartyMember].SlotOne == null)
                        slotSelection = 0;
                    else if (Party.Singleton.GameState.Fighters[currentPartyMember].SlotTwo == null)
                        slotSelection = 1;
                    else
                        slotSelection = 2;
                }
                else
                {
                    SoundSystem.Play(AudioCues.menuDeny);
                }
            }
        }


        /// <summary>
        /// Controls the Fast Scrolling effect when up or down is held.
        /// </summary>
        private void UpdateFastScrolling(GameTime gameTime)
        {
            if (actionState == ActionState.SelectItem)
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
        }


        #endregion


        #endregion

        #region Render


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color whiteColor = Color.White * TransitionAlpha;
            Color blackColor = Color.Black * TransitionAlpha;
            Color redColor = Color.Red * TransitionAlpha;

            spriteBatch.Draw(background, screenLocation, whiteColor);

            //spriteBatch.Draw(helpSheet, helpSheetLocation, color);
            spriteBatch.DrawString(Fonts.MenuItem2, helpSheetText,
                new Vector2(helpSheetLocation.X + 20, helpSheetLocation.Y + 25), Fonts.MenuItemColor * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuItem2, helpSheetText2,
                new Vector2(helpSheetLocation.X + 200, helpSheetLocation.Y + 25), Fonts.MenuItemColor * TransitionAlpha);

            spriteBatch.DrawString(Fonts.MenuItem2, "Item Name", itemNameLocation + new Vector2(29, -3), Fonts.MenuItemColor * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuItem2, "Qty", quantityLocation + new Vector2(14, -3), Fonts.MenuItemColor * TransitionAlpha);

            int lastDisplayItem = Math.Min(inventory.Items.Count, firstDisplayItem + maxItemDisplay);

            spriteBatch.Draw(upArrow, upArrowLocation, firstDisplayItem > 0 ? whiteColor : redColor);
            spriteBatch.Draw(downArrow, downArrowLocation, lastDisplayItem < inventory.Items.Count ? whiteColor : redColor);

            RenderPartyMember(spriteBatch, Fonts.MenuItemColor * TransitionAlpha);
            RenderItemList(spriteBatch, Fonts.MenuItemColor * TransitionAlpha);
            RenderEquipment(spriteBatch, Fonts.MenuItemColor * TransitionAlpha);
            RenderAccessories(spriteBatch, Fonts.MenuItemColor * TransitionAlpha);
        }


        #region Helpers


        private void RenderPartyMember(SpriteBatch spriteBatch, Color color)
        {
            PCFightingCharacter member = Party.Singleton.GameState.Fighters[currentPartyMember];

//            Rectangle portraitLocation = new Rectangle((int)screenLocation.X + 78 + 10, (int)screenLocation.Y + 66 + 10, 124, 128);
            spriteBatch.Draw(member.Portrait, portraitLocation, Color.White * TransitionAlpha);
//            spriteBatch.Draw(member.Portrait, new Vector2(screenLocation.X + 78 + 15, screenLocation.Y + 66 + 15), Color.White * TransitionAlpha);

            Vector2 location = new Vector2(portraitLocation.Right, portraitLocation.Y + 20);

            spriteBatch.DrawString(Fonts.MenuTitle2, member.Name, new Vector2(location.X + 50, location.Y - 25), color);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                "HP " + member.FighterStats.Health.ToString() + " / " + member.FighterStats.ModifiedMaxHealth.ToString(),
                new Vector2(location.X + 15, location.Y + 25),
                color);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                "MP " + member.FighterStats.Energy.ToString() + " / " + member.FighterStats.ModifiedMaxEnergy.ToString(),
                new Vector2(location.X + 15, location.Y + 50),
                color);

            RenderStat(spriteBatch, color, "Str", member.FighterStats.ModifiedStrength, new Vector2(location.X + 15, location.Y + 80));
            RenderStat(spriteBatch, color, "Def", member.FighterStats.ModifiedDefense, new Vector2(location.X + 15, location.Y + 105));
            RenderStat(spriteBatch, color, "Will", member.FighterStats.ModifiedWillpower, new Vector2(location.X + 180, location.Y + 55));
            RenderStat(spriteBatch, color, "Int", member.FighterStats.ModifiedIntelligence, new Vector2(location.X + 180, location.Y + 80));
            RenderStat(spriteBatch, color, "Agil", member.FighterStats.ModifiedAgility, new Vector2(location.X + 180, location.Y + 105));
        }

        private void RenderStat(SpriteBatch spriteBatch, Color color, string name, int stat, Vector2 location)
        {
            spriteBatch.DrawString(Fonts.MenuItem2, name, location, color);
            spriteBatch.DrawString(Fonts.MenuItem2, stat.ToString(), new Vector2(location.X + 62, location.Y), color);
        }

        private void RenderEquipment(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(slotBox, equipmentSlotLocation + new Vector2(0, 7), color);
            spriteBatch.Draw(slotBox, equipmentSlotLocation + new Vector2(0, 37), color);

            if (!string.IsNullOrEmpty(Party.Singleton.GameState.Fighters[currentPartyMember].ArmorClassName))
            {
                Equipment armor = EquipmentPool.RequestEquipment(Party.Singleton.GameState.Fighters[currentPartyMember].ArmorClassName);
                
                // Draw Icon
                spriteBatch.Draw(
                    armor.Graphic,
                    new Vector2(equipmentSlotLocation.X + 3, equipmentSlotLocation.Y + 10),
                    color);

                // Draw Armor Name
                spriteBatch.DrawString(
                    Fonts.MenuItem2,
                    armor.DisplayName,
                    new Vector2(equipmentSlotLocation.X + 30, equipmentSlotLocation.Y + 7),
                    color);

                //spriteBatch.DrawString(Fonts.MenuItem2, ParseDescription(armor.Description), descriptionLocation, color);
            }

            if (!string.IsNullOrEmpty(Party.Singleton.GameState.Fighters[currentPartyMember].WeaponClassName))
            {
                Equipment weapon = EquipmentPool.RequestEquipment(Party.Singleton.GameState.Fighters[currentPartyMember].WeaponClassName);

                // Draw Icon
                spriteBatch.Draw(
                    weapon.Graphic,
                    new Vector2(equipmentSlotLocation.X + 3, equipmentSlotLocation.Y + 40),
                    color);
                
                // Draw Weapon Name
                spriteBatch.DrawString(
                    Fonts.MenuItem2,
                    weapon.DisplayName,
                    new Vector2(equipmentSlotLocation.X + 30, equipmentSlotLocation.Y + 36),
                    color);

                //spriteBatch.DrawString(Fonts.MenuItem2, ParseDescription(weapon.Description), descriptionLocation, color);
            }
        }

        private void RenderAccessories(SpriteBatch spriteBatch, Color color)
        {           
            Vector2 location = accessorySlotLocation;

            for (int i = 0; i < accessorySlotCount; ++i)
            {
                spriteBatch.Draw(slotBox, location, color);

                if ((actionState == ActionState.UnequipAccessory || actionState == ActionState.EquipAccessory) && (slotSelection == i || slotSelection == 3 || slotSelection == 4))
                {
                    Vector2 arrowPosition = location + new Vector2(-29, -1);
                    if (slotSelection == 3)
                    {
                        arrowPosition = new Vector2(186, 305);
                    }
                    else if (slotSelection == 4)
                    {
                        arrowPosition = new Vector2(186, 334);
                    }

                    spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, Color.White * TransitionAlpha, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);

                    Item selectedItem = null;
                    Equipment selectedEquipment = null;
                    PCFightingCharacter pc = Party.Singleton.GameState.Fighters[currentPartyMember];
                    if (slotSelection == 0 && pc.SlotOne != null)
                    {
                        selectedItem = ItemPool.RequestItem(pc.SlotOne);
                    }
                    else if (slotSelection == 1 && pc.SlotTwo != null)
                    {
                        selectedItem = ItemPool.RequestItem(pc.SlotTwo);
                    }
                    else if (slotSelection == 2 && pc.SlotThree != null)
                    {
                        selectedItem = ItemPool.RequestItem(pc.SlotThree);
                    }
                    else if (slotSelection == 3) //SlotFour would never be null
                    {
                        selectedEquipment = EquipmentPool.RequestEquipment(pc.ArmorClassName);
                    }
                    else if (slotSelection == 4) //SlotFive would never be null
                    {
                        selectedEquipment = EquipmentPool.RequestEquipment(pc.WeaponClassName);
                    }
                    if (selectedItem != null)
                    {
                        spriteBatch.DrawString(Fonts.MenuItem2, ParseDescription(selectedItem.Description), descriptionLocation, color);
                    }
                    if (selectedEquipment != null)
                    {
                        spriteBatch.DrawString(Fonts.MenuItem2, ParseDescription(selectedEquipment.Description), descriptionLocation, color);
                    }
                }

                location.Y += Fonts.MenuItem2.LineSpacing + 2;
            }

            location = accessorySlotLocation;

            string slotone = "";
            string slottwo = "";
            string slotthree = "";
            if (Party.Singleton.GameState.Fighters[currentPartyMember].SlotOne != null)
            {
                slotone = Party.Singleton.GameState.Fighters[currentPartyMember].SlotOne;
                RenderAccessorySlot(spriteBatch, color, ItemPool.RequestItem(slotone) as Accessory, location);
            }
            location.Y += Fonts.MenuItem2.LineSpacing + 2;
            if (Party.Singleton.GameState.Fighters[currentPartyMember].SlotTwo != null)
            {
                slottwo = Party.Singleton.GameState.Fighters[currentPartyMember].SlotTwo;
                RenderAccessorySlot(spriteBatch, color, ItemPool.RequestItem(slottwo) as Accessory, location);
            }
            location.Y += Fonts.MenuItem2.LineSpacing + 2;
            if (Party.Singleton.GameState.Fighters[currentPartyMember].SlotThree != null)
            {
                slotthree = Party.Singleton.GameState.Fighters[currentPartyMember].SlotThree;
                RenderAccessorySlot(spriteBatch, color, ItemPool.RequestItem(slotthree) as Accessory, location);
            }
        }

        private void RenderAccessorySlot(SpriteBatch spriteBatch, Color color, Accessory accessory, Vector2 location)
        {
            if (accessory == null)
                return;

            if (!string.IsNullOrEmpty(accessory.DisplayName))
            {
                spriteBatch.Draw(accessory.Graphic, new Vector2(location.X + 3, location.Y + 3), Color.White * TransitionAlpha);
                spriteBatch.DrawString(Fonts.MenuItem2, accessory.DisplayName, new Vector2(location.X + slotBox.Width + 5, location.Y), color);
            }
        }

        private void RenderItemList(SpriteBatch spriteBatch, Color color)
        {
            Vector2 location = itemLocation;

            int lastDisplayItem = Math.Min(inventory.Items.Count, firstDisplayItem + maxItemDisplay);

            for (int i = firstDisplayItem; i < lastDisplayItem; ++i)
            {
                Item item = ItemPool.RequestItem(inventory.Items[i].ItemName);

                if (actionState == ActionState.SelectItem && i == currentSelection)
                {
                    Vector2 arrowPosition = location + new Vector2(-29, -1);
                    spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, color, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Fonts.MenuItem2, ParseDescription(item.Description), descriptionLocation, color);
                }

                spriteBatch.DrawString(Fonts.MenuItem2, item.DisplayName, location, color);

                string quantity = (inventory.Items[i].Quantity).ToString();
                Vector2 quantityTextDimensions = Fonts.MenuItem2.MeasureString(quantity);

                float offset = 290 - quantityTextDimensions.X;

                spriteBatch.DrawString(Fonts.MenuItem2, quantity, new Vector2(location.X + offset, location.Y), color, 0.0f,
                    new Vector2(/*quantityTextDimensions.X*/0, 0), 1.0f, SpriteEffects.None, 0.0f);

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
                        if (Fonts.MenuItem2.MeasureString(tempLine + textToAdd).X <= 350)
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
                if (Fonts.MenuItem2.MeasureString(tempLine + textToAdd).X <= 332)
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
