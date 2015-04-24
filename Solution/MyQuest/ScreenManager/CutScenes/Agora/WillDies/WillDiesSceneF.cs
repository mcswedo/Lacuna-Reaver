using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class WillDiesSceneF : Scene
    {
        #region Dialog
        Dialog dialog; 

        Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, false, Strings.ZA036, Strings.ZA037);

        Dialog malDialog = new Dialog(DialogPrompt.NeedsClose, false, Strings.Z842);

        Dialog nathanDialog2 = new Dialog(DialogPrompt.NeedsClose, false, Strings.ZA038);

        Dialog skillDialog = new Dialog(DialogPrompt.NeedsClose, false, Strings.ZA039, Strings.ZA040, Strings.ZA041);

        #endregion 

        CombatScreen combatScreen;

        public WillDiesSceneF(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.Leader.FaceDirection(Direction.East);

            dialog = nathanDialog;
            dialog.DialogCompleteEvent += malResponse;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
            
            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }


        #region Callbacks

        void malResponse(object sender, EventArgs e)
        {
            SoundSystem.Play(AudioCues.MalticarLaugh);
            dialog.DialogCompleteEvent -= malResponse;

            dialog = malDialog;
            dialog.DialogCompleteEvent += nathansResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Malticar"));
        }

        void nathansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse;

            Party.Singleton.Leader.FaceDirection(Direction.North);

            dialog = nathanDialog2;

            dialog.DialogCompleteEvent += skillResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Nathan"));

            state = SceneState.Complete;
        }

        void skillResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= skillResponse;

            dialog = skillDialog;

            dialog.DialogCompleteEvent += CutToCombat;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight));

            state = SceneState.Complete;
        }

        void CutToCombat(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CutToCombat;

            Party.Singleton.RemoveFightingCharacter("Will");
            Party.Singleton.RemoveFightingCharacter("Cara");

            TurnExecutor.Singleton.FinalBattle = true;

            Nathan.Instance.SkillNames.Clear();
            Nathan.Instance.AddSkillName("BattleRift");

            combatScreen = new CombatScreen(CombatZonePool.malticarZone2);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Malticar",
                Point.Zero,
                ModAction.Remove,
                false);

            combatScreen.ExitScreenEvent += EndScene;
            Nathan.Instance.FighterStats.Health = Nathan.Instance.FighterStats.ModifiedMaxHealth;
            Nathan.Instance.FighterStats.Energy = Nathan.Instance.FighterStats.ModifiedMaxEnergy;
            ScreenManager.Singleton.AddScreen(combatScreen);
        }

        void EndScene(object sender, EventArgs e)
        {
            combatScreen.ExitScreenEvent -= EndScene;

            state = SceneState.Complete;
        }


        #endregion
    }
}
