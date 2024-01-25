using System;

namespace Labitint {
    internal class Program {
        static void Main() {
            Game game = new Game(@"C:/Users/1995-21/desktop/map.txt");

            while (true) {
                game.Draw();
                game.Move(Console.ReadKey(true).Key);
                if (game.CheckWin()) {
                    game.GenerateNewLevel();
                }
            }
        }
    }
}