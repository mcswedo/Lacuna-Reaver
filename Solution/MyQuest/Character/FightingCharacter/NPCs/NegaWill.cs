using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    class NegaWill : NPCFightingCharacter
    {
        static readonly Vector2 commonDrawOffset = new Vector2(-168 + 110, -95);

        static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.075,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 250, 200),
                new Rectangle(250, 0, 250, 200),
                new Rectangle(500, 0, 250, 200),
                new Rectangle(750, 0, 250, 200),
                new Rectangle(1000, 0, 250, 200),
                new Rectangle(1250, 0, 250, 200),
                new Rectangle(1500, 0, 250, 200),
                new Rectangle(1750, 0, 250, 200),
                new Rectangle(0, 200, 250, 200),
                new Rectangle(250, 200, 250, 200),
                new Rectangle(500, 200, 250, 200),
                new Rectangle(750, 200, 250, 200),
                new Rectangle(1000, 200, 250, 200),
                new Rectangle(1250, 200, 250, 200),
                new Rectangle(1500, 200, 250, 200),
                new Rectangle(1750, 200, 250, 200),
                new Rectangle(0, 400, 250, 200),
                new Rectangle(250, 400, 250, 200),
                new Rectangle(500, 400, 250, 200),
                new Rectangle(750, 400, 250, 200)
            }
        };

        static readonly FrameAnimation ScytheWarpFrames = new FrameAnimation
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 251, 199),
                new Rectangle(251, 0, 251, 199),
                new Rectangle(502, 0, 251, 199),
                new Rectangle(753, 0, 251, 199),

                new Rectangle(0, 199, 251, 199),
                new Rectangle(251, 199, 251, 199),
                new Rectangle(502, 199, 251, 199),
                new Rectangle(753, 199, 251, 199),

                new Rectangle(0, 398, 251, 199),
                new Rectangle(251, 398, 251, 199),
                new Rectangle(502, 398, 251, 199),
            }
        };

        static readonly FrameAnimation ScytheWarpBackFrames = new FrameAnimation
        {
            FrameDelay = 0.05,
            Frames = new List<Rectangle>
            {
                new Rectangle(502, 398, 251, 199),
                new Rectangle(251, 398, 251, 199),
                new Rectangle(0, 398, 251, 199),

                new Rectangle(753, 199, 251, 199),
                new Rectangle(502, 199, 251, 199),
                new Rectangle(251, 199, 251, 199),
                new Rectangle(0, 199, 251, 199),

                new Rectangle(753, 0, 251, 199),
                new Rectangle(502, 0, 251, 199),
                new Rectangle(251, 0, 251, 199),
                new Rectangle(0, 0, 251, 199),
            }
        };

        static readonly FrameAnimation IdleNoScytheFrames = new FrameAnimation
        {
            FrameDelay = 0.75,
            Frames = new List<Rectangle>
            {
                new Rectangle(502, 398, 251, 199),
            }
        };
        
        static readonly FrameAnimation ShadowBlastFrames = new FrameAnimation()
        {
            FrameDelay = 0.085,
            Frames = new List<Rectangle>()
            {
                new Rectangle(0, 0, 300, 200),
                new Rectangle(300, 0, 300, 200),
                new Rectangle(600, 0, 300, 200),
                new Rectangle(900, 0, 300, 200),
                new Rectangle(1200, 0, 300, 200),
                new Rectangle(1500, 0, 300, 200),
                new Rectangle(0, 200, 300, 200),
                new Rectangle(300, 200, 300, 200),
                new Rectangle(600, 200, 300, 200),
                new Rectangle(900, 200, 300, 200),
                new Rectangle(1200, 200, 300, 200),
                new Rectangle(1500, 200, 300, 200),
                new Rectangle(0, 400, 300, 200),
                new Rectangle(300, 400, 300, 200),
                new Rectangle(600, 400, 300, 200),
                new Rectangle(900, 400, 300, 200),
                new Rectangle(1200, 400, 300, 200),
                new Rectangle(1500, 400, 300, 200),
                new Rectangle(0, 600, 300, 200),
                new Rectangle(300, 600, 300, 200),
                new Rectangle(600, 600, 300, 200),
                new Rectangle(900, 600, 300, 200),
                new Rectangle(1200, 600, 300, 200),
                new Rectangle(1500, 600, 300, 200),
                new Rectangle(0, 800, 300, 200),
                new Rectangle(300, 800, 300, 200),
                new Rectangle(600, 800, 300, 200),
                new Rectangle(900, 800, 300, 200),
                new Rectangle(1200, 800, 300, 200),
                new Rectangle(1500, 800, 300, 200),
                new Rectangle(0, 1000, 300, 200),
                new Rectangle(300, 1000, 300, 200),
                new Rectangle(600, 1000, 300, 200),
                new Rectangle(900, 1000, 300, 200),
                new Rectangle(1200, 1000, 300, 200),
                new Rectangle(1500, 1000, 300, 200),
                new Rectangle(0, 1200, 300, 200),
                new Rectangle(300, 1200, 300, 200),
                new Rectangle(600, 1200, 300, 200),
                new Rectangle(900, 1200, 300, 200),
                new Rectangle(0, 1400, 315, 200),
                new Rectangle(315, 1400, 315, 200),
                new Rectangle(630, 1400, 315, 200),
                new Rectangle(945, 1400, 315, 200)
            }
        };

        public NegaWill()
        {
            Name = "Nega Will";
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BaseAttackName = "WillAttack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.MaxHit;
            OnDeathSoundCue = AudioCues.MaxDeath;
            BehaviorType = AIType.Knowledgeable;

            FighterStats = new FighterStats
            {
                BaseMaxHealth = Will.Instance.FighterStats.BaseMaxHealth + 2000,
                Health = Will.Instance.FighterStats.BaseMaxHealth + 2000,
                BaseMaxEnergy = 999,
                Energy = 999,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = Will.Instance.FighterStats.BaseStrength,
                BaseDefense = Will.Instance.FighterStats.BaseDefense,
                BaseIntelligence = Will.Instance.FighterStats.BaseIntelligence,
                BaseWillpower = Will.Instance.FighterStats.BaseWillpower,
                BaseAgility = Will.Instance.FighterStats.BaseAgility,
                Level = Will.Instance.FighterStats.Level + 1,
                Experience = 500,
                Gold = 225,
            };

            damageMessageOffset = new Vector2(0, -80);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-100, 87);

            CombatAnimation IdleAnimation = new CombatAnimation()
            {
            Name = "Idle",
            TextureName = "will_combat_idle"/*"nega_will_combat_idle"*/,
            Animation = IdleFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
            };

            CombatAnimation ScytheWarpAnimation = new CombatAnimation()
            {
            Name = "ScytheWarp",
            TextureName = "will_attack"/*"nega_will_attack"*/,
            Animation = ScytheWarpFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
            };

            CombatAnimation ScytheWarpBackAnimation = new CombatAnimation()
            {
            Name = "ScytheWarpBack",
            TextureName = "will_attack"/*"nega_will_attack"*/,
            Animation = ScytheWarpBackFrames,
            Loop = false,
            DrawOffset = commonDrawOffset
            };

            CombatAnimation IdleNoScytheAnimation = new CombatAnimation()
            {
            Name = "IdleNoScythe",
            TextureName = "will_attack"/*"nega_will_attack"*/,
            Animation = IdleNoScytheFrames,
            Loop = true,
            DrawOffset = commonDrawOffset
            };

            CombatAnimation ShadowBlastAnimation = new CombatAnimation()
            {
                Name = "ShadowBlast",
                TextureName = "will_shadow_blast",
                Animation = ShadowBlastFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            ItemsDropped = new List<string>()
            {
                "LargeEnergyPotion",
                "LargeHealthPotion"
            };

            SkillNames = new List<string>()
            {
                "WillAttack",
                "Disintegrate"
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(ScytheWarpAnimation);
            AddCombatAnimation(ScytheWarpBackAnimation);
            AddCombatAnimation(IdleNoScytheAnimation);
            AddCombatAnimation(ShadowBlastAnimation);

            spriteEffects = SpriteEffects.FlipHorizontally;
        }
    }
}
