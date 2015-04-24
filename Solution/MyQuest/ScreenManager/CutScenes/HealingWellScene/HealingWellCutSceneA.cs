using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class HealingWellCutSceneA : Scene
    {
        Dialog dialog; 

        static readonly Dialog healedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA530, Strings.ZA531);

        static readonly Dialog likeToSaveDialog = new Dialog(DialogPrompt.YesNo, Strings.Z002);

        static readonly Dialog yesToSaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.ZA529);

        public HealingWellCutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog = healedDialog;
            dialog.DialogCompleteEvent += AfterHealedCallback;
            foreach (PCFightingCharacter character in Party.Singleton.GameState.Fighters)
            {
                character.FighterStats.Health = character.FighterStats.ModifiedMaxHealth;
                character.FighterStats.Energy = character.FighterStats.ModifiedMaxEnergy;
            }
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

        void AfterHealedCallback(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= AfterHealedCallback;
            dialog = likeToSaveDialog;
            dialog.DialogCompleteEvent += AfterSaveCallback;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
        }

        void AfterSaveCallback(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= AfterSaveCallback;
            if (e.Response == PartyResponse.Yes)
            {
                dialog = yesToSaveDialog;
                Party.Singleton.SaveGameState(Party.saveFileName);
                ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
            }
            else
            {
            }

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
