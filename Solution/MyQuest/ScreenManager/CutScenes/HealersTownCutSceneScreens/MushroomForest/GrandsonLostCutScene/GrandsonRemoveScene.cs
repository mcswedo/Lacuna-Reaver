using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class GrandsonRemoveScene : Scene
    {
        public GrandsonRemoveScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper(
                "Grandson",
                new Point( Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y),
                1.7f);

            moveHelper1.OnCompleteEvent += new EventHandler(moveHelper1_OnCompleteEvent);
            helpers.Add(moveHelper1);
        }

        void moveHelper1_OnCompleteEvent(object sender, EventArgs e)
        {
            MusicSystem.InterruptMusic(AudioCues.JoinedPartySFX);

            Party.Singleton.ModifyNPC(
                       "mushroom_forest",
                       "Grandson",
                       Party.Singleton.Leader.TilePosition,
                       ModAction.Remove,
                       true);

            state = SceneState.Complete;
        }

  

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}

