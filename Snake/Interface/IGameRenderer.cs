using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Interface
{
    public interface IGameRenderer
    {
        void HideCursor();
        void ClearScreen();
        void RenderObject(int x, int y, string textd, ConsoleColor color = ConsoleColor.White);
    }
}
