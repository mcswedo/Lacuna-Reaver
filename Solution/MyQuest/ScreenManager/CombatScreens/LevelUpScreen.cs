using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class LevelUpScreen : Screen
    {

        #region Graphics

        Texture2D background;
        Vector2 topLevelStatPosition = new Vector2();
        Vector2 mainTitlePosition = new Vector2();
        Vector2 skillTitlePosition = new Vector2();

        #endregion

        #region Fields

        PCFightingCharacter character;
        FighterStats lastLevelStats = new FighterStats();
        FighterStats currentLevelStats = new FighterStats();
        Skill skillReward;

        bool isLevelUpJinglePlayed = false;
        
        #endregion

        #region Constructor

        public LevelUpScreen(PCFightingCharacter character)
        {
            this.character = character;

            int oldSkillListSize = character.SkillNames.Count;

            //Take a snapshot of our stats before level up
            lastLevelStats = new FighterStats(character.FighterStats); 

            CharacterLevelUp.LevelCharacter(character);

            //Snapshot of stats after level up
            currentLevelStats = new FighterStats(character.FighterStats);

            if (oldSkillListSize == character.SkillNames.Count)
            {
                skillReward = null;
            }
            else
            {
                skillReward = SkillPool.RequestByName(character.SkillNames[0]);
            }
        }

        #endregion

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);
            IsPopup = true;
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Yes_no_box");
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (!isLevelUpJinglePlayed)
            {
                SoundSystem.Play(AudioCues.LevelUp);
                isLevelUpJinglePlayed = true;
            }

            if (InputState.IsMenuSelect())
            {
                SoundSystem.Play(AudioCues.menuConfirm);
                ScreenManager.RemoveScreen(this);
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            /*
            if (!coveredByOtherScreen && !isLevelUpJinglePlayed)
            {
                SoundSystem.Play(AudioCues.LevelUp);
                isLevelUpJinglePlayed = true;
            }
            */
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            topLevelStatPosition.X = 410;
            topLevelStatPosition.Y = 140;
            mainTitlePosition.X = 420;
            mainTitlePosition.Y = 105;

            int verticalTextOffset = 25;

            Color textColor = Fonts.CombatMenuItemColor * TransitionAlpha;

            spriteBatch.Draw(background, new Rectangle(400, 80, 375, 375), Color.White * TransitionAlpha);
            spriteBatch.DrawString(Fonts.CombatMenuItem2, character.Name + " " + Strings.ZA182 +" " + character.FighterStats.Level.ToString() + "!", mainTitlePosition, textColor);

            string health = (currentLevelStats.BaseMaxHealth - lastLevelStats.BaseMaxHealth).ToString();
            spriteBatch.DrawString(Fonts.CombatMenuItem2, "+" + health + " HP", topLevelStatPosition, textColor);
            topLevelStatPosition.Y += verticalTextOffset;

            string mp = (currentLevelStats.BaseMaxEnergy - lastLevelStats.BaseMaxEnergy).ToString();
            spriteBatch.DrawString(Fonts.CombatMenuItem2, "+" + mp + " MP", topLevelStatPosition, textColor);
            topLevelStatPosition.Y += verticalTextOffset;

            string strength = (currentLevelStats.BaseStrength - lastLevelStats.BaseStrength).ToString();
            spriteBatch.DrawString(Fonts.CombatMenuItem2, "+" + strength + " " + Strings.AnalyzeScreen_Strength, topLevelStatPosition, textColor);
            topLevelStatPosition.Y += verticalTextOffset;

            string intelligence = (currentLevelStats.BaseIntelligence - lastLevelStats.BaseIntelligence).ToString();
            spriteBatch.DrawString(Fonts.CombatMenuItem2, "+" + intelligence + " " + Strings.AnalyzeScreen_Intelligence, topLevelStatPosition, textColor);
            topLevelStatPosition.Y += verticalTextOffset;

            string defense = (currentLevelStats.BaseDefense - lastLevelStats.BaseDefense).ToString();
            spriteBatch.DrawString(Fonts.CombatMenuItem2, "+" + defense + " " + Strings.AnalyzeScreen_Defense, topLevelStatPosition, textColor);
            topLevelStatPosition.Y += verticalTextOffset;

            string willpower = (currentLevelStats.BaseWillpower - lastLevelStats.BaseWillpower).ToString();
            spriteBatch.DrawString(Fonts.CombatMenuItem2, "+" + willpower + " " + Strings.AnalyzeScreen_Willpower, topLevelStatPosition, textColor);
            topLevelStatPosition.Y += verticalTextOffset;

            string agility = (currentLevelStats.BaseAgility - lastLevelStats.BaseAgility).ToString();
            spriteBatch.DrawString(Fonts.CombatMenuItem2, "+" + agility + " " + Strings.AnalyzeScreen_Agility, topLevelStatPosition, textColor);
            topLevelStatPosition.Y += verticalTextOffset;

            if (skillReward != null)
            {
                skillTitlePosition.Y = topLevelStatPosition.Y + 40;
                skillTitlePosition.X = topLevelStatPosition.X;

                spriteBatch.DrawString(Fonts.CombatMenuItem2, Strings.ZA183, skillTitlePosition, textColor);
                skillTitlePosition.Y += verticalTextOffset;

                spriteBatch.DrawString(Fonts.CombatMenuItem2, skillReward.Name, skillTitlePosition, textColor);
            }
        }
    }
}
