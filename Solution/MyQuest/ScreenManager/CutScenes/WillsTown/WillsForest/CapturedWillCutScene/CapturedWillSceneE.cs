using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CapturedWillSceneE : Scene
    {
        #region Fields

        const int reward = 700;

        const int totalStolenItems = 18;

        #endregion

        #region Dialog

        Dialog capturedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z942, Strings.Z943);

        Dialog justiceDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z944, Strings.Z945, Strings.Z946, Strings.Z947, Strings.Z948);

        Dialog standardRewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z347);

        Dialog bonusRewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z950 + " " + reward + " " + Strings.ZA612);

        #endregion
        
        public CapturedWillSceneE(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

        }

        public override void Update(GameTime gameTime)
        {
            capturedDialog.DialogCompleteEvent += nextDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(capturedDialog, DialogScreen.Location.TopLeft, "Cara"));

        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void nextDialog(object sender, PartyResponseEventArgs e)
        {
            capturedDialog.DialogCompleteEvent -= nextDialog;

            justiceDialog.DialogCompleteEvent += finalDialog;

            ScreenManager.Singleton.AddScreen(new DialogScreen(justiceDialog, DialogScreen.Location.TopRight, "MansionOwner"));
        }
        void finalDialog(object sender, PartyResponseEventArgs e)
        {
            justiceDialog.DialogCompleteEvent -= finalDialog;
            
            int stolenItemsCollected = Party.Singleton.GameState.Inventory.ItemCount(typeof(StolenItem));

            Dialog dialog;

            Party.Singleton.GameState.Inventory.AddItem(typeof(ObsidianCharm), 1);

            if (stolenItemsCollected == totalStolenItems)
            {
                dialog = bonusRewardDialog;
                Party.Singleton.GameState.Gold += reward;        
            }
            else
            {
                dialog = standardRewardDialog;             
            }

            if (stolenItemsCollected > 0)
            {
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(StolenItem), stolenItemsCollected);
            }

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
          
            state = SceneState.Complete;
        }

    }
}
