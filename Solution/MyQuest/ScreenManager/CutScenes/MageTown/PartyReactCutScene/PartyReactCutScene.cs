using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class PartyReactCutScene : Scene
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Dialog

        Dialog jannieDialog0 = new Dialog(DialogPrompt.NeedsClose, Strings.Z711);

        Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z712);

        Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z713);

        Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z714);

        Dialog jannieDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z715, Strings.Z716);

        Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z717);

        Dialog jannieDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z718);

        Dialog willDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z719, Strings.Z720);

        Dialog rewardDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA701);

        #endregion

        public PartyReactCutScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = jannieDialog0;
            dialog.DialogCompleteEvent += moveHelper1_OnCompleteEvent;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Jannie"));           
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= moveHelper1_OnCompleteEvent;

            MoveNpcCharacterHelper moveHelper1 = null;
            //if (Party.Singleton.Leader.TilePosition != new Point(2, 2))
            //{
            //}
            Party.Singleton.Leader.TilePosition = new Point(2, 2);
            Party.Singleton.Leader.Direction = Direction.North;
            moveHelper1 = new MoveNpcCharacterHelper(
                "Will",
                new Point(2, 2),
                false,
                new Point(1, 2),
                1.7f);
            Debug.Assert(Party.Singleton.Leader.Direction == Direction.North);
            //}
            //else
            //{
            //    throw new Exception("Party leader in invalid position.");
            //}
            helpers.Add(moveHelper1);
            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            MoveNpcCharacterHelper moveHelper2 = null;
            if (Party.Singleton.Leader.TilePosition == new Point(2, 2))
            {
                moveHelper2 = new MoveNpcCharacterHelper(
                 "Cara",
                  new Point(2, 2),
                  false,
                  new Point(3, 2),
                  1.7f);
                Debug.Assert(Party.Singleton.Leader.Direction == Direction.North);
            }
            else
            {
                throw new Exception("Party leader in invalid position.");
            }
            helpers.Add(moveHelper2);
            moveHelper2.OnCompleteEvent += new EventHandler(Will1);

            NPCMapCharacter will = Party.Singleton.CurrentMap.GetNPC("Will");
            will.FaceDirection(Direction.South);
        }

        void Will1(object sender, EventArgs e)
        {
            NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC("Cara");
            cara.FaceDirection(Direction.North);
            dialog = willDialog1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
            dialog.DialogCompleteEvent += Cara1;
        }

        void Cara1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Cara1;
            dialog = caraDialog1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
            dialog.DialogCompleteEvent += Will2;
        }

        void Will2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Will2;
            NPCMapCharacter will = Party.Singleton.CurrentMap.GetNPC("Will");
            will.FaceDirection(Direction.West);
            dialog = willDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
            dialog.DialogCompleteEvent += Jannie1;
        }

        void Jannie1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Jannie1;
            dialog = jannieDialog1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Jannie"));
            dialog.DialogCompleteEvent += Cara2;
        }

        void Cara2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Cara2;
            dialog = caraDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
            dialog.DialogCompleteEvent += Jannie2;
        }

        void Jannie2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Jannie2;
            dialog = jannieDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Jannie"));
            dialog.DialogCompleteEvent += Will3;
        }

        void Will3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Will3;
            dialog = willDialog3;
            dialog.DialogCompleteEvent += RewardCallBack;

            Equipment book = EquipmentPool.RequestEquipment("AdvancedBook");
            Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(book);
            Party.Singleton.GameState.Inventory.RemoveItem(typeof (MysteriousPage), 1);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void RewardCallBack(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= RewardCallBack;
            dialog = rewardDialog;

            state = SceneState.Complete;
            SoundSystem.Play(AudioCues.ChestOpen);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight));
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
