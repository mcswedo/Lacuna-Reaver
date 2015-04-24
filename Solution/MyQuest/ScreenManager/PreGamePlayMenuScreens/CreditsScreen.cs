using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class CreditsScreen : Screen
    {
        delegate void Contributor();

        List<Texture2D> backgroundList = new List<Texture2D>();
        int currentBackgroundIndex = 0;

        int currentScreen = 0;
        SpriteBatch spriteBatch;
        List<Contributor> contributors = new List<Contributor>();

        double secondsRemaining = 4;

        bool isEndGame;

        public CreditsScreen(bool gameEnded)
        {
            isEndGame = gameEnded;
        }

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.25);

            contributors.Add(Programmers);
            contributors.Add(Artists);
            contributors.Add(Other1);
            contributors.Add(Other2);
            contributors.Add(Other3);
            contributors.Add(CSE440);
            contributors.Add(CSE441);
            contributors.Add(NSF);
            contributors.Add(CSE);
        }

        public override void LoadContent(ContentManager content)
        {
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.castle3BGC));
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.castle4BGC));
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.forestBGC));
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.caveBGC));
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.desertBGC));
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.wastelandBGC));
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.ruinsBGC));
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.lavaBGC));
            backgroundList.Add(content.Load<Texture2D>(backgroundTextureFolder + CombatZonePool.lavaBGC));
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (!isEndGame)
            {
                if (InputState.IsSwitchCharacterRight() || InputState.IsMenuSelect())
                {
                    currentScreen = (currentScreen + 1) % contributors.Count;
                    currentBackgroundIndex = (currentBackgroundIndex + 1) % backgroundList.Count;
                }
                else if (InputState.IsSwitchCharacterLeft())
                {
                    currentScreen = (currentScreen + contributors.Count - 1) % contributors.Count;
                    currentBackgroundIndex = (currentBackgroundIndex + backgroundList.Count - 1) % backgroundList.Count;
                }
                else if (InputState.IsMenuCancel())
                {
                    ExitAfterTransition();
                }
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (isEndGame)
            {
                secondsRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
                if (currentScreen == contributors.Count - 1)
                {
                    if (secondsRemaining <= 0 || InputState.IsMenuCancel())
                    {
                        ExitAfterTransition();
                        ScreenManager.Singleton.AddScreen(new MainMenuScreen());
                    }
                }
                else
                {
                    if (secondsRemaining <= 0 || InputState.IsMenuCancel())
                    {
                        currentScreen++;// (currentScreen + 1) % contributors.Count;
                        currentBackgroundIndex++;// (currentBackgroundIndex + 1) % backgroundList.Count;
                        secondsRemaining = 4;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch = GameLoop.Instance.AltSpriteBatch;
            spriteBatch.Draw(backgroundList[currentBackgroundIndex], Vector2.Zero, Color.White * TransitionAlpha);
            contributors[currentScreen]();
        }

        Vector2 pos;

        private void Init()
        {
            pos = new Vector2(100, 100);
        }

        private void SingleName(String name)
        {
            pos.X = 390;
            spriteBatch.DrawString(Fonts.CreditsItem, name, pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 32;
        }

        private void Programmers()
        {
            Init();
            pos.X = 240;
            pos.Y = 230;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA236, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X += 40;
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "Gerren Willis", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.X -= 40;
            pos.Y += 100;
            spriteBatch.DrawString(Fonts.CreditsTitle, "Lead Programmer", pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X += 40;
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "James Braudaway", pos, Fonts.CreditsItemColor * TransitionAlpha);

            pos.X = 760;
            pos.Y = 230;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA237, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X += 40;
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "Kyle Elder", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "Glen Elder", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "Gerren Willis", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "Matthew Compean", pos, Fonts.CreditsItemColor * TransitionAlpha);
        }

        private void Artists()
        {
            Init();

            pos.X = 290;
            pos.Y = 320;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA238, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X += 10;
            pos.Y += 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "Ryan Melody", pos, Fonts.CreditsItemColor * TransitionAlpha);

            int left = 740;
            int down = 260;
            pos.X = left + 40;
            pos.Y = down;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA239, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X = left - 80;
            pos.Y = down + 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "Gerren Willis", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Matt Douglas", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Celina Lopez", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Robert Rojas", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.X = left + 100;
            pos.Y = down + 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "David Garcia", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Marc Galang", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Derek Cervantez", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Ziduan Liu", pos, Fonts.CreditsItemColor * TransitionAlpha);
        }

        private void Other1()
        {
            Init();

            int left = 485;

            pos.X = left + 30;
            pos.Y = 205;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA240, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X += 10;
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "Jonathan Breitner", pos, Fonts.CreditsItemColor * TransitionAlpha);

            pos.X = left + 290;
            pos.Y += 105;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA241, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X += -3;
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "Glen Elder", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Kyle Elder", pos, Fonts.CreditsItemColor * TransitionAlpha);

            pos.X = left - 160;
            pos.Y += 105;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA242, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X += 90;
            pos.Y += 50;
            spriteBatch.DrawString(Fonts.CreditsItem, "Nikolay Figurin", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Gerren Willis", pos, Fonts.CreditsItemColor * TransitionAlpha);

        }

        private void Other2()
        {
            Init();

            int left = 510;
            int top = 185;
            //int inc = 35;

            pos.X = left - 100;
            pos.Y = top;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA243, pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 60;
            pos.X = left + 25;
            spriteBatch.DrawString(Fonts.CreditsItem, "Michael Swedo", pos, Fonts.CreditsItemColor * TransitionAlpha);

            pos.X = left - 160;
            pos.Y += 99;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA700, pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 60;
            pos.X = left + 47;
            spriteBatch.DrawString(Fonts.CreditsItem, "Ryan Davis", pos, Fonts.CreditsItemColor * TransitionAlpha);

            pos.X = left - 20;
            pos.Y += 99;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA244, pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.X = left + 46;
            pos.Y += 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "Collin Wymer", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.X = left + 25;
            pos.Y += 58;
            spriteBatch.DrawString(Fonts.CreditsItem, "Thomas Diendorf", pos, Fonts.CreditsItemColor * TransitionAlpha);


            /*
            
            
            Init();

            int left = 510;
            int top = 245;

            pos.X = left - 240;
            pos.Y = top;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA243, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.Y += 60;
            pos.X += 30;
            spriteBatch.DrawString(Fonts.CreditsItem, "Michael Swedo", pos, Fonts.CreditsItemColor * TransitionAlpha);

            pos.X = left + 240;
            pos.Y = top;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA244, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.Y += 60;
            pos.X += 65;
            spriteBatch.DrawString(Fonts.CreditsItem, "Collin Wymer", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            pos.X += -25;
            spriteBatch.DrawString(Fonts.CreditsItem, "Thomas Diendorf", pos, Fonts.CreditsItemColor * TransitionAlpha);

            pos.X = left - 10;
            pos.Y = top + 177;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA245, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X = left + 38;
            pos.Y += 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "David Turner", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.X = left + 5;
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Arturo Concepcion", pos, Fonts.CreditsItemColor * TransitionAlpha);
    */
        }

        private void Other3()
        {
            Init();

            int left = 510;
            int top = 235;
            int inc = 35;

            pos.X = left + 20;
            pos.Y = top;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA246, pos, Color.Black * TransitionAlpha);
            pos.Y += 60;
            pos.X = left + 0;
            spriteBatch.DrawString(Fonts.CreditsItem, "Mac Chou (French)", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            pos.X -= 30;
            spriteBatch.DrawString(Fonts.CreditsItem, "Cristhal Poron (Spanish)", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            pos.X -= 20;
            spriteBatch.DrawString(Fonts.CreditsItem, "Hsiang-Po Chuang (Chinese)", pos, Color.Black * TransitionAlpha);

            pos.X = left - 10;
            pos.Y += 99;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA245, pos, Color.Black * TransitionAlpha);
            pos.X = left + 41;
            pos.Y += 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "David Turner", pos, Color.Black * TransitionAlpha);
            pos.X = left + 5;
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Arturo Concepcion", pos, Color.Black * TransitionAlpha);
        }

        private void CSE440()
        {
            Init();

            int left = 500;
            int top = 220;
            int inc = 35;

            pos.X = left + 0;
            pos.Y = top;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA247, pos, Color.Black * TransitionAlpha);

            pos.X = left - 80;
            pos.Y = top + 55;
            spriteBatch.DrawString(Fonts.CreditsItem, "Jared Bruhn", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Ryan Davis", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Nik Figurin", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Kyle Johnson", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Ashley Koyama", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Gabriel Matthews", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Josue Mendoza", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Reggie Norman", pos, Color.Black * TransitionAlpha);

            pos.X = left + 200;
            pos.Y = top + 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "Jack Price", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Christian Serna-lua", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Robert Reynolds", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Christian Venegas", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Adam Weschler", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Collin Wymer", pos, Color.Black * TransitionAlpha);
            pos.Y += inc;
            spriteBatch.DrawString(Fonts.CreditsItem, "Theo Phillips", pos, Color.Black * TransitionAlpha);
        }

        private void CSE441()
        {
            Init();

            int left = 460;
            int top = 260;

            pos.Y = top;
            pos.X = left;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA248, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X -= 90;
            pos.Y += 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "Taylor Baldwin", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "James Braudaway", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Jonathan Breitner", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Chad Easton", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Glen Elder", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Kyle Elder", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Celina Lopez", pos, Fonts.CreditsItemColor * TransitionAlpha);

            pos.X += 300;
            pos.Y = top + 60;
            spriteBatch.DrawString(Fonts.CreditsItem, "Ryan Melody", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Donovan Neighbor", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Josh Richardson", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Luke Simpson", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "David Sturgeon", pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "Takenori Tsuruga", pos, Fonts.CreditsItemColor * TransitionAlpha);
        }

        private void NSF()
        {
            Init();
            pos.Y = 310;
            pos.X = 490;
            spriteBatch.DrawString(Fonts.CreditsTitle, Strings.ZA249, pos, Fonts.CreditsTitleColor * TransitionAlpha);
            pos.X += -40;
            pos.Y += 60;
            spriteBatch.DrawString(Fonts.CreditsItem, Strings.ZA250, pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 32;
            spriteBatch.DrawString(Fonts.CreditsItem, Strings.ZA251, pos, Fonts.CreditsItemColor * TransitionAlpha);
            pos.Y += 32;
            spriteBatch.DrawString(Fonts.CreditsItem, Strings.ZA252, pos, Fonts.CreditsItemColor * TransitionAlpha);
            //SingleName(Strings.ZA250);
            //SingleName(Strings.ZA251);
            //SingleName(Strings.ZA252);
            //pos.Y += 22;
            //SingleName("The School of Computer Science and Engineering at");
            //pos.Y += 0;
            //SingleName("California State University of San Bernardino");
            //SingleName(Strings.ZA253);
        }

        private void CSE()
        {
            Init();
            pos.Y = 340;
            pos.X = 320;
            spriteBatch.DrawString(Fonts.CreditsItem, "The School of Computer Science and Engineering at", pos, Fonts.CreditsItemColor * TransitionAlpha);
            //SingleName("The School of Computer Science and Engineering at");
            pos.Y += 38;
            pos.X += 40;
            spriteBatch.DrawString(Fonts.CreditsItem, "California State University of San Bernardino", pos, Fonts.CreditsItemColor * TransitionAlpha);
            //SingleName("California State University of San Bernardino");
        }
    }
}
