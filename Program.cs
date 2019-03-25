using System;

namespace FlappyXna
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new FlappyGame())
                game.Run();
        }
    }
}
