using System;
using System.Collections.Generic;

namespace MyQuest
{
    public static class SkillPool
    {
        static Dictionary<Type, Skill> skillPool = new Dictionary<Type, Skill>();

        public static Skill RequestByName(string skillName)
        {
            return RequestByType(Type.GetType("MyQuest." + skillName));
        }

        public static Skill RequestByType(Type skillType)
        {
            Skill skill;
              
            try
            {
                skill = skillPool[skillType];
            }
            catch (KeyNotFoundException)
            {
                skill = (Skill)Activator.CreateInstance(skillType);
                skillPool.Add(skillType, skill);
            }

            return skill;
        }
    }
}
