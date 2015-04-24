using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyQuest
{
    public class CourtYardDoorSceneA : Scene
    {
        Dialog dialog; 

        Dialog lockedDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z993);

        Dialog oneMoreDialog = new Dialog(DialogPrompt.NeedsClose, Strings.Z994);

        public CourtYardDoorSceneA(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Complete()
        {
            Party.Singleton.AddAchievement(CourtYardDoorCutSceneScreen.achievement);
        }

        public override void Initialize()
        {
           //Testing
              //Party.Singleton.GameState.Inventory.AddItem(typeof(Courtyardkey), 2);
           
            if (Party.Singleton.GameState.Inventory.ItemCount(typeof(Courtyardkey)) > 1)
            {
                SoundSystem.Play(AudioCues.ChestOpen);
                Party.Singleton.GameState.Inventory.RemoveItem(typeof(Courtyardkey), 2); 
                Complete();    

                ScreenManager.Singleton.AddScreen(
                    (CutSceneScreen)Utility.CreateInstanceFromName<CutSceneScreen>(this.GetType().Namespace, "CourtYardDoorCutSceneScreen"));

                state = SceneState.Complete;
                return; 
            }

            else if (Party.Singleton.GameState.Inventory.ItemCount(typeof(Courtyardkey)) == 1)
            {
                SoundSystem.Play(AudioCues.menuDeny);
                
                Party.Singleton.Leader.SetAutoMovement(Direction.South, Party.Singleton.CurrentMap);
                Party.Singleton.Leader.FaceDirection(Direction.North);
                Party.Singleton.Leader.Velocity *= .5f;
                
                dialog = oneMoreDialog;
            }

            else
            {
                SoundSystem.Play(AudioCues.menuDeny);
                
                Party.Singleton.Leader.SetAutoMovement(Direction.South, Party.Singleton.CurrentMap);
                Party.Singleton.Leader.FaceDirection(Direction.North);
                Party.Singleton.Leader.Velocity *= .5f;
                
                dialog = lockedDialog;
            }         

            ScreenManager.Singleton.AddScreen(
                new DialogScreen(dialog, DialogScreen.Location.TopLeft, "Nathan"));

        }

        public override void Update(GameTime gameTime)
        {
            Party.Singleton.Leader.Update(gameTime.ElapsedGameTime.TotalMilliseconds, Party.Singleton.CurrentMap);

            Camera.Singleton.CenterOnTarget(
              Party.Singleton.Leader.WorldPosition,
              Party.Singleton.CurrentMap.DimensionsInPixels,
              ScreenManager.Singleton.ScreenResolution);

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
