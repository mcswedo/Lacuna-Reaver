using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class CaraUpgradeSceneA : Scene
    {
        Dialog dialog;

        Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA635, Strings.ZA636);

        Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA637);

        Dialog zellaDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA638, Strings.ZA639);

        Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA640);

        public CaraUpgradeSceneA(Screen screen)
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
                SceneHelper caraHelper = new MoveNpcCharacterHelper(
                    Party.cara,
                    Party.Singleton.Leader.TilePosition,
                    false,
                    new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y + 1),
                    3f);

                helpers.Add(caraHelper);

                caraHelper.OnCompleteEvent += CaraReact;
            }
            else if (Party.Singleton.Leader.TilePosition.Y == 5 && Party.Singleton.Leader.TilePosition.X < 8)
            {
                SceneHelper caraHelper = new MoveNpcCharacterHelper(
                    Party.cara,
                    Party.Singleton.Leader.TilePosition,
                    false,
                    new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y),
                    3f);

                helpers.Add(caraHelper);

                caraHelper.OnCompleteEvent += CaraReact;
            }
            else //if (Party.Singleton.Leader.TilePosition.Equals(new Point(8, 3)))
            {
                SceneHelper caraHelper = new MoveNpcCharacterHelper(
                    Party.cara,
                    Party.Singleton.Leader.TilePosition,
                    false,
                    new Point(Party.Singleton.Leader.TilePosition.X - 1, Party.Singleton.Leader.TilePosition.Y),
                    3f);

                helpers.Add(caraHelper);

                caraHelper.OnCompleteEvent += CaraReact;
            }
        }

        void CaraReact(object sender, EventArgs e)
        {

            NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);

            if (Party.Singleton.Leader.TilePosition.X == 4)
            {
                cara.FaceDirection(Direction.East);
            }
            else// if (Party.Singleton.Leader.TilePosition.Y == 5 && Party.Singleton.Leader.TilePosition.X < 8)
            {
                cara.FaceDirection(Direction.North);
            }

            dialog = caraDialog1;
            dialog.DialogCompleteEvent -= CaraReact;
            dialog.DialogCompleteEvent += Reward;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void Reward(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= Reward;

            dialog = rewardDialog;
            dialog.DialogCompleteEvent += ZellaResponse;

            Equipment caraArmor = EquipmentPool.RequestEquipment("ExpertsClothArmor");
            Equipment caraWeapon = EquipmentPool.RequestEquipment("ExpertsBook");

            SoundSystem.Play(AudioCues.ChestOpen);
            Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(caraArmor);
            Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(caraWeapon);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft));
        }

        void ZellaResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ZellaResponse;

            dialog = zellaDialog;
            dialog.DialogCompleteEvent += CaraResponse;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "MasterBlacksmith"));
        }

        void CaraResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse;

            dialog = caraDialog2;
            dialog.DialogCompleteEvent += CaraReturn;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void CaraReturn(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraReturn;

            SceneHelper caraHelper = new MoveNpcCharacterHelper(
                Party.cara,
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            caraHelper.OnCompleteEvent += SceneDone;

            helpers.Add(caraHelper);
        }

        void SceneDone(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                Party.cara,
                Point.Zero,
                ModAction.Remove,
                true);

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