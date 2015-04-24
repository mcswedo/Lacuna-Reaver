using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToCarasHouse : Scene
    {
        Portal portal;

        public PortalToCarasHouse(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            portal = new Portal { DestinationMap = "healers_village_mayors_house_f2", DestinationPosition = new Point(3, 5), Position = Point.Zero };

            Party.Singleton.ModifyNPC(
                "mushroom_forest",
                "Mayor",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "mushroom_forest",
                "NathanInjured",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "mushroom_forest",
                "Cara",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "healers_village_mayors_house_f1",
                "Mayor",
                new Point(8,3),
                Direction.South,
                false,
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "healers_village_mayors_house_f2",
                "Cara",
                new Point(4,5),
                ModAction.Add,
                true);
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.PortalToMap(portal);

            Camera.Singleton.CenterOnTarget(
                 Party.Singleton.Leader.WorldPosition,
                 Party.Singleton.CurrentMap.DimensionsInPixels,
                 ScreenManager.Singleton.ScreenResolution);

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
 