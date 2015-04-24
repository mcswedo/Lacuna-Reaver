using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class ItemTargetSelectionScreen : Screen
    {
        #region Fields

        PCFightingCharacter fighter;
        ConsumableItem chosenItem;
        int target;
        int numberOfPCFighters;

        #endregion

        #region Initialization

        public ItemTargetSelectionScreen(PCFightingCharacter fighter, ConsumableItem chosenItem)
        {
            this.fighter = fighter;
            this.chosenItem = chosenItem;
            numberOfPCFighters = TurnExecutor.Singleton.PCFighters.Count;
            target = (TurnExecutor.Singleton.PCFighters.IndexOf(fighter));
        }

        public override void Initialize()
        {
            IsPopup = true;

            TurnExecutor.Singleton.ItemTargets.Add(TurnExecutor.Singleton.PCFighters.IndexOf(fighter));
        }

        public override void LoadContent(ContentManager content)
        {
        }


        #endregion

        #region Handle Input


        public override void HandleInput(GameTime gameTime)
        {
            if (InputState.IsMenuCancel())
            {
                TurnExecutor.Singleton.ItemTargets.Clear();
                ScreenManager.RemoveScreen(this);
            }

            if (InputState.IsMenuDown())
            {
                TargetDown();
                target = TurnExecutor.Singleton.GetNextPC(target + 1, true);
            }
            else if (InputState.IsMenuUp())
            {
                TargetUp();
                target = TurnExecutor.Singleton.GetPreviousPC(target - 1, true);
            }
            else if (InputState.IsMenuSelect())
            {
                Party.Singleton.GameState.Fighters[target].ConsumeItem(chosenItem);
                

                TurnExecutor.Singleton.Action = FighterAction.Item;
                ScreenManager.RemoveScreen(this);
                TurnExecutor.Singleton.ItemTargets.Clear();
            }
        }

        void TargetDown()
        {
            TurnExecutor.Singleton.ItemTargets[0] = TurnExecutor.Singleton.GetNextPC(TurnExecutor.Singleton.ItemTargets[0] + 1, true); //Change this to false to target dead members.
        }

        void TargetUp()
        {
            TurnExecutor.Singleton.ItemTargets[0] = TurnExecutor.Singleton.GetPreviousPC(TurnExecutor.Singleton.ItemTargets[0] - 1, true);
        }
        #endregion
    }
}
