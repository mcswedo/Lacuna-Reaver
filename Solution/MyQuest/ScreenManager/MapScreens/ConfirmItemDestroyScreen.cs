using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class ConfirmItemDestroyScreen : MenuScreen
    {
        #region Defaults


        string message = Strings.ConfirmItemDestroyScreen_DestroyThisItem; //"Delete this item?";

        static readonly Vector2 defaultLocation = new Vector2(710, 348);


        #endregion

        #region Fields


        Texture2D background;
        //Texture2D pointer;

        Vector2 location;
        Vector2 messageLocation;

        int itemIndex;


        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new ConfirmItemDestroyScreen.
        /// </summary>
        /// <param name="itemSlot">The item index within the party's inventory that is in question</param>
        /// <param name="location">The location on screen where this screen should appear. If null is used,
        /// this screen will assume its default location</param>
        public ConfirmItemDestroyScreen(int itemSlot, Vector2? location)
        {
            this.location = location ?? defaultLocation;

            itemIndex = itemSlot;

            StaticMenuOption yesOption = new StaticMenuOption(
                this, 
                Strings.Yes, 
                new Vector2(this.location.X + 60, this.location.Y + 80));

            StaticMenuOption noOption = new StaticMenuOption(
                this, 
                Strings.No, 
                new Vector2(this.location.X + 60, this.location.Y + 105));

            yesOption.OnSelectEvent += new EventHandler(yesOption_OnSelectEvent);
            noOption.OnSelectEvent += new EventHandler(noOption_OnSelectEvent);

            menuOptions.Add(yesOption);
            menuOptions.Add(noOption);

            selectedOption = 1;
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.15);
            TransitionOffTime = TimeSpan.FromSeconds(0.15);

            messageLocation = new Vector2(
                (location.X + background.Width / 2) - (Fonts.MenuTitle2.MeasureString(message).X / 2),
                location.Y + 10);
        }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(backgroundTextureFolder + "Instruction_box");
            //pointer = content.Load<Texture2D>(interfaceTextureFolder + "Small_arrow");
        }


        #endregion

        #region Callbacks


        void noOption_OnSelectEvent(object sender, EventArgs e)
        {
            ExitAfterTransition();
        }

        void yesOption_OnSelectEvent(object sender, EventArgs e)
        {
            Party.Singleton.GameState.Inventory.RemoveItem(itemIndex);
            ExitAfterTransition();
        }


        #endregion

        #region Update and Draw

        public override void HandleInput(GameTime gameTime)
        {
            base.HandleInput(gameTime);

            if (InputState.IsMenuCancel())
                ExitAfterTransition();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameLoop.Instance.AltSpriteBatch;

            spriteBatch.Draw(background, location, Color.White * TransitionAlpha);
            spriteBatch.DrawString(Fonts.MenuTitle2, message, messageLocation + new Vector2(0, 15), Fonts.MenuTitleColor * TransitionAlpha);
            foreach (MenuOption option in menuOptions)
            {
                option.Draw(spriteBatch, gameTime);
            }
            Vector2 arrowPosition = menuOptions[selectedOption].Position + new Vector2(-35, -1);
            spriteBatch.Draw(ScreenManager.PointerTexture, arrowPosition, null, Color.White * TransitionAlpha, 0, Vector2.Zero, ScreenManager.ArrowScale, SpriteEffects.None, 0);
        }

        #endregion
    }
}
