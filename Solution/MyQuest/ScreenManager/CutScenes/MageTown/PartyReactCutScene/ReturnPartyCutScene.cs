using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class ReturnPartyCutScene : Scene
    {
        public ReturnPartyCutScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            partyJoin();
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void partyJoin()
        {
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
             Party.will, new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y));

            SceneHelper moveHelper2 = new MoveNpcCharacterHelper(
             Party.cara, new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y));

            moveHelper2.OnCompleteEvent += new EventHandler(moveHelper2_OnCompleteEvent);

            helpers.Add(moveHelper1);
            helpers.Add(moveHelper2);
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
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
    }
}
