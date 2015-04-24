using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class QuitConfirmationScreen : MenuScreen
    {
        #region Fields


        Texture2D background;
        Texture2D pointer;

        readonly static Vector2 position = new Vector2(GlobalSettings.TitleSafeArea.Left + 5,
                                                       GlobalSettings.TitleSafeArea.Top + 5);
        static readonly Rectangle DefaultTextBoxPosition = new Rectangle(
            GlobalSettings.TitleSafeArea.Left + 5,
            GlobalSettings.TitleSafeArea.Top + 5,
            400,
            GlobalSettings.TitleSafeArea.Top + 155);


        #endregion

        #region Initialization


        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            IsPopup = true;

            menuOptions = new List<MenuOption>();

            float xpos = position.X + 330;
            float ypos = position.Y + 145;
            StaticMenuOption yes = new StaticMenuOption(this, Strings.Yes, new Vector2(xpos, ypos));
            StaticMenuOption no = new StaticMenuOption(this, Strings.No, new Vector2(xpos, ypos + 30));

            yes.OnSelectEvent += new EventHandler(yes_OnSelectEvent);
            no.OnSelectEvent += new EventHandler(no_OnSelectEvent);

            menuOptions.Add(yes);
            menuOptions.Add(no);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Instruction_box");
            pointer = content.Load<Texture2D>(interfaceTextureFolder + "Arrow");
        }


        #endregion

        #region Callbacks


        void yes_OnSelectEvent(object sender, EventArgs e)
        {
            ScreenManager.ExitAllScreens();
            //TileMapScreen.Instance.IsCutScenePlaying = false;
            TileMapScreen.Reset();
            ScreenManager.AddScreen(new MainMenuScreen());
        }

        void no_OnSelectEvent(object sender, EventArgs e)
        {
            ExitAfterTransition();
        }


        #endregion

        #region Methods


        public override void HandleInput(GameTime gameTime)
        {
            base.HandleInput(gameTime);
            if (InputState.IsMenuCancel())
            {
                ExitAfterTransition();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            spriteBatch.Draw(background, DefaultTextBoxPosition, Color.White * TransitionAlpha);

            Vector2 textPosition = position;
            textPosition.X = position.X + 35;
            textPosition.Y = position.Y + 25;

            spriteBatch.DrawString(Fonts.MenuTitle2, Strings.QuitConfirmationScreen_AreYouSure1, textPosition, Fonts.MenuTitleColor * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuTitle2, Strings.QuitConfirmationScreen_AreYouSure2, new Vector2(textPosition.X, textPosition.Y + 30), Fonts.MenuTitleColor * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuTitle2, Strings.QuitConfirmationScreen_AreYouSure3, new Vector2(textPosition.X, textPosition.Y + 60), Fonts.MenuTitleColor * TransitionAlpha);

            foreach (MenuOption entry in menuOptions)
            {
                entry.Draw(spriteBatch, gameTime);
            }

            spriteBatch.Draw(pointer, menuOptions[selectedOption].Position - new Vector2(pointer.Width + 10, 12), Color.White * TransitionAlpha);
        }

        #endregion        
    }
}
