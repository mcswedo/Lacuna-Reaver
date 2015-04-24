using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PoorGuysCutSceneB : Scene
    {
        NPCMapCharacter poorGuy;

        public PoorGuysCutSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            poorGuy = Party.Singleton.CurrentMap.GetNPC("PoorGuy");

            Party.Singleton.ModifyNPC(
                  Party.Singleton.CurrentMap.Name,
                  "PoorGuy",
                  new Point(5, 1),
                  ModAction.Remove,
                  true);

            Party.Singleton.ModifyNPC(
                  Party.Singleton.CurrentMap.Name,
                  "Merchant3",
                  new Point(23, 10),
                  ModAction.Add,
                  true);

            Party.Singleton.ModifyNPC(
                  Maps.snowTownNpchouseSSe,
                  "PoorGuy",
                  new Point(4, 10),
                  ModAction.Add,
                  true);

        }

        public override void Update(GameTime gameTime)
        {
            poorGuy.FaceDirection(Direction.South);
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