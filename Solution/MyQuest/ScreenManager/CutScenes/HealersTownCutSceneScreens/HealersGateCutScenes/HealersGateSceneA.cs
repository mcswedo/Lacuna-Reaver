using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class HealersGateSceneA : Scene
    {
        Dialog cantLeaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z523, Strings.Z524);

        public HealersGateSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.SetAutoMovement(Direction.North, Party.Singleton.CurrentMap);
            Party.Singleton.Leader.FaceDirection(Direction.South);
            Party.Singleton.Leader.Velocity *= .5f;
            ScreenManager.Singleton.AddScreen(new DialogScreen(cantLeaveDialog, DialogScreen.Location.TopLeft, "Guard"));
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.Leader.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);         

            if (Party.Singleton.Leader.IsMoving == false)
            {
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
