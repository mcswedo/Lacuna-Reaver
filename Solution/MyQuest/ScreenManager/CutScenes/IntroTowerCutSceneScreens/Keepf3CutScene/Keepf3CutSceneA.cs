using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Keepf3CutSceneA : Scene
    {
        NPCMapCharacter trappedGirl;

        public Keepf3CutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void Complete()
        {
            trappedGirl = Party.Singleton.CurrentMap.GetNPC("TrappedGirl");

            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "SavedGirl",
                   new Point(5, 1),
                   ModAction.Remove,
                   true);

            Party.Singleton.ModifyNPC(
                Maps.snowTownNpchouseES2,
                "SavedGirl",
                new Point(7, 11),
                ModAction.Add,
                true);

        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Complete();

            Portal portal;
            portal = new Portal { DestinationMap = "keepf3_after", DestinationPosition = new Point(9, 13), Position = Point.Zero };
            Party.Singleton.PortalToMap(portal);
            Party.Singleton.Leader.FaceDirection(Direction.West);

            state = SceneState.Complete;
        }

        public override void Update(GameTime gameTime)
        {
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

