using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class IntroCutSceneH : Scene
    {
        public IntroCutSceneH(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MoveCameraHelper helper = new MoveCameraHelper(Party.Singleton.Leader.TilePosition, 9f);
            helper.OnCompleteEvent += new EventHandler(helper_OnCompleteEvent);

            helpers.Add(helper);
        }

        void helper_OnCompleteEvent(object sender, EventArgs e)
        {
            InputState.SetVibration(0f, 0f);
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

