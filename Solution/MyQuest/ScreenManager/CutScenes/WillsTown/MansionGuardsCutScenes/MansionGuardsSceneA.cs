using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class MansionGuardsSceneA : Scene
    {
        Dialog cantLeaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z919);

        public MansionGuardsSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.SetAutoMovement(Direction.South, Party.Singleton.CurrentMap);
            Party.Singleton.Leader.FaceDirection(Direction.North);
            Party.Singleton.Leader.Velocity *= .5f;
            ScreenManager.Singleton.AddScreen(new DialogScreen(cantLeaveDialog, DialogScreen.Location.TopLeft, "Guard"));
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Singleton.CenterOnTarget(
                 Party.Singleton.Leader.WorldPosition,
                 Party.Singleton.CurrentMap.DimensionsInPixels,
                 ScreenManager.Singleton.ScreenResolution);

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
