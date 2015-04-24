using System;
using System.Runtime.InteropServices;
using System.IO;

namespace MyQuest
{
    static class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        static void Main(string[] args)
        {
            using (GameLoop game = new GameLoop())  // Makes sure Dispose() is called on game when Run throws an exception.
            {
#if DEBUG
                game.Run();
#else
                try
                {
                    game.Run();
                }
                catch (Exception e)
                {
                    MessageBox(new IntPtr(0), e.StackTrace, e.Message, 0);
                }
#endif
            }
        }
    }
}

