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

        public void RenderObject(int x, int y, string text, ConsoleColor color = ConsoleColor.White)
        {
            if (x >= 0 && y >= 0 && x < Console.WindowWidth && y < Console.WindowHeight)
            {
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = color;
                Console.Write(text);
                Console.ResetColor();
            }
        }
    }
}
