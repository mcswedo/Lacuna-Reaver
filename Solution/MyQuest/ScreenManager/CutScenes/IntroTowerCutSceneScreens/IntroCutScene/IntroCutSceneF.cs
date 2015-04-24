using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class IntroCutSceneF : Scene
    {
        static readonly Dialog dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z558);

        public IntroCutSceneF(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "ScriptedMonster1",
                   new Point(6, 6),
                   ModAction.Add,
                   false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "ScriptedMonster2",
                new Point(7, 6),
                ModAction.Add,
                false);

            NPCMapCharacter ScriptedMonster1 = Party.Singleton.CurrentMap.GetNPC("ScriptedMonster1");
            NPCMapCharacter ScriptedMonster2 = Party.Singleton.CurrentMap.GetNPC("ScriptedMonster2");

            ScriptedMonster1.IdleOnly = true;
            ScriptedMonster2.IdleOnly = true;
            ScriptedMonster1.FaceDirection(Direction.South);
            ScriptedMonster2.FaceDirection(Direction.South);

            MoveCameraHelper helper = new MoveCameraHelper(new Point(6, 6), 7f);
            helper.OnCompleteEvent += new EventHandler(helper_OnCompleteEvent);

            helpers.Add(helper);
        }

        void helper_OnCompleteEvent(object sender, EventArgs e)
        {
            state = SceneState.Complete;

            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));

            SoundSystem.Play(AudioCues.Earthquake);
            SoundSystem.Play(AudioCues.MonsterRoar);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}

