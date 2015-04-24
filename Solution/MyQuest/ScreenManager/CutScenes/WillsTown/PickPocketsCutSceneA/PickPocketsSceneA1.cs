using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PickPocketsSceneA1 : Scene
    {
        NPCMapCharacter pickPocket;

        public PickPocketsSceneA1(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            pickPocket = Party.Singleton.CurrentMap.GetNPC("PickPocket");

            Party.Singleton.ModifyNPC(
                  Party.Singleton.CurrentMap.Name,
                  "PickPocket",
                  new Point(18, 17),
                  ModAction.Remove,
                  true);

            Party.Singleton.ModifyNPC(
                  Party.Singleton.CurrentMap.Name,
                  "PickPocket",
                  new Point(25, 27),
                  ModAction.Add,
                  true);
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
            ScreenManager.Singleton.TintBackBuffer(1, Color.Black, spriteBatch);
        }
    }
}