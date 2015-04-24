using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyQuest
{
    class CombatMessage
    {
        private class Message
        {
            private string text;
            private double secondsToLive;
            private Vector2 position;
            private Color messageColor;

            public Message(string text, Vector2 position, Color messageColor, double secondsToLive)
            {
                this.text = text;
                this.secondsToLive = secondsToLive;
                this.position = position;
                this.messageColor = messageColor;
            }

            public Message(string text, Vector2 position, double secondsToLive)
            {
                this.text = text;
                this.secondsToLive = secondsToLive;
                this.position = position;
                this.messageColor = Color.Red;
            }

            public string Text
            {
                get { return text; }
                set { text = value; }
            }

            public double SecondsToLive
            {
                get { return secondsToLive; }
                set { secondsToLive = value; }
            }

            public Vector2 Position
            {
                get { return position; }
                set { position = value; }
            }

            public Color MessageColor
            {
                get { return messageColor; }
                set { messageColor = value; }
            }
        }

        private const double defaultSecondsToLive = 1;

        private static SpriteBatch spriteBatch;
        private static SpriteFont spriteFont;
        private static List<Message> messageList = new List<Message>();

        public static void Init(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            Debug.Assert(spriteBatch != null);
            Debug.Assert(spriteFont != null);
            CombatMessage.spriteBatch = spriteBatch;
            CombatMessage.spriteFont = spriteFont;
        }

        public static void AddMessage(string text, Vector2 position)
        {
            AddMessage(text, position, defaultSecondsToLive);
        }

        public static void AddMessage(string text, Vector2 position, double secondsToLive)
        {
            Message message = null;
            message = new Message(text, position, secondsToLive);

            messageList.Add(message);
        }

        public static void AddMessage(string text, Vector2 position, Color color)
        {
            AddMessage(text, position, color, defaultSecondsToLive);
        }

        public static void AddMessage(string text, Vector2 position, Color color, double secondsToLive)
        {
            Message message = null;
            message = new Message(text, position, color, secondsToLive);

            messageList.Add(message);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (messageList.Count == 0)
            {
                return;
            }

            foreach (Message message in messageList)
            {
                spriteBatch.DrawString(spriteFont, message.Text, message.Position, message.MessageColor);
                message.SecondsToLive -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            for (int i = messageList.Count - 1; i >= 0; --i)
            {
                Message message = messageList[i];
                if (message.SecondsToLive <= 0)
                {
                    messageList.RemoveAt(i);
                }
            }
        }
    }  
}
