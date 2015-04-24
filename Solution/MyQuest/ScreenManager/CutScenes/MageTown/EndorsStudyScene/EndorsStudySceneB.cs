using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class EndorsStudySceneB : Scene
    {

        Portal portal;
        public static Point savePoint;
        public static Direction saveDirection;
        NPCMapCharacter will;
        NPCMapCharacter cara;
        NPCMapCharacter endor;

        public EndorsStudySceneB(Screen screen)
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
            portal = new Portal { DestinationMap = "mage_town_endors_study", DestinationPosition = new Point(6, 8), Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.PortalToMap(portal);
            endor = Party.Singleton.CurrentMap.GetNPC("Endor");
         
            Party.Singleton.ModifyNPC(
                       "mage_town_endors_study",
                       Party.cara,
                       new Point(5, 8),
                       ModAction.Add,
                       false);
            Party.Singleton.ModifyNPC(
                       "mage_town_endors_study",
                       Party.will,
                       new Point(7, 8),
                       ModAction.Add,
                       false);
            will.FaceDirection(Direction.East);
            cara.FaceDirection(Direction.South);
            endor.FaceDirection(Direction.South);
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
