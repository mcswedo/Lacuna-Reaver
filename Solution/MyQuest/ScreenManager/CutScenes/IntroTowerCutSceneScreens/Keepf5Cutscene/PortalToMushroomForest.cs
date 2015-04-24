using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToMushroomForest : Scene
    {
        Portal portal;

        public PortalToMushroomForest(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            portal = new Portal { DestinationMap = "mushroom_forest", DestinationPosition = new Point(0, 0), Position = Point.Zero };
            Party.Singleton.PortalToMap(portal);
            MoveCameraHelper cameraHelper = new MoveCameraHelper(new Point(26, 26), 9999f);

            helpers.Add(cameraHelper);

            cameraHelper.OnCompleteEvent += EndScene;
        }

        void EndScene(object sender, EventArgs e)
        {
            Party.Singleton.ModifyNPC(
                "healers_village_mayors_house_f1",
                "Mayor",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "healers_village_mayors_house_f2",
                "Cara",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "mushroom_forest",
                "Mayor",
                new Point(25,32),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "mushroom_forest",
                "Cara",
                new Point(27,32),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "mushroom_forest",
                "NathanInjured",
                new Point(26, 26),
                ModAction.Add,
                true);

            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(1, Color.White, spriteBatch); 
        }
    }
}
 