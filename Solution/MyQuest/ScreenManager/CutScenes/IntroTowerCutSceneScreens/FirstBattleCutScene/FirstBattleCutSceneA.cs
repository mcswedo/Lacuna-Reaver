using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class FirstBattleCutSceneA : Scene
    {
        Dialog dialog =
            new Dialog(DialogPrompt.NeedsClose, Strings.Z552);


        public FirstBattleCutSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            dialog.DialogCompleteEvent += TriggerScriptedMonsterBattle;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft));
            SoundSystem.Play(AudioCues.MonsterRoar);
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

        void TriggerScriptedMonsterBattle(object sender, PartyResponseEventArgs e)
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "ScriptedMonster1",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
               Party.Singleton.CurrentMap.Name,
               "ScriptedMonster2",
               Point.Zero,
               ModAction.Remove,
               true);

            CombatZone zone = CombatZonePool.keepZoneDoubleDemon1;  

            ScreenManager.Singleton.AddScreen(new CombatScreen(zone));

            state = SceneState.Complete;
        }
    }
}
