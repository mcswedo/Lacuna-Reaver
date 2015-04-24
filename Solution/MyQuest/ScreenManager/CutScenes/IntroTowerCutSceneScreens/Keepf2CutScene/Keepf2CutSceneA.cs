using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf2CutSceneA : Scene
    {
        Dialog dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z560);

        int onCompleteEventCount = 0;

        public Keepf2CutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));

            InputState.SetVibration(1f,1f);
            Camera.Singleton.Shake(TimeSpan.FromSeconds(1), 5);
            SoundSystem.Play(AudioCues.MonsterRoar);

            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
             "ScriptedMonster1",
             new Point(1, 9),
             false,
             new Point(3, 9),
             2.8f);

            SceneHelper moveHelper2 = new MoveNpcCharacterHelper(
             "ScriptedMonster2",
             new Point(9, 9),
             false,
             new Point(8, 9),
             2.8f);

            helpers.Add(moveHelper1);
            helpers.Add(moveHelper2);

            moveHelper1.OnCompleteEvent += new EventHandler(onCompleteEvent);
            moveHelper2.OnCompleteEvent += new EventHandler(onCompleteEvent);
        }

        void onCompleteEvent(object sender, EventArgs e)
        {
            ++onCompleteEventCount;

            if (onCompleteEventCount == 2)
            {
                state = SceneState.Complete;
                InputState.SetVibration(0f, 0f);
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}
