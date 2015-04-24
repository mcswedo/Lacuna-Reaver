using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Keepf1CutSceneA : Scene
    {
        Dialog dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z559);
        //Make a dialog with sid or max that explains defending and how it restores more energy.

        public Keepf1CutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));

            MoveNpcCharacterHelper moveHelper = new MoveNpcCharacterHelper(
                   "ScriptedMonster2",
                   new Point(5, 1),
                   false,
                   new Point(5, 3),
                   1.7f);

            moveHelper.OnCompleteEvent += new EventHandler(moveHelper_OnCompleteEvent);
            helpers.Add(moveHelper);
            
            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        void moveHelper_OnCompleteEvent(object sender, EventArgs e)
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);
            InputState.SetVibration(0f, 0f);
            state = SceneState.Complete;

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "ScriptedMonster2",
                Point.Zero,
                ModAction.Remove,
                true);

            CombatZone zone = CombatZonePool.keepZoneSingleDemon1;
            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));
        }
    }
}
