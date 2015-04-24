using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class BossRoar : Scene
    {       
        float alpha;
        bool isLaugh;

        public BossRoar(Screen screen, bool isLaugh = false)
            : base(screen)
        {
            this.isLaugh = isLaugh;
        }

        public override void LoadContent(ContentManager content)
        {
           
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.FaceDirection(Direction.North);

            alpha = 0;

            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);

            if (!isLaugh)
            {
                SoundSystem.Play(AudioCues.MonsterRoar);
            }
            else
            {
                SoundSystem.Play(AudioCues.MalticarLaugh);
            }
        }

        public override void Update(GameTime gameTime)
        {         
            alpha += .05f;
            if (alpha >= 1)
            {
                alpha = 0;
                InputState.SetVibration(0f, 0f);
                state = SceneState.Complete;
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
