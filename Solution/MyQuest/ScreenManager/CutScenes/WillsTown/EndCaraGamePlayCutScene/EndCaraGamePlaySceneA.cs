using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class EndCaraGamePlaySceneA : Scene
    {
        Dialog dialog;

        Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z902, Strings.Z903);

        public EndCaraGamePlaySceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            //Put fighting characters back in the correct order.
            Party.Singleton.RemoveFightingCharacter(Cara.Instance);

            Party.Singleton.AddFightingCharacter(Nathan.Instance);
            Party.Singleton.AddFightingCharacter(Cara.Instance);

            if (Party.Singleton.PartyAchievements.Contains(CapturedWillCutSceneScreen.achievement))
            {
                Party.Singleton.AddFightingCharacter(Will.Instance);
            }
            Cara.Instance.FighterStats.Health = Cara.Instance.FighterStats.ModifiedMaxHealth;
            Cara.Instance.FighterStats.Energy = Cara.Instance.FighterStats.ModifiedMaxEnergy;

            dialog = caraDialog;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        public override void Update(GameTime gameTime)
        {
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
