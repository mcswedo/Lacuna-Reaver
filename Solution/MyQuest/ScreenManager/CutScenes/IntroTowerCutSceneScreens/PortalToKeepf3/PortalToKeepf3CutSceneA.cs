using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PortalToKeepf3CutSceneA : Scene
    {
        public PortalToKeepf3CutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Portal portal;

            if (Party.Singleton.CurrentMap.Name == Maps.keepf2 && Party.Singleton.PartyAchievements.Contains(ScriptedMonster3Controller.monsterAchievement))
            {
                portal = new Portal { DestinationMap = "keepf3_after", DestinationPosition = new Point(5, 12), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
                Party.Singleton.Leader.FaceDirection(Direction.North);
            }
            else if (Party.Singleton.CurrentMap.Name == Maps.keepf2 && !Party.Singleton.PartyAchievements.Contains(ScriptedMonster3Controller.monsterAchievement))
            {
                portal = new Portal { DestinationMap = "keepf3", DestinationPosition = new Point(5, 12), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
                Party.Singleton.Leader.FaceDirection(Direction.North);
            }
            else if (Party.Singleton.CurrentMap.Name == Maps.keepf4treasury && Party.Singleton.PartyAchievements.Contains(ScriptedMonster3Controller.monsterAchievement))
            {
                portal = new Portal { DestinationMap = "keepf3_after", DestinationPosition = new Point(5, 1), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
                Party.Singleton.Leader.FaceDirection(Direction.South);
            }
            if (Party.Singleton.CurrentMap.Name == Maps.keepf4treasury && !Party.Singleton.PartyAchievements.Contains(ScriptedMonster3Controller.monsterAchievement))
            {
                portal = new Portal { DestinationMap = "keepf3", DestinationPosition = new Point(5, 1), Position = Point.Zero };
                Party.Singleton.PortalToMap(portal);
                Party.Singleton.Leader.FaceDirection(Direction.South);
            }

            Camera.Singleton.CenterOnTarget(
                 Party.Singleton.Leader.WorldPosition,
                 Party.Singleton.CurrentMap.DimensionsInPixels,
                 ScreenManager.Singleton.ScreenResolution);

            Party.Singleton.ClearAllLogEntries();

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

