using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class FinalBattleSceneH : Scene
    {
        #region Dialog

        static readonly Dialog newSkillDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA040, Strings.ZA041);

        static readonly Dialog nathanDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA283);

        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA284);

        static readonly Dialog nathanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA285);

        #endregion

        Dialog dialog;

        CombatScreen combatScreen;

        public FinalBattleSceneH(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = newSkillDialog;
            dialog.DialogCompleteEvent += NathanResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void NathanResponse1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanResponse1;

            dialog = nathanDialog1;
            dialog.DialogCompleteEvent += CaraResponse1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
        }

        void CaraResponse1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= CaraResponse1;

            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);

            dialog = caraDialog1;
            dialog.DialogCompleteEvent += NathanResponse2;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Cara"));
        }

        void NathanResponse2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanResponse2;
            Party.Singleton.Leader.FaceDirection(Direction.East);

            dialog = nathanDialog2;
            dialog.DialogCompleteEvent += StartCombat;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
        }

        void StartCombat(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= StartCombat;

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

            MusicSystem.Pause();
            Nathan.Instance.FighterStats.Health = Nathan.Instance.FighterStats.ModifiedMaxHealth;
            Nathan.Instance.FighterStats.Energy = Nathan.Instance.FighterStats.ModifiedMaxEnergy;
            ScreenManager.Singleton.AddScreen(combatScreen);
        }

        void EndScene(object sender, EventArgs e)
        {
            MusicSystem.Play(AudioCues.willTheme); // After the batlle is complete.
            combatScreen.ExitScreenEvent -= EndScene;

            state = SceneState.Complete;
        }
    }
}
