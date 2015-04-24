using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class ChepetawaSceneD : Scene
    {
        #region Achievements

        internal const string achievement = "defeatChepetawaBoss";

        #endregion

        public ChepetawaSceneD(Screen screen)
            : base(screen)
        {
        }

        public override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);

            Party.Singleton.ModifyNPC(
                       Party.Singleton.CurrentMap.Name,
                       "Chepetawa",
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
        }
        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Complete();

            ScreenManager.Singleton.AddScreen(new CombatScreen(CombatZonePool.chepetawaZone));

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
