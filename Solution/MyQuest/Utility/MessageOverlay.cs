using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MyQuest
{
    /**
     * This class provides a means to display multiple messages to the user.
     * 
     * The class name includes the word "overlay" to suggest that it be drawn last
     * in order to display messages over top of other gui or sceen elements.
     * 
     * Call AddMessage() to display a message.  You can call this any number of times:
     * messages will scroll down as needed.  However, if you call AddMessage too frequently,
     * some messages will be rendered below the botton edge of the screen and therefore
     * not be visible.
     * 
     * You need to call the message overlay's draw method in the draw phase of the game loop.
     */
    public static class MessageOverlay
    {
        private class Message
        {
            private string text;
            private double secondsToLive;

            public Message(string text, double secondsToLive)
            {
                this.text = text;
                this.secondsToLive = secondsToLive;
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
        }

        private const int startScreenX = 100;
        private const int startScreenY = 50;
        private const double defaultSecondsToLive = 5;

        private static List<Message> messageList = new List<Message>();
        private static List<Message> reusableMessageList = new List<Message>();
        private static Vector2 linePosition = new Vector2(startScreenX, startScreenY);

        public static void AddMessage(string text, double ?secondsToLive)
        {
            double life = secondsToLive ?? defaultSecondsToLive;

            Message message = null;
            if (reusableMessageList.Count > 0)
            {
                message = reusableMessageList[0];
                reusableMessageList.RemoveAt(0);
                message.Text = text;
                message.SecondsToLive = life;
            }
            else
            {
                message = new Message(text, life);
            }
            messageList.Add(message);
        }

        public static void Draw(GameTime gameTime)
        {
            if (messageList.Count == 0)
            {
                return;
            }
            foreach (Message message in messageList)
            {
                GameLoop.Instance.AltSpriteBatch.DrawString(Fonts.MenuItem2, message.Text, linePosition, Fonts.MenuItemColor);
                message.SecondsToLive -= gameTime.ElapsedGameTime.TotalSeconds;
                linePosition.Y += Fonts.MenuItem2.LineSpacing;
            }
            for (int i = messageList.Count - 1; i >= 0; --i)
            {
                Message message = messageList[i];
                if (message.SecondsToLive <= 0)
                {
                    reusableMessageList.Add(messageList[i]);
                    messageList.RemoveAt(i);
                }
            }
            linePosition.Y = startScreenY;
        }

    }
}