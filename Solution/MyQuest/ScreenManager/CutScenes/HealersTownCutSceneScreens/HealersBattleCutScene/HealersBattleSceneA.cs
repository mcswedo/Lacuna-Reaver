using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class HealersBattleSceneA : Scene
    {
        Dialog dialog;

        #region Dialog

        static readonly Dialog banditDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z518);

        static readonly Dialog banditDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z519);

        #endregion

        #region Achievement

        internal const string savedVillageAchievement = "savedVillage";

        #endregion

        public HealersBattleSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {         
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;
            dialog = banditDialog1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Bandit"));

            dialog.DialogCompleteEvent += BanditDialog2;
        }

        void BanditDialog2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= BanditDialog2;
            dialog = banditDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Bandit"));

            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
