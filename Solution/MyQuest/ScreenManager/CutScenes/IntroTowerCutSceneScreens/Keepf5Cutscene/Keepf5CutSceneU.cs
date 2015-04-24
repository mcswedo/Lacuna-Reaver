using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf5CutSceneU : Scene
    {

        public Keepf5CutSceneU(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MovePCMapCharacterHelper helper = new MovePCMapCharacterHelper(
                new Point(5, 3),
                3.5f);

            helper.OnCompleteEvent += new EventHandler(helper_OnCompleteEvent);

            helpers.Add(helper);
        }

        void helper_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "ArlanS",
                Point.Zero,
                ModAction.Remove,
                false);

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
