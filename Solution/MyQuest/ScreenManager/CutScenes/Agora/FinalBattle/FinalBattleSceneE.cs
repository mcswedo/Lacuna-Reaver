using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class FinalBattleSceneE : Scene
    {
        #region Dialog

        static readonly Dialog malDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA019);

        static readonly Dialog  caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA020);

        static readonly Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA021);

        static readonly Dialog arlanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA022, Strings.ZA023);

        static readonly Dialog malDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA024, Strings.ZA025);

        static readonly Dialog arlanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA026, Strings.ZA027);

        static readonly Dialog willDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA028);

        static readonly Dialog caraDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA029);

        static readonly Dialog nathanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA030, Strings.ZA031);

        static readonly Dialog willDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA032);

        static readonly Dialog malDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA033);


        #endregion

        Dialog dialog;

        CombatScreen combatScreen;

        public FinalBattleSceneE(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = malDialog;
            dialog.DialogCompleteEvent += CarasResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Malticar"));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void CarasResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CarasResponse;

            dialog = caraDialog;
            dialog.DialogCompleteEvent += NathansResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara")); 
        }

        void NathansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= NathansResponse;

            dialog = nathanDialog;

            dialog.DialogCompleteEvent += ArlansResponse;      

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Nathan"));

            state = SceneState.Complete;
        }

        void ArlansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ArlansResponse;

            dialog = arlanDialog;

            dialog.DialogCompleteEvent += MalResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "InjuredArlan"));

            state = SceneState.Complete;
        }

        void MalResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= MalResponse;

            //InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);

            SoundSystem.Play(AudioCues.MonsterRoar);

            dialog = malDialog2;

            dialog.DialogCompleteEvent += ArlansResponse2;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Malticar"));
        }

        void ArlansResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ArlansResponse2;

            dialog = arlanDialog2;

            dialog.DialogCompleteEvent += WillsResponse;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "InjuredArlan"));

            state = SceneState.Complete;
        }

        void WillsResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= WillsResponse;

            dialog = willDialog;

            dialog.DialogCompleteEvent += CarasResponse2;          

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Will"));

            state = SceneState.Complete;
        }

        void CarasResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CarasResponse2;

            dialog = caraDialog2;
            dialog.DialogCompleteEvent += NathansResponse2;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void NathansResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= NathansResponse2;

            dialog = nathanDialog2;

            dialog.DialogCompleteEvent += WillsResponse2;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Nathan"));

            state = SceneState.Complete;
        }

        void WillsResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= WillsResponse2;

            dialog = willDialog2;

            dialog.DialogCompleteEvent += MalResponse2;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));

            state = SceneState.Complete;
        }

        void MalResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= MalResponse2;

            dialog = malDialog3;

            dialog.DialogCompleteEvent += CutToCombat;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Malticar"));
        }

        void CutToCombat(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= CutToCombat;

            foreach (PCFightingCharacter character in Party.Singleton.GameState.Fighters)
            {
                character.FighterStats.Health = character.FighterStats.ModifiedMaxHealth;
                character.FighterStats.Energy = character.FighterStats.ModifiedMaxEnergy;
            }

            combatScreen = new CombatScreen(CombatZonePool.malticarZone);

            combatScreen.ExitScreenEvent += EndScene;

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
