using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class RiftSkillScreen : Screen
    {
        #region Positions and Fields

        static readonly Vector2 screenLocation = new Vector2(375, 80);
        static readonly Vector2 townNameLocation = new Vector2(screenLocation.X + 45, screenLocation.Y + 20);
        static readonly Vector2 upArrowLocation = new Vector2(screenLocation.X + 303, screenLocation.Y + 20);
        static readonly Vector2 downArrowLocation = new Vector2(screenLocation.X + 303, screenLocation.Y + 170);

        const int maxTownDisplay = 7;

        PCFightingCharacter castingPartyMember;
        Skill skill;

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
        Texture2D upArrow;
        Texture2D downArrow;


        #endregion

        #region Initialization


        public RiftSkillScreen(PCFightingCharacter character, Skill skill)
        {
            castingPartyMember = character;
            this.skill = skill;
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Instruction_box");
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
            }
            else if (InputState.IsMenuUp())
            {
                AdjustPointerUp();
            }
            else if (InputState.IsMenuSelect())
            {
                if (Party.Singleton.GameState.InAgora && Nathan.Instance.unlockedAgoraRiftDestinations.Count > 0 ||
                    (!Party.Singleton.GameState.InAgora) && Nathan.Instance.unlockedElathiaRiftDestinations.Count > 0)
                {
                    int destinationIndex = Party.Singleton.GameState.InAgora ?
                        Nathan.Instance.unlockedAgoraRiftDestinations[currentSelection] :
                        Nathan.Instance.unlockedElathiaRiftDestinations[currentSelection];

                    Rift riftSkill = skill as Rift;
                    riftSkill.SetDestinationByIndex(destinationIndex);
                    skill.OutOfCombatActivate(castingPartyMember);
                }
            }
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
            int destinationCount = Party.Singleton.GameState.InAgora ?
                Nathan.Instance.unlockedAgoraRiftDestinations.Count :
                Nathan.Instance.unlockedElathiaRiftDestinations.Count;

            if (currentSelection + 1 != destinationCount)
            {
                SoundSystem.Play(AudioCues.menuMove);
            }

            currentSelection = Math.Min(currentSelection + 1, destinationCount - 1);
            if (currentSelection - firstDisplayTown >= maxTownDisplay)
            {
                firstDisplayTown = Math.Min(firstDisplayTown + 1, destinationCount - maxTownDisplay);
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

            int lastDisplayTown = Party.Singleton.GameState.InAgora ?                
                Math.Min(Nathan.Instance.unlockedAgoraRiftDestinations.Count, firstDisplayTown + maxTownDisplay) :
                Math.Min(Nathan.Instance.unlockedElathiaRiftDestinations.Count, firstDisplayTown + maxTownDisplay);

            spriteBatch.Draw(background, screenLocation, null, whiteColor, 0.0f, Vector2.Zero, 1.25f, SpriteEffects.None, 0.0f);

            int destinationCount = Party.Singleton.GameState.InAgora ?
                Nathan.Instance.unlockedAgoraRiftDestinations.Count :
                Nathan.Instance.unlockedElathiaRiftDestinations.Count;

            if (destinationCount == 0)
            {
                return;
            }

            spriteBatch.Draw(upArrow, upArrowLocation, null,
                firstDisplayTown > 0 ? whiteColor : redColor,
                0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            spriteBatch.Draw(downArrow, downArrowLocation, null,
                lastDisplayTown < destinationCount ? whiteColor : redColor,
                0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            RenderTownNames(spriteBatch);
        }


        private void RenderTownNames(SpriteBatch spriteBatch)
        {
            Vector2 printNameLocation = townNameLocation;
            printNameLocation.X -= 5;

            int destinationCount = Party.Singleton.GameState.InAgora ?
                Nathan.Instance.unlockedAgoraRiftDestinations.Count :
                Nathan.Instance.unlockedElathiaRiftDestinations.Count;

            int lastDisplayTown = Math.Min(destinationCount, firstDisplayTown + maxTownDisplay);

            for (int i = firstDisplayTown; i < lastDisplayTown; ++i)
            {
                if (i == currentSelection)
                {
                    Vector2 arrowPosition = printNameLocation + new Vector2(-29, -1);
                    spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, Color.White * TransitionAlpha, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
                }

                int destinationIndex = Party.Singleton.GameState.InAgora ?
                    Nathan.Instance.unlockedAgoraRiftDestinations[i] :
                    Nathan.Instance.unlockedElathiaRiftDestinations[i];

//                int destinationIndex = Nathan.Instance.unlockedRiftDestinations[i];
                Rift.RiftDestination destination = Party.Singleton.GameState.InAgora ?
                    Rift.agoraDestinations[destinationIndex] :
                    Rift.elathiaDestinations[destinationIndex];
                spriteBatch.DrawString(Fonts.MenuItem2, destination.Name, printNameLocation, Fonts.CombatMenuItemColor * TransitionAlpha);
                printNameLocation.Y += Fonts.MenuItem2.LineSpacing;
            }
        }

        #endregion        
    }
}
