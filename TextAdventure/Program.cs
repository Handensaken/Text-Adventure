    using System;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager manager = new GameManager();
            manager.MainGameLoop();
        }
    }
}
