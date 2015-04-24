using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class MayorsHouseSceneB : Scene
    {
        Point destinationPoint;
        
        public MayorsHouseSceneB(Screen screen)
            : base(screen)
        {
        }
        
        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            destinationPoint = Party.Singleton.Leader.TilePosition;
            destinationPoint.X -= 1;

            /// Setup Nathan to perform an automove (movement without input) in the specified direction
            Party.Singleton.Leader.SetAutoMovement(Direction.West, Party.Singleton.CurrentMap);

            /// SetAutoMovement sets the velocity based on the direction
            /// of movement but we can slow him down for added effect
            Party.Singleton.Leader.Velocity *= 0.50f;
            
            /// SetAutoMovement sets the character's direction but we can override that with this call
            Party.Singleton.Leader.FaceDirection(Direction.South);
        }

        public override void Update(GameTime gameTime)
        {
            /// Update Nathan as you normally would. This updates his movement and animation.
            Party.Singleton.Leader.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            /// Once he's reached his destination, we're done
            if (Party.Singleton.Leader.TilePosition == destinationPoint)
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
