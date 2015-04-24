using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CaraRejoinScene : Scene
    {        
        public CaraRejoinScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MoveNpcCharacterHelper caraHelper = new MoveNpcCharacterHelper(
                "Cara",
                new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                1.25f);

            helpers.Add(caraHelper);

            caraHelper.OnCompleteEvent += RemoveCara;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void RemoveCara(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                       Party.Singleton.CurrentMap.Name,
                       Party.cara,
                       Party.Singleton.Leader.TilePosition,
                       ModAction.Remove,
                       true);

            state = SceneState.Complete;
        }
    }
}