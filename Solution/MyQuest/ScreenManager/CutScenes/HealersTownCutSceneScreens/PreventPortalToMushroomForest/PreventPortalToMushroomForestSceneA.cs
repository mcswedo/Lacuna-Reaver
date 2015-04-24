using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class PreventPortalToMushroomForestSceneA : Scene
    {
        Dialog cantLeaveDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z544);

        public PreventPortalToMushroomForestSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.Leader.SetAutoMovement(Direction.West, Party.Singleton.CurrentMap);
            Party.Singleton.Leader.FaceDirection(Direction.West);
            Party.Singleton.Leader.Velocity = new Vector2(-0.01f,0);
            Party.Singleton.Leader.IsMoving = true;
            
            ScreenManager.Singleton.AddScreen(new DialogScreen(cantLeaveDialog, DialogScreen.Location.TopLeft, "Guard"));
        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.Leader.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);
            Party.Singleton.Leader.CurrentAnimation = Party.Singleton.Leader.WalkingAnimation;
            Party.Singleton.Leader.WorldPosition += new Vector2(-2, 0);
            //if (Party.Singleton.Leader.IsMoving == false)
            if(Party.Singleton.Leader.WorldPosition.X <= 2336)
            {
                Party.Singleton.Leader.IsMoving = false;
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
