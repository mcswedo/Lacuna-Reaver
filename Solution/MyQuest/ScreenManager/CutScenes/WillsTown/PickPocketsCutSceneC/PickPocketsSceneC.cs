using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PickPocketsSceneC : Scene
    {
        NPCMapCharacter pickPocket;

        Portal portal;

        public PickPocketsSceneC(Screen screen)
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
                  "blind_mans_forest_2",
                  "PickPocket",
                  new Point(34, 37),
                  ModAction.Add,
                  true);

            Party.Singleton.Leader.FaceDirection(Direction.South);
            portal = new Portal { DestinationMap = "blind_mans_town_inn_f2", DestinationPosition = new Point(8, 5), Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.PortalToMap(portal);
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