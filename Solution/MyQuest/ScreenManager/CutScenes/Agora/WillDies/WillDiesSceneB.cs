using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    class WillDiesSceneB : Scene
    {
        public WillDiesSceneB(Screen screen)
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
                    "Cara",
                    Point.Zero,
                    ModAction.Remove,
                    false);
           
            Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "InjuredCara",
                    new Point(19, 4),
                    ModAction.Add,
                    false);
            Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.East);
            SoundSystem.Play(AudioCues.CaraHit);
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
