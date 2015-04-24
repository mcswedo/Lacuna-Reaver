using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Globalization;

/// Needs more work and refactoring

namespace MyQuest
{
    /**
     * The purpose of this class is to keep all the application's
     * sprite fonts in one place.
     * 
     * You need to call this class's Init method when the application 
     * initializes.
     */
    public static class Fonts
    {
        private static SpriteFont menuItem;
        private static SpriteFont menuTitle;
        private static SpriteFont combatMenuItem;
        private static SpriteFont combatStatusEffectMessage;
        private static SpriteFont combatDamageMessage;
        private static SpriteFont combatHealingMessage;
        private static SpriteFont creditsTitle;
        private static SpriteFont creditsItem;
        private static SpriteFont splashScreenText;
        private static SpriteFont combatStatusText;

        private static Color globalFontColor = new Color(180, 180, 180);

        public static void Initialize(ContentManager contentManager)
        {
            Debug.Assert(contentManager != null);

            //if (CultureInfo.GetCultureInfoByIetfLanguageTag("fr") == Strings.Culture ||
            //    CultureInfo.GetCultureInfoByIetfLanguageTag("es") == Strings.Culture ||
            //    CultureInfo.GetCultureInfoByIetfLanguageTag("zh-Hans") == Strings.Culture ||
            //    CultureInfo.GetCultureInfoByIetfLanguageTag("zh-Hant") == Strings.Culture)
            //{
            if (CultureInfo.GetCultureInfoByIetfLanguageTag("zh-Hans") == Strings.Culture ||
                CultureInfo.GetCultureInfoByIetfLanguageTag("zh-Hant") == Strings.Culture)
            {
                menuItem = MyContentManager.LoadFont("Medium");
                menuTitle = MyContentManager.LoadFont("Large");
                combatMenuItem = MyContentManager.LoadFont("Medium");
                combatDamageMessage = MyContentManager.LoadFont("Medium");
                combatHealingMessage = MyContentManager.LoadFont("Medium");
                combatStatusEffectMessage = MyContentManager.LoadFont("Medium");
                creditsTitle = MyContentManager.LoadFont("Huge");
                creditsItem = MyContentManager.LoadFont("Large");
                splashScreenText = MyContentManager.LoadFont("Small");
                combatStatusText = MyContentManager.LoadFont("Small");
                //splashScreenText = MyContentManager.LoadFont("zh-Hant-Small");
            }
            else
            {
                menuItem = MyContentManager.LoadFont("Medium");
                menuTitle = MyContentManager.LoadFont("Large");
                combatMenuItem = MyContentManager.LoadFont("Medium");
                combatDamageMessage = MyContentManager.LoadFont("Medium");
                combatHealingMessage = MyContentManager.LoadFont("Medium");
                combatStatusEffectMessage = MyContentManager.LoadFont("Medium");
                creditsTitle = MyContentManager.LoadFont("Huge");
                creditsItem = MyContentManager.LoadFont("Large");
                splashScreenText = MyContentManager.LoadFont("Small");
                combatStatusText = MyContentManager.LoadFont("Small");
            }
        }

        public static SpriteFont MenuItem2
        {
            get { return menuItem; }
        }

        public static SpriteFont CombatStatus
        {
            get { return combatStatusText; }
        }

        public static Color MenuItemColor
        {
            get { return globalFontColor; }
        }

        public static SpriteFont MenuTitle2
        {
            get { return menuTitle; }
        }

        public static Color MenuTitleColor
        {
            get { return globalFontColor; }
        }

        public static SpriteFont CombatMenuItem2
        {
            get { return combatMenuItem; }
        }

        public static Color CombatMenuItemColor
        {
            get { return globalFontColor; }
        }

        public static SpriteFont CombatStatusEffectMessage
        {
            get { return combatStatusEffectMessage; }
        }

        public static SpriteFont CombatDamageMessage
        {
            get { return combatDamageMessage; }
        }

        public static SpriteFont CombatHealingMessage
        {
            get { return combatHealingMessage; }
        }

        public static SpriteFont CreditsTitle
        {
            get { return creditsTitle; }
        }

        public static Color CreditsTitleColor
        {
            get { return globalFontColor; }
        }

        public static SpriteFont CreditsItem
        {
            get { return creditsItem; }
        }

        public static Color CreditsItemColor
        {
            get { return globalFontColor; }
        }

        public static SpriteFont SplashScreenText
        {
            get { return splashScreenText; }
        }

        public static Color SplashScreenTextColor
        {
            get { return new Color(100, 100, 100); }
        }
    }
}