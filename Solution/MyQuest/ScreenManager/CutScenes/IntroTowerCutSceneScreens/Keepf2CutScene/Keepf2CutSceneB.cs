using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    class Keepf2CutSceneB : Scene
    {
        int onCompleteEventCount = 0;

        public Keepf2CutSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            SceneHelper moveHelper1 = new MoveNpcCharacterHelper("ScriptedMonster1", new Point(Party.Singleton.Leader.TilePosition.X - 1, Party.Singleton.Leader.TilePosition.Y), 2.8f);

            SceneHelper moveHelper2 = new MoveNpcCharacterHelper("ScriptedMonster2", new Point(Party.Singleton.Leader.TilePosition.X + 1, Party.Singleton.Leader.TilePosition.Y), 2.8f);

            helpers.Add(moveHelper1);
            helpers.Add(moveHelper2);

            moveHelper1.OnCompleteEvent += new EventHandler(onCompleteEvent);
            moveHelper2.OnCompleteEvent += new EventHandler(onCompleteEvent);

        }

        void onCompleteEvent(object sender, EventArgs e)
        {
            ++onCompleteEventCount;

            if (onCompleteEventCount == 2)
            {
                state = SceneState.Complete;

                Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "ScriptedMonster1",
                    Point.Zero,
                    ModAction.Remove,
                    true);

                Party.Singleton.ModifyNPC(
                    Party.Singleton.CurrentMap.Name,
                    "ScriptedMonster2",
                    Point.Zero,
                    ModAction.Remove,
                    true);

                CombatZone zone = CombatZonePool.keepZoneDemons;

                ScreenManager.Singleton.AddScreen(new CombatScreen(zone));
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
