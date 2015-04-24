using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class SkillScreen : Screen
    {
        Color whiteColor;
        Color redColor;

        #region Positions


        static readonly Vector2 screenLocation = new Vector2(128, 72);
        static readonly Vector2 skillNameLocation = new Vector2(screenLocation.X + 618, screenLocation.Y + 68);
        static readonly Vector2 mpCostLocation = new Vector2(skillNameLocation.X + 230, skillNameLocation.Y);
        static readonly Vector2 skillLocation = new Vector2(skillNameLocation.X - 10, skillNameLocation.Y + 50);
        static readonly Vector2 descriptionLocation = new Vector2(skillLocation.X - 10, skillLocation.Y + 280);

        static readonly Vector2 equipmentSlotLocation = new Vector2(screenLocation.X + 88, screenLocation.Y + 227);
        static readonly Vector2 accessorySlotLocation = new Vector2(screenLocation.X + 320, screenLocation.Y + 227);

        static readonly Vector2 upArrowLocation = new Vector2(screenLocation.X + 939, screenLocation.Y + 60);
        static readonly Vector2 downArrowLocation = new Vector2(screenLocation.X + 939, screenLocation.Y + 325);

        static readonly Rectangle portraitLocation = new Rectangle((int)screenLocation.X + 78, (int)screenLocation.Y + 65, 144, 148);

        static readonly Vector2 helpSheetLocation = new Vector2(screenLocation.X + 65, screenLocation.Y + 345);

        const int maxSkillDisplay = 9;
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
                      + "Q/E: Move Skill\n"
                      + "Esc: Back";

        #endregion

        #region Input


        int currentSelection;
        int firstDisplayItem;
        int currentPartyMember;

        List<Skill> skillList = new List<Skill>();

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
            currentPartyMember = 0;
            LoadSkills();
        }

        public void LoadSkills()
        {
            skillList.Clear();
            PCFightingCharacter currentCharacter = Party.Singleton.GameState.Fighters[currentPartyMember];

            foreach (string skillName in currentCharacter.SkillNames)
            {
                skillList.Add(SkillPool.RequestByName(skillName));
            }

            /// Shouldn't the currentSelection reset to 0 when the list changes ?
            /// 
            if (currentSelection >= skillList.Count)
            {
                currentSelection = skillList.Count - 1;
            }

            if (currentSelection < 0)
            {
                currentSelection = 0;
            }
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
            UpdateFastScrolling(gameTime);

            if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();

                SoundSystem.Play(AudioCues.menuDeny);
                return;
            }

            if (InputState.IsSwitchCharacterRight())
            {
                currentPartyMember = Math.Min(currentPartyMember + 1, Party.Singleton.GameState.Fighters.Count - 1);
                LoadSkills();

                SoundSystem.Play(AudioCues.menuMove);
            }
            else if (InputState.IsSwitchCharacterLeft())
            {
                currentPartyMember = Math.Max(currentPartyMember - 1, 0);
                LoadSkills();

                SoundSystem.Play(AudioCues.menuMove);
            }

            if (skillList.Count > 0)
                SelectSkill();
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
            if (currentSelection + 1 != skillList.Count)
            {
                SoundSystem.Play(AudioCues.menuMove);
            }

            currentSelection = Math.Min(currentSelection + 1, skillList.Count - 1);
            if (currentSelection - firstDisplayItem >= maxSkillDisplay)
            {
                firstDisplayItem = Math.Min(firstDisplayItem + 1, skillList.Count - maxSkillDisplay);
            }
        }

        /// <summary>
        /// Handles logic for selecting a Skill from the Inventory
        /// </summary>
        private void SelectSkill()
        {
            if (InputState.IsMenuDown())
            {
                AdjustPointerDown();
            }
            else if (InputState.IsMenuUp())
            {
                AdjustPointerUp();
            }
            else if (InputState.IsMenuSelect())
            {
                Skill skill = skillList[currentSelection];

                if (skill.MapSkill == true)
                {
                    PCFightingCharacter caster = Party.Singleton.GameState.Fighters[currentPartyMember];
                    if (skill.Name == "Rift") 
                    {
                        ScreenManager.AddScreen(new RiftSkillScreen(caster, skill));
                    }                 
                    else if (skill.Name == "DemoPhase")
                    {
                        ScreenManager.AddScreen(new DemoPhaseSelectionScreen());
                    }
                    else if (skill.Name == "GamePhase")
                    {
                        ScreenManager.AddScreen(new GamePhaseSelectionScreen());
                    }
                    else
                    {
                        ScreenManager.AddScreen(new TargetedSkillScreen(caster, skill));
                    }
                }
                else
                {
                    /// maybe play a sound effect indicating that
                    /// the current skill can not be used in this context
                }
            }
            else if (InputState.IsMoveItemUp())
            {
                if (skillList.Count > 0 && currentSelection > 0)
                {
                    Skill temp = skillList[currentSelection - 1];
                    skillList[currentSelection - 1] = skillList[currentSelection];
                    skillList[currentSelection] = temp;

                    PCFightingCharacter currentCharacter = Party.Singleton.GameState.Fighters[currentPartyMember];
                    string tempString = currentCharacter.SkillNames[currentSelection - 1];
                    currentCharacter.SkillNames[currentSelection - 1] = currentCharacter.SkillNames[currentSelection];
                    currentCharacter.SkillNames[currentSelection] = tempString;

                    AdjustPointerUp();
                }
            }
            else if (InputState.IsMoveItemDown())
            {
                if (skillList.Count > 0 && currentSelection < skillList.Count - 1)
                {
                    Skill temp = skillList[currentSelection + 1];
                    skillList[currentSelection + 1] = skillList[currentSelection];
                    skillList[currentSelection] = temp;

                    PCFightingCharacter currentCharacter = Party.Singleton.GameState.Fighters[currentPartyMember];
                    string tempString = currentCharacter.SkillNames[currentSelection + 1];
                    currentCharacter.SkillNames[currentSelection + 1] = currentCharacter.SkillNames[currentSelection];
                    currentCharacter.SkillNames[currentSelection] = tempString;

                    AdjustPointerDown();
                }
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

            whiteColor = Color.White * TransitionAlpha;
            //            Color blackColor = Color.Black * TransitionAlpha;
            redColor = Color.Red * TransitionAlpha;

            spriteBatch.Draw(background, screenLocation, whiteColor);

            spriteBatch.DrawString(Fonts.MenuItem2, helpSheetText, new Vector2(helpSheetLocation.X + 20, helpSheetLocation.Y + 25), Fonts.MenuItemColor * TransitionAlpha);

            spriteBatch.DrawString(Fonts.MenuItem2, "Skill Name", skillNameLocation, Fonts.MenuItemColor * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuItem2, "MP", mpCostLocation, Fonts.MenuItemColor * TransitionAlpha);

            int lastDisplayItem = Math.Min(skillList.Count, firstDisplayItem + maxSkillDisplay);

            spriteBatch.Draw(upArrow, upArrowLocation, firstDisplayItem > 0 ? whiteColor : redColor);
            spriteBatch.Draw(downArrow, downArrowLocation, lastDisplayItem < skillList.Count ? whiteColor : redColor);

            RenderPartyMember(spriteBatch);
            RenderSkillList(spriteBatch, Fonts.MenuItemColor * TransitionAlpha);
            RenderEquipment(spriteBatch, Fonts.MenuItemColor * TransitionAlpha);
            RenderAccessories(spriteBatch, Fonts.MenuItemColor * TransitionAlpha);
        }

        #region Helpers


        private void RenderPartyMember(SpriteBatch spriteBatch)
        {
            PCFightingCharacter member = Party.Singleton.GameState.Fighters[currentPartyMember];

            spriteBatch.Draw(member.Portrait, portraitLocation, whiteColor);
            //spriteBatch.Draw(member.Portrait, new Vector2(screenLocation.X + 78 + 15, screenLocation.Y + 66 + 15), Color.White * TransitionAlpha);

            Vector2 location = new Vector2(portraitLocation.Right, portraitLocation.Y + 20);

            spriteBatch.DrawString(Fonts.MenuTitle2, member.Name, new Vector2(location.X + 50, location.Y - 25), Fonts.MenuTitleColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                "HP " + member.FighterStats.Health.ToString() + " / " + member.FighterStats.ModifiedMaxHealth.ToString(),
                new Vector2(location.X + 15, location.Y + 25),
                Fonts.MenuItemColor * TransitionAlpha);

            spriteBatch.DrawString(
                Fonts.MenuItem2,
                "MP " + member.FighterStats.Energy.ToString() + " / " + member.FighterStats.ModifiedMaxEnergy.ToString(),
                new Vector2(location.X + 15, location.Y + 50),
                Fonts.MenuItemColor * TransitionAlpha);

            RenderStat(spriteBatch, Fonts.MenuItemColor * TransitionAlpha, "Str", member.FighterStats.ModifiedStrength, new Vector2(location.X + 15, location.Y + 80));
            RenderStat(spriteBatch, Fonts.MenuItemColor * TransitionAlpha, "Def", member.FighterStats.ModifiedDefense, new Vector2(location.X + 15, location.Y + 105));
            RenderStat(spriteBatch, Fonts.MenuItemColor * TransitionAlpha, "Will", member.FighterStats.ModifiedWillpower, new Vector2(location.X + 180, location.Y + 55));
            RenderStat(spriteBatch, Fonts.MenuItemColor * TransitionAlpha, "Int", member.FighterStats.ModifiedIntelligence, new Vector2(location.X + 180, location.Y + 80));
            RenderStat(spriteBatch, Fonts.MenuItemColor * TransitionAlpha, "Agil", member.FighterStats.ModifiedAgility, new Vector2(location.X + 180, location.Y + 105));
        }

        private void RenderStat(SpriteBatch spriteBatch, Color color, string name, int stat, Vector2 location)
        {
            spriteBatch.DrawString(Fonts.MenuItem2, name, location, color);
            spriteBatch.DrawString(Fonts.MenuItem2, stat.ToString(), new Vector2(location.X + 52, location.Y), color);
        }

        private void RenderEquipment(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(slotBox, equipmentSlotLocation, whiteColor);
            spriteBatch.Draw(slotBox, new Vector2(equipmentSlotLocation.X, equipmentSlotLocation.Y + Fonts.MenuItem2.LineSpacing + 8), whiteColor);

            if (!string.IsNullOrEmpty(Party.Singleton.GameState.Fighters[currentPartyMember].ArmorClassName))
            {
                Equipment armor = EquipmentPool.RequestEquipment(Party.Singleton.GameState.Fighters[currentPartyMember].ArmorClassName);

                // Draw Icon
                spriteBatch.Draw(
                    armor.Graphic,
                    new Vector2(equipmentSlotLocation.X + 3, equipmentSlotLocation.Y + 3),
                    whiteColor);

                // Draw Armor Name
                spriteBatch.DrawString(
                    Fonts.MenuItem2,
                    armor.DisplayName,
                    new Vector2(equipmentSlotLocation.X + slotBox.Width + 5, equipmentSlotLocation.Y),
                    color);
            }

            if (!string.IsNullOrEmpty(Party.Singleton.GameState.Fighters[currentPartyMember].WeaponClassName))
            {
                Equipment weapon = EquipmentPool.RequestEquipment(Party.Singleton.GameState.Fighters[currentPartyMember].WeaponClassName);

                // Draw Icon
                spriteBatch.Draw(
                    weapon.Graphic,
                    new Vector2(equipmentSlotLocation.X + 3, equipmentSlotLocation.Y + 3 + Fonts.MenuItem2.LineSpacing + 8),
                    whiteColor);

                // Draw Weapon Name
                spriteBatch.DrawString(
                    Fonts.MenuItem2,
                    weapon.DisplayName,
                    new Vector2(equipmentSlotLocation.X + slotBox.Width + 5, equipmentSlotLocation.Y + Fonts.MenuItem2.LineSpacing + 13),
                    color);
            }
        }

        private void RenderAccessories(SpriteBatch spriteBatch, Color color)
        {
            Vector2 location = accessorySlotLocation;

            for (int i = 0; i < accessorySlotCount; ++i)
            {
                spriteBatch.Draw(slotBox, location, whiteColor);

                location.Y += Fonts.MenuItem2.LineSpacing + 2;
            }

            location = accessorySlotLocation;

            string slotone = "";
            string slottwo = "";
            string slotthree = "";
            if (Party.Singleton.GameState.Fighters[currentPartyMember].SlotOne != null)
            {
                slotone = Party.Singleton.GameState.Fighters[currentPartyMember].SlotOne;
                RenderAccessorySlot(spriteBatch, whiteColor, ItemPool.RequestItem(slotone) as Accessory, location);
            }
            location.Y += Fonts.MenuItem2.LineSpacing + 2;
            if (Party.Singleton.GameState.Fighters[currentPartyMember].SlotTwo != null)
            {
                slottwo = Party.Singleton.GameState.Fighters[currentPartyMember].SlotTwo;
                RenderAccessorySlot(spriteBatch, whiteColor, ItemPool.RequestItem(slottwo) as Accessory, location);
            }
            location.Y += Fonts.MenuItem2.LineSpacing + 2;
            if (Party.Singleton.GameState.Fighters[currentPartyMember].SlotThree != null)
            {
                slotthree = Party.Singleton.GameState.Fighters[currentPartyMember].SlotThree;
                RenderAccessorySlot(spriteBatch, whiteColor, ItemPool.RequestItem(slotthree) as Accessory, location);
            }
        }

        private void RenderAccessorySlot(SpriteBatch spriteBatch, Color color, Accessory accessory, Vector2 location)
        {
            if (accessory == null)
                return;

            if (!string.IsNullOrEmpty(accessory.DisplayName))
            {
                spriteBatch.Draw(accessory.Graphic, new Vector2(location.X + 3, location.Y + 3), whiteColor);
                spriteBatch.DrawString(Fonts.MenuItem2, accessory.DisplayName, new Vector2(location.X + slotBox.Width + 5, location.Y), color);
            }
        }

        private void RenderSkillList(SpriteBatch spriteBatch, Color color)
        {
            Vector2 location = skillLocation;

            int lastDisplayItem = Math.Min(skillList.Count, firstDisplayItem + maxSkillDisplay);

            for (int i = firstDisplayItem; i < lastDisplayItem; ++i)
            {
                if (skillList.Count > i)
                {
                    Skill skill = skillList[i];

                    if (i == currentSelection)
                    {
                        Vector2 position = location + new Vector2(-17, 4);
                        spriteBatch.Draw(ScreenManager.PointerTexture, position, null, whiteColor, 0, Vector2.Zero, ScreenManager.SmallArrowScale, SpriteEffects.None, 0);
                        spriteBatch.DrawString(Fonts.MenuItem2, ParseDescription(skill.Description), descriptionLocation, color);
                    }
                    spriteBatch.DrawString(Fonts.MenuItem2, skill.Name, location, color);

                    string mpCost = skillList[i].MpCost.ToString();
                    Vector2 mpCostTextDimensions = Fonts.MenuItem2.MeasureString(mpCost);

                    spriteBatch.DrawString(Fonts.MenuItem2, mpCost, new Vector2(location.X + 310, location.Y), color, 0.0f,
                        new Vector2(mpCostTextDimensions.X, 0), 1.0f, SpriteEffects.None, 0.0f);

                    location.Y += Fonts.MenuItem2.LineSpacing;
                }
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
                        if (Fonts.MenuItem2.MeasureString(tempLine + textToAdd).X <= 360)
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
                if (Fonts.MenuItem2.MeasureString(tempLine + textToAdd).X <= 360)
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
