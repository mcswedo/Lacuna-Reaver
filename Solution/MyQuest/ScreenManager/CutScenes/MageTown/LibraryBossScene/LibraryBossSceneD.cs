using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class LibraryBossSceneD : Scene
    {

        #region Achievements

        internal const string achievement = "defeatLibraryBoss";

        #endregion

        public LibraryBossSceneD(Screen screen)
            : base(screen)
        {
        }

        public override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);

            Party.Singleton.ModifyNPC(
                        "possessed_library_boss",
                        "ElderMantis",
                        Party.Singleton.Leader.TilePosition,
                        ModAction.Remove,
                        true);

            Party.Singleton.ModifyNPC(
                      "possessed_library_boss",
                      "Apprentice1",
                      Party.Singleton.Leader.TilePosition,
                      ModAction.Remove,
                      true);

            Party.Singleton.ModifyNPC(
                   "possessed_library_boss",
                   "Apprentice2",
                   Party.Singleton.Leader.TilePosition,
                   ModAction.Remove,
                   true);

            Party.Singleton.ModifyNPC(
                     "possessed_library_boss",
                     Party.cara,
                     Party.Singleton.Leader.TilePosition,
                     ModAction.Remove,
                     true);

            Party.Singleton.ModifyNPC(
                    "possessed_library_boss",
                     Party.will,
                     Party.Singleton.Leader.TilePosition,
                     ModAction.Remove,
                     true);
        }
        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {

            Complete(); 

            ScreenManager.Singleton.AddScreen(new CombatScreen(CombatZonePool.elderMantisZone));

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
        }

    }
}
