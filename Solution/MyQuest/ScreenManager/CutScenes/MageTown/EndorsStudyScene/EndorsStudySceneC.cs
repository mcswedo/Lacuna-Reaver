using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class EndorsStudySceneC : Scene
    {
        Dialog dialog;
  

        #region Dialog

        static readonly Dialog endorDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z575);

        static readonly Dialog caraDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z576);

        static readonly Dialog endorDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z577, Strings.Z578, Strings.Z579, Strings.Z580);

        static readonly Dialog willDialog1 = new Dialog(
              DialogPrompt.NeedsClose,
              Strings.Z581);

        static readonly Dialog endorDialog3 = new Dialog(
              DialogPrompt.NeedsClose,
              Strings.Z582);

        static readonly Dialog endorDialog4 = new Dialog(
             DialogPrompt.NeedsClose,
             Strings.Z583,
             Strings.Z584,
             Strings.Z585,
             Strings.Z586,
             Strings.Z587,
             Strings.Z588,
             Strings.Z589, 
             Strings.Z590, 
             Strings.Z591, 
             Strings.Z592,
             Strings.Z593,
             Strings.Z594,
             Strings.Z595, 
             Strings.Z596,
             Strings.Z597,
             Strings.Z598,
             Strings.Z599, 
             Strings.Z600, 
             Strings.Z601,
             Strings.Z602,
             Strings.Z603);

        static readonly Dialog endorDialog5 = new Dialog(
             DialogPrompt.NeedsClose,
             Strings.Z604, 
             Strings.Z605, 
             Strings.Z606,
             Strings.Z607,
             Strings.Z608,
             Strings.Z609, 
             Strings.Z610
             );

        static readonly Dialog willDialog2 = new Dialog(
             DialogPrompt.NeedsClose,
             Strings.Z611);

        static readonly Dialog caraDialog2 = new Dialog(
             DialogPrompt.NeedsClose,
             Strings.Z612);

        static readonly Dialog willDialog3 = new Dialog(
             DialogPrompt.NeedsClose,
             Strings.Z613);

        static readonly Dialog endorDialog6 = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z614);

        static readonly Dialog nathanDialog = new Dialog(
            DialogPrompt.NeedsClose,
            "...");

        static readonly Dialog endorDialog7 = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z615);

        static readonly Dialog rewardDialog = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z616);

        static readonly Dialog endorDialog8 = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z617,
            Strings.Z618);

        static readonly Dialog willDialog4 = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z619);

        static readonly Dialog endorDialog9 = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z620,
            Strings.Z621,
            Strings.Z622,
            Strings.Z623,
            Strings.Z624,
            Strings.Z625);

        static readonly Dialog caraDialog3 = new Dialog(
            DialogPrompt.NeedsClose,
            Strings.Z626, 
            Strings.Z627);

        #endregion

        double ticker = 0;

        int currentFrame = 0;

        Vector2 position;

        Texture2D spriteSheet;

        bool playCharging;

        Vector2 offSet = new Vector2(-32, -32);

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

        public EndorsStudySceneC(Screen screen)
            : base(screen)
        {
        }

        public override void Complete()
        {
            Party.Singleton.GetFightingCharacter(Party.nathan).AddSkillName("Rift");
            //Nathan.Instance.unlockedElathiaRiftDestinations.Add(0); // these are already present
            //Nathan.Instance.unlockedElathiaRiftDestinations.Add(1);
            //Nathan.Instance.unlockedElathiaRiftDestinations.Add(2);
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            playCharging = false; 

            ticker = TimeSpan.FromSeconds(nathanChargeing.FrameDelay).TotalMilliseconds;

            position = Utility.ToWorldCoordinates(Party.Singleton.Leader.TilePosition, Party.Singleton.CurrentMap.TileDimensions);

            spriteSheet = MyContentManager.LoadTexture(ContentPath.ToMapCharacterTextures + "nathan_charging");

            Camera.Singleton.CenterOnTarget(
                Party.Singleton.Leader.WorldPosition,
                Party.Singleton.CurrentMap.DimensionsInPixels,
                ScreenManager.Singleton.ScreenResolution);

            Party.Singleton.Leader.FaceDirection(Direction.North);
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            dialog = endorDialog1;

            dialog.DialogCompleteEvent += caraResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
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
                      
                        dialog.DialogCompleteEvent += endorResponse7;

                        Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation; 

                        ScreenManager.Singleton.AddScreen(
                            new DialogScreen(dialog, DialogScreen.Location.TopLeft));

                        SoundSystem.Play("ChestOpen");
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

        #region Callbacks

        void caraResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse;

            dialog = caraDialog;

            dialog.DialogCompleteEvent += endorResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void endorResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse;

            dialog = endorDialog2;

            dialog.DialogCompleteEvent += willResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
        }

        void willResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willResponse;

            dialog = willDialog1;

            dialog.DialogCompleteEvent += endorResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void endorResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse2;

            dialog = endorDialog3;

            dialog.DialogCompleteEvent += endorResponse3;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
        }

        void endorResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse3;

            Party.Singleton.CurrentMap.GetNPC("Endor").FaceDirection(Direction.North);

            dialog = endorDialog4;

            dialog.DialogCompleteEvent += endorResponse4;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
        }

        void endorResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse4;

            Party.Singleton.CurrentMap.GetNPC("Endor").FaceDirection(Direction.South);

            dialog = endorDialog5;

            dialog.DialogCompleteEvent += willResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
        }

        void willResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willResponse2;

            dialog = willDialog2;

            dialog.DialogCompleteEvent += caraResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void caraResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse2;

            dialog = caraDialog2;

            dialog.DialogCompleteEvent += willResponse3;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));
        }

        void willResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willResponse3;

            dialog = willDialog3;

            dialog.DialogCompleteEvent += endorResponse5;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }


        void endorResponse5(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse5;

            dialog = endorDialog6;

            dialog.DialogCompleteEvent += nathanResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
        }


        void nathanResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathanResponse;

            dialog = nathanDialog;

            dialog.DialogCompleteEvent += endorResponse6;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Nathan"));
        }


        void endorResponse6(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse6;

            dialog = endorDialog7;

            dialog.DialogCompleteEvent += reward;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
        }

        void reward(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= reward;

            Party.Singleton.Leader.FaceDirection(Direction.South);

            Party.Singleton.Leader.CurrentAnimation = null; 
           
            SoundSystem.Play(AudioCues.Focus);

            Complete();

            playCharging = true; 

            dialog = rewardDialog;
        }

        void endorResponse7(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse7;

            Party.Singleton.Leader.FaceDirection(Direction.North);

            dialog = endorDialog8;

            dialog.DialogCompleteEvent += willResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
        }

        void willResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= willResponse4;

            dialog = willDialog4;

            dialog.DialogCompleteEvent += endorResponse8;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Will"));
        }

        void endorResponse8(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endorResponse8;

            dialog = endorDialog9;
            Party.Singleton.AddLogEntry("Celindar", "Endor", dialog.Text);

            dialog.DialogCompleteEvent += caraResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomRight, "Endor"));
        }

        void caraResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= caraResponse3;

            dialog = caraDialog3;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.BottomLeft, "Cara"));

            state = SceneState.Complete;
        }

        #endregion

     
    }
}
