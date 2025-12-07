using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class
{
    public class Snake : IGameObject
    {
        public List<(int x, int y)> Body { get; private set; } = new List<(int x, int y)>();
        public int x => Body.First().x;
        public int y => Body.First().y;
        public char symbol => 'o';

        public int Dx { get; private set; } = 1;
        public int Dy { get; private set; } = 0;
        private ConsoleKey lastMove = ConsoleKey.RightArrow;

        public Snake(int startx, int starty)
        {
            Body.Add((startx, starty));
        }

        public void ChangeDirection(ConsoleKeyInfo? keyInfo)
        {
            if (!keyInfo.HasValue) return;

            var newKey = keyInfo.Value.Key;

            if ((newKey == ConsoleKey.LeftArrow && lastMove == ConsoleKey.RightArrow) ||
                (newKey == ConsoleKey.RightArrow && lastMove == ConsoleKey.LeftArrow) ||
                (newKey == ConsoleKey.UpArrow && lastMove == ConsoleKey.DownArrow) ||
                (newKey == ConsoleKey.DownArrow && lastMove == ConsoleKey.UpArrow))
            {
                return;
            }

            switch (newKey)
            {
                case ConsoleKey.UpArrow: Dx = 0; Dy = -1; lastMove = newKey; break;
                case ConsoleKey.DownArrow: Dx = 0; Dy = 1; lastMove = newKey; break;
                case ConsoleKey.LeftArrow: Dx = -1; Dy = 0; lastMove = newKey; break;
                case ConsoleKey.RightArrow: Dx = 1; Dy = 0; lastMove = newKey; break;
            }
        }

        public void Move(bool grow)
        {
            var newHead = (x: x + Dx, y: y + Dy);

            Body.Insert(0, newHead);

            if (!grow)
            {
                Body.RemoveAt(Body.Count - 1);
            }
        }

        public bool CheckCollision(int width, int height)
        {
            if (x <= 0 || x >= width - 1 || y <= 0 || y >= height - 1)
                return true;

            if (Body.Skip(1).Any(b => b.x == x && b.y == y))
                return true;
            

            return false;
        }

        public bool WillCollideWithFood(Food food)
        {
            int nextx = x + Dx;
            int nexty = y + Dy;
            return nextx == food.x && nexty == food.y;
        }
    }
}
