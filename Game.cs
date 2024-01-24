using System;

namespace Labitint {
    public class Game {
        private const string BrickSymbol = "■";
        private const string PlayerSymbol = "@";
        private const string ScoreSymbol = "$";
        private string[,] map;
        private int playerX;
        private int playerY;
        private int score;

        Random rand = new Random();
        public Game(int width, int height) {
            map = new string[height, width];
            GenerateMap();
            PlacePlayer();
        }

        public Game(string[,] customMap) {
            map = customMap;
            PlacePlayer();
        }

        private void GenerateMap() {
            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    map[i, j] = (i == 0 || i == map.GetLength(0) - 1 || j == 0 || j == map.GetLength(1) - 1) ? BrickSymbol : " ";
                }
            }
            PlaceObjects(ScoreSymbol, rand.Next(5, 10));
            PlaceObjects(BrickSymbol, rand.Next(5));
        }

        private void PlaceObjects(string symbol, int count) {
            for (int i = 0; i < count; i++) {
                int x, y;
                do {
                    x = rand.Next(1, map.GetLength(0) - 1);
                    y = rand.Next(1, map.GetLength(1) - 1);
                } while (map[x, y] != " ");

                map[x, y] = symbol;
            }
        }

        private void PlacePlayer() {
            int x, y;
            do {
                x = rand.Next(1, map.GetLength(0) - 1);
                y = rand.Next(1, map.GetLength(1) - 1);
            } while (map[x, y] != " ");

            map[x, y] = PlayerSymbol;
            playerX = x;
            playerY = y;
        }

        public void Move(ConsoleKey direction) {
            int newX = playerX;
            int newY = playerY;

            switch (direction) {
                case ConsoleKey.UpArrow:
                    newX--;
                    break;
                case ConsoleKey.DownArrow:
                    newX++;
                    break;
                case ConsoleKey.LeftArrow:
                    newY--;
                    break;
                case ConsoleKey.RightArrow:
                    newY++;
                    break;
            }

            if (map[newX, newY] == BrickSymbol)
                return;

            if (map[newX, newY] == ScoreSymbol)
                score++;

            map[playerX, playerY] = " ";
            map[newX, newY] = PlayerSymbol;

            playerX = newX;
            playerY = newY;
        }

        public void Draw() {
            Console.Clear();
            Console.CursorVisible = false;

            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    switch (map[i, j]) {
                        case BrickSymbol:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case PlayerSymbol:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case ScoreSymbol:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case " ":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                    }
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score: {score}");
        }
    }
}