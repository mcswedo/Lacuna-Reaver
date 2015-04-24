using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class GazeOfDespair : Skill
    {
        enum PandoraState
        {
            Charging,
            Impact
        }

        static readonly FrameAnimation GazeFrames = new FrameAnimation()
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 200, 200),
                new Rectangle(200, 0, 200, 200),
                new Rectangle(400, 0, 200, 200),
                new Rectangle(600, 0, 200, 200),
                new Rectangle(800, 0, 200, 200),

                new Rectangle(0, 200, 200, 200),
                new Rectangle(200, 200, 200, 200),
                new Rectangle(400, 200, 200, 200),
                new Rectangle(600, 200, 200, 200),
                new Rectangle(800, 200, 200, 200),

                new Rectangle(0, 400, 200, 200),
                new Rectangle(200, 400, 200, 200),
                new Rectangle(400, 400, 200, 200),
                new Rectangle(600, 400, 200, 200),
                new Rectangle(800, 400, 200, 200),

                new Rectangle(0, 600, 200, 200),
                new Rectangle(200, 600, 200, 200),
                new Rectangle(400, 600, 200, 200),
                new Rectangle(600, 600, 200, 200),
                new Rectangle(800, 600, 200, 200),
              
            }
        };

        #region Fields

        CombatAnimation gazeAnimations;

        Vector2 screenPosition;

        PandoraState state;

        #endregion

        #region Constructor


        public GazeOfDespair()
        {
            Name = Strings.ZA400;
            Description = Strings.ZA402; //This effect doesn't work, switch to blindness? 7/15/12

            MpCost = 20;
            SpCost = 4;

            SpellPower = 8.5f;//5.0f;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = false;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-100, -100);
            gazeAnimations = new CombatAnimation()
            {
                Name = "Charging",
                TextureName = "gaze",
                Loop = false,
                Animation = GazeFrames
            };


            gazeAnimations.LoadContent(ContentPath.ToSkillTextures);
        }


        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {
            base.Activate(actor, targets);

            SubtractCost(actor);

            screenPosition = targets[0].ScreenPosition + DrawOffset;

            state = PandoraState.Charging;
            actor.SetAnimation("GazeAttack");
            SoundSystem.Play(AudioCues.GazeOfDespair);
        }



        public override void Update(GameTime gameTime)
        {            
            switch (state)
            {
                case PandoraState.Charging:
                    if (actor.CurrentAnimation.IsRunning == false)
                    {
                        gazeAnimations.Play();
                        actor.SetAnimation("Idle"); 
                        state = PandoraState.Impact;
                    }
                    break;

                case PandoraState.Impact:
                    
                    gazeAnimations.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                    if (gazeAnimations.IsRunning == false)
                    {
                        isRunning = false;
                        if (actor is AgoraElderMantis)
                        {
                            SpellPower = 16.5f;
                        }
                        DealMagicDamage(actor, targets.ToArray());
                    }
                    break;                   
            }
        }
                            
         public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == PandoraState.Impact)
            {
                gazeAnimations.Draw(spriteBatch, screenPosition, SpriteEffects.None);
            }
        }
    }
}
