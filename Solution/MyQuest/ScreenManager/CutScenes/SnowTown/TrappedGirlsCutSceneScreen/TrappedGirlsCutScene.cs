using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class TrappedGirlsCutScene : Scene
    {
        #region Fields

        Dialog dialog;

        #endregion

        #region Dialog

        Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z845);

        Dialog nathanDialog1 = new Dialog(DialogPrompt.NeedsClose, "?");

        Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z846, Strings.Z847);

        Dialog girlDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z848, Strings.Z849, Strings.Z850);

        Dialog caraDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z851, Strings.Z852);

        Dialog girlDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z853, Strings.Z854);

        Dialog itemDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z855);

        #endregion

        public TrappedGirlsCutScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MoveNpcCharacterHelper moveHelper1 = null;
            NPCMapCharacter savedGirl = Party.Singleton.CurrentMap.GetNPC("SavedGirl");

            if (Party.Singleton.Leader.TilePosition == new Point(7, 4))
            {
                savedGirl.FaceDirection(Direction.East);

                moveHelper1 = new MoveNpcCharacterHelper(
                 "Cara",
                 new Point(7, 4),
                 false,
                 new Point(8, 4),
                 1.7f);
                Debug.Assert(Party.Singleton.Leader.Direction == Direction.West);
            }
            else if(Party.Singleton.Leader.TilePosition == new Point(6,5))
            {
                savedGirl.FaceDirection(Direction.South);

                moveHelper1 = new MoveNpcCharacterHelper(
                 "Cara",
                 new Point(6, 5),
                 false,
                 new Point(7, 5),
                 1.7f);
                Debug.Assert(Party.Singleton.Leader.Direction == Direction.North);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(5, 4))
            {
                savedGirl.FaceDirection(Direction.West);

                moveHelper1 = new MoveNpcCharacterHelper(
                 "Cara",
                 new Point(5, 4),
                 false,
                 new Point(5, 5),
                 1.7f);
                Debug.Assert(Party.Singleton.Leader.Direction == Direction.East);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(6, 11))
            {
                moveHelper1 = new MoveNpcCharacterHelper(
                 "Cara",
                 new Point(6, 11),
                 false,
                 new Point(5, 11),
                 1.7f);
                Debug.Assert(Party.Singleton.Leader.Direction == Direction.East);
            }
            else
            {
                throw new Exception("Party leader in invalid position.");
            }
            helpers.Add(moveHelper1);
            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            dialog = caraDialog1;

            if (Party.Singleton.Leader.TilePosition == new Point(7, 4))
            {
                Party.Singleton.Leader.FaceDirection(Direction.East);
                NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC("Cara");
                cara.FaceDirection(Direction.West);
            }            
            else if(Party.Singleton.Leader.TilePosition == new Point(6,5))
            {
                Party.Singleton.Leader.FaceDirection(Direction.East);
                NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC("Cara");
                cara.FaceDirection(Direction.West);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(5, 4))
            {
                Party.Singleton.Leader.FaceDirection(Direction.South);
                NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC("Cara");
                cara.FaceDirection(Direction.North);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(6, 11))
            {
                Party.Singleton.Leader.FaceDirection(Direction.West);
                NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC("Cara");
                cara.FaceDirection(Direction.East);
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
            dialog.DialogCompleteEvent += Nathan1;
        }

        void Nathan1(object sender, PartyResponseEventArgs e)
        {
            dialog = nathanDialog1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
            dialog.DialogCompleteEvent += Cara2;
        }

        void Cara2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= Cara2;
            dialog = caraDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
            dialog.DialogCompleteEvent += Girl1;
        }

        void Girl1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Girl1;
            dialog = girlDialog1;

            if (Party.Singleton.Leader.TilePosition == new Point(7, 4))
            {
                Party.Singleton.Leader.FaceDirection(Direction.West);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(6, 5))
            {
                Party.Singleton.Leader.FaceDirection(Direction.North);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(5, 4))
            {
                Party.Singleton.Leader.FaceDirection(Direction.East);

            }
            else if (Party.Singleton.Leader.TilePosition == new Point(6, 11))
            {
                Party.Singleton.Leader.FaceDirection(Direction.East);

            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "TrappedGirl"));
            dialog.DialogCompleteEvent += Cara3;
        }

        void Cara3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Cara3;
            dialog = caraDialog3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
            dialog.DialogCompleteEvent += Girl2;
        }

        void Girl2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Girl2;
            dialog = girlDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "TrappedGirl"));
            dialog.DialogCompleteEvent += RecieveItem;
        }

        void RecieveItem(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= RecieveItem;
            Party.Singleton.GameState.Inventory.AddItem(typeof(EarthenGloves), 1);
            dialog = itemDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft));
            MusicSystem.InterruptMusic(AudioCues.ChestOpen);
            dialog.DialogCompleteEvent += ReturnCara;
        }

        void ReturnCara(object sender, PartyResponseEventArgs e)
        {
            MoveNpcCharacterHelper moveHelper2 = null;

            if (Party.Singleton.Leader.TilePosition == new Point(7, 4))
            {
                moveHelper2 = new MoveNpcCharacterHelper(
                 "Cara",
                 new Point(7, 4),
                 1.7f);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(6, 5))
            {
                moveHelper2 = new MoveNpcCharacterHelper(
                 "Cara",
                 new Point(6, 5),
                 1.7f);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(5, 4))
            {
                moveHelper2 = new MoveNpcCharacterHelper(
                 "Cara",
                 new Point(5, 4),
                 1.7f);
            }
            else if (Party.Singleton.Leader.TilePosition == new Point(6, 11))
            {
                moveHelper2 = new MoveNpcCharacterHelper(
                 "Cara",
                 new Point(6, 11),
                 1.7f);
            }
            helpers.Add(moveHelper2);
            moveHelper2.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent2);
        }

        void moveHelper1_OnCompleteEvent2(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "Cara",
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
