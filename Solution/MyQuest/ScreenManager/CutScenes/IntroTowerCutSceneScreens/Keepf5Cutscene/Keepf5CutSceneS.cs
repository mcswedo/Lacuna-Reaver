using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf5CutSceneS : Scene
    {

        public Keepf5CutSceneS(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MovePCMapCharacterHelper helper = new MovePCMapCharacterHelper(
                new Point(3, 3),
                3.5f);

            helper.OnCompleteEvent += new EventHandler(helper_OnCompleteEvent);

            helpers.Add(helper);
        }

        void helper_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Arlan",
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
