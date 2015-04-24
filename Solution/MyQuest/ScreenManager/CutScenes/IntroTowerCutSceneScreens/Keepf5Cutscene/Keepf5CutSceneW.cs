using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf5CutSceneW : Scene
    {
        Dialog dialog = new Dialog(DialogPrompt.NeedsClose, false, Strings.Z573);

        public Keepf5CutSceneW(Screen screen)
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
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Arlan"));

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
