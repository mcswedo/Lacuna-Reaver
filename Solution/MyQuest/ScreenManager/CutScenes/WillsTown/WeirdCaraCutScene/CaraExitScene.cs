using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CaraExitScene : Scene
    {

        public CaraExitScene(Screen screen)
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
                   Party.cara,
                   Party.Singleton.Leader.TilePosition,
                   ModAction.Add,
                   false);

            MoveNpcCharacterHelper caraHelper = new MoveNpcCharacterHelper(
                "Cara",
                new Point(Party.Singleton.Leader.TilePosition.X +1, Party.Singleton.Leader.TilePosition.Y),
                1.25f);

            helpers.Add(caraHelper);

            caraHelper.OnCompleteEvent += TurnCara;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void TurnCara(object sender, EventArgs e)
        {
            NPCMapCharacter cara = Party.Singleton.CurrentMap.GetNPC("Cara");

            cara.FaceDirection(Direction.West);

            state = SceneState.Complete;
        }
    }
}