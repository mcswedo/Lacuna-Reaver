using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class WillUpgradeSceneB : Scene
    {
        Dialog dialog;

        Dialog zellaDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA627);

        Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA628);

        Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA629, Strings.ZA630);

        Dialog starUpgradeDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA631, Strings.ZA632);

        public WillUpgradeSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            if (Party.Singleton.Leader.TilePosition.X == 4)
            {
                Party.Singleton.Leader.FaceDirection(Direction.South);
            }
            else if (Party.Singleton.Leader.TilePosition.Y == 5 && Party.Singleton.Leader.TilePosition.X < 8)
            {
                Party.Singleton.Leader.FaceDirection(Direction.East);
            }
            else// if (Party.Singleton.Leader.TilePosition.Equals(new Point(8, 3)))
            {
                Party.Singleton.Leader.FaceDirection(Direction.West);
            }

            dialog = zellaDialog;
            dialog.DialogCompleteEvent += Reward;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "MasterBlacksmith"));
        }

        void Reward(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= Reward;

            dialog = rewardDialog;
            dialog.DialogCompleteEvent += WillResponse;

            Equipment willArmor = EquipmentPool.RequestEquipment("ExpertsLeatherArmor");
            Equipment willWeapon = EquipmentPool.RequestEquipment("ExpertsScythe");

            SoundSystem.Play(AudioCues.ChestOpen);
            Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(willArmor);
            Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(willWeapon);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft));
        }

        void WillResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= WillResponse;

            dialog = willDialog;
            dialog.DialogCompleteEvent += WillReturn;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void WillReturn(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= WillReturn;

            SceneHelper willHelper = new MoveNpcCharacterHelper(
                Party.will,
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

             willHelper.OnCompleteEvent += OnCompleteEvent;

            helpers.Add(willHelper);
        }

        void OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                Party.will,
                Point.Zero,
                ModAction.Remove,
                true);

            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(StarfireOre)) >= 1)
            {
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(StarfireOre), 1);

                dialog = starUpgradeDialog;
                dialog.DialogCompleteEvent += CaraSceneTransition;

                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "MasterBlacksmith"));
            }
            else
            {
                state = SceneState.Complete;
            }
        }

        void CaraSceneTransition(object sender, EventArgs e)
        {
            Party.Singleton.AddAchievement(MasterBlacksmithsController.starfireAchievement);

            ScreenManager.Singleton.AddScreen(
                (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "CaraUpgradeCutSceneScreen"));

            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}