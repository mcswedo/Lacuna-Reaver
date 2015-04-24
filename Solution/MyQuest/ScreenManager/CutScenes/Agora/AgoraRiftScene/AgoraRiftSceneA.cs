using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class AgoraRiftSceneA : Scene
    {
        #region Dialog
        
        Dialog dialog;

        static readonly Dialog endorDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z983, Strings.Z984);

        static readonly Dialog ruithDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z985);

        static readonly Dialog endor2Dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z986);

         static readonly Dialog nathanDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z987);

        #endregion

        public AgoraRiftSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Complete()
        {
            Party.Singleton.CurrentMap.GetNPC("Ruith").FaceDirection(Direction.East);
            Party.Singleton.CurrentMap.GetNPC("Lydia").FaceDirection(Direction.West);
            Party.Singleton.CurrentMap.ResetSpawnDirection("Ruith", Direction.East);
            Party.Singleton.CurrentMap.ResetSpawnDirection("Lydia", Direction.West);
        }
        public override void Initialize()
        {
            Camera.Singleton.CenterOnTarget(
               Party.Singleton.Leader.WorldPosition,
               Party.Singleton.CurrentMap.DimensionsInPixels,
               ScreenManager.Singleton.ScreenResolution);

            MoveCameraHelper cameraHelper = new MoveCameraHelper(new Point(4,12), 4.0f);

            MovePCMapCharacterHelper nathanHelper1 = new MovePCMapCharacterHelper(new Point(4,12));

            helpers.Add(nathanHelper1);
            helpers.Add(cameraHelper);
            nathanHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
           
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            dialog = endorDialog; 
            ScreenManager.Singleton.AddScreen(
           new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));

            dialog.DialogCompleteEvent += ruithsResponse; 

        }

        void ruithsResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= ruithsResponse; 
            dialog = ruithDialog;
            
            ScreenManager.Singleton.AddScreen(
            new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Ruith"));

            dialog.DialogCompleteEvent += endors2Response;           
        }

        void endors2Response(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endors2Response; 
            dialog = endor2Dialog;

            ScreenManager.Singleton.AddScreen(
            new DialogScreen(dialog, DialogScreen.Location.TopRight, "Endor"));

            dialog.DialogCompleteEvent += nathansResponse;
        }

        void nathansResponse(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= nathansResponse;
            dialog = nathanDialog;

            dialog.DialogCompleteEvent += endScene;

            ScreenManager.Singleton.AddScreen(
            new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));
        
        }

        void endScene(object sender, EventArgs e)
        {
            dialog.DialogCompleteEvent -= endScene;

            Complete(); 
            state = SceneState.Complete;
        }
        #endregion
    }
}
