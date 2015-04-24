using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf5RememberedSceneW : Scene
    {
        Dialog dialog;

        Dialog nathanDialog1 = new Dialog(DialogPrompt.NeedsClose, true, Strings.ZA535);
        Dialog arlanDialog1 = new Dialog(DialogPrompt.NeedsClose, true, Strings.ZA536, Strings.ZA537);
        Dialog nathanDialog2 = new Dialog(DialogPrompt.NeedsClose, true, Strings.ZA538);
        Dialog arlanDialog2 = new Dialog(DialogPrompt.NeedsClose, true, Strings.ZA539);
        Dialog nathanDialog3 = new Dialog(DialogPrompt.NeedsClose, true, Strings.ZA540);
        Dialog arlanDialog3 = new Dialog(DialogPrompt.NeedsClose, true, Strings.ZA541, Strings.Z573);

        public Keepf5RememberedSceneW(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            MovePCMapCharacterHelper helper1 = new MovePCMapCharacterHelper(
                new Point(5, 1),
                3.5f);

            MoveNpcCharacterHelper helper2 = new MoveNpcCharacterHelper(
                "Arlan",
                new Point(5, 3),
                2f);

            helper1.OnCompleteEvent += new EventHandler(nathansHelper_OnCompleteEvent);
            helper2.OnCompleteEvent += new EventHandler(helper_OnCompleteEvent);

            helpers.Add(helper1);
            helpers.Add(helper2);
        }
      
        void helper_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.Leader.FaceDirection(Direction.South);
            NPCMapCharacter arlan = Party.Singleton.CurrentMap.GetNPC("Arlan");
            arlan.FaceDirection(Direction.North);
        }

        void nathansHelper_OnCompleteEvent(object sender, EventArgs e)
        {
            dialog = nathanDialog1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));

            dialog.DialogCompleteEvent += ArlanResponse1;
        }

        void ArlanResponse1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ArlanResponse1;

            dialog = arlanDialog1;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Arlan"));

            dialog.DialogCompleteEvent += NathanResponse1;
        }

        void NathanResponse1(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanResponse1;

            dialog = nathanDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));

            dialog.DialogCompleteEvent += ArlanResponse2;
        }

        void ArlanResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ArlanResponse2;

            dialog = arlanDialog2;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Arlan"));

            dialog.DialogCompleteEvent += NathanResponse2;
        }

        void NathanResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= NathanResponse2;

            dialog = nathanDialog3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));

            dialog.DialogCompleteEvent += ArlanResponse3;
        }

        void ArlanResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ArlanResponse3;

            dialog = arlanDialog3;
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Arlan"));

            dialog.DialogCompleteEvent += SceneEnd;
        }

        void SceneEnd(object sender, EventArgs e)
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
