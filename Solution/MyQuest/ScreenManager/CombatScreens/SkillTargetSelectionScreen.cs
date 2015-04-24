using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    public class SkillTargetSelectionScreen : Screen
    {
        #region Fields

        bool targetingParty;

        PCFightingCharacter fighter;
        Skill chosenSkill;

        #endregion

        #region Initialization


        public SkillTargetSelectionScreen(PCFightingCharacter fighter, Skill chosenSkill)
        {
            this.fighter = fighter;
            this.chosenSkill = chosenSkill;

            targetingParty = chosenSkill.CanTargetAllies;
        }

        public override void Initialize()
        {
            IsPopup = true;

            if (chosenSkill.CanTargetAllies)
            {
                if (chosenSkill.TargetsAll)
                {
                    for (int i = 0; i < TurnExecutor.Singleton.PCFighters.Count; i++)
                    {
                        if (TurnExecutor.Singleton.PCFighters[i].State != State.Dead)
                        {
                            TurnExecutor.Singleton.PCTargets.Add(i);
                        }
                    }
                }
                else
                {
                    TurnExecutor.Singleton.PCTargets.Add(TurnExecutor.Singleton.PCFighters.IndexOf(fighter));
                }
            }
            else if (chosenSkill.CanTargetEnemy)
            {
                if (chosenSkill.TargetsAll)
                {
                    for (int i = 0; i < TurnExecutor.Singleton.NPCFighters.Count; i++)
                    {
                        if (TurnExecutor.Singleton.NPCFighters[i].State != State.Dead)
                        {
                            TurnExecutor.Singleton.NPCTargets.Add(i);
                        }
                    }
                }
                else
                {
                    TurnExecutor.Singleton.NPCTargets.Add(TurnExecutor.Singleton.GetNextNPC(0, true));
                }
            }
            else // For targeting self.
            {
                TurnExecutor.Singleton.Action = FighterAction.Skill;
                TurnExecutor.Singleton.ActiveSkill = chosenSkill;

                TurnExecutor.Singleton.ActiveSkill.Activate(fighter, fighter);
                ScreenManager.RemoveScreen(this);
            }
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
                TurnExecutor.Singleton.NPCTargets.Clear();
                TurnExecutor.Singleton.PCTargets.Clear();
                ScreenManager.RemoveScreen(this);
            }
            
            if (InputState.IsMenuDown() && chosenSkill.TargetsAll == false)
            {
                TargetDown();
            }
            else if (InputState.IsMenuUp() && chosenSkill.TargetsAll == false)
            {
                TargetUp();
            }
            else if (InputState.IsMenuSelect())
            {
                TurnExecutor.Singleton.Action = FighterAction.Skill;
                TurnExecutor.Singleton.ActiveSkill = chosenSkill;

                List<FightingCharacter> selectedTargets = new List<FightingCharacter>();

                foreach (int i in TurnExecutor.Singleton.PCTargets)
                {
                    selectedTargets.Add(TurnExecutor.Singleton.PCFighters[i]);
                }
                foreach (int i in TurnExecutor.Singleton.NPCTargets)
                {
                    selectedTargets.Add(TurnExecutor.Singleton.NPCFighters[i]);
                }
                if (chosenSkill is Ressurection && selectedTargets[0].State != State.Dead) //So that the player can't select a living target.
                {
                    SoundSystem.Play(AudioCues.menuDeny);
                }
                else
                {
                    TurnExecutor.Singleton.ActiveSkill.Activate(fighter, selectedTargets.ToArray());
                    ScreenManager.RemoveScreen(this);
                    TurnExecutor.Singleton.NPCTargets.Clear();
                    TurnExecutor.Singleton.PCTargets.Clear();
                    TurnExecutor.Singleton.ItemTargets.Clear();
                }
            }
        }

        void TargetUp()
        {
            if (targetingParty)
            {
                if (chosenSkill is Ressurection)
                {
                    TurnExecutor.Singleton.PCTargets[0] = TurnExecutor.Singleton.GetPreviousPC(TurnExecutor.Singleton.PCTargets[0] - 1, false);  //False only if its a reviving spell
                }
                else
                {
                    TurnExecutor.Singleton.PCTargets[0] = TurnExecutor.Singleton.GetPreviousPC(TurnExecutor.Singleton.PCTargets[0] - 1, true);
                }
            }
            else
            {
                TurnExecutor.Singleton.NPCTargets[0] = TurnExecutor.Singleton.GetPreviousNPC(TurnExecutor.Singleton.NPCTargets[0] - 1, true);
            }
        }

        void TargetDown()
        {
            if (targetingParty)
            {
                if (chosenSkill is Ressurection)
                {
                    TurnExecutor.Singleton.PCTargets[0] = TurnExecutor.Singleton.GetNextPC(TurnExecutor.Singleton.PCTargets[0] + 1, false);  //False only if its a reviving spell
                }
                else
                {
                    TurnExecutor.Singleton.PCTargets[0] = TurnExecutor.Singleton.GetNextPC(TurnExecutor.Singleton.PCTargets[0] + 1, true);
                }
            }
            else
            {
                TurnExecutor.Singleton.NPCTargets[0] = TurnExecutor.Singleton.GetNextNPC(TurnExecutor.Singleton.NPCTargets[0] + 1, true);
            }
        }


        #endregion
    }
}
