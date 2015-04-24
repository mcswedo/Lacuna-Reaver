using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MyQuest
{
    public class HealersBattleSceneC : Scene
    {
        Dialog dialog;
        NPCMapCharacter bandit1;
        NPCMapCharacter bandit2;
        NPCMapCharacter bob;

        #region Dialog

        static readonly Dialog banditDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z520, Strings.Z521);

        #endregion

        #region Achievement

        internal const string savedVillageAchievement = "savedVillage";

        #endregion

        public HealersBattleSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void Complete()
        {
            bob = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "Bob");
            Party.Singleton.AddAchievement(savedVillageAchievement);
            bob.IdleOnly = false;
        }

        public override void LoadContent(ContentManager content)
        {         
        }

        public override void Initialize()
        {
            if (Party.Singleton.Leader.TilePosition == new Point(28, 6))
            {
                Party.Singleton.ModifyNPC(
                    Maps.healersVillage,
                    "Bandit1",
                    new Point(27, 6),
                    ModAction.Add,
                    false);

                Party.Singleton.ModifyNPC(
                    Maps.healersVillage,
                    "Bandit2",
                    new Point(29, 6),
                    ModAction.Add,
                    false);
            }

            if (Party.Singleton.Leader.TilePosition == new Point(28, 7))
            {
                Party.Singleton.ModifyNPC(
                    Maps.healersVillage,
                    "Bandit1",
                    new Point(27, 7),
                    ModAction.Add,
                    false);

                Party.Singleton.ModifyNPC(
                    Maps.healersVillage,
                    "Bandit2",
                    new Point(29, 7),
                    ModAction.Add,
                    false);
            }

            bandit1 = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "Bandit1");
            bandit2 = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "Bandit2");
            bandit1.FaceDirection(Direction.East);
            bandit2.FaceDirection(Direction.West);


            Complete();

            dialog = banditDialog1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Bandit"));

            dialog.DialogCompleteEvent += BanditBattle;

        }

        void BanditBattle(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= BanditBattle;
            CombatZone zone = CombatZonePool.forestBanditZone;
            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));

            Party.Singleton.ModifyNPC(
                "healers_village",
                "Bob",
                new Point(24,9),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Maps.healersVillage,
                "Bandit1",
                new Point(27, 7),
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.healersVillage,
                "Bandit2",
                new Point(29, 7),
                ModAction.Remove,
                true);

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
