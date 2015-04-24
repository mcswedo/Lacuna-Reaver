
using System.Diagnostics;
namespace MyQuest
{
    /// <summary>
    /// Defines a combat monster
    /// </summary>
    public class Monster
    {
        #region Monster Pool

        public const string abominableSnowMan = "AbominableSnowMan";
        public const string apprentice = "Apprentice";
        public const string arlan = "Arlan";
        public const string boggimus = "Boggimus";
        public const string boggimusTadpole = "BoggimusTadpole";
        public const string burtle = "Burtle";
        public const string carasBandit = "CarasBandit";
        public const string caveCrab = "CaveCrab";
        public const string caveDemon1 = "CaveDemon1";
        public const string caveDemon2 = "CaveDemon2";
        public const string chepetawa = "Chepetawa";
        public const string demon1 = "Demon1";
        public const string agoraDemon2 = "AgoraDemon2";
        public const string agoraDemon3 = "AgoraDemon3";
        public const string desertBandit = "DesertBandit";
        public const string demonBandit = "DemonBandit";
        public const string elderMantis = "ElderMantis";
        public const string feesh = "Feesh";
        public const string fireFlyder = "FireFlyder";
        public const string fireImp = "FireImp";
        public const string flyder = "Flyder";
        public const string frostFlyder = "FrostFlyder";
        public const string ghost = "Ghost";
        public const string ghost2 = "Ghost2";
        public const string hauntedBook = "HauntedBook";
        public const string poisonousHauntedBook = "PoisonousHauntedBook";
        public const string hauntedChair = "HauntedChair";
        public const string hauntedTree = "HauntedTree";
        public const string SewnBogHauntedTree = "SewnBogHauntedTree";
        public const string iceFeesh = "IceFeesh";
        public const string imp = "Imp";
        public const string keepDemon = "KeepDemon";
        public const string keepImp = "KeepImp";
        public const string malticar = "Malticar";
        public const string finalMalticar = "FinalMalticar";
        public const string negaWill = "NegaWill";
        public const string polarBurtle = "PolarBurtle";
        public const string serlynx = "Serlynx";
        public const string snowBandit = "SnowBandit";
        public const string tippers = "TippersCombat";
        public const string bandit = "Bandit";
        public const string banditKing = "BanditKing";
        public const string bandit2 = "Bandit2";
        public const string tohmey = "Tohmey";
        public const string voodooDoll = "VoodooDoll";
        public const string witchDoctor = "WitchDoctor";
        public const string agoraCaveDemon1 = "AgoraCaveDemon1";
        public const string agoraCaveDemon2 = "AgoraCaveDemon2";
        public const string agoraImp = "AgoraImp";
        public const string agoraCaveCrab = "AgoraCaveCrab";
        public const string agoraFireFlyder = "AgoraFireFlyder";
        public const string agoraGhost = "AgoraGhost";
        public const string agoraElderMantis = "AgoraElderMantis";
        public const string agoraHauntedTree = "AgoraHauntedTree";
        public const string agoraTohmey = "AgoraTohmey";

        #endregion

        string name;

        /// <summary>
        /// The class name of the monster
        /// </summary>
        public string Name
        {
            get { return name; }
        }


        int weight;

        /// <summary>
        /// The weight this monster contributes to the battle screen
        /// </summary>
        public int Weight
        {
            get { return weight; }
        }

        int numberOfOccurrences;


        /// <summary>
        /// The number of this monster that should explicitly appear in combat. 
        /// If ammount is zero, then we will place this enemy in the combat at random.
        /// </summary>
        public int NumberOfOccurrences
        {
            get { return numberOfOccurrences; }
            set { numberOfOccurrences = value; }
        }

        SlotSize size; // = SlotSize.Medium;

        public SlotSize Size
        {
            get { return size; }
        }

        /// <summary>
        /// Constructs a new monster specification
        /// </summary>
        /// <param name="name">The class name of the monster</param>
        /// <param name="weight">The weight this monster contributes to battle</param>
        public Monster(string name, int weight, SlotSize size)
        {
            this.name = name;
            this.weight = weight;
            this.size = size;
            this.numberOfOccurrences = 0;
        }

        /// <summary>
        /// Constructs a new monster with a default SlotSize and amount
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        public Monster(string name, int weight)
        {
            this.name = name;
            this.weight = weight;
            this.size = SlotSize.Medium;
            this.numberOfOccurrences = 0;
        }

        /// <summary>
        /// Constructs a monster with values for all fields
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        /// <param name="size"></param>
        /// <param name="numberOfOccurrences"></param>
        public Monster(string name, int weight, SlotSize size, int numberOfOccurrences)
        {
            Debug.Assert(numberOfOccurrences >= 0);

            this.name = name;
            this.weight = weight;
            this.size = size;
            this.numberOfOccurrences = numberOfOccurrences;
        }

        /// <summary>
        /// Constructs a monster with only a default slot size.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        /// <param name="numberOfOccurences"></param>
        public Monster(string name, int weight, int numberOfOccurences)
        {
            Debug.Assert(numberOfOccurences >= 0);

            this.name = name;
            this.weight = weight;
            this.size = SlotSize.Medium;
            this.numberOfOccurrences = numberOfOccurences;
        }
    }
}
