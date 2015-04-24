using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class HealerJoinsSceneB : Scene
    {
        #region Fields

        List<Direction> Steps;

        Point destinationPoint;

        NPCMapCharacter cara;

        bool arrivedDestination1;
        bool arrivedDestination2;
        bool arrivedDestination3;

        #endregion


        #region Dialog

        Dialog joinDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z514, Strings.Z515, Strings.Z516);

        Dialog partyDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z517);

        #endregion

        public HealerJoinsSceneB(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
 
        }

        public override void Complete()
        {
             Party.Singleton.ModifyNPC(
                Maps.healersVillage,
                Party.cara,
                Point.Zero,
                ModAction.Remove,
                true);

             Party.Singleton.ModifyNPC(
                 Maps.mushroomForest,
                 "Drifter",
                 Point.Zero,
                 ModAction.Remove,
                true); 

             Party.Singleton.AddFightingCharacter(Party.cara);
             if (Party.Singleton.GetFightingCharacter(Party.nathan).FighterStats.Level > 1)
             {
                 Party.Singleton.GetFightingCharacter(Party.cara).SetLevel(Party.Singleton.GetFightingCharacter(Party.nathan).FighterStats.Level - 1);
             }
             else
             {
                 Party.Singleton.GetFightingCharacter(Party.cara).SetLevel(1);
             }

             Equipment book = EquipmentPool.RequestEquipment("PlainBook");
             Equipment clothArmor = EquipmentPool.RequestEquipment("ClothArmor");

             Party.Singleton.GetFightingCharacter(Party.cara).EquipWeapon(book);
             Party.Singleton.GetFightingCharacter(Party.cara).EquipArmor(clothArmor);
        }
        public override void Initialize()
        {

            cara = Party.Singleton.CurrentMap.GetNPC(Party.cara);
            cara.MoveSpeed = 3;

            Steps = Utility.GetPathTo(
                    cara.TilePosition,
                    new Point(17,12));//Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y - 1));       

            destinationPoint = Utility.GetMapPositionFromDirection(cara.TilePosition, Steps[0]);
            ///destinationPoint = Utility.GetMapPositionFromDirection(cara.TilePosition, Steps2[0]);

            cara.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {

            if (arrivedDestination1 == false)
            {
                arrivedDestination1 = MoveNPC(gameTime);
                return; 
            }
            if (arrivedDestination2 == false)
            {
                cara.IdleOnly = false;

                Steps = Utility.GetPathTo(
                         cara.TilePosition,
                         new Point(20, 12));

                arrivedDestination2 = MoveNPC(gameTime);

                return;
            }
            if (arrivedDestination3 == false)
            {
                cara.IdleOnly = false;

                Steps = Utility.GetPathTo(
                         cara.TilePosition,
                         new Point(Party.Singleton.Leader.TilePosition.X, Party.Singleton.Leader.TilePosition.Y - 2));

                arrivedDestination3 = MoveNPC(gameTime);
            }
            else
            {
                cara.FaceDirection(Direction.South);
                joinDialog.DialogCompleteEvent += CaraJoins;
                ScreenManager.Singleton.AddScreen(new DialogScreen(joinDialog, DialogScreen.Location.TopLeft, "Cara"));
            }

        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
       
        #region Callbacks

        void CaraJoins(object sender, PartyResponseEventArgs e)
        {
            joinDialog.DialogCompleteEvent -= CaraJoins;

            MusicSystem.Pause();

            Complete();
 
            ScreenManager.Singleton.AddScreen(new DialogScreen(partyDialog, DialogScreen.Location.TopLeft, "Cara"));

            MusicSystem.InterruptMusic(AudioCues.JoinedPartySFX);

            state = SceneState.Complete;
        }
        #endregion

        private bool MoveNPC(GameTime gameTime)
        {
            cara.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            if (cara.TilePosition == destinationPoint)
            {
                Steps.RemoveAt(0);

                if (Steps.Count == 0)
                {
                    cara.IdleOnly = true;

                    return true;
                }
                else
                {
                    cara.SetAutoMovement(Steps[0], Party.Singleton.CurrentMap);

                    destinationPoint = Utility.GetMapPositionFromDirection(cara.TilePosition, Steps[0]);
                }
            }

            return false;
        }
    }
}
