using System;

namespace Gabiac
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Gabiac())
                game.Run();
        }
    }
}
