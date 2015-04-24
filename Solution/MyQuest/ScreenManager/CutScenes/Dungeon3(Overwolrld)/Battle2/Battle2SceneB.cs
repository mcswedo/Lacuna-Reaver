using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Battle2SceneB : Scene
    {
        Dialog dialog;
        Dialog bandit1Dialog1 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA579);

        Dialog bandit2Dialog1 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA580);

        Dialog bandit1Dialog2 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA581);

        Dialog bandit2Dialog2 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA582);

        Dialog bandit1Dialog3 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA583);

        NPCMapCharacter bandit1;
        NPCMapCharacter bandit2;

        public Battle2SceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.ModifyNPC(
                Maps.dungeon3,
                "Bandit1",
                new Point(0, 13),
                ModAction.Add,
                false);

            Party.Singleton.ModifyNPC(
                Maps.dungeon3,
                "Bandit2",
                new Point(2, 13),
                ModAction.Add,
                false);

            bandit1 = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "Bandit1");
            bandit2 = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "Bandit2");
            bandit1.FaceDirection(Direction.East);
            bandit2.FaceDirection(Direction.West);

            dialog = bandit1Dialog1;
            dialog.DialogCompleteEvent += Bandit2Dialog1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Bandit"));
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void Bandit2Dialog1(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Bandit2Dialog1;
            dialog = bandit2Dialog1;
            dialog.DialogCompleteEvent += Bandit1Dialog2;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Bandit"));
        }

        void Bandit1Dialog2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Bandit1Dialog2;
            dialog = bandit1Dialog2;
            dialog.DialogCompleteEvent += Bandit2Dialog2;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Bandit"));
        }

        void Bandit2Dialog2(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Bandit2Dialog2;
            dialog = bandit2Dialog2;
            dialog.DialogCompleteEvent += Bandit1Dialog3;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopRight, "Bandit"));
        }

        void Bandit1Dialog3(object sender, PartyResponseEventArgs e)
        {
            dialog.DialogCompleteEvent -= Bandit1Dialog3;
            dialog = bandit1Dialog3;
            dialog.DialogCompleteEvent += TriggerScriptedMonsterBattle;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Bandit"));
        }

        void TriggerScriptedMonsterBattle(object sender, PartyResponseEventArgs e)
        {
            CombatZone zone = CombatZonePool.dungeon3CaveHauntedTreeBanditZone;  

            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));

            Party.Singleton.ModifyNPC(
                Maps.dungeon3,
                "Bandit1",
                new Point(27, 7),
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Maps.dungeon3,
                "Bandit2",
                new Point(29, 7),
                ModAction.Remove,
                true);

            state = SceneState.Complete;
        }
    }
}
