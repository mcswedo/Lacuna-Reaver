using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class MovePeeperCutScene : Scene
    {
        NPCMapCharacter peeper;

        public MovePeeperCutScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            peeper = Party.Singleton.CurrentMap.GetNPC("Peeper");

            Party.Singleton.ModifyNPC(
                  Party.Singleton.CurrentMap.Name,
                  "Peeper",
                  Point.Zero,
                  ModAction.Remove,
                  true);

            Party.Singleton.ModifyNPC(
                  Maps.caveLabyrinth,
                  "Peeper",
                  new Point(98, 30),
                  ModAction.Add,
                  true);

        }

        public override void Update(GameTime gameTime)
        {
            peeper.FaceDirection(Direction.South);
            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(1, Color.Black, spriteBatch);
        }
    }
}