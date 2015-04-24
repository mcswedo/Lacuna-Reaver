using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    class AgoraCaveDemon1 : CaveDemon1
    {
        #region Constructor

        public AgoraCaveDemon1()
        {
            Name = "Wraith";
            /*
            PortraitName = "generic_man_portrait";
            IconName = "GenericIcon";
            BehaviorType = AIType.Knowledgeable;
            baseAttackName = "Demon1Attack";
            HitNoiseSoundCue = AudioCues.Flesh;
            OnHitSoundCue = AudioCues.Demon1Hit;
            OnDeathSoundCue = AudioCues.Demon1Death;*/

            FighterStats = new FighterStats
            {
                BaseMaxHealth = 18000,
                Health = 18000,
                BaseMaxEnergy = 5000,
                Energy = 5000,
                BaseMaxStamina = 10,
                Stamina = 10,
                BaseStrength = 175,
                BaseDefense = 217,
                BaseIntelligence = 167,
                BaseWillpower = 147,
                BaseAgility = 167,
                Level = 23,
                Experience = 2700,
                Gold = 96,
            };

            //offsets
            /*damageMessageOffset = new Vector2(0, -100);
            defenseShieldOffset = new Vector2(0, 0);
            statusEffectMessageOffset = new Vector2(-125, -50);
            pointerOffset = new Vector2(-103, 100);

            CombatAnimation IdleAnimation = new CombatAnimation
            {
                Name = "Idle",
                TextureName = "demon1_idle",
                Animation = IdleFrames,
                Loop = true,
                DrawOffset = commonDrawOffset
            };
            CombatAnimation DashAnimation = new CombatAnimation()
            {
                Name = "Dash",
                TextureName = "demon1_idle",
                Animation = DashFrames,
                Loop = false,
                DrawOffset = commonDrawOffset
            };

            CombatAnimation AttackAnimation = new CombatAnimation()
            {
                Name = "Attack",
                TextureName = "demon_basic_attack",
                Animation = AttackFrames,
                Loop = false,
                DrawOffset = commonDrawOffset + new Vector2(-100, -10)
            };

            ItemsDropped = new List<string>()
            {
                "SkirmishRing",
                "SpikedChain"
            };

            SkillNames = new List<string>()
            {
                "Demon1Attack",
                "Siphon"
//                "Demon1ShadowStrike",
            };

            AddCombatAnimation(IdleAnimation);
            AddCombatAnimation(DashAnimation);
            AddCombatAnimation(AttackAnimation);*/
        }

        #endregion
    }
}
