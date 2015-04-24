using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    enum IceStormState
    {
        Charging,
        Traveling,
        Impact
    }

    class IceStorm : Skill
    {

        #region Frame Animations


        static readonly FrameAnimation ChargingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 102, 102),
                new Rectangle(102, 0, 102, 102),
                new Rectangle(204, 0, 102, 102),
                new Rectangle(306, 0, 102, 102),
                new Rectangle(408, 0, 102, 102),
                new Rectangle(510, 0, 102, 102)
            }
        };

        static readonly FrameAnimation TravelingFrames = new FrameAnimation()
        {
            FrameDelay = 0.09,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 102, 102, 102),
                new Rectangle(102, 102, 102, 102),
                new Rectangle(204, 102, 102, 102),
                new Rectangle(306, 102, 102, 102),
                new Rectangle(408, 102, 102, 102),
                new Rectangle(510, 102, 102, 102)
            }
        };

        static readonly FrameAnimation ImpactFrames = new FrameAnimation()
        {
            FrameDelay = 0.07, //.09
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 250, 300),
                new Rectangle(250, 0, 250, 300),
                new Rectangle(500, 0, 250, 300),
                new Rectangle(750, 0, 250, 300),
                new Rectangle(1000, 0, 250, 300),
                new Rectangle(1250, 0, 250, 300),
                new Rectangle(1500, 0, 250, 300),
                new Rectangle(1750, 0, 250, 300),
                new Rectangle(0, 300, 250, 300),
                new Rectangle(250, 300, 250, 300),
                new Rectangle(500, 300, 250, 300),
                new Rectangle(750, 300, 250, 300),
                new Rectangle(1000, 300, 250, 300),
                new Rectangle(1250, 300, 250, 300),
                new Rectangle(1500, 300, 250, 300),
                new Rectangle(1750, 300, 250, 300),
                new Rectangle(0, 600, 250, 300),
                new Rectangle(250, 600, 250, 300)
            }
        };


        #endregion

        #region Fields

        CombatAnimation iceStormAnimation;

        Vector2 destinationPosition;

        StrikeState state;
        int targetsHit;

        #endregion

        #region Constructor

        public IceStorm()
        {
            Name = Strings.ZA413;
            Description = Strings.ZA414;

            MpCost = 225;
            SpCost = 9;

            SpellPower = Cara.Instance.FighterStats.Level;
            DamageModifierValue = 0f;

            BattleSkill = true;
            MapSkill = false;
            HealingSkill = false;
            MagicSkill = true;
            IsBasicAttack = false;

            TargetsAll = true;
            CanTargetAllies = false;
            CanTargetEnemy = true;

            DrawOffset = new Vector2(-125, -225);
        
            iceStormAnimation = new CombatAnimation()
            {
                Name = "Impact",
                TextureName = "cara_ice_storm",
                Loop = false,
                Animation = ImpactFrames
            };
   
            iceStormAnimation.LoadContent(ContentPath.ToSkillTextures);
        }

        #endregion

        public override void Activate(FightingCharacter actor, params FightingCharacter[] targets)
        {                                      
            base.Activate(actor, targets);
            
            SubtractCost(actor);

            destinationPosition = targets[0].HitLocation + DrawOffset;

            targetsHit = 0;

            state = StrikeState.Traveling;
            iceStormAnimation.Play();
            if (actor.Name == "Cara")
            {
                actor.SetAnimation("AnalyzeAttack");
            }
            SoundSystem.Play(AudioCues.IceStorm);
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case StrikeState.Traveling:
                    iceStormAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (iceStormAnimation.IsRunning == false)
                    {
                        state = StrikeState.Striking;
                    }
                    break;

                case StrikeState.Striking:
                    targetsHit++;
                    if (targetsHit < targets.Count)
                    {
                        destinationPosition = targets[targetsHit].HitLocation + DrawOffset;
                        iceStormAnimation.Play();
                        SoundSystem.Play(AudioCues.IceStorm);
                        state = StrikeState.Traveling;
                    }

                    else
                    {
                        //foreach (FightingCharacter target in targets)
                        //{
                           DealMagicDamage(actor, targets.ToArray());
                        //}
                        if (actor.Name == "Cara")
                        {
                            actor.SetAnimation("Idle");
                        }
                        isRunning = false;
                    }

                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == StrikeState.Traveling)
            {
                iceStormAnimation.Draw(spriteBatch, destinationPosition, SpriteEffects.None);
            }
        }
    }
}
