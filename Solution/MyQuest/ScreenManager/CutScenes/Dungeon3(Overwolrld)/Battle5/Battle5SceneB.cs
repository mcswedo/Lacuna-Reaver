using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Battle5SceneB : Scene
    {
        Dialog dialog;

        Dialog bandit1Dialog1 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA590); //Well we're in a bit of a predicament here I think.

        Dialog bandit2Dialog1 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA591); //What's the problem Sam?

        Dialog bandit1Dialog2 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA592); //Where do I begin. Most of the guild has been killed by these punks, our strongest team was killed, and now most of the ghosts are running rampant due to sudden loss of guild members.

        Dialog bandit2Dialog2 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA593); //Our guild was taken down all because we had to protect this chest? It's not even worth dying for!

        Dialog bandit1Dialog3 =
            new Dialog(DialogPrompt.NeedsClose, Strings.ZA594); //Well we have no choice now. We're trapped now. Might as well die a hero protecting the most ancient treasure of all treasures.

        NPCMapCharacter bandit1;
        NPCMapCharacter bandit2;

        public Battle5SceneB(Screen screen)
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
                new Point(0, 4),
                ModAction.Add,
                false);

            Party.Singleton.ModifyNPC(
                Maps.dungeon3,
                "Bandit2",
                new Point(2, 4),
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
            CombatZone zone = CombatZonePool.dungeon3CaveCarasBanditGhostZone;  

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
