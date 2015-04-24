using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class DoneScene : Scene
    {
        Dialog dialog;

        static readonly Dialog itemDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA264);

        public DoneScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.GameState.Inventory.AddItem(typeof(AmuletOfInsight), 1);
            dialog = itemDialog;
            dialog.DialogCompleteEvent += SetNathan;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight));
        }
        
        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(1, Color.Black, spriteBatch);
        }

        void SetNathan(object sender, EventArgs e)
        {
            Party.Singleton.SetPartyLeaderNathan();

            //Put fighting characters back in the correct order.
            Party.Singleton.RemoveFightingCharacter(Will.Instance);

            Party.Singleton.AddFightingCharacter(Nathan.Instance);
            Party.Singleton.AddFightingCharacter(Cara.Instance);
            Party.Singleton.AddFightingCharacter(Will.Instance);

            Will.Instance.FighterStats.Health = Will.Instance.FighterStats.ModifiedMaxHealth;
            Will.Instance.FighterStats.Energy = Will.Instance.FighterStats.ModifiedMaxEnergy;

            state = SceneState.Complete;
        }
    }
}
