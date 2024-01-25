using System;
using System.IO;

namespace Labitint {
    public class Game {
        private const string BrickSymbol = "█";
        private const string PlayerSymbol = "@";
        private const string ScoreSymbol = "$";
        private string[,] map;
        private int playerX;
        private int playerY;
        private int score;
        private int level;
        Random rand = new Random();
        public Game(int width, int height) {
            map = new string[height, width];
            GenerateNewLevel();
        }

        public Game(string fileName) {
            GenerateNewLevel(fileName);
        }

        private void GenerateMap() {
            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    map[i, j] = (i == 0 || i == map.GetLength(0) - 1 || j == 0 || j == map.GetLength(1) - 1) ? BrickSymbol : " ";
                }
            }
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
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Level: {level}");
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
        public bool CheckWin() {
            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    if (map[i, j] == ScoreSymbol) {
                        return false;
                    }
                }
            }
            return true;
        }
        public void GenerateNewLevel(string fileName = null) {
            level++;
            if (fileName == null)
            {
                GenerateMap();
                PlacePlayer();
                PlaceObjects(ScoreSymbol, rand.Next(5, 10));
                PlaceObjects(BrickSymbol, rand.Next(5));
            }
            else LoadMap(fileName);
        }

        public void LoadMap(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            map = new string[lines.Length, lines[0].Split(' ').Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var tmp = lines[i].Split(' ');
                for (int j = 0; j < tmp.Length; j++)
                {
                    switch (Convert.ToInt32(tmp[j]))
                    {
                        case 0:
                            map[i, j] = PlayerSymbol;
                            break;
                        case 1:
                            map[i, j] = BrickSymbol;
                            break;
                        case 2:
                            map[i, j] = ScoreSymbol;
                            break;
                    }
                }
            }
        }
    }
}