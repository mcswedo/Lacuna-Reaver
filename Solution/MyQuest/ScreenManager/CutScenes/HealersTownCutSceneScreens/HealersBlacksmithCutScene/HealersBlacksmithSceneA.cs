using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyQuest
{
    public class HealersBlacksmithSceneA : Scene
    {        
        NPCMapCharacter bob;

        NPCMapCharacter blacksmith;

        Dialog dialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z522);

        public HealersBlacksmithSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void Complete()
        {
            Party.Singleton.ModifyNPC(
                 "healers_village",
                 "GateGuard1",
                 Point.Zero,
                 ModAction.Remove,
                 true);

            Party.Singleton.ModifyNPC(
                "healers_village",
                "GateGuard2",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "healers_village",
                "Bob",
                Point.Zero,
                ModAction.Remove,
                true);

            Party.Singleton.ModifyNPC(
                "healers_village",
                "DeadGateGuard",
                new Point(22, 13),
                ModAction.Add,
                true);

            Party.Singleton.ModifyNPC(
                "healers_village",
                "InjuredGateGuard",
                new Point(19, 15),
                ModAction.Add,
                true);
        }

        public override void LoadContent(ContentManager content)
        {
            bob = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "Bob");
            blacksmith = MyContentManager.LoadNPCMapCharacter(ContentPath.ToNPCMapCharacters + "HealersBlacksmith");
        }

        void MoveHelperOnCompleteEvent(object sender, EventArgs e)
        {
            bob.IdleOnly = true;
            Party.Singleton.Leader.FaceDirection(Direction.South);
            ScreenManager.Singleton.AddScreen(new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Bob"));
            state = SceneState.Complete;
        }

        public override void Initialize()
        {
            Complete();

            SceneHelper bobHelper = new MoveNpcCharacterHelper(
                "Bob",
                new Point(6, 8),
                false,
                new Point(6, 5),
                2.8f);

            bobHelper.OnCompleteEvent += new EventHandler(MoveHelperOnCompleteEvent);
            helpers.Add(bobHelper);

            SoundSystem.Play("Door");          
        }


        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
