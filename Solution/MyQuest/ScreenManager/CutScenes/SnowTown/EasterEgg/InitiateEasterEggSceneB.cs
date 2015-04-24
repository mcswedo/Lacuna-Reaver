using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class InitiateEasterEggSceneB : Scene
    {
        NPCMapCharacter stub = Party.Singleton.CurrentMap.GetNPC("Stub");

        public InitiateEasterEggSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper stubHelper = new MoveNpcCharacterHelper("Stub", new Point(21, 0), 1.5f);

            stubHelper.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(stubHelper);
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
            Party.Singleton.ModifyNPC(
                Maps.caveLabyrinth,
                "Stub",
                Point.Zero,
                ModAction.Remove,
                true);

            state = SceneState.Complete;
        }

        #endregion


    }
}
