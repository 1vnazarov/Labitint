using System;

namespace Labitint {
    internal class Program {
        static void Main() {
            Game game = new Game(30, 20);

            while (true) {
                game.Draw();
                game.Move(Console.ReadKey(true).Key);
            }
        }
    }
}
