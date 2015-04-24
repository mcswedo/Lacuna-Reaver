using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class FinalBattleSceneG : Scene
    {
        public FinalBattleSceneG(Screen screen)
            : base(screen)
        {
        }

        double ticker = 0;

        int currentFrame = 0;

        Vector2 position;

        Texture2D spriteSheet;

        bool playCharging;

        Vector2 offSet = new Vector2(-32, 0);
        static readonly FrameAnimation nathanChargeing = new FrameAnimation()
        {
            FrameDelay = .075,
            Frames = new List<Rectangle>()
                {
                    new Rectangle (0,0,128,128), 
                    new Rectangle (128,0,128,128), 
                    new Rectangle (256,0,128,128),
                    new Rectangle (384,0,128,128),
                    new Rectangle (512,0,128,128),
                    new Rectangle (640,0,128,128),
                    new Rectangle (512,0,128,128),
                    new Rectangle (384,0,128,128),
                    new Rectangle (256,0,128,128),
                    new Rectangle (128,0,128,128), 
                    new Rectangle (0,0,128,128),
 
                    new Rectangle (0,0,128,128), 
                    new Rectangle (128,0,128,128), 
                    new Rectangle (256,0,128,128),
                    new Rectangle (384,0,128,128),
                    new Rectangle (512,0,128,128),
                    new Rectangle (640,0,128,128),
                    new Rectangle (512,0,128,128),
                    new Rectangle (384,0,128,128),
                    new Rectangle (256,0,128,128),
                    new Rectangle (128,0,128,128), 
                    new Rectangle (0,0,128,128)              
                }
        };

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            // Add animation for Arlan's sacrifice here

            playCharging = true; 

            ticker = TimeSpan.FromSeconds(nathanChargeing.FrameDelay).TotalMilliseconds;

            position = Utility.ToWorldCoordinates(Party.Singleton.Leader.TilePosition, Party.Singleton.CurrentMap.TileDimensions);

            spriteSheet = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "nathan_charging");

            SoundSystem.Play(AudioCues.Focus);

            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);
            Party.Singleton.Leader.FaceDirection(Direction.South);

            Party.Singleton.Leader.CurrentAnimation = null;

        }

        public override void Update(GameTime gameTime)
        {

            if (playCharging)
            {
                ticker -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (ticker <= 0)
                {
                    ticker = TimeSpan.FromSeconds(nathanChargeing.FrameDelay).TotalMilliseconds;

                    currentFrame++;

                    if (currentFrame >= nathanChargeing.Frames.Count)
                    {
                        currentFrame = 11;

                        playCharging = false;


                        Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;

                        Party.Singleton.Leader.FaceDirection(Direction.South);

                        SoundSystem.Play("ChestOpen");

                        Party.Singleton.ModifyNPC(
                            Party.Singleton.CurrentMap.Name,
                            "ArlanInjured",
                            Point.Zero,
                            ModAction.Remove,
                            true);

                        Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);
                        Party.Singleton.CurrentMap.GetNPC("Will").FaceDirection(Direction.South);

                        state = SceneState.Complete;
                    }
                }
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (playCharging)
            {
                spriteBatch.Draw(
                    spriteSheet,
                    position + new Vector2(Camera.Singleton.OffsetX, Camera.Singleton.OffsetY) + offSet,
                    nathanChargeing.Frames[currentFrame],
                    Color.White);
            }
        }
    }
}
