using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class HealerJoinsSceneA : Scene
    {

        #region Dialog

        Dialog waitDialog =
        new Dialog(DialogPrompt.NeedsClose, Strings.Z513);

        #endregion

        public HealerJoinsSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Complete()
        {
            Party.Singleton.ModifyNPC(
                            "healers_village_mayors_house_f2",
                            Party.cara,
                            Point.Zero,
                            ModAction.Remove,
                            true);

            Party.Singleton.ModifyNPC(
                            "healers_village",
                            Party.cara,
                            new Point(17, 7),
                            ModAction.Add,
                            false);
        }
        public override void Initialize()
        {
            Complete();
            Party.Singleton.Leader.FaceDirection(Direction.North);
            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.IdleAnimation;

        }

        public override void Update(GameTime gameTime)
        {

            waitDialog.DialogCompleteEvent += wait;
            ScreenManager.Singleton.AddScreen(new DialogScreen(waitDialog, DialogScreen.Location.TopLeft, "Cara"));

        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        #region Callbacks
 
        void wait(object sender, PartyResponseEventArgs e)
        {
            waitDialog.DialogCompleteEvent -= wait;
            state = SceneState.Complete;
        }

        #endregion

    }
}
