using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    class Keepf5CutSceneD : Scene
    {
        public Keepf5CutSceneD(Screen screen)
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
                "Friend1",
                new Point(4, 5),
                ModAction.Remove,
                false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Friend2",
                new Point(6, 5),
                ModAction.Remove,
                false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Friend1",
                new Point(4, 5),
                ModAction.Add,
                false);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "DeadSid",
                new Point(6, 5),
                ModAction.Add,
                false);
            SoundSystem.Play(AudioCues.MaxHit);
        }

        public override void Update(GameTime gameTime)
        {
            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(1, Color.White, spriteBatch); 
        }
    }
}
