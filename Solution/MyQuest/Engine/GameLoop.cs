using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;

namespace MyQuest
{
    public class GameLoop : Game 
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)] 
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        public const string textureFolder = "LacunaTextures/";
        public const string characterFolder = "LacunaCharacters/";
        public const string fontsFolder = "LacunaFonts/";
        public const string mapFolder = "LacunaMaps/";
        public const string musicFolder = "LacunaMusic/";
        public const string soundFolder = "LacunaSounds/";

        const int originalResolutionWidth = 1280;
        const int originalResolutionHeight = 720;

        int windowedScreenWidth = 854 - 2; //originalResolutionWidth;   // Maybe read these from a settings file in the gave save folder.
        int windowedScreenHeight = 480 - 1; //originalResolutionHeight;

        public Matrix windowedScalingMatrix = Matrix.Identity;
        float windowedScalingFactor = 1;

        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        ContentManager myContentManager;
        ContentManager temporaryContentManager;

        public ContentManager TemporaryContentManager
        {
            get { return temporaryContentManager; }
        }

        ContentManager permanentContentManager;

        public ContentManager PermanentContentManager
        {
            get { return permanentContentManager; }
        }

        SpriteBatch altSpriteBatch;

        public SpriteBatch AltSpriteBatch
        {
            get { return altSpriteBatch; }
        }

        public static bool ShouldExit
        {
            get;
            set;
        }

        public bool IsFullScreen
        {
            get { return graphics.IsFullScreen; }
        }

        public void ToggleFullScreen()
        {
            if (IsFullScreen)
            {
                SetWindowed();
            }
            else
            {
                SetFullScreen();
            }
        }

        static GameLoop instance;

        public static GameLoop Instance
        {
            get { return instance; }
        }

        public GameLoop()
        {
            Debug.Assert(instance == null);
            instance = this;
            Strings.Culture = CultureInfo.CurrentCulture;
            //Strings.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("fr");
            //Strings.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("es");
            //Strings.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("zh");
            //Strings.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("zh-Hans");
            //Strings.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("zh-Hant");
            graphics = new GraphicsDeviceManager(this); 
            graphics.PreferMultiSampling = false; //solve tearing by setting to false. 
            Window.Title = "Lacuna Reaver";
        }

        void WindowSizeChanged(object sender, EventArgs e)
        {
            if (!IsFullScreen)
            {
                windowedScreenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
                windowedScreenHeight = (int)(windowedScreenWidth * originalResolutionHeight / (float)originalResolutionWidth + 0.5f);
                windowedScalingFactor = windowedScreenWidth / (float)originalResolutionWidth;
                windowedScalingMatrix = Matrix.CreateScale(windowedScalingFactor);
                graphics.PreferredBackBufferWidth = windowedScreenWidth;
                graphics.PreferredBackBufferHeight = windowedScreenHeight;
                graphics.ApplyChanges();
            }
        }

        public Rectangle ScreenDimensions
        {
//            get { return new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height); }
            get { return new Rectangle(0, 0, originalResolutionWidth, originalResolutionHeight); }
        }

        void SetFullScreen()
        {
            graphics.PreferredBackBufferWidth = originalResolutionWidth;
            graphics.PreferredBackBufferHeight = originalResolutionHeight;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        void SetWindowed()
        {
            graphics.PreferredBackBufferWidth = windowedScreenWidth;
            graphics.PreferredBackBufferHeight = windowedScreenHeight;
            graphics.IsFullScreen = false;
            windowedScalingFactor = windowedScreenWidth / (float)originalResolutionWidth;
            windowedScalingMatrix = Matrix.CreateScale(windowedScalingFactor);
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.AllowUserResizing = false;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(WindowSizeChanged);
#if DEBUG
            SetWindowed();
#else
            SetFullScreen();
#endif
            myContentManager = new ContentManager(Services);
            MyContentManager.Initialize(myContentManager, new ContentManager(Services));

            Fonts.Initialize(Content);
            ItemPool.Initialize();
            EquipmentPool.Initialize();
            MusicSystem.Initialize();
            SoundSystem.Initialize();

            temporaryContentManager = new ContentManager(Services);
            permanentContentManager = new ContentManager(Services);

            //screenContentManager = new ContentManager(Services);
            //screenContentManager.RootDirectory = "LacunaTextures/";
            //ScreenManager.Singleton.Initialize(screenContentManager);
            ScreenManager.Singleton.Initialize();
            ScreenManager.Singleton.AddScreen(SplashScreen.Singleton);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            altSpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (ShouldExit)
            {
                Exit();
            }

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }

            //if (Guide.IsVisible)
            //{
            //    return;
            //}

            InputState.Update();
            MusicSystem.Update();
            SoundSystem.Update();
            Camera.Singleton.Update(gameTime);
            ScreenManager.Singleton.Update(gameTime);

            base.Update(gameTime);
        }

        void BeginNormalDraw()
        {
            if (IsFullScreen)
            {
                altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null);
            }
            else
            {
                altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null, null, windowedScalingMatrix);
            }
        }

        internal void BeginTileMapDraw()
        {
            altSpriteBatch.End();
            if (IsFullScreen)
            {
                altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            }
            else
            {
                altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, windowedScalingMatrix);
            }
        }

        internal void RestoreNormalDraw()
        {
            altSpriteBatch.End();
            BeginNormalDraw();
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            BeginNormalDraw();
            ScreenManager.Singleton.Draw(gameTime);
            altSpriteBatch.End();
            base.Draw(gameTime);
        }

        // BlendState needs to stay at NonPremultiplied for white alpha.
        internal void BeginNonPremultipliedBlendStateDraw()
        {
            altSpriteBatch.End();
            if (IsFullScreen)
            {
                altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, null, null);
            }
            else
            {
                altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, null, null, null, windowedScalingMatrix);
            }
        }

        //internal void EndNonPremultipliedBlendStateDraw()
        //{
        //    altSpriteBatch.End();
        //    if (IsFullScreen)
        //    {
        //        altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null);
        //    }
        //    else
        //    {
        //        altSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null, null, windowedScalingMatrix);
        //    }
        //}
    }
}
