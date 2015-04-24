using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{   
    public class FinalBattleSceneD : Scene
    {  
        float alpha;

        public FinalBattleSceneD(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            alpha = 0;

            InputState.SetVibration(.5f, .5f);

            Camera.Singleton.Shake(TimeSpan.FromSeconds(1.5), 5);

            SoundSystem.Play(AudioCues.Earthquake);

            SoundSystem.Play(AudioCues.MonsterRoar);

            //Party.Singleton.ModifyNPC(
            //    Party.Singleton.CurrentMap.Name,
            //    "ArlanS",
            //    Point.Zero,
            //    ModAction.Remove,
            //    false);

            //Party.Singleton.ModifyNPC(
            //    Party.Singleton.CurrentMap.Name,
            //    "HealersBlacksmith",
            //    new Point(20,3),
            //    ModAction.Add,
            //    false);

            Party.Singleton.Leader.FaceDirection(Direction.East);
            Party.Singleton.ModifyNPC(Party.Singleton.CurrentMap.Name, "Malticar", new Point(22, 2), Direction.South, true, ModAction.Add, false);
            Party.Singleton.CurrentMap.GetNPC("Malticar").FaceDirection(Direction.South);
            Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.West);
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);
        }


        public override void HandleInput(GameTime gameTime)
        {
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(alpha, Color.White, spriteBatch);
        }
    }
}
