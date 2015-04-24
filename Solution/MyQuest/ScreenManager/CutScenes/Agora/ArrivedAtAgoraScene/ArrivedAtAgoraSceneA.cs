using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class ArrivedAtAgoraSceneA : Scene
    {
        #region Dialog

        static readonly Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z988);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z989);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z990, Strings.Z991);

        static readonly Dialog nathanSecondDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z992);

        #endregion

        public ArrivedAtAgoraSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.MapMusicSwitch(Party.Singleton.CurrentMap);
            MusicSystem.Play(AudioCues.agoraOverworld);

            SceneHelper nathanHelper1 = new MovePCMapCharacterHelper(new Point(Party.Singleton.Leader.TilePosition.X, 
                Party.Singleton.Leader.TilePosition.Y + 1));


            nathanHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(nathanHelper1);
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            SceneHelper caraHelper = new MoveNpcCharacterHelper(
                Party.cara,
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                false,
                new Point(Party.Singleton.Leader.TilePosition.X - 1, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            caraHelper.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);

            helpers.Add(caraHelper);
         
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);

            SceneHelper willHelper = new MoveNpcCharacterHelper(
                Party.will,
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                false,
                new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            willHelper.OnCompleteEvent += new EventHandler(moveHelper3_OnCompleteEvent);
            helpers.Add(willHelper);
        }

        void moveHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.North);
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);

            nathanDialog.DialogCompleteEvent += willResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(nathanDialog, DialogScreen.Location.TopRight, "Nathan"));
        }

        void willResponse(object sender, EventArgs e)
        {
            willDialog.DialogCompleteEvent += caraResponse;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(willDialog, DialogScreen.Location.TopLeft, "Will"));
        }

        void caraResponse(object sender, EventArgs e)
        {
            caraDialog.DialogCompleteEvent += nathanResponse;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(caraDialog, DialogScreen.Location.TopRight, "Cara"));
        }

        void nathanResponse(object sender, EventArgs e)
        {
            nathanSecondDialog.DialogCompleteEvent += partyJoin;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(nathanSecondDialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void partyJoin(object sender, EventArgs e)
        {
            SceneHelper moveHelper3 = new MoveNpcCharacterHelper(
             Party.will, new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y), 3.0f);

            SceneHelper moveHelper4 = new MoveNpcCharacterHelper(
             Party.cara, new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y), 3.0f);

            moveHelper4.OnCompleteEvent += new EventHandler(moveHelper4_OnCompleteEvent);

            helpers.Add(moveHelper3);
            helpers.Add(moveHelper4);
        }

        void moveHelper4_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                        Party.Singleton.CurrentMap.Name,
                        Party.cara,
                        Party.Singleton.Leader.TilePosition,
                        ModAction.Remove,
                        true);

            Party.Singleton.ModifyNPC(
                        Party.Singleton.CurrentMap.Name,
                        Party.will,
                        Party.Singleton.Leader.TilePosition,
                        ModAction.Remove,
                        true);

            state = SceneState.Complete;
        }

        #endregion
    }
}
