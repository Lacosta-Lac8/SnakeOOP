using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class
{
    public class ConsoleGameRenderer : IGameRenderer
    {
        public void HideCursor() => Console.CursorVisible = false;
        public void ClearScreen() => Console.Clear();

        public void RenderObject(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        public void RenderScore(int score, int height)
        {
            Console.SetCursorPosition(0, height + 1);
            Console.Write($"Score: {score}");
        }

        public void RenderGameOver(int score)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(10, 20);
            Console.Write("Game Over!");
            Console.SetCursorPosition(10, 21);
            Console.Write($"Final Score: {score}");
            Console.ResetColor();

            Console.CursorVisible = true;
        }

        public void RenderWin(int score)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(10, 20);
            Console.Write("YOU WIN! Field Full!");
            Console.SetCursorPosition(10, 21);
            Console.Write($"Final Score: {score}");
            Console.ResetColor();

            Console.CursorVisible = true;
        }
    }
}
