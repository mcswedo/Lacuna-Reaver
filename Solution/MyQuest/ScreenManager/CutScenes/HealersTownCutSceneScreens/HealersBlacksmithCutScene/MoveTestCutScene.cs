using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class MoveTestCutScene : Scene
    {
        Point currentDest;

        NPCMapCharacter bob;

        List<Direction> steps = new List<Direction>();

        #region Achievement

        internal const string gateOpenAchievement = "GateOpen";

        #endregion

        public MoveTestCutScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            Party.Singleton.ModifyNPC(
                "healers_village",
                "Bob",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "Bob",
                new Point(6, 8),
                ModAction.Add,
                true);

            bob = Party.Singleton.CurrentMap.GetNPC("Bob");
        }

        public override void Initialize()
        {
            bob.MoveSpeed = 2.8f;

            bob.MoveDelay = 1;

            steps = Utility.GetPathTo(bob.TilePosition, Party.Singleton.Leader.TilePosition);

            currentDest = Utility.GetMapPositionFromDirection(bob.TilePosition, steps[0]);
            bob.SetAutoMovement(steps[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            bob.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (bob.TilePosition == currentDest)
            {
                steps.RemoveAt(0);
                if (steps.Count == 0)
                {
                    state = SceneState.Complete;
                }
                else
                {
                    currentDest = Utility.GetMapPositionFromDirection(bob.TilePosition, steps[0]);
                    bob.SetAutoMovement(steps[0], Party.Singleton.CurrentMap);
                }
            }
        }
        
        public override void HandleInput()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
