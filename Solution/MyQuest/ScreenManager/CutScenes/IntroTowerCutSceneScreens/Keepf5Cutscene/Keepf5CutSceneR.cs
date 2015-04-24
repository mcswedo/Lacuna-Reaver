using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf5CutSceneR : Scene
    {
        public Keepf5CutSceneR(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Arlan",
                new Point(2, 5),
                ModAction.Add,
                false);

            Party.Singleton.CurrentMap.GetNPC("Arlan").FaceDirection(Direction.East);
        }

        public override void Update(GameTime gameTime)
        {
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
