namespace MyQuest
{
    /// <summary>
    /// Specifies paths to the content within the ContentFolder
    /// </summary>
    public static class ContentPath
    {
        /// <summary>
        /// Provids the path to the Font content folder
        /// </summary>
 //       public const string ToFonts = "Content\\Fonts\\";

        /// <summary>
        /// Provides the path to the Map content Folder
        /// </summary>
        public const string ToMaps = "";

        #region Characters

        /// <summary>
        /// Provides the path to the NPC content folder
        /// </summary>
        public const string ToNPCMapCharacters = "NPCMapCharacters\\";

        /// <summary>
        /// Provides the path to the PartyMember content folder
        /// </summary>
        public const string ToPCMapCharacters = "PCMapCharacters\\";

        public const string ToPCFighters = "PCFightingCharacters\\";

        public const string ToNPCFighters = "NPCFightingCharacters\\";

        public const string ToTestCharacters = "TestCharacters\\";

        #endregion

        #region Items

        /// <summary>
        /// Provides the path to the Items content folder
        /// </summary>
//        public const string ToItems = "Content\\Items\\";


        /// <summary>
        /// Provides the path to the Accessories content folder
        /// </summary>
//        public const string ToAccessories = "Content\\Items\\Accessories\\";


        /// <summary>
        /// Provides the path to the Equipment content folder
        /// </summary>
 //       public const string ToEquipment = "Content\\Items\\Equipment\\";


        #endregion

        #region Texture Paths


        /// <summary>
        /// Provides the path to the Textures content folder
        /// </summary>
        public const string ToTextures = "";


        /// <summary>
        /// Provides the path to the Backgrounds content folder
        /// </summary>
        public const string ToBackgrounds = ToTextures + "Backgrounds\\";


        /// <summary>
        /// Provides the path to the Map textures content folder
        /// </summary>
        public const string ToMapTextures = ToTextures + "Maps\\";


        /// <summary>
        /// Provides the path to the Sprite texture content folder
        /// </summary>
        public const string ToMapCharacterTextures = ToTextures + "Characters\\Map\\";


        public const string ToCombatCharacterTextures = ToTextures + "Characters\\Combat\\";


        /// <summary>
        /// Provides the path to the Character portrait content folder
        /// </summary>
        public const string ToPortraits = ToTextures + "Portraits\\";


        /// <summary>
        /// Provides the path to the item icons content folder
        /// </summary>
        public const string ToItemIcons = ToTextures + "ItemIcons\\";


        /// <summary>
        /// Provides the path to interface related texture content
        /// </summary>
        public const string ToInterface = ToTextures + "Interface\\";


        public const string ToSkillTextures = ToTextures + "SkillAnimations\\";

        public const string ToStatusEffectIcons = ToTextures + "StatusEffectIcons\\";


        #endregion
    }
}
