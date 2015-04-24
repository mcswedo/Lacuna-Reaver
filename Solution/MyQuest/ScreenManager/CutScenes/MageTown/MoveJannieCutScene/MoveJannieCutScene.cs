using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class MoveJannieCutScene : Scene
    {
        NPCMapCharacter jannie;

        public MoveJannieCutScene(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            jannie = Party.Singleton.CurrentMap.GetNPC("Jannie");

            Party.Singleton.ModifyNPC(
                  Maps.mageTown,
                  "Jannie",
                  new Point(5, 1),
                  ModAction.Remove,
                  true);

            Party.Singleton.ModifyNPC(
                  Maps.mageTownNpcHouse2,
                  "Jannie_Tippers",
                  new Point(2, 1),
                  ModAction.Add,
                  true);
        }

        public override void Update(GameTime gameTime)
        {
            jannie.FaceDirection(Direction.South);
            state = SceneState.Complete;
        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(1, Color.Black, spriteBatch);
        }
    }
}