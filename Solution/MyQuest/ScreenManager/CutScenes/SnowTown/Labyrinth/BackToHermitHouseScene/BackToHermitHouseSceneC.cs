using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class BackToHermitHouseSceneC : Scene
    {
        Dialog dialog;

        #region Dialog

        static readonly Dialog carasDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z791, Strings.Z792, Strings.Z793);

        static readonly Dialog nathansDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z794);

        static readonly Dialog carasDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z795);

        static readonly Dialog nathansDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z796, Strings.Z797, Strings.Z798, Strings.Z799, Strings.Z800, Strings.Z801, Strings.Z802, Strings.Z803, Strings.Z804);

        static readonly Dialog carasDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z805);

        static readonly Dialog blacksmithsDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z806);

        static readonly Dialog carasDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z807);

        static readonly Dialog blacksmithsDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z808);

        static readonly Dialog carasDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z809, Strings.Z810);

        static readonly Dialog blacksmithsDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z811);

        static readonly Dialog carasDialog6 = new Dialog(DialogPrompt.NeedsClose, Strings.Z812);

        static readonly Dialog blacksmithsDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z813, Strings.Z814, Strings.Z815, Strings.Z816, Strings.Z817, Strings.Z818, Strings.Z819, Strings.Z820, Strings.Z821, Strings.Z822);

        static readonly Dialog nathansDialog3 = new Dialog(DialogPrompt.NeedsClose, Strings.Z823);

        static readonly Dialog blacksmithsDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z824, Strings.Z825);

        static readonly Dialog nathansDialog4 = new Dialog(DialogPrompt.NeedsClose, Strings.Z826);

        static readonly Dialog blacksmithsDialog6 = new Dialog(DialogPrompt.NeedsClose, Strings.Z827);

        static readonly Dialog rewardDialog  = new Dialog(DialogPrompt.NeedsClose, Strings.Z828);

        static readonly Dialog blacksmithsDialog7 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA616, Strings.ZA617);

        static readonly Dialog rewardDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA618);

        static readonly Dialog blacksmithsDialog8 = new Dialog(DialogPrompt.NeedsClose, Strings.Z829, Strings.Z830);

        static readonly Dialog nathansDialog5 = new Dialog(DialogPrompt.NeedsClose, Strings.Z831);

        static readonly Dialog blacksmithsDialog9 = new Dialog(DialogPrompt.NeedsClose, Strings.Z832);

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
        public BackToHermitHouseSceneC(Screen screen)
            : base(screen)
        {
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
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.East);

            Nathan.Instance.EquipWeapon(EquipmentPool.RequestEquipment("ExpertsSword"));
            Nathan.Instance.EquipArmor(EquipmentPool.RequestEquipment("ExpertsArmor"));

            dialog = carasDialog;

            dialog.DialogCompleteEvent += nathansResponse;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
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

                        dialog.DialogCompleteEvent += blacksmithsResponse7;
                        ScreenManager.Singleton.AddScreen(
                             new DialogScreen(dialog, DialogScreen.Location.BottomLeft));

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

        void nathansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse;

            dialog = nathansDialog;

            Party.Singleton.GameState.Inventory.AddItem(typeof(DivineRing), 1);

            dialog.DialogCompleteEvent += carasResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void carasResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse;

            dialog = carasDialog2;

            dialog.DialogCompleteEvent += nathansResponse2;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void nathansResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse2;

            dialog = nathansDialog2;

            dialog.DialogCompleteEvent += carasResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void carasResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse2;

            dialog = carasDialog3;

            dialog.DialogCompleteEvent += blacksmithsResponse;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse;

            dialog = blacksmithsDialog;

            dialog.DialogCompleteEvent += carasResponse3;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void carasResponse3(object sender, EventArgs e)
        {
            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            dialog.DialogCompleteEvent -= carasResponse3;

            dialog = carasDialog4;

            dialog.DialogCompleteEvent += blacksmithsResponse2;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse2;

            dialog = blacksmithsDialog2;

            dialog.DialogCompleteEvent += carasResponse4;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void carasResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse4;

            dialog = carasDialog5;

            dialog.DialogCompleteEvent += blacksmithsResponse3;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse3;

            dialog = blacksmithsDialog3;

            dialog.DialogCompleteEvent += carasResponse5;
            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void carasResponse5(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= carasResponse5;

            dialog = carasDialog6;

            dialog.DialogCompleteEvent += blacksmithsResponse4;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Cara"));
        }

        void blacksmithsResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse4;

            dialog = blacksmithsDialog4;

            dialog.DialogCompleteEvent += nathansResponse3;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void nathansResponse3(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse3;

            dialog = nathansDialog3;

            dialog.DialogCompleteEvent += blacksmithsResponse5;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void blacksmithsResponse5(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse5;

            dialog = blacksmithsDialog5;

            dialog.DialogCompleteEvent += nathansResponse4;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void nathansResponse4(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse4;

            dialog = nathansDialog4;

            dialog.DialogCompleteEvent += blacksmithsResponse6;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void blacksmithsResponse6(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse6;

            dialog = blacksmithsDialog6;

            dialog.DialogCompleteEvent += reward;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void reward(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= reward;

            Party.Singleton.Leader.FaceDirection(Direction.South);

            Party.Singleton.Leader.CurrentAnimation = null;
            
            SoundSystem.Play(AudioCues.Focus);
          
            playCharging = true; 

            dialog = rewardDialog;         
        }

        void blacksmithsResponse7(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse7;

            Party.Singleton.Leader.FaceDirection(Direction.North);

            dialog = blacksmithsDialog7;

            dialog.DialogCompleteEvent += reward2;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void reward2(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= reward2;

            dialog = rewardDialog2;

            dialog.DialogCompleteEvent += blacksmithsResponse8;

            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight));
        }

        void blacksmithsResponse8(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse8;

            dialog = blacksmithsDialog8;

            dialog.DialogCompleteEvent += nathansResponse5;
            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));
        }

        void nathansResponse5(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse5;

            dialog = nathansDialog5;

            dialog.DialogCompleteEvent += blacksmithsResponse9;
            ScreenManager.Singleton.AddScreen(
               new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        }

        void blacksmithsResponse9(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= blacksmithsResponse9;

            dialog = blacksmithsDialog9;

            ScreenManager.Singleton.AddScreen(
                 new DialogScreen(dialog, DialogScreen.Location.TopRight, "MasterBlacksmith"));

            state = SceneState.Complete;
        }

        #endregion
    }
}
