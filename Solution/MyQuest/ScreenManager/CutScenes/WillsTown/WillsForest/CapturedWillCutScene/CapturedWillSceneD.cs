using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CapturedWillSceneD : Scene
    {


        Portal portal;
        public static Point savePoint;
        public static Direction saveDirection;
        NPCMapCharacter will;
        NPCMapCharacter cara;
        NPCMapCharacter richMan;

        public override void Complete()
        {
            cleanForests();
        }

        public CapturedWillSceneD(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            Party.Singleton.Leader.FaceDirection(Direction.South);
            portal = new Portal { DestinationMap = "blind_mans_town_mansion", DestinationPosition = new Point(7, 5), Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Complete(); 
            Party.Singleton.PortalToMap(portal);
            richMan = Party.Singleton.CurrentMap.GetNPC("MansionOwner1");
            Party.Singleton.ModifyNPC(
                       "blind_mans_town_mansion",
                       Party.cara,
                       new Point(6, 5),
                       ModAction.Add,
                       false);
            Party.Singleton.ModifyNPC(
                       "blind_mans_town_mansion",
                       Party.will,
                       new Point(8, 5),
                       ModAction.Add,
                       false);
            will.FaceDirection(Direction.East);
            cara.FaceDirection(Direction.South);
            richMan.FaceDirection(Direction.North); 
            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(1, Color.Black, spriteBatch); 
        }

        private void cleanForests()
        {
            for (int i = 1; i <= 6; i++)
            {
                Party.Singleton.ModifyNPC(
                  "blind_mans_forest_1",
                   "StolenItem"+i.ToString(),
                  Point.Zero,
                  ModAction.Remove,
                  true);
            }
           
            for (int i = 1; i <= 6; i++)
            {
                Party.Singleton.ModifyNPC(
                  "blind_mans_forest_3",
                   "StolenItem" + i.ToString(),
                  Point.Zero,
                  ModAction.Remove,
                  true);
            }

            for (int i = 1; i <= 6; i++)
            {
                Party.Singleton.ModifyNPC(
                  "blind_mans_forest_4",
                   "StolenItem" + i.ToString(),
                  Point.Zero,
                  ModAction.Remove,
                  true);
            }

        }
    }
}
 