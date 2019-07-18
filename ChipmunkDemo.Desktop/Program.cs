using System;

namespace ChipmunkDemo
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Aqui 2");

            using (var game = new ChipmunkDemoGame())
                game.Run();
        }
    }
}
