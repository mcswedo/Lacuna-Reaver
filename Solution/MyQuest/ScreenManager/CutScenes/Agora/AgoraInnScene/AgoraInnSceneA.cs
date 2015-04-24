using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class AgoraInnSceneA : Scene
    {
        #region Dialog

        static readonly Dialog npc1Dialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z971);

        static readonly Dialog npc2Dialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z972, Strings.Z973);

        static readonly Dialog npc1Dialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z974);

        static readonly Dialog nathanDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z975, Strings.Z976, Strings.Z977);

        static readonly Dialog npc2Dialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z978);

        static readonly Dialog nathanDialog2 = new Dialog(DialogPrompt.NeedsClose, Strings.Z979);

        static readonly Dialog caraDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z980);

        static readonly Dialog willDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.Z981, Strings.Z982);

        #endregion

        SceneHelper nathanHelper1;
        SceneHelper caraHelper;
        SceneHelper willHelper;
        SceneHelper moveHelper4;
        SceneHelper moveHelper5;
        public AgoraInnSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            nathanHelper1 = new MovePCMapCharacterHelper(new Point(Party.Singleton.Leader.TilePosition.X, 
                Party.Singleton.Leader.TilePosition.Y - 2));


            nathanHelper1.OnCompleteEvent += moveHelper1_OnCompleteEvent;
            helpers.Add(nathanHelper1);

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
             nathanHelper1.OnCompleteEvent -= new EventHandler(moveHelper1_OnCompleteEvent);
             caraHelper = new MoveNpcCharacterHelper(
             Party.cara,
             new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
             false,
             new Point(Party.Singleton.Leader.TilePosition.X - 1, Party.Singleton.Leader.TilePosition.Y),
             1.7f);

            caraHelper.OnCompleteEvent += moveHelper2_OnCompleteEvent;

            helpers.Add(caraHelper);
         
        }

        void moveHelper2_OnCompleteEvent(object sender, EventArgs e)
        {
            caraHelper.OnCompleteEvent -= moveHelper2_OnCompleteEvent;

            Party.Singleton.CurrentMap.GetNPC("Cara").FaceDirection(Direction.North);

            willHelper = new MoveNpcCharacterHelper(
            Party.will,
            new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
            false,
            new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y),
            1.7f);

            willHelper.OnCompleteEvent += moveHelper3_OnCompleteEvent;
            helpers.Add(willHelper);
        }

        void moveHelper3_OnCompleteEvent(object sender, EventArgs e)
        {
            willHelper.OnCompleteEvent -= moveHelper3_OnCompleteEvent;

            npc1Dialog1.DialogCompleteEvent += npc2Response1;

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(npc1Dialog1, DialogScreen.Location.TopRight, "Bill"));
        }

        void npc2Response1(object sender, EventArgs e)
        {
            npc1Dialog1.DialogCompleteEvent -= npc2Response1;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(npc2Dialog1, DialogScreen.Location.TopLeft, "Guard"));

            npc2Dialog1.DialogCompleteEvent += npc1Response2;
        }

        void npc1Response2(object sender, EventArgs e)
        {
            npc2Dialog1.DialogCompleteEvent -= npc1Response2;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(npc1Dialog2, DialogScreen.Location.TopRight, "Refman"));

            npc1Dialog2.DialogCompleteEvent += nathanResponse1;
        }

        void nathanResponse1(object sender, EventArgs e)
        {
            npc1Dialog2.DialogCompleteEvent -= nathanResponse1;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(nathanDialog1, DialogScreen.Location.TopLeft, "Nathan"));

            nathanDialog1.DialogCompleteEvent += npc2Response2;
        }

        void npc2Response2(object sender, EventArgs e)
        {
            nathanDialog1.DialogCompleteEvent -= npc2Response2;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(npc2Dialog2, DialogScreen.Location.TopRight, "Guard"));

            npc2Dialog2.DialogCompleteEvent += nathanResponse2;
        }

        void nathanResponse2(object sender, EventArgs e)
        {
            npc2Dialog2.DialogCompleteEvent -= nathanResponse2;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(nathanDialog2, DialogScreen.Location.TopLeft, "Nathan"));

           nathanDialog2.DialogCompleteEvent += caraResponse;
        }

        void caraResponse(object sender, EventArgs e)
        {
            nathanDialog2.DialogCompleteEvent -= caraResponse;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(caraDialog1, DialogScreen.Location.TopLeft, "Cara"));

            caraDialog1.DialogCompleteEvent += willResponse;
        }

        void willResponse(object sender, EventArgs e)
        {
            caraDialog1.DialogCompleteEvent -= willResponse;

            ScreenManager.Singleton.AddScreen(
               new DialogScreen(willDialog1, DialogScreen.Location.TopLeft, "Will"));

            willDialog1.DialogCompleteEvent += returnWill;
        }

        void returnWill(object sender, EventArgs e)
        {
            willDialog1.DialogCompleteEvent -= returnWill;
            moveHelper4 = new MoveNpcCharacterHelper(
                      "Will", Party.Singleton.Leader.TilePosition, 1.5f);

            moveHelper4.OnCompleteEvent += returnCara;
            helpers.Add(moveHelper4);
        }

        void returnCara(object sender, EventArgs e)
        {
            moveHelper4.OnCompleteEvent -= returnCara;
            Party.Singleton.ModifyNPC(Party.Singleton.CurrentMap.Name, "Will", Point.Zero, ModAction.Remove, true);

             moveHelper5 = new MoveNpcCharacterHelper(
             "Cara", Party.Singleton.Leader.TilePosition, 1.5f);
            helpers.Add(moveHelper5);

            moveHelper5.OnCompleteEvent += endScene;
        }

        void endScene(object sender, EventArgs e)
        {
            moveHelper5.OnCompleteEvent -= endScene;
            Party.Singleton.ModifyNPC(Party.Singleton.CurrentMap.Name, "Cara", Point.Zero, ModAction.Remove, true);

            state = SceneState.Complete;
        }

        #endregion
    }
}
