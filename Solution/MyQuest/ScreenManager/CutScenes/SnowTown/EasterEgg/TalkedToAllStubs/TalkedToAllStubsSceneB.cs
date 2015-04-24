using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class TalkedToAllStubsSceneB : Scene
    {
        Dialog dialog;

        Dialog stubDialog1 = new Dialog(DialogPrompt.NeedsClose, Strings.ZA705, Strings.ZA706, Strings.ZA707);

        public TalkedToAllStubsSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            if (Party.Singleton.Leader.TilePosition.Y > 5)
            {
                Party.Singleton.Leader.FaceDirection(Direction.North);
            }
            else
            {
                Party.Singleton.Leader.FaceDirection(Direction.South);
            }
            dialog = stubDialog1;

            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Stub"));
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
            state = SceneState.Complete;
        }
    }
}
