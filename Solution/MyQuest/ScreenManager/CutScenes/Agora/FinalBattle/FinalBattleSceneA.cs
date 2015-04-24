using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class FinalBattleSceneA : Scene
    {
        public FinalBattleSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Camera.Singleton.CenterOnTarget(
              Party.Singleton.Leader.WorldPosition,
              Party.Singleton.CurrentMap.DimensionsInPixels,
              ScreenManager.Singleton.ScreenResolution);

            MoveCameraHelper cameraHelper = new MoveCameraHelper(new Point(20, 4), 4.0f);

            MovePCMapCharacterHelper nathanHelper1 = new MovePCMapCharacterHelper(new Point(20, 4));


            nathanHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(nathanHelper1);
            helpers.Add(cameraHelper);
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
            Party.Singleton.Leader.FaceDirection(Direction.North);
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
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

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
            Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.East);
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            state = SceneState.Complete;
        }
        #endregion
    }
}
