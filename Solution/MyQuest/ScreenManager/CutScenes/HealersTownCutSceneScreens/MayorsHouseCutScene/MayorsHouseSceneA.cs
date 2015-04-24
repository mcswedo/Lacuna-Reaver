using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    public class MayorsHouseSceneA : Scene
    {
        Dialog dialog;

        Dialog awakeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z525, Strings.Z526, Strings.Z527);

        Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, ".....");

        Dialog forgotDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z528, Strings.Z529);

        Dialog nathanDialog2 = new Dialog(DialogPrompt.NeedsClose, "???");

        Dialog walkDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z530, Strings.Z531, Strings.Z532, Strings.Z533, Strings.Z534, Strings.Z535);

        NPCMapCharacter cara;

        public MayorsHouseSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
  
        }

        public override void Initialize()
        {
            Nathan.Instance.Reset();
            Cara.Instance.Reset();
            Will.Instance.Reset();
            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            cara.FaceDirection(Direction.West);
            Party.Singleton.RemoveAllFightingCharacters();
            PCFightingCharacter nathan = Party.Singleton.AddFightingCharacter(Party.nathan);
            Nathan.Instance.Reset();
            Sid.Instance.Reset();
            Max.Instance.Reset();
            Cara.Instance.Reset();
            Will.Instance.Reset();
            //Party.Singleton.GameState.Fighters[0].UnequipAccessory(0);
            //Party.Singleton.GameState.Fighters[0].UnequipAccessory(1);
            //Party.Singleton.GameState.Fighters[0].UnequipAccessory(2);
            //Nathan.Instance.SetLevel(1);
            //Party.Singleton.GetFightingCharacter(Party.nathan).SetLevel(1);
            Party.Singleton.GameState.Fighters[0].EquipArmor(EquipmentPool.RequestEquipment("Armor"));
            Party.Singleton.GameState.Fighters[0].EquipWeapon(EquipmentPool.RequestEquipment("TrainingSword"));
            Party.Singleton.GetFightingCharacter(Party.nathan).FighterStats.Experience = 0;
            Party.Singleton.GameState.Gold = 0;
            Party.Singleton.GameState.Inventory.Items.Clear();

            ScreenManager.Singleton.AddScreen(new DialogScreen(awakeDialog, DialogScreen.Location.TopRight, "Cara"));
            awakeDialog.DialogCompleteEvent += NathanDialog;

            // SAVE HERE
            Party.Singleton.SaveGameState(Party.saveFileName);

            Party.Singleton.Leader.FaceDirection(Direction.South);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void NathanDialog(object sender, EventArgs e)
        {
            awakeDialog.DialogCompleteEvent -= NathanDialog;
            dialog = nathanDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
            dialog.DialogCompleteEvent += ForgotDialog;
        }

        void ForgotDialog(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ForgotDialog;
            dialog = forgotDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Cara"));
            dialog.DialogCompleteEvent += NathanDialog2;
        }

        void NathanDialog2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanDialog2;
            dialog = nathanDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
            dialog.DialogCompleteEvent += WalkDialog;
        }

        void WalkDialog(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= WalkDialog;
            dialog = walkDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Cara"));

            state = SceneState.Complete;
        }

        #endregion
    }
}
