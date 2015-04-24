﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class BackToQueenSceneA : Scene
    {
        #region Dialog

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z856);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z857);

        #endregion

        public BackToQueenSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
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
             new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y - 2),
             1.7f);

            SceneHelper willHelper = new MoveNpcCharacterHelper(
            Party.will,
            new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
            false,
            new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y + 2),
            1.7f);

            caraHelper.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);
         
            helpers.Add(caraHelper);
            helpers.Add(willHelper);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            willDialog.DialogCompleteEvent += caraResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(willDialog, DialogScreen.Location.TopRight, "Will"));
        }

        void caraResponse(object sender, EventArgs e)
        {

            willDialog.DialogCompleteEvent -= caraResponse;
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.South);
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(caraDialog, DialogScreen.Location.TopLeft, "Cara"));

            state = SceneState.Complete;

        }

        #endregion


    }
}