using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CapturedWillSceneJ : Scene
    {
        #region Dialog

        Dialog willJoinsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z959);

        #endregion

        public override void Complete()
        {
            Party.Singleton.ModifyNPC(
                         "healers_village",
                         "InjuredGateGuard",
                         Party.Singleton.Leader.TilePosition,
                         ModAction.Remove,
                         true);

            Party.Singleton.ModifyNPC(
                         "healers_village",
                         "GateGuard1",
                         new Point(22, 17),
                         ModAction.Add,
                         true);

            Party.Singleton.ModifyNPC(
                        "healers_village",
                        "DeadGateGuard",
                        Party.Singleton.Leader.TilePosition,
                        ModAction.Remove,
                        true);

            Party.Singleton.ModifyNPC(
                        "healers_village_npchouse_se",
                        "SickGrandson",
                        Party.Singleton.Leader.TilePosition,
                        ModAction.Remove,
                        true);

            Party.Singleton.ModifyNPC(
                        "mushroom_forest",
                        "Grandson",
                        new Point(27, 45),
                        ModAction.Add,
                        true);

            Party.Singleton.ModifyNPC(
                  "mushroom_forest",
                  "ScarfBandit",
                  new Point(34, 3),
                  ModAction.Add,
                  true);

            Party.Singleton.ModifyNPC(
                  "mushroom_forest",
                  "BanditKing",
                  new Point(44, 30),
                  Direction.West,
                  true,
                  ModAction.Add,
                  true);

            Party.Singleton.ModifyNPC(
                  "mushroom_forest",
                  "Bandit1",
                  new Point(43, 29),
                  Direction.West,
                  true,
                  ModAction.Add,
                  true);

            Party.Singleton.ModifyNPC(
                  "mushroom_forest",
                  "Bandit2",
                  new Point(43, 30),
                  Direction.West,
                  true,
                  ModAction.Add,
                  true);

            Party.Singleton.ModifyNPC(
                  "mushroom_forest",
                  "Bandit3",
                  new Point(43, 31),
                  Direction.West,
                  true,
                  ModAction.Add,
                  true);

            Party.Singleton.ModifyNPC(
                  "mushroom_forest",
                  "Bandit4",
                  new Point(43, 32),
                  Direction.West,
                  true,
                  ModAction.Add,
                  true);
        }

        public CapturedWillSceneJ(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Complete();
        }

        public override void Update(GameTime gameTime)
        {
            MusicSystem.InterruptMusic(AudioCues.JoinedPartySFX);

            ScreenManager.Singleton.AddScreen(new DialogScreen(willJoinsDialog, DialogScreen.Location.TopLeft));

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
