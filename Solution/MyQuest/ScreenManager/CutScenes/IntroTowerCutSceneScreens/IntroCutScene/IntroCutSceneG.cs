using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class IntroCutSceneG : Scene
    {
        static readonly Dialog dialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z558);

        public IntroCutSceneG(Screen screen)
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
            //MoveNpcCharacterHelper helper = new MoveNpcCharacterHelper(
            //    "ScriptedMonster1",
            //    new Point(6, 5),
            //    false,
            //    new Point(6, 6),
            //    1.7f);

            //MoveNpcCharacterHelper helper1 = new MoveNpcCharacterHelper(
            //    "ScriptedMonster2",
            //    new Point(7, 5),
            //    false,
            //    new Point(7, 6),
            //    1.7f);

            //helper.SetIdleOnlyOnComplete();
            //helper1.SetIdleOnlyOnComplete();

            //helper.OnCompleteEvent += new EventHandler(helper_OnCompleteEvent);

            //helpers.Add(helper);
            //helpers.Add(helper1);
        }

        void helper_OnCompleteEvent(object sender, EventArgs e)
        {
            state = SceneState.Complete;
            InputState.SetVibration(0f, 0f);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, NPCPool.stub));
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
