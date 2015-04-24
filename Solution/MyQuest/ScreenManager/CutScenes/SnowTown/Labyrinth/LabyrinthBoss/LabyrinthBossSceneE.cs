using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class LabyrinthBossSceneE : Scene
    {
        #region Achievements

        internal const string achievement = "rescuedMasterBlacksmith";

        #endregion

        public LabyrinthBossSceneE(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);

            Party.Singleton.ModifyNPC(
                       Party.Singleton.CurrentMap.Name,
                       "Serlynx",
                       Party.Singleton.Leader.TilePosition,
                       ModAction.Remove,
                       true);

            Party.Singleton.ModifyNPC(
                     Party.Singleton.CurrentMap.Name,
                     Party.cara,
                     Party.Singleton.Leader.TilePosition,
                     ModAction.Remove,
                     true);

            Party.Singleton.ModifyNPC(
                     Party.Singleton.CurrentMap.Name,
                     Party.will,
                     Party.Singleton.Leader.TilePosition,
                     ModAction.Remove,
                     true);

            Party.Singleton.CurrentMap.Portals.Clear(); 
        }
        public override void Initialize()
        {
            Complete(); 

            ScreenManager.Singleton.AddScreen(new CombatScreen(CombatZonePool.serlynxZone));

            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {      
        }
     
    }
}
