using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class SwampQueenSceneC : Scene
    {
        public SwampQueenSceneC(Screen screen)
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

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {      
        }

        void partyJoin()
        {
            SceneHelper moveHelper3 = new MoveNpcCharacterHelper(
             Party.will, new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y));

            SceneHelper moveHelper4 = new MoveNpcCharacterHelper(
             Party.cara, new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y));     

            moveHelper4.OnCompleteEvent += new EventHandler(moveHelper4_OnCompleteEvent);           
           
            helpers.Add(moveHelper3);
            helpers.Add(moveHelper4);
        }

        void moveHelper4_OnCompleteEvent(object sender, EventArgs e)
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

            Party.Singleton.ModifyMapLayer("overworld", Layer.Foreground, new Point(23, 22), 9, true);
            Party.Singleton.ModifyMapLayer("overworld", Layer.Collision, new Point(23, 22), 1, true);
            state = SceneState.Complete;
        }
    }
}
