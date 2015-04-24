using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class WillEndorsStudySceneB : Scene
    {
        public WillEndorsStudySceneB(Screen screen)
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
                "NegaWill",
                new Point(6, 10),
                ModAction.Add,
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
