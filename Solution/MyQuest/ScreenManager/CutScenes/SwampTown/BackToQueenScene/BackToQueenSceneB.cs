using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class BackToQueenSceneB : Scene
    {

        Portal portal;
        public static Point savePoint;
        public static Direction saveDirection;
        NPCMapCharacter will;
        NPCMapCharacter cara;

        public BackToQueenSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Complete()
        {
            Party.Singleton.ModifyNPC("swamp_village_neva_lees_wigwam", "SwampQueenSick", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC("swamp_village_npchut_e1", "SarahSick", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC("swamp_village_npchut_e2", "JessicaSick", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC("swamp_village_npchut_e1", "SwampGirlSick", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC("swamp_village_npchut_e2", "SwampBoySick", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC("swamp_village_npchut_w", "ParkerSick", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC("swamp_village_npcwigwam_w", "VillageIdiotSick", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC("swamp_village_npcwigwam_e", "SwampOldManSick", Point.Zero, ModAction.Remove, true);
            Party.Singleton.ModifyNPC("swamp_village_npcwigwam_e", "SwampOldWomanSick", Point.Zero, ModAction.Remove, true);

            Party.Singleton.ModifyNPC("swamp_village_neva_lees_wigwam", "SwampQueen", new Point(7, 3), Direction.South, false, ModAction.Add, true);
            Party.Singleton.ModifyNPC("swamp_village_npchut_e1", "Sarah", new Point(6, 5), Direction.South, true, ModAction.Add, true);
            Party.Singleton.ModifyNPC("swamp_village_npchut_e2", "Jessica", new Point(6, 5), Direction.South, true, ModAction.Add, true);
            Party.Singleton.ModifyNPC("swamp_village", "SwampGirl", new Point(38, 45), Direction.East, false, ModAction.Add, true);
            Party.Singleton.ModifyNPC("swamp_village", "SwampBoy", new Point(50, 45), Direction.West, false, ModAction.Add, true);
            Party.Singleton.ModifyNPC("swamp_village", "VillageIdiot", new Point(21, 67), Direction.South, false, ModAction.Add, true);
            Party.Singleton.ModifyNPC("swamp_village", "Parker", new Point(30, 51), Direction.South, false, ModAction.Add, true);
            Party.Singleton.ModifyNPC("swamp_village_npcwigwam_e", "SwampOldMan", new Point(4, 6), Direction.South, true, ModAction.Add, true);
            Party.Singleton.ModifyNPC("swamp_village_npcwigwam_e", "SwampOldWoman", new Point(8, 6), Direction.South, true, ModAction.Add, true);

        }

        public override void Initialize()
        {
            will = Party.Singleton.CurrentMap.GetNPC(Party.will);
            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            Party.Singleton.Leader.FaceDirection(Direction.South);
            portal = new Portal { DestinationMap = "swamp_village_neva_lees_wigwam", DestinationPosition = new Point(7, 4), Position = Point.Zero };
        }

        public override void Update(GameTime gameTime)
        {
            Complete();

            Party.Singleton.PortalToMap(portal);
            Party.Singleton.ModifyNPC(
                       "swamp_village_neva_lees_wigwam",
                       Party.cara,
                       new Point(6, 4),
                       ModAction.Add,
                       false);
            Party.Singleton.ModifyNPC(
                       "swamp_village_neva_lees_wigwam",
                       Party.will,
                       new Point(8, 4),
                       ModAction.Add,
                       false);
            will.FaceDirection(Direction.East);
            cara.FaceDirection(Direction.South);

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
