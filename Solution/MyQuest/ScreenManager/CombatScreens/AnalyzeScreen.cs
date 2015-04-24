using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class AnalyzeScreen : Screen
    {
        bool waitingForUserInput;
        FightingCharacter enemy;
        Texture2D background;
        Texture2D hpMpBar;
        Texture2D staminaBar;
        Texture2D RightArrow;
        Texture2D UpArrowGrey;
        Texture2D DownArrowBlue;
        Texture2D DownArrowGrey;
        Texture2D UpArrowBlue;
        float recWidth = 0.0f;
        int pageNumber = 1;
        int maxPages = 2;

        public AnalyzeScreen(FightingCharacter target)
        {
            enemy = target;
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            IsPopup = true;

            waitingForUserInput = true; 
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
            hpMpBar = content.Load<Texture2D>(interfaceTextureFolder + "HpManaBar");
            staminaBar = content.Load<Texture2D>(interfaceTextureFolder + "Stamina_Bar");
            background = content.Load<Texture2D>(backgroundTextureFolder + "Conversation_Log_Screen");
            RightArrow = content.Load<Texture2D>(interfaceTextureFolder + "Arrow");
            UpArrowGrey = content.Load<Texture2D>(interfaceTextureFolder + "Large_up_arrow_grey");
            DownArrowBlue = content.Load<Texture2D>(interfaceTextureFolder + "Large_down_arrow");
            DownArrowGrey = content.Load<Texture2D>(interfaceTextureFolder + "Large_down_arrow_grey");
            UpArrowBlue = content.Load<Texture2D>(interfaceTextureFolder + "Large_up_arrow");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (waitingForUserInput && (InputState.IsMenuSelect() || InputState.IsMenuCancel()))
            {
                waitingForUserInput = false;
                ExitAfterTransition();
            }

            if (InputState.IsScrollDown() && pageNumber < maxPages)
            {
                pageNumber++;
            }
            if (InputState.IsScrollUp() && pageNumber > 1)
            {
                pageNumber--;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color whiteColor = Color.White * TransitionAlpha;

            spriteBatch.Draw(background, new Vector2(100, 120), whiteColor);

            if (pageNumber == 1)
            {
                spriteBatch.Draw(UpArrowGrey, new Vector2(1005, 200), whiteColor);
                spriteBatch.Draw(DownArrowBlue, new Vector2(1005, 560), whiteColor);

                spriteBatch.DrawString(Fonts.MenuItem2,
                                            enemy.Name + " " + Strings.ZA174,
                                            new Vector2(190, 205), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2,
                                            Strings.StatusScreen_HP + " " + enemy.FighterStats.Health + " / " + enemy.FighterStats.ModifiedMaxHealth,
                                            new Vector2(200, 247), Fonts.MenuItemColor * TransitionAlpha);

                recWidth = 160 * ((float)enemy.FighterStats.Health / (float)enemy.FighterStats.ModifiedMaxHealth);
                spriteBatch.Draw(
                    ScreenManager.BlankTexture,
                    new Rectangle(410, 255, (int)recWidth + 1, 9),
                    Color.Red);
                spriteBatch.Draw(hpMpBar, new Vector2(410, 255), whiteColor);


                spriteBatch.DrawString(Fonts.MenuItem2,
                                            Strings.StatusScreen_MP + " " + enemy.FighterStats.Energy + " / " + enemy.FighterStats.ModifiedMaxEnergy,
                                            new Vector2(200, 297), Fonts.MenuItemColor * TransitionAlpha);

                recWidth = 160 * ((float)enemy.FighterStats.Energy / (float)enemy.FighterStats.ModifiedMaxEnergy);
                spriteBatch.Draw(
                    ScreenManager.BlankTexture,
                    new Rectangle(410, 305, (int)recWidth + 1, 9),
                    Color.Blue * TransitionAlpha);

                spriteBatch.Draw(hpMpBar, new Vector2(410, 305), whiteColor);


                spriteBatch.DrawString(Fonts.MenuItem2,
                                           Strings.StatusScreen_Level + " " + enemy.FighterStats.Level,
                                            new Vector2(600, 250), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2,
                                           Strings.AnalyzeScreen_Stamina + ": " + enemy.FighterStats.Stamina + " / " + enemy.FighterStats.ModifiedMaxStamina,
                                            new Vector2(200, 347), Fonts.MenuItemColor * TransitionAlpha);

                recWidth = 160 * ((float)enemy.FighterStats.Stamina / (float)enemy.FighterStats.ModifiedMaxStamina);
                spriteBatch.Draw(
                    ScreenManager.BlankTexture,
                    new Rectangle(410, 355, (int)recWidth + 1, 9),
                    Color.Yellow * TransitionAlpha);

                spriteBatch.Draw(hpMpBar, new Vector2(410, 355), whiteColor);


                spriteBatch.DrawString(Fonts.MenuItem2,
                                            Strings.AnalyzeScreen_Strength + ": " + enemy.FighterStats.ModifiedStrength,
                                            new Vector2(600, 300), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2,
                                           Strings.AnalyzeScreen_Defense + ": " + enemy.FighterStats.ModifiedDefense,
                                            new Vector2(600, 350), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2,
                                           Strings.AnalyzeScreen_Intelligence + ": " + enemy.FighterStats.ModifiedIntelligence,
                                           new Vector2(785, 250), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2,
                                           Strings.AnalyzeScreen_Willpower + ": " + enemy.FighterStats.ModifiedWillpower,
                                           new Vector2(785, 300), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2,
                                           Strings.AnalyzeScreen_Agility + ": " + enemy.FighterStats.ModifiedAgility,
                                           new Vector2(785, 350), Fonts.MenuItemColor * TransitionAlpha);

                spriteBatch.DrawString(Fonts.MenuItem2,
                                           Strings.StatusScreen_Skills + ": ",
                                           new Vector2(200, 400), Fonts.MenuItemColor * TransitionAlpha);

                int i = 0;
                int j = 1;
                foreach (String skill in enemy.SkillNames)
                {
                    Skill combatSkill = Utility.CreateInstanceFromName<Skill>("MyQuest", skill);

                    spriteBatch.DrawString(Fonts.MenuItem2,
                                             j + ". " + combatSkill.Name,
                                             new Vector2(270, 435 + i), Fonts.MenuItemColor * TransitionAlpha);
                    i += 35;
                    j += 1;
                }
            }

            else if (pageNumber == 2)
            {
                spriteBatch.Draw(UpArrowBlue, new Vector2(1005, 200), whiteColor);
                spriteBatch.Draw(DownArrowGrey, new Vector2(1005, 560), whiteColor);

                int i = 0;
                foreach (String skill in enemy.SkillNames)
                {
                    Skill combatSkill = Utility.CreateInstanceFromName<Skill>("MyQuest", skill);

                    spriteBatch.DrawString(Fonts.MenuItem2,
                                             enemy.Name + " " + Strings.AnalyzeScreen_SkillDescriptions,
                                             new Vector2(200, 220), Fonts.MenuItemColor * TransitionAlpha);

                    spriteBatch.DrawString(Fonts.MenuItem2,
                                              combatSkill.Name + " : " + ParseDescription(combatSkill.Description), // "This attack does something cool",
                                              new Vector2(220, 270 + i), Fonts.MenuItemColor * TransitionAlpha);

                    if (Fonts.MenuItem2.MeasureString(combatSkill.Description).X >= 620)
                    {
                        i += 25;
                    }

                    i += 50;
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
                        if (Fonts.MenuItem2.MeasureString(tempLine + textToAdd).X <= 1000/*450*/)
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
                if (Fonts.MenuItem2.MeasureString(tempLine + textToAdd).X <= 570/*620*/)
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
    }
}
