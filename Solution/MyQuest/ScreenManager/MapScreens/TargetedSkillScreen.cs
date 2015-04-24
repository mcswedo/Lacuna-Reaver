using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// Please add support for skills that target the entire party
/// Add logic to ensure that the caster has enough energy to use the skill

namespace MyQuest
{
    public class TargetedSkillScreen : Screen
    {
        #region Positions


        static readonly Vector2 screenLocation = new Vector2(375, 80);
        static readonly Vector2 characterNameLocation = new Vector2(screenLocation.X + 45, screenLocation.Y + 20);
        static readonly Vector2 hpLocation = new Vector2(characterNameLocation.X + 150, characterNameLocation.Y);
        static readonly Vector2 mpLocation = new Vector2(hpLocation.X, hpLocation.Y + 25);

        protected bool groupEffect = false;
        


        #endregion

        #region Graphics

        Texture2D background;
        //Texture2D pointer;

        #endregion

        #region Input

        int currentSelection;
        int numberOfCharacters;
        PCFightingCharacter castingPartyMember;
        Skill skill;

        #endregion

        #region Initialization

        public TargetedSkillScreen(PCFightingCharacter character, Skill theSkill)
        {
            castingPartyMember = character;
            skill = theSkill;
            groupEffect = theSkill.TargetsAll;
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            numberOfCharacters = Party.Singleton.GameState.Fighters.Count;
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Instruction_box");
            //pointer = content.Load<Texture2D>(interfaceTextureFolder + "Small_arrow");
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
            
            SelectCharacter();
        }

        #region Helpers


        /// <summary>
        /// Attempts to decrement the currentSelection by one.
        /// </summary>
        private void AdjustPointerUp()
        {
            currentSelection = Math.Max(currentSelection - 1, 0);
        }


        /// <summary>
        /// Attempts to increment the currentSelection by one
        /// </summary>
        private void AdjustPointerDown()
        {
            currentSelection = Math.Min(currentSelection + 1, numberOfCharacters - 1);
        }


        /// <summary>
        /// Handles logic for selecting an Item from the Inventory
        /// </summary>
        private void SelectCharacter()
        {
            if (!groupEffect)
            {
                if (InputState.IsMenuDown())
                {
                    AdjustPointerDown();
                }
                else if (InputState.IsMenuUp())
                {
                    AdjustPointerUp();
                }
            }
            if (InputState.IsMenuSelect() && numberOfCharacters > 0)
            {
                if (castingPartyMember.FighterStats.Energy >= skill.MpCost)
                {
                    List<PCFightingCharacter> fighters = Party.Singleton.GameState.Fighters;

                    if (groupEffect)
                    {
                        switch (numberOfCharacters)
                        {
                            case 1:
                                skill.OutOfCombatActivate(castingPartyMember, fighters[0]);
                                break;
                            case 2:
                                skill.OutOfCombatActivate(castingPartyMember, fighters[0], fighters[1]);
                                break;
                            case 3:
                                skill.OutOfCombatActivate(castingPartyMember, fighters[0], fighters[1], fighters[2]);
                                break;
                        }
                    }
                    else
                    {
                        skill.OutOfCombatActivate(castingPartyMember, fighters[currentSelection]);
                    }
                }
                else
                {
                    SoundSystem.Play(AudioCues.menuDeny);
                }
            }
        }


        #endregion


        #endregion

        #region Render


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            Color color = Color.White * TransitionAlpha;

            spriteBatch.Draw(background, screenLocation, null, color, 0.0f, Vector2.Zero, 1.25f, SpriteEffects.None, 0.0f);

            RenderPartyNames(spriteBatch);
        }

        private void RenderPartyNames(SpriteBatch spriteBatch)
        {
            string[] partyMember = new string[3];
            string[] health = new string[3];
            string[] mp = new string[3];

            for (int i = 0; i < 3; i++)
            {
                if (numberOfCharacters > i)
                {
                    partyMember[i] = Party.Singleton.GameState.Fighters[i].Name;
                    health[i] = Strings.StatusScreen_HP + " " + Party.Singleton.GameState.Fighters[i].FighterStats.Health.ToString()
                                + "/" + Party.Singleton.GameState.Fighters[i].FighterStats.ModifiedMaxHealth.ToString();
                    mp[i] = Strings.StatusScreen_MP + " " + Party.Singleton.GameState.Fighters[i].FighterStats.Energy.ToString()
                                + "/" + Party.Singleton.GameState.Fighters[i].FighterStats.ModifiedMaxEnergy.ToString();
                }
                else
                {
                    partyMember[i] = "";
                    health[i] = "";
                    mp[i] = "";
                }
            }

            Vector2 printNameLocation = new Vector2(screenLocation.X + 50, screenLocation.Y + 19);
            Vector2 printHealthLocation = new Vector2(screenLocation.X + 135, screenLocation.Y + 20);
            Vector2 printEnergyLocation = new Vector2(printHealthLocation.X, printHealthLocation.Y + 25);

            for (int i = 0; i < numberOfCharacters; i++)
            {
                spriteBatch.DrawString(Fonts.MenuItem2, partyMember[i], printNameLocation, Fonts.MenuItemColor * TransitionAlpha);
                spriteBatch.DrawString(Fonts.MenuItem2, health[i], printHealthLocation, Fonts.MenuItemColor * TransitionAlpha);
                spriteBatch.DrawString(Fonts.MenuItem2, mp[i], printEnergyLocation, Fonts.MenuItemColor * TransitionAlpha);
                if (groupEffect || i == currentSelection)
                {
                    Vector2 arrowPosition = printNameLocation + new Vector2(-29, -1);
                    spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, Color.White * TransitionAlpha, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
                }

                printNameLocation.Y += 61;
                printHealthLocation.Y += 61;
                printEnergyLocation.Y += 61;
            }

        }

        #endregion
    }
}
