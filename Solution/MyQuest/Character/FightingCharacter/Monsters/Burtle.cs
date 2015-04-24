using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Burtle : NPCFightingCharacter
    {
        #region Static FrameAnimations


        protected static readonly FrameAnimation IdleFrames = new FrameAnimation
        {
            FrameDelay = 0.075,
            Frames = new List<Rectangle>
            {
                new Rectangle(0, 0, 100, 142),
                new Rectangle(100, 0, 100, 142),
                new Rectangle(200, 0, 100, 142),
                new Rectangle(300, 0, 100, 142),
                new Rectangle(400, 0, 100, 142),
                new Rectangle(500, 0, 100, 142),
                new Rectangle(600, 0, 100, 142),
                new Rectangle(700, 0, 100, 142),
                new Rectangle(800, 0, 100, 142),
                new Rectangle(900, 0, 100, 142)
            }
        };


        #endregion

        #region Constructor


        public Burtle()
        {
            Name = "Burtle";
            PortraitName = "generic_man_portrait";

            Stats = new FighterStats
            {
                Health = 50,
                MaxHealth = 50,
                Energy = 25,
                MaxEnergy = 25,
                Strength = 15,
                Defense = 15,
                Intelligence = 7,
                Willpower = 7,
                Agility = 5,
                Level = 4,
                Experience = 200,
            };

            IdleAnimation = new SpriteAnimation
            {
                Name = "Idle",
                SpriteSheetName = "TestCombatAnimation",
                Animations = new Dictionary<string, FrameAnimation>
                {
                    { "Idle", IdleFrames }
                }
            };

            DefendingAnimation = new SpriteAnimation
            {
                Name = "Defending",
                SpriteSheetName = "TestCombatAnimationDefend",
                Animations = new Dictionary<string, FrameAnimation>
                {
                    { "Defending", IdleFrames }
                }
            };
            
            string nameSpace = this.GetType().Namespace;
            Skills = new List<Skill>
            {
                Utility.CreateInstanceFromName<Skill>(nameSpace, "FireBall")
            };

            SkillAnimations = new SerializableDictionary<Skill, SpriteAnimation>();
        }


        #endregion
    }
}
