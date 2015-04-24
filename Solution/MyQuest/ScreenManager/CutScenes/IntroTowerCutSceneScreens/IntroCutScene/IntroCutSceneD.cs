using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class IntroCutSceneD : Scene
    {
        public IntroCutSceneD(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            helpers.Add(new MoveNpcCharacterHelper(
                "Friend1",
                Party.Singleton.Leader.TilePosition,
                1.7f));

            helpers.Add(new MoveNpcCharacterHelper(
                "Friend2",
                Party.Singleton.Leader.TilePosition,
                1.7f));

            Party.Singleton.Leader.FaceDirection(Direction.North);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            /// Normally you would perform this logic in the helper.OnCompleteEvent but
            /// we want both helpers to complete before we remove the npc from the
            /// map. We can use the HelperesAreComplete property to check if all of
            /// the helpers have completed their tasks
            if (HelperesAreComplete)
            {
                Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "Friend1",
                   new Point(4, 14),
                   ModAction.Remove,
                   true);

                Party.Singleton.ModifyNPC(
                   Party.Singleton.CurrentMap.Name,
                   "Friend2",
                   new Point(4, 14),
                   ModAction.Remove,
                   true);

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
