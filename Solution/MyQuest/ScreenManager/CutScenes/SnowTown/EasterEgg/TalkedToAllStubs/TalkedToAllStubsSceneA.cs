using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class TalkedToAllStubsSceneA : Scene
    {
        public TalkedToAllStubsSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubKyle",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubGlen",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubRyan",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubGerren",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubDavid",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubMichael",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubJonathan",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubMatt",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubJames",
                Point.Zero,
                ModAction.Remove,
                true);


            //Re add all stubs in an organized position
            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubKyle",
                new Point(3, 5),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubGlen",
                new Point(3, 4),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubRyan",
                new Point(4, 3),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubGerren",
                new Point(5, 3),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubDavid",
                new Point(6, 3),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubMichael",
                new Point(7, 3),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubJames",
                new Point(8, 3),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubMatt",
                new Point(9, 4),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                Party.Singleton.CurrentMap.Name,
                "StubJonathan",
                new Point(9, 5),
                ModAction.Add,
                true);

            NPCMapCharacter stubK = Party.Singleton.CurrentMap.GetNPC("StubKyle");
            NPCMapCharacter stubGl = Party.Singleton.CurrentMap.GetNPC("StubGlen");
            NPCMapCharacter stubR = Party.Singleton.CurrentMap.GetNPC("StubRyan");
            NPCMapCharacter stubGe = Party.Singleton.CurrentMap.GetNPC("StubGerren");
            NPCMapCharacter stubD = Party.Singleton.CurrentMap.GetNPC("StubDavid");
            NPCMapCharacter stubM = Party.Singleton.CurrentMap.GetNPC("StubMichael");
            NPCMapCharacter stubJo = Party.Singleton.CurrentMap.GetNPC("StubJonathan");
            NPCMapCharacter stubJa = Party.Singleton.CurrentMap.GetNPC("StubJames");
            NPCMapCharacter stubMa = Party.Singleton.CurrentMap.GetNPC("StubMatt");

            stubK.FaceDirection(Direction.East);
            stubGl.FaceDirection(Direction.East);
            stubR.FaceDirection(Direction.South);
            stubGe.FaceDirection(Direction.South);
            stubD.FaceDirection(Direction.South);
            stubM.FaceDirection(Direction.South);
            stubJo.FaceDirection(Direction.West);
            stubJa.FaceDirection(Direction.South);
            stubMa.FaceDirection(Direction.West);
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Singleton.TintBackBuffer(1, Color.Black, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            state = SceneState.Complete;
        }
    }
}
