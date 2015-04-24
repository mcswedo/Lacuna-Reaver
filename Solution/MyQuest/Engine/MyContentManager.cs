using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace MyQuest
{
    public static class MyContentManager
    {
        static ContentManager contentManager;
        static ContentManager mapContentManager;

        public static void Clear()
        {
            //contentManager.Unload();
        }

        public static void Initialize(ContentManager contentManager, ContentManager mapContentManager)
        {
            Debug.Assert(MyContentManager.contentManager == null);
            MyContentManager.contentManager = contentManager;
            MyContentManager.mapContentManager = mapContentManager;
        }

        public static void ClearAllMaps()
        {
            mapContentManager.Unload();
        }

        public static Texture2D LoadTexture(string name)
        {
            return contentManager.Load<Texture2D>("LacunaTextures/" + name);
        }

        public static Texture2D LoadMapTexture(string name)
        {
            return mapContentManager.Load<Texture2D>("LacunaTextures/" + name);
        }

        public static Map LoadMap(string name)
        {
            Map map = mapContentManager.Load<Map>("LacunaMaps/" + name);
            map.LoadContent();
            return map;
        }

        public static PCMapCharacter LoadPCMapCharacter(string name)
        {
            PCMapCharacter pc = mapContentManager.Load<PCMapCharacter>("LacunaCharacters/" + name);
            pc.LoadContent();
            return pc;
        }

        public static NPCMapCharacter LoadNPCMapCharacter(string name)
        {
            NPCMapCharacter npc = mapContentManager.Load<NPCMapCharacter>("LacunaCharacters/" + name);
            npc.LoadContent();
            return npc;
        }

        public static SpriteFont LoadFont(string name)
        {
            SpriteFont font = contentManager.Load<SpriteFont>("LacunaFonts/" + name);
            return font;
        }
    }
}
