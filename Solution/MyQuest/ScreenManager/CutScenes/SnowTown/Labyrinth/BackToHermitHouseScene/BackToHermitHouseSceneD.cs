using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class BackToHermitHouseSceneD : Scene
    {
        public BackToHermitHouseSceneD(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper caraHelper = new MoveNpcCharacterHelper(
           Party.cara,
           new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
           1.7f);

            SceneHelper willHelper = new MoveNpcCharacterHelper(
            Party.will,
            new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
            1.7f);

            caraHelper.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);

            helpers.Add(caraHelper);
            helpers.Add(willHelper);
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
            Party.Singleton.ModifyNPC(Party.Singleton.CurrentMap.Name,"Cara", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC(Party.Singleton.CurrentMap.Name, "Will", Point.Zero, ModAction.Remove, true);

            state = SceneState.Complete;
        }

        #endregion

    }
}
